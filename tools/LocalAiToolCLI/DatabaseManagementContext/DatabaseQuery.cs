using System.Globalization;
using SQLitePCL;

namespace LocalAiToolCLI.DatabaseManagementContext;

public sealed class DatabaseQuery(sqlite3_stmt statement) : IDisposable
{
    private readonly sqlite3_stmt _statement = statement;
    private bool _isDisposed = false;

    public void Dispose()
    {
        if (_isDisposed)
        {
            return;
        }

        DatabaseInstance.ExecuteActionStatic(() => raw.sqlite3_finalize(_statement));
        _statement.Dispose();
        _isDisposed = true;
    }

    public DatabaseQuery AddStringParameter(int index, string value)
    {
        int rc = raw.sqlite3_bind_text(_statement, index, value);
        DatabaseInstance.HandlePossibleError(rc);
        return this;
    }

    public DatabaseQuery AddBlobParameter(int index, byte[] value)
    {
        int rc = raw.sqlite3_bind_blob(_statement, index, value);
        DatabaseInstance.HandlePossibleError(rc);
        return this;
    }

    public DatabaseQuery AddVectorAsJson(int index, float[] value)
    {
        string json =
            "["
            + string.Join(",", value.Select(v => v.ToString("R", CultureInfo.InvariantCulture)))
            + "]";
        AddStringParameter(index, json);
        return this;
    }

    public DatabaseQuery AddVectorAsBlob(int index, float[] value)
    {
        int bytesLength = sizeof(float) * value.Length;
        byte[] bytes = new byte[bytesLength];
        Buffer.BlockCopy(value, 0, bytes, 0, bytesLength);
        int rc = raw.sqlite3_bind_blob(_statement, index, bytes);
        DatabaseInstance.HandlePossibleError(rc);
        return this;
    }

    public DatabaseQuery AddGuidParameter(int index, Guid value)
    {
        string guidString = value.ToString();
        AddStringParameter(index, guidString);
        return this;
    }

    public void ExecuteAction()
    {
        int rc = raw.sqlite3_step(_statement);
        HandleError(rc);
    }

    public bool ExecuteExistsByCount()
    {
        int rc = raw.sqlite3_step(_statement);
        HandleError(rc);
        if (rc != raw.SQLITE_ROW)
        {
            return false;
        }

        int count = raw.sqlite3_column_int(_statement, 0);
        return count != 0;
    }

    public bool ExecuteExists()
    {
        int rc = raw.sqlite3_step(_statement);
        HandleError(rc);
        return rc == raw.SQLITE_ROW;
    }

    private void HandleError(int rc)
    {
        if (DatabaseInstance.IsError(rc))
        {
            raw.sqlite3_finalize(_statement);
            DatabaseInstance.HandleError(rc);
        }
    }
}
