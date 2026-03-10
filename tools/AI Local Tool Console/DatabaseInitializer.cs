using AI_Local_Tool_Console.Extensions.Database;
using AI_Local_Tool_Console.Settings;
using Microsoft.Extensions.Configuration;
using SQLitePCL;

namespace AI_Local_Tool_Console;

public sealed class DatabaseInitializer
{
    public DatabaseInitializer(ConfigurationManager manager)
    {
        _settings = manager.GetSection(nameof(DatabaseSettings)).Get<DatabaseSettings>();
    }

    private readonly DatabaseSettings? _settings;
    private string DatabasePath =>
        _settings?.DatabasePath
        ?? throw new InvalidOperationException("Database path is not configured.");
    private string OriginalDatabasePath =>
        _settings?.OriginalDatabasePath
        ?? throw new InvalidOperationException("Original database path is not configured.");
    private string VectorExtensionPath =>
        _settings?.VectorExtensionPath
        ?? throw new InvalidOperationException("Vector extension path is not configured.");

    public void InitializeDatabase(string[]? args = null)
    {
        Batteries_V2.Init();
        HandleArgs(args);
        "Проверка наличия базы данных...".PrintAsConsoleSystemMessage();
        if (File.Exists(DatabasePath))
        {
            "База данных уже была создана.".PrintAsWarningMessage();
            Console.ForegroundColor = ConsoleColor.White;
            return;
        }

        CreateDatabaseCloneFromOriginal();
        DatabaseExt.OpenDb(DatabasePath, out sqlite3 db);
        try
        {
            InitializeVectorExtension(db);
            CreateDocumentsTable(db);
        }
        catch (Exception ex)
        {
            "При установке расширения vector.so возникла ошибка.".PrintAsConsoleErrorMessage();
            ex.Message.PrintAsConsoleErrorMessage();
            "Удаление остаточных артефактов".PrintAsWarningMessage();
            if (File.Exists(DatabasePath))
            {
                File.Delete(DatabasePath);
                "База данных была удалена.".PrintAsConsoleSystemMessage();
            }

            throw;
        }
        finally
        {
            DatabaseExt.CloseDatabase(db);
        }
    }

    private void CreateDatabaseCloneFromOriginal()
    {
        "База данных не найдена. Создание базы данных...".PrintAsConsoleSystemMessage();
        File.Copy(OriginalDatabasePath, DatabasePath);
        if (File.Exists(DatabasePath))
        {
            "База данных была создана.".PrintAsConsoleSystemMessage();
        }
    }

    private void InitializeVectorExtension(sqlite3 db)
    {
        "Проверка наличия файла расширения vector.so".PrintAsConsoleSystemMessage();
        if (!File.Exists(VectorExtensionPath))
        {
            $"Ошибка: Не найден файл {VectorExtensionPath}".PrintAsConsoleErrorMessage();
            throw new InvalidOperationException($"Не найден файл {VectorExtensionPath}");
        }

        DatabaseExt.EnableExtensionsLoad(db);
        DatabaseExt.LoadExtension(db, VectorExtensionPath);
    }

    private static void CreateDocumentsTable(sqlite3 db)
    {
        const string create_table_sql = """
            CREATE TABLE documents (
                id UUID PRIMARY KEY,
                file_path TEXT NOT NULL,
                file_name TEXT NOT NULL,
                embedding BLOB
            );
            """;
        const string init_vector_sql = """
                SELECT vector_init('documents', 'embedding', 'type=FLOAT32,dimension=1024');
            """;

        DatabaseExt.BeginTransaction(db);
        const string tableName = "documents";
        try
        {
            if (DatabaseExt.TableExists(db, tableName))
            {
                "Таблица 'documents' уже существует.".PrintAsWarningMessage();
                return;
            }
            else
            {
                "Таблица 'documents' не найдена. Создание таблицы...".PrintAsConsoleSystemMessage();
            }

            DatabaseExt.ExecuteCommand(db, create_table_sql);
            "Таблица 'documents' была успешно создана.".PrintAsConsoleSystemMessage();
            DatabaseExt.ExecuteCommand(db, init_vector_sql);
            "Поле 'embedding' было инициализировано как векторное поле.".PrintAsConsoleSystemMessage();
            DatabaseExt.CommitWithRollback(db);
        }
        finally
        {
            DatabaseExt.CloseDatabase(db);
        }
    }

    private void HandleArgs(string[]? args)
    {
        if (args != null && args.Length > 0)
        {
            if (args.Contains("--recreate"))
            {
                RemoveDatabase();
            }
        }
    }

    private void RemoveDatabase()
    {
        "Параметр --recreate обнаружен. Пересоздание базы данных...".PrintAsConsoleSystemMessage();
        if (File.Exists(DatabasePath))
        {
            File.Delete(DatabasePath);
            "Существующая база данных была удалена.".PrintAsConsoleSystemMessage();
        }
    }
}
