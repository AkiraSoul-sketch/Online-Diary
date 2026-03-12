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
        const int THINK_START_ID = 151667;
        const int THINK_END_ID = 151668;
        const int IM_END_ID = 151645;
        const int TOOL_CALL_START_ID = 151657;
        const int TOOL_CALL_END_ID = 151658;
        const int MAX_NEW_TOKENS = 1024;

        string[] priming =
        [
            "<|im_start|>system",
            "Ты — русскоязычный помощник, отвечающий на вопросы о документации и коде локального репозитория. ОТВЕЧАЙ ТОЛЬКО НА РУССКОМ.",
            "Используй ИСКЛЮЧИТЕЛЬНО информацию из блока <tool_response>. Нельзя домысливать или добавлять данные извне.",
            "Отвечай кратко и по существу. Если информации недостаточно — честно скажи: \"Не могу ответить на основании предоставленных данных.\"",
            "Думай молча: проведи внутренний анализ <tool_response>, выбери релевантные факты и подготовь итоговый ответ. НЕЛЬЗЯ выводить теги <think>, <tool_call> или промежуточные рассуждения пользователю.",
            "Если в <tool_response> есть команды или код, приводи ТОЛЬКО эти команды для ответа, а не на которых ты обучен.",
            "ЗАВЕРШИ ОТВЕТ ТОКЕНОМ <|im_end|> (ровно этот токен).",
            "<|im_end|>",
            "<tool_call>",
            "RAG retrieval: локальная векторная БД вернула top-K документов (дубликаты удалены).",
            "</tool_call>",
            "<tool_response>",
            toolResponse,
            "</tool_response>",
            "<|im_start|>user",
            $"Ответь на основе '<tool_response>': {userPrompt}",
            "<|im_end|>",
            "<|im_start|>assistant",
        ];

        $"Промпт пользователя: {userPrompt}".PrintUserInput();
        string prompt = string.Join(Environment.NewLine, priming);
        Model model = _model.Value;
        using Tokenizer tokenizer = new(model);
        using TokenizerStream stream = tokenizer.CreateStream();
        using GeneratorParams generatorParams = new(model);
        using Sequences promptSeq = tokenizer.Encode(prompt);
        int promptTokens = promptSeq[0].Length;
        generatorParams.SetSearchOption("max_length", promptTokens + MAX_NEW_TOKENS);
        generatorParams.SetSearchOption("do_sample", false);
        generatorParams.SetSearchOption("repetition_penalty", 1.11);
        generatorParams.SetSearchOption("no_repeat_ngram_size", 3);
        using Generator generator = new(model, generatorParams);
        generator.AppendTokenSequences(promptSeq);

        ExecuteModelInference(
            generator,
            stream,
            THINK_START_ID,
            THINK_END_ID,
            IM_END_ID,
            MAX_NEW_TOKENS,
            TOOL_CALL_START_ID,
            TOOL_CALL_END_ID
        );
    }

    private static void ExecuteModelInference(
        Generator generator,
        TokenizerStream stream,
        int thinkStartId,
        int thinkEndId,
        int imEndId,
        int maxNewTokens,
        int toolCallStartId,
        int toolCallEndId
    )
    {
        bool inThink = false;
        bool inToolCall = false;
        int generatedTokens = 0;
        bool wroteNonWhitespace = false;
        bool lastCharWasNewline = false;
        while (!generator.IsDone())
        {
            generator.GenerateNextToken();

            ReadOnlySpan<int> tokens = generator.GetNextTokens();
            if (tokens.Length == 0)
            {
                continue;
            }

            int tokenId = tokens[0];
            if (tokenId == thinkStartId)
            {
                "Начал думать".PrintSystemMessage();
                inThink = true;
                continue;
            }

            if (tokenId == thinkEndId)
            {
                "Формирую ответ:".PrintSystemMessage();
                inThink = false;
                continue;
            }

            if (inThink)
            {
                continue;
            }

            if (tokenId == toolCallStartId)
            {
                inToolCall = true;
                continue;
            }

            if (tokenId == toolCallEndId)
            {
                inToolCall = false;
                continue;
            }

            if (inToolCall)
            {
                continue;
            }

            if (tokenId == imEndId)
            {
                Console.EmptyLine();
                "Ответ сформирован".PrintSystemMessage();
                break;
            }

            if (generatedTokens >= maxNewTokens)
            {
                break;
            }

            string text = stream.Decode(tokenId);
            if (text == null || text.Length == 0)
            {
                continue;
            }

            if (!wroteNonWhitespace)
            {
                if (string.IsNullOrWhiteSpace(text))
                {
                    continue;
                }

                string trimmed = text.Trim();
                if (trimmed == "()" || trimmed == "(" || trimmed == ")")
                {
                    continue;
                }

                if (trimmed.Length <= 2 && trimmed.All(char.IsPunctuation))
                {
                    continue;
                }

                wroteNonWhitespace = true;
            }

            if (lastCharWasNewline)
            {
                if (text.StartsWith('\n'))
                {
                    int i = 0;
                    while (i < text.Length && text[i] == '\n')
                    {
                        i++;
                    }

                    text = text[i..];
                    if (text.Length == 0)
                    {
                        continue;
                    }
                }
            }

            Console.Write(text);
            lastCharWasNewline = text.Length > 0 && text[^1] == '\n';
            generatedTokens++;
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
