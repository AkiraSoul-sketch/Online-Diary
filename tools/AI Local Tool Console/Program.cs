using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using AI_Local_Tool_Console.Extensions.Database;
using AI_Local_Tool_Console.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.ML.OnnxRuntimeGenAI;
using SQLitePCL;

namespace AI_Local_Tool_Console
{
    public sealed class FileInformation
    {
        public required string FileName { get; set; }
        public required string FilePath { get; set; }
    }

    internal class Program
    {
        private static readonly JsonSerializerOptions options = new()
        {
            Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
            WriteIndented = true,
        };

        static async Task Main(string[] args)
        {
            ConfigurationManager config = LoadConfiguration();
            ProcessInitializationOperations(config, args);
            // FillWithTestData(config);
            ExecuteVectorPrompt(config, "Выведи мне какой шрифт использовать в нашем приложении");
        }

        private static void ExecuteVectorPrompt(ConfigurationManager config, string text)
        {
            DatabaseSettings dbSettings = GetDbSettingsFromConfig(config);
            DatabaseExt.OpenDb(dbSettings.DatabasePath, out sqlite3 db);
            DatabaseExt.EnableExtensionsLoad(db);
            DatabaseExt.LoadExtension(dbSettings, db);
            DatabaseExt.InitializeVectorForColumn(db, "documents", "embedding");
            using EmbeddingsGenerator generator = new(config);
            float[] embedding = generator.GenerateEmbeddings(text);
            const string sql = """
                SELECT
                    v.rowId,
                    (1 - v.distance) as relevance,
                    d.id,
                    d.file_path,
                    d.file_name
                FROM vector_full_scan(
                    'documents',
                    'embedding',
                    vector_as_f32(?1)
                ) AS v
                JOIN documents d ON d.rowId = v.rowId
                ORDER BY relevance DESC
                """;
            DatabaseExt.CreateStatement(db, sql, out sqlite3_stmt stmt);
            DatabaseExt.AddVectorToStatementAsBlob(stmt, 1, embedding);

            int rc;
            while ((rc = raw.sqlite3_step(stmt)) == raw.SQLITE_ROW)
            {
                string rowId = raw.sqlite3_column_text(stmt, 0).utf8_to_string();
                double distance = raw.sqlite3_column_double(stmt, 1);
                string id = raw.sqlite3_column_text(stmt, 2).utf8_to_string();
                string filePath = raw.sqlite3_column_text(stmt, 3).utf8_to_string();
                string fileName = raw.sqlite3_column_text(stmt, 4).utf8_to_string();
                $"{fileName} имеет релевантность {distance:0.0000}".PrintAsWarningMessage();
                string fileContent = File.ReadAllText(filePath);
                AnswerUserPrompt(text, fileContent);
                break;
            }
        }

        private static void AnswerUserPrompt(string userPrompt, string toolResponse)
        {
            userPrompt.PrintAsConsoleSystemMessage();
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

            const string generativeModel = "generative_model";

            using Model model = new(generativeModel);
            using Tokenizer tokenizer = new(model);
            using TokenizerStream stream = tokenizer.CreateStream();
            using GeneratorParams generatorParams = new(model);
            generatorParams.SetSearchOption("do_sample", false);
            generatorParams.SetSearchOption("temperature", 0.0);
            generatorParams.SetSearchOption("num_beams", 1);
            generatorParams.SetSearchOption("max_length", 1024);
            generatorParams.SetSearchOption("early_stopping", true);
            generatorParams.SetSearchOption("no_repeat_ngram_size", 3);
            // generatorParams.SetSearchOption("repetition_penalty", 1.1);
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

        private static void FillWithTestData(ConfigurationManager config)
        {
            FileInformation[] fileInfos = GetFileInformations();
            DatabaseSettings dbSettings = GetDbSettingsFromConfig(config);
            DatabaseExt.OpenDb(dbSettings.DatabasePath, out sqlite3 db);
            DatabaseExt.EnableExtensionsLoad(db);
            DatabaseExt.LoadExtension(dbSettings, db);
            using EmbeddingsGenerator generator = new(config);
            foreach (FileInformation fileInfo in fileInfos)
            {
                Guid docId = Guid.NewGuid();
                string fileContent = ReadTextFromFile(fileInfo.FilePath);
                float[] embedding = generator.GenerateEmbeddings(fileContent);
                const string sql = """
                        INSERT INTO documents (id, file_path, file_name, embedding)
                        VALUES (?1, ?2, ?3, vector_as_f32(?4));
                    """;
                DatabaseExt.CreateStatement(db, sql, out sqlite3_stmt stmt);
                DatabaseExt.AddGuidToStatement(stmt, 1, docId);
                DatabaseExt.AddTextToStatement(stmt, 2, fileInfo.FilePath);
                DatabaseExt.AddTextToStatement(stmt, 3, fileInfo.FileName);
                DatabaseExt.AddVectorToStatementAsJson(stmt, 4, embedding);
                DatabaseExt.ExecuteStatement(stmt);
            }
        }

        private static string ReadTextFromFile(string filePath)
        {
            using StreamReader reader = new(filePath);
            return reader.ReadToEnd();
        }

        private static void ProcessInitializationOperations(
            ConfigurationManager config,
            string[] args
        )
        {
            DatabaseInitializer initializer = new(config);
            initializer.InitializeDatabase(args);
            FileInformation[] fileInfos = GetFileInformations();
        }

        private static DatabaseSettings GetDbSettingsFromConfig(ConfigurationManager manager)
        {
            return manager.GetSection(nameof(DatabaseSettings)).Get<DatabaseSettings>()!;
        }

        private static FileInformation[] GetFileInformations()
        {
            const string folder = "documentation";
            DirectoryInfo directory = new(folder);
            SearchOption searchOption = SearchOption.AllDirectories;
            return
            [
                .. directory
                    .EnumerateFiles("*.typ", searchOption)
                    .Select(f => new FileInformation() { FileName = f.Name, FilePath = f.FullName })
                    .Where(f => f.FileName != "presets.typ")
                    .DistinctBy(f => f.FileName),
            ];
        }

        private static ConfigurationManager LoadConfiguration()
        {
            ConfigurationManager manager = new();
            manager.AddJsonFile("appsettings.json");
            return manager;
        }
    }
}
