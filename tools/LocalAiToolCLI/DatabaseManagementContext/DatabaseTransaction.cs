using SQLitePCL;

namespace LocalAiToolCLI.DatabaseManagementContext;

public sealed class DatabaseTransaction : IDisposable
{
    private DatabaseTransaction(DatabaseInstance instance)
    {
        _instance = instance;
    }

    private bool _isDisposed = false;
    private bool _transactionCommited = false;
    private readonly DatabaseInstance _instance;

    public void Dispose()
    {
        if (_isDisposed)
        {
            return;
        }

        _isDisposed = true;
        if (!_transactionCommited)
        {
            ExecuteRollback();
        }
    }

    public static DatabaseTransaction Create(DatabaseInstance instance)
    {
        const string sql = "BEGIN TRANSACTION;";
        instance.ExecuteNonQuery(sql);
        return new DatabaseTransaction(instance);
    }

    public void Commit()
    {
        if (DatabaseInstance.IsError(ExecuteCommit(out string message)))
        {
            string error = $"Ошибка при коммите транзакции: {message}";
            ExecuteRollback();
            throw new ApplicationException(error);
        }
    }

    private int ExecuteCommit(out string error)
    {
        const string sql = "COMMIT;";
        int rc = raw.sqlite3_exec(_instance.Db, sql, out error);
        _transactionCommited = true;
        return rc;
    }

    private int ExecuteRollback()
    {
        const string sql = "ROLLBACK;";
        return raw.sqlite3_exec(_instance.Db, sql, out _);
    }
}
