using LocalAiToolCLI.FilesManagementContext;
using LocalAiToolCLI.LoggingManagement;
using LocalAiToolCLI.SettingsManagementContext;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace LocalAiToolCLI.OnStartup;

public static class ValidateSettingsOnStartupExtension
{
    extension(OnStartupDelegate)
    {
        public static void AddSettingsValidation(IServiceCollection services)
        {
            services.AddSingleton<OnStartupDelegate>(
                (sp) =>
                {
                    "Проверка конфигурации приложения".PrintSystemMessage();
                    DatabaseSettings database = GetSettingsFromServices<DatabaseSettings>(sp);
                    EmbeddingModelSettings embeddingModel =
                        GetSettingsFromServices<EmbeddingModelSettings>(sp);
                    GenerativeModelSettings generativeModel =
                        GetSettingsFromServices<GenerativeModelSettings>(sp);
                    DocumentationFolderSettings docs =
                        GetSettingsFromServices<DocumentationFolderSettings>(sp);

                    ValidateDatabaseSettings(database);
                    ValidateGenerativeModelSettings(generativeModel);
                    ValidateEmbeddingModelSettings(embeddingModel);
                    ValidateDocumentationSettings(docs);

                    "конфигурация приложения прошла успешную проверку".PrintSuccessMessage();
                    Console.EmptyLine();
                }
            );
        }

        private static void ValidateDocumentationSettings(DocumentationFolderSettings settings)
        {
            if (string.IsNullOrWhiteSpace(settings.FolderPath))
            {
                string error = "Не указан путь к корневой папке документации.";
                error.PrintErrorMessage();
                throw new ApplicationException(error);
            }

            "Проверка на существование указанной в конфигурации директории документации".PrintSystemMessage();
            if (!Directory.Exists(settings.FolderPath))
            {
                string error = "Директория, указанная в документации конфигурации не существует";
                error.PrintErrorMessage();
                throw new ApplicationException(error);
            }
        }

        private static void ValidateGenerativeModelSettings(GenerativeModelSettings settings)
        {
            if (string.IsNullOrWhiteSpace(settings.BaseFolderPath))
            {
                string error =
                    $"В файле конфигурации не указана настройка: {nameof(GenerativeModelSettings)}:{settings.BaseFolderPath}";
                error.PrintErrorMessage();
                throw new ApplicationException(error);
            }

            "Проверка конфигурации генеративной модели".PrintSystemMessage();
            ValidateByDirectory(settings.BaseFolderPath);
        }

        private static void ValidateEmbeddingModelSettings(EmbeddingModelSettings settings)
        {
            if (string.IsNullOrWhiteSpace(settings.ModelPath))
            {
                string error =
                    $"В конфигурации не указана настройка: {nameof(EmbeddingModelSettings)}:{settings.ModelPath}";
                error.PrintErrorMessage();
                throw new ApplicationException(error);
            }

            if (string.IsNullOrWhiteSpace(settings.TokenizerPath))
            {
                string error =
                    $"В конфигурации не указана настройка: {nameof(EmbeddingModelSettings)}:{settings.TokenizerPath}";
                error.PrintErrorMessage();
                throw new ApplicationException(error);
            }

            "Проверка конфигурации модели эмбеддингов: ".PrintSystemMessage();
            ValidateByPath(settings.ModelPath);
            ValidateByPath(settings.TokenizerPath);
        }

        private static void ValidateDatabaseSettings(DatabaseSettings settings)
        {
            if (string.IsNullOrWhiteSpace(settings.OriginalDatabasePath))
            {
                string error =
                    $"В конфигурации не указана настройка: {nameof(DatabaseSettings)}:{nameof(settings.OriginalDatabasePath)}";
                error.PrintErrorMessage();
                throw new ApplicationException(error);
            }

            "Проверка конфигурации базы данных: ".PrintSystemMessage();
            ValidateByPath(settings.OriginalDatabasePath);
        }

        private static void ValidateByPath(string path)
        {
            $"Проверка наличия файла: {path}".PrintSystemMessage();
            string fullFilePath = Path.CombineWithExecutableDirectory(path);
            bool exists = File.Exists(fullFilePath);
            if (!exists)
            {
                string error = $"Файл: {path} не найден";
                error.PrintErrorMessage();
                throw new ApplicationException(error);
            }
            "Файл существует.".PrintSuccessMessage();
        }

        private static void ValidateByDirectory(string path)
        {
            $"Проверка наличия папки: {path}".PrintSystemMessage();
            string fullFilePath = Path.CombineWithExecutableDirectory(path);
            bool exists = Directory.Exists(fullFilePath);
            if (!exists)
            {
                string error = $"Папка: {path} не найдена";
                error.PrintErrorMessage();
                throw new ApplicationException(error);
            }
            "Папка существует.".PrintSuccessMessage();
        }

        private static T GetSettingsFromServices<T>(IServiceProvider services)
            where T : class
        {
            IOptions<T>? settings = services.GetRequiredService<IOptions<T>>();
            if (settings is null)
            {
                string type = typeof(T).Name;
                string error =
                    $"Тип: {type} не зарегистрирован как настройки в IServiceCollection.AddOptions()";
                error.PrintErrorMessage();
                throw new ApplicationException(error);
            }

            return settings.Value;
        }
    }
}
