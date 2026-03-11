using LocalAiToolCLI.FilesManagementContext;
using LocalAiToolCLI.LoggingManagement;
using LocalAiToolCLI.SettingsManagementContext;
using Microsoft.Extensions.Options;
using SQLitePCL;

namespace LocalAiToolCLI.DatabaseManagementContext;

public static class DatabaseConstructionModule
{
    extension(DatabaseInstance)
    {
        public static sqlite3 Create(IOptions<DatabaseSettings> options)
        {
            DatabaseSettings settings = options.Value;
            string path = settings.UsingDatabasePath;
            sqlite3 db = Open(path);
            PushExtensions(settings.ExtensionFiles, db);
            return db;
        }

        public static sqlite3 Open(string path)
        {
            Batteries_V2.Init();
            int rc = raw.sqlite3_open(path, out sqlite3 db);
            if (DatabaseInstance.IsError(rc))
            {
                DatabaseInstance.HandleError(rc);
            }

            return db;
        }

        public static void PushExtensions(string[]? ExtensionFiles, sqlite3 db)
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

            int rc = raw.sqlite3_enable_load_extension(db, 1);
            if (DatabaseInstance.IsError(rc))
            {
                DatabaseInstance.HandleError(rc);
            }

            utf8z nullPointerWrapper = utf8z.FromIntPtr(IntPtr.Zero);
            foreach (string path in ExtensionFiles)
            {
                string fullPath = PathExtensions.CombineWithExecutableDirectory(path);
                utf8z pointer = utf8z.FromString(fullPath);
                rc = raw.sqlite3_load_extension(
                    db,
                    pointer,
                    nullPointerWrapper,
                    out utf8z errorMessage
                );
                if (DatabaseInstance.IsError(rc))
                {
                    DatabaseInstance.HandleError(rc, errorMessage);
                }

                $"Загружено расширение: {path}".PrintSuccessMessage();
            }
        }
    }
}
