using LocalAiToolCLI.LoggingManagement;
using LocalAiToolCLI.SettingsManagementContext;
using Microsoft.Extensions.Options;
using SQLitePCL;

namespace LocalAiToolCLI.DatabaseManagementContext;

public sealed class DatabaseInstance : IDisposable
{
    public DatabaseInstance(IOptions<DatabaseSettings> settings)
    {
        Db = DatabaseInstance.Create(settings);
        string baseDir = AppDomain.CurrentDomain.BaseDirectory;
        FullDbPath = Path.Combine(baseDir, settings.Value.UsingDatabasePath);
        FullOrignDbPath = Path.Combine(baseDir, settings.Value.OriginalDatabasePath);
        DbPath = settings.Value.UsingDatabasePath;
        OrigDbPath = settings.Value.OriginalDatabasePath;
    }

    public string OrigDbPath { get; }
    public string DbPath { get; }
    public string FullOrignDbPath { get; }
    public string FullDbPath { get; }
    private bool _closed = false;
    public sqlite3 Db { get; }

    public bool Exists()
    {
        return File.Exists(FullDbPath);
    }

    public void Generate()
    {
        File.Copy(FullOrignDbPath, DbPath);
    }

    public void Dispose()
    {
        if (_closed)
        {
            return;
        }

        this.ExecuteAction(() => raw.sqlite3_close_v2(Db));
        _closed = true;
        Db.Dispose();
        GC.Collect(GC.MaxGeneration, GCCollectionMode.Aggressive, blocking: true, compacting: true);
        GC.WaitForPendingFinalizers();
        GC.Collect(GC.MaxGeneration, GCCollectionMode.Aggressive, blocking: true, compacting: true);
    }

    public void ThrowErrorIfClosed()
    {
        if (_closed)
        {
            string error = "Инстанс базы данных закрыт";
            error.PrintErrorMessage();
            throw new ApplicationException(error);
        }
    }

    public DatabaseTransaction BeginTransaction()
    {
        return DatabaseTransaction.Create(this);
    }
}
