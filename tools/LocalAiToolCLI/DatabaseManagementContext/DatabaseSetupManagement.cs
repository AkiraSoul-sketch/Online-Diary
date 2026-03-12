using LocalAiToolCLI.LoggingManagement;
using LocalAiToolCLI.SettingsManagementContext;

namespace LocalAiToolCLI.DatabaseManagementContext;

public static class DatabaseSetupManagement
{
    private static readonly string BasePath = AppDomain.CurrentDomain.BaseDirectory;

    extension(DatabaseSettings settings)
    {
        public bool DatabaseCreated()
        {
            string usingDbPath = FormCurrentExecutionFolderBasedPath(settings.UsingDatabasePath);
            return File.Exists(usingDbPath);
        }

        public void RemoveProductionDatabase()
        {
            if (!settings.DatabaseCreated())
            {
                string error = "Продуктовая база данных не существует. Отмена удаления.";
                error.PrintErrorMessage();
                return;
            }

            string fullUsingDbPath = FormCurrentExecutionFolderBasedPath(
                settings.UsingDatabasePath
            );
            File.Delete(fullUsingDbPath);
            if (settings.DatabaseCreated())
            {
                string error =
                    $"Не удалось удалить продуктовую базу данных: {settings.UsingDatabasePath}";
                error.PrintErrorMessage();
                throw new InvalidOperationException(error);
            }
        }

        public void CloneDatabase()
        {
            if (settings.DatabaseCreated())
            {
                string error =
                    $"Продуктовая база данных: {settings.UsingDatabasePath} уже существует. Отмена создания.";
                error.PrintWarningMessage();
                return;
            }

            string originalDbPath = settings.OriginalDatabasePath;
            string destinationDbPath = settings.UsingDatabasePath;
            if (!settings.OriginalDatabaseExists())
            {
                string error = $"Оригинальный файл: {originalDbPath} не найден.";
                error.PrintErrorMessage();
                throw new InvalidOperationException(error);
            }

            $"Файл {originalDbPath} существует, создаем клон продуктовой базы данных.".PrintSystemMessage();
            string fullOriginalDbPath = FormCurrentExecutionFolderBasedPath(originalDbPath);
            string fullDestinationDbPath = FormCurrentExecutionFolderBasedPath(destinationDbPath);
            File.Copy(fullOriginalDbPath, fullDestinationDbPath);
            if (!settings.DatabaseCreated())
            {
                string error = $"Не удалось создать продуктовную базу данных: {destinationDbPath}";
                error.PrintErrorMessage();
                throw new InvalidOperationException(error);
            }

            $"Клон базы данных успешно создан.".PrintSuccessMessage();
        }

        private bool OriginalDatabaseExists()
        {
            string origDbPath = FormCurrentExecutionFolderBasedPath(settings.OriginalDatabasePath);
            return File.Exists(origDbPath);
        }
    }

    private static string FormCurrentExecutionFolderBasedPath(string path)
    {
        return Path.Combine(BasePath, path);
    }
}
