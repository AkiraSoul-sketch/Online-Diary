using LocalAiToolCLI.LoggingManagement;
using LocalAiToolCLI.SettingsManagementContext;
using Microsoft.Extensions.Options;
using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.OnnxRuntime.Tensors;

namespace LocalAiToolCLI.EmbeddingContext;

public sealed class EmbeddingModel : IDisposable
{
    public EmbeddingModel(IOptions<EmbeddingModelSettings> options)
    {
        EmbeddingModelSettings settings = options.Value;
        _options = new();
        _options.RegisterOrtExtensions();
        _model = new(() =>
        {
            string path = settings.ModelPath;
            if (!File.Exists(path))
            {
                string error = $"Файл модели: {path} не найден.";
                error.PrintErrorMessage();
                throw new InvalidOperationException(error);
            }

            return new InferenceSession(settings.ModelPath, _options);
        });
        _tokenizer = new(() =>
        {
            string path = settings.TokenizerPath;
            if (!File.Exists(path))
            {
                string error = $"Файл токенизатора: {path} не найден.";
                error.PrintErrorMessage();
                throw new InvalidOperationException(error);
            }

            return new InferenceSession(path, _options);
        });
    }

    private readonly SessionOptions _options;

    private readonly Lazy<InferenceSession> _model;
    private readonly Lazy<InferenceSession> _tokenizer;
    private bool _disposed = false;
    private InferenceSession Model => _model.Value;
    private InferenceSession Tokenizer => _tokenizer.Value;

    public float[] Generate(ReadOnlySpan<char> input)
    {
        TokenizerOutput output = GenerateTokenizerOutput(Tokenizer, input);

        int[] tokenIds =
        [
            .. output
                .InputIds.Zip(output.TokenIndices, (t, i) => (Token: t, Index: i))
                .OrderBy(p => p.Index)
                .Select(p => p.Token),
        ];

        int sequenceLength = tokenIds.Length;
        DenseTensor<long> inputIdsTensor = new([1, sequenceLength]);
        DenseTensor<long> attentionMaskTensor = new([1, sequenceLength]);
        int padToken = 1;
        for (int i = 0; i < tokenIds.Length; i++)
        {
            inputIdsTensor[0, i] = tokenIds[i];
            attentionMaskTensor[0, i] = tokenIds[i] == padToken ? 0L : 1L;
        }

        ModelOutput modelOutput = ModelOutput.FromModelInference(
            Model,
            inputIdsTensor,
            attentionMaskTensor
        );
        float[] pooled = new float[modelOutput.HiddenSize];
        long validTokens = 0;
        for (int t = 0; t < modelOutput.SequenceLength; t++)
        {
            if (attentionMaskTensor[0, t] == 0)
            {
                continue;
            }

            int id = (int)inputIdsTensor[0, t];
            if (id == 0 || id == 2)
            {
                continue;
            }

            validTokens++;
            for (int h = 0; h < modelOutput.HiddenSize; h++)
            {
                pooled[h] += modelOutput.RawTokens[t * modelOutput.HiddenSize + h];
            }
        }

        for (int h = 0; h < modelOutput.HiddenSize; h++)
        {
            pooled[h] /= validTokens;
        }

        double norm = 0;
        for (int h = 0; h < modelOutput.HiddenSize; h++)
        {
            norm += pooled[h] * pooled[h];
        }

        norm = Math.Sqrt(norm);
        if (norm > 0)
        {
            for (int h = 0; h < modelOutput.HiddenSize; h++)
            {
                pooled[h] = (float)(pooled[h] / norm);
            }
        }

        return pooled;
    }

    public void Dispose()
    {
        if (_disposed)
        {
            return;
        }

        if (_model.IsValueCreated)
        {
            _model.Value.Dispose();
        }

        if (_tokenizer.IsValueCreated)
        {
            _tokenizer.Value.Dispose();
        }

        _disposed = true;
        GC.Collect(GC.MaxGeneration, GCCollectionMode.Aggressive, blocking: true, compacting: true);
        GC.WaitForPendingFinalizers();
        GC.Collect(GC.MaxGeneration, GCCollectionMode.Aggressive, blocking: true, compacting: true);
    }

    private static TokenizerOutput GenerateTokenizerOutput(
        InferenceSession tokenizer,
        ReadOnlySpan<char> text
    )
    {
        string textString = text.ToString();
        string[] batch = [textString];
        DenseTensor<string> inputTensor = new(batch, [batch.Length]);
        NamedOnnxValue inputs = NamedOnnxValue.CreateFromTensor("inputs", inputTensor);
        using IDisposableReadOnlyCollection<DisposableNamedOnnxValue> tokenizedInputs =
            tokenizer.Run([inputs]);
        DisposableNamedOnnxValue[] tokens = [.. tokenizedInputs];
        TokenizerOutput output = new(tokens);
        tokens[0].Dispose();
        tokens[1].Dispose();
        tokens[2].Dispose();
        return output;
    }

    private static void PrintModelInputs(InferenceSession session)
    {
        foreach (KeyValuePair<string, NodeMetadata> meta in session.InputMetadata)
        {
            string key = meta.Key;
            string type = meta.Value.ElementType.Name;
            string dims = string.Join(", ", meta.Value.Dimensions);
            Console.WriteLine($"Input: {key}, Type: {type}, Dims: {dims}");
        }
    }

    private sealed class ModelOutput
    {
        public float[] RawTokens { get; }
        public int Batch { get; }
        public int SequenceLength { get; }
        public int HiddenSize { get; }

        public ModelOutput(DisposableNamedOnnxValue lastHiddenValue)
        {
            ReadOnlySpan<int> dimensions = lastHiddenValue.AsTensor<float>().Dimensions;
            RawTokens = [.. lastHiddenValue.AsTensor<float>()];
            Batch = dimensions[0];
            SequenceLength = dimensions[1];
            HiddenSize = dimensions[2];
        }

        public static ModelOutput FromModelInference(
            InferenceSession model,
            DenseTensor<long> inputIdsTensor,
            DenseTensor<long> attentionMaskTensor
        )
        {
            // PrintModelInputs(model);
            NamedOnnxValue[] inputs =
            [
                NamedOnnxValue.CreateFromTensor("input_ids", inputIdsTensor),
                NamedOnnxValue.CreateFromTensor("attention_mask", attentionMaskTensor),
            ];
            using IDisposableReadOnlyCollection<DisposableNamedOnnxValue> modelResults = model.Run(
                inputs
            );
            using DisposableNamedOnnxValue lastHiddenValue = modelResults.Single(r =>
                r.Name == "last_hidden_state"
            );
            return new ModelOutput(lastHiddenValue);
        }
    }

    private sealed class TokenizerOutput(NamedOnnxValue[] tokens)
    {
        private const string TOKENS = "tokens";
        private const string TOKEN_INDICES = "token_indices";
        public int[] InputIds { get; init; } =
        [.. tokens.Single(t => t.Name == TOKENS).AsTensor<int>()];
        public int[] TokenIndices { get; init; } =
        [.. tokens.Single(t => t.Name == TOKEN_INDICES).AsTensor<int>()];
    }
}
