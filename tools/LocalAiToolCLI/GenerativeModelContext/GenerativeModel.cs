using LocalAiToolCLI.LoggingManagement;
using LocalAiToolCLI.SettingsManagementContext;
using Microsoft.Extensions.Options;
using Microsoft.ML.OnnxRuntimeGenAI;

namespace LocalAiToolCLI.GenerativeModelContext;

public sealed class GenerativeModel : IDisposable
{
    public GenerativeModel(IOptions<GenerativeModelSettings> settings)
    {
        _model = new(() => new Model(settings.Value.BaseFolderPath));
    }

    private readonly Lazy<Model> _model;

    private bool _isDisposed = false;

    public void RunInference(string userPrompt, string toolResponse)
    {
        userPrompt.PrintUserInput();
        const string T_SYSTEM = "<|system|>";
        const string T_USER = "<|user|>";
        const string T_ASSISTANT = "<|assistant|>";
        const string T_END = "<|end|>";
        const string T_TOOL_RESPONSE_OPEN = "<|tool_response|>";

        string systemText =
            "Вы — справочник по локальному репозиторию. Формируйте ответ КРАТКО и ТОЛЬКО на основе информации из блока '<|tool_response|>'. "
            + "Если в '<|tool_response|>' сообщите, что не можете дать ответ.";

        string prompt =
            T_SYSTEM
            + systemText
            + T_END
            + T_TOOL_RESPONSE_OPEN
            + toolResponse
            + T_END
            + T_USER
            + userPrompt
            + T_END
            + T_ASSISTANT;

        Model model = _model.Value;
        using Tokenizer tokenizer = new(model);
        using TokenizerStream stream = tokenizer.CreateStream();
        using GeneratorParams generatorParams = new(model);
        generatorParams.SetSearchOption("do_sample", false);
        generatorParams.SetSearchOption("temperature", 0.0);
        generatorParams.SetSearchOption("num_beams", 1);
        generatorParams.SetSearchOption("max_length", 1024);
        generatorParams.SetSearchOption("early_stopping", true);
        generatorParams.SetSearchOption("no_repeat_ngram_size", 3);
        using Generator generator = new(model, generatorParams);
        using Sequences sequences = tokenizer.Encode(prompt);
        generator.AppendTokenSequences(sequences);
        while (!generator.IsDone())
        {
            generator.GenerateNextToken();
            ReadOnlySpan<int> token = generator.GetNextTokens();
            string text = stream.Decode(token[0]);
            Console.Write(text);
        }
    }

    public void Dispose()
    {
        if (_isDisposed)
        {
            return;
        }

        if (_model.IsValueCreated)
        {
            _model.Value.Dispose();
            _isDisposed = true;
            GC.Collect(
                GC.MaxGeneration,
                GCCollectionMode.Aggressive,
                blocking: true,
                compacting: true
            );
            GC.WaitForPendingFinalizers();
            GC.Collect(
                GC.MaxGeneration,
                GCCollectionMode.Aggressive,
                blocking: true,
                compacting: true
            );
        }
    }
}
