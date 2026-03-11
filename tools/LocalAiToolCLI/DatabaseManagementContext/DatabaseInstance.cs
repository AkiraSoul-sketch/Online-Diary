using LocalAiToolCLI.LoggingManagement;
using SQLitePCL;

namespace LocalAiToolCLI.DatabaseManagementContext;

public sealed class DatabaseInstance(sqlite3 db, string path) : IDisposable
{
    public sqlite3 Db { get; private init; } = db;
    public string Path { get; private set; } = path;
    public bool Closed { get; private set; } = false;
    public bool ExtensionLoadingEnabled { get; set; } = false;

    public void Dispose()
    {
        if (Closed)
        {
            return;
        }

        this.ExecuteAction(() => raw.sqlite3_close_v2(Db));
        Closed = true;
        Db.Dispose();
    }

    private void AllowExtensionsLoad()
    {
        ThrowErrorIfClosed();
        if (ExtensionLoadingEnabled)
        {
            return;
        }

        this.ExecuteAction(() => raw.sqlite3_enable_load_extension(Db, 1));
        ExtensionLoadingEnabled = true;
    }

    public void InstallExtension(string extensionPath)
    {
        ThrowErrorIfClosed();
        if (!ExtensionLoadingEnabled)
        {
            AllowExtensionsLoad();
        }

        this.ExecuteAction(() =>
        {
            utf8z nullPointerWrapper = utf8z.FromIntPtr(IntPtr.Zero);
            string fullPath = PathExtensions.CombineWithExecutableDirectory(extensionPath);
            utf8z pointer = utf8z.FromString(fullPath);
            return raw.sqlite3_load_extension(
                Db,
                pointer,
                nullPointerWrapper,
                out utf8z errorMessage
            );
        });
    }

    public void ThrowErrorIfClosed()
    {
        if (Closed)
        {
            string error = "Инстанс базы данных закрыт";
            error.PrintErrorMessage();
            throw new ApplicationException(error);
        }
    }
}

public sealed class DatabaseTransaction : IDisposable
{
    private DatabaseTransaction(DatabaseInstance instance)
    {
        _instance = instance;
    }

    private bool _isDisposed = false;
    private readonly DatabaseInstance _instance;

    public void Dispose()
    {
        throw new NotImplementedException();
    }

    public static DatabaseTransaction Create(DatabaseInstance instance)
    {
        const string sql = "BEGIN TRANSACTION;";
        instance.ExecuteNonQuery(sql);
        return new DatabaseTransaction(instance);
    }

    private void CommitTransaction()
    {
        const string sql = "COMMIT;";
        _instance.ExecuteNonQuery(sql);
    }

    private void Rollback()
    {
        const string sql = "ROLLBACK;";
        _instance.ExecuteNonQuery(sql);
    }
}
