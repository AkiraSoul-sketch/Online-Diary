using System.Text;
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
        const string T_SYSTEM = "<|system|>";
        const string T_USER = "<|user|>";
        const string T_ASSISTANT = "<|assistant|>";
        const string T_END = "<|end|>";
        const string T_TOOL_RESPONSE_OPEN = "<|tool_response|>";

        string systemText =
            "Вы — справочник по локальному репозиторию. Формируйте ответ КРАТКО и ТОЛЬКО на основе информации из блока '<|tool_response|>'. "
            + " Если в '<|tool_response|>' сообщите, что не можете дать ответ. ";

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

        using Tokenizer tokenizer = new Tokenizer(model);
        using TokenizerStream stream = tokenizer.CreateStream();

        using GeneratorParams generatorParams = new GeneratorParams(model);
        generatorParams.SetSearchOption("do_sample", false);
        using Sequences promptSeq = tokenizer.Encode(prompt);
        int promptTokens = promptSeq[0].Length;
        const int desiredNewTokens = 512;
        generatorParams.SetSearchOption("max_length", promptTokens + desiredNewTokens);

        using var generator = new Generator(model, generatorParams);
        generator.AppendTokenSequences(promptSeq);

        while (!generator.IsDone())
        {
            generator.GenerateNextToken();

            ReadOnlySpan<int> tokens = generator.GetNextTokens();
            if (tokens.Length == 0)
            {
                continue;
            }

            int tokenId = tokens[0];
            string text = stream.Decode(tokenId);
            Console.Write(text);
        }
    }

    private static string EscapeJson(string s)
    {
        if (string.IsNullOrEmpty(s))
            return "";
        return s.Replace("\\", "\\\\")
            .Replace("\"", "\\\"")
            .Replace("\r", "\\r")
            .Replace("\n", "\\n");
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
