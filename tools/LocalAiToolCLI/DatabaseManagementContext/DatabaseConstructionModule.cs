using LocalAiToolCLI.LoggingManagement;
using LocalAiToolCLI.SettingsManagementContext;
using SQLitePCL;

namespace LocalAiToolCLI.DatabaseManagementContext;

public static class DatabaseConstructionModule
{
    extension(DatabaseInstance)
    {
        public static DatabaseInstance Create(DatabaseSettings settings)
        {
            string path = settings.UsingDatabasePath;
            sqlite3 db = Open(path);
            DatabaseInstance instance = new(db, path);
            PushExtensions(settings.ExtensionFiles, instance);
            return instance;
        }

        private static sqlite3 Open(string path)
        {
            int rc = raw.sqlite3_open(path, out sqlite3 db);
            if (DatabaseInstance.IsError(rc))
            {
                DatabaseInstance.HandleError(rc);
            }

            return db;
        }

        private static void PushExtensions(string[]? ExtensionFiles, DatabaseInstance instance)
        {
            if (ExtensionFiles is null)
            {
                "Расширения для базы данных sqlite не указаны.".PrintSystemMessage();
                return;
            }

            if (ExtensionFiles.Length == 0)
            {
                "Список расширений для базы данных sqlite пуст.".PrintSystemMessage();
                return;
            }

            foreach (string path in ExtensionFiles)
            {
                instance.InstallExtension(path);
                $"Загружено расширение: {path}".PrintSuccessMessage();
            }
        }
    }
}
