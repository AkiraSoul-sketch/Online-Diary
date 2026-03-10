using System.Security.AccessControl;
using AI_Local_Tool_Console.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.OnnxRuntime.Tensors;
using SQLitePCL;

namespace AI_Local_Tool_Console;

public sealed class EmbeddingsGenerator : IDisposable
{
    public EmbeddingsGenerator(ConfigurationManager manager)
    {
        SessionOptions options = new();
        options.RegisterOrtExtensions();

        _embModelSettings = manager
            .GetSection(nameof(EmbeddingModelSettings))
            .Get<EmbeddingModelSettings>();

        _disposed = false;
        _modelSession = new InferenceSession(ModelPath);
        _tokenizerSession = new InferenceSession(TokenizerPath, options);
        PrintModelInputMetadata(_modelSession, "embedding model");
        PrintModelInputMetadata(_tokenizerSession, "tokenizer model");
        PrintModelOutputMetadata(_modelSession, "embedding model");
        PrintModelOutputMetadata(_tokenizerSession, "tokenizer model");
    }

    private readonly EmbeddingModelSettings? _embModelSettings;
    private readonly InferenceSession _tokenizerSession;
    private readonly InferenceSession _modelSession;
    private bool _disposed;
    private string ModelPath =>
        _embModelSettings?.ModelPath
        ?? throw new InvalidOperationException("Model path is not configured.");
    private string TokenizerPath =>
        _embModelSettings?.TokenizerPath
        ?? throw new InvalidOperationException("Tokenizer path is not configured.");

    public float[] GenerateEmbeddings(ReadOnlySpan<char> input)
    {
        TokenizerOutput output = GenerateTokenizerOutput(input);

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

        List<NamedOnnxValue> inputs =
        [
            NamedOnnxValue.CreateFromTensor("input_ids", inputIdsTensor),
            NamedOnnxValue.CreateFromTensor("attention_mask", attentionMaskTensor),
        ];

        using IDisposableReadOnlyCollection<DisposableNamedOnnxValue> modelResults =
            _modelSession.Run(inputs);
        DisposableNamedOnnxValue lastHiddenValue = modelResults.Single(r =>
            r.Name == "last_hidden_state"
        );
        ReadOnlySpan<int> dimensions = lastHiddenValue.AsTensor<float>().Dimensions;
        float[] raw = [.. lastHiddenValue.AsTensor<float>()];
        int batch = dimensions[0];
        int seqLen = dimensions[1];
        int hiddenSize = dimensions[2];
        float[] pooled = new float[hiddenSize];
        long validTokens = 0;
        for (int t = 0; t < seqLen; t++)
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
            for (int h = 0; h < hiddenSize; h++)
            {
                pooled[h] += raw[t * hiddenSize + h];
            }
        }

        for (int h = 0; h < hiddenSize; h++)
        {
            pooled[h] /= validTokens;
        }

        double norm = 0;
        for (int h = 0; h < hiddenSize; h++)
        {
            norm += pooled[h] * pooled[h];
        }

        norm = Math.Sqrt(norm);
        if (norm > 0)
        {
            for (int h = 0; h < hiddenSize; h++)
            {
                pooled[h] = (float)(pooled[h] / norm);
            }
        }

        return pooled;
    }

    private TokenizerOutput GenerateTokenizerOutput(ReadOnlySpan<char> text)
    {
        string textString = text.ToString();
        string[] batch = [textString];
        DenseTensor<string> inputTensor = new(batch, [batch.Length]);
        NamedOnnxValue inputs = NamedOnnxValue.CreateFromTensor("inputs", inputTensor);
        using IDisposableReadOnlyCollection<DisposableNamedOnnxValue> tokenizedInputs =
            _tokenizerSession.Run([inputs]);
        DisposableNamedOnnxValue[] tokens = [.. tokenizedInputs];
        TokenizerOutput output = new(tokens);
        tokens[0].Dispose();
        tokens[1].Dispose();
        tokens[2].Dispose();
        return output;
    }

    private static void PrintModelInputMetadata(InferenceSession session, string? modelName = null)
    {
        $"Метаданные входов модели {modelName}:".PrintAsConsoleSystemMessage();
        foreach (KeyValuePair<string, NodeMetadata> kv in session.InputMetadata)
        {
            string name = kv.Key;
            NodeMetadata meta = kv.Value;
            $"Name: {name}".PrintAsConsoleSystemMessage();
            $"Element type: {meta.ElementType}".PrintAsConsoleSystemMessage();
            $"Dimensions: {string.Join(", ", meta.Dimensions)}".PrintAsConsoleSystemMessage();
        }
    }

    private static void PrintModelOutputMetadata(InferenceSession session, string? modelName = null)
    {
        $"Метаданные выходов модели {modelName}:".PrintAsConsoleSystemMessage();
        foreach (KeyValuePair<string, NodeMetadata> kv in session.OutputMetadata)
        {
            string name = kv.Key;
            NodeMetadata meta = kv.Value;
            $"Name: {name}".PrintAsConsoleSystemMessage();
            $"Element type: {meta.ElementType}".PrintAsConsoleSystemMessage();
            $"Dimensions: {string.Join(", ", meta.Dimensions)}".PrintAsConsoleSystemMessage();
        }
    }

    public void Dispose()
    {
        if (_disposed)
        {
            return;
        }

        _tokenizerSession.Dispose();
        _modelSession.Dispose();
        _disposed = true;
        GC.Collect(GC.MaxGeneration, GCCollectionMode.Aggressive, blocking: true, compacting: true);
        GC.WaitForPendingFinalizers();
        GC.Collect(GC.MaxGeneration, GCCollectionMode.Aggressive, blocking: true, compacting: true);
    }

    private sealed class TokenizerOutput
    {
        public int[] InputIds { get; init; } = [];
        public int[] TokenIndices { get; init; } = [];

        public TokenizerOutput(NamedOnnxValue[] tokens)
        {
            InputIds = [.. tokens.Single(t => t.Name == "tokens").AsTensor<int>()];
            TokenIndices = [.. tokens.Single(t => t.Name == "token_indices").AsTensor<int>()];
        }
    }
}
