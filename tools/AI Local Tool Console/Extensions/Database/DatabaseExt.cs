using System.Globalization;
using System.Text.Json;
using AI_Local_Tool_Console.Settings;
using SQLitePCL;

namespace AI_Local_Tool_Console.Extensions.Database;

public static class DatabaseExt
{
    public static void CloseDatabase(sqlite3 db)
    {
        raw.sqlite3_close(db);
    }

    public static void InitializeVectorForColumn(sqlite3 db, string tableName, string columnName)
    {
        const string init_vector_sql = """
                SELECT vector_init(?1, ?2, 'type=FLOAT32,dimension=1024');
            """;
        int rc = raw.sqlite3_prepare_v2(db, init_vector_sql, out sqlite3_stmt stmt);
        try
        {
            HandleDbError(rc, db);
            raw.sqlite3_bind_text(stmt, 1, tableName);
            HandleDbError(rc, db);
            raw.sqlite3_bind_text(stmt, 2, columnName);
            HandleDbError(rc, db);
            rc = raw.sqlite3_step(stmt);
            HandleDbError(rc, db);
        }
        finally
        {
            raw.sqlite3_finalize(stmt);
            stmt.Dispose();
        }
    }

    public static void FinalizeStatement(sqlite3_stmt stmt)
    {
        raw.sqlite3_finalize(stmt);
        stmt.Dispose();
    }

    public static void OpenDb(string filePath, out sqlite3 db)
    {
        int result = raw.sqlite3_open(filePath, out db);
        HandleError(result);
    }

    public static void BeginTransaction(sqlite3 db)
    {
        ExecuteCommand(db, "BEGIN TRANSACTION;");
    }

    public static void CommitTransaction(sqlite3 db)
    {
        ExecuteCommand(db, "COMMIT;");
    }

    public static void CommitWithRollback(sqlite3 db)
    {
        int rc = raw.sqlite3_exec(db, "COMMIT;");
        if (IsError(rc))
        {
            raw.sqlite3_exec(db, "ROLLBACK;");
            HandleDbError(rc, db);
        }
    }

    public static void ExecuteCommand(sqlite3 db, string sql)
    {
        int result = raw.sqlite3_exec(db, sql);
        HandleDbError(result, db);
    }

    public static void OpenDb(DatabaseSettings settings, out sqlite3 db)
    {
        OpenDb(settings.DatabasePath, out db);
    }

    public static void CreateStatement(sqlite3 db, string sql, out sqlite3_stmt stmt)
    {
        int rc = raw.sqlite3_prepare_v2(db, sql, out stmt);
        HandleDbError(rc, db);
    }

    public static void AddVectorToStatementAsJson(sqlite3_stmt stmt, int index, float[] vector)
    {
        string json =
            "["
            + string.Join(",", vector.Select(f => f.ToString("R", CultureInfo.InvariantCulture)))
            + "]";
        utf8z parameter = utf8z.FromString(json);
        int rc = raw.sqlite3_bind_text(stmt, index, parameter);
        HandleError(rc);
    }

    public static void AddVectorToStatementAsBlob(sqlite3_stmt stmt, int index, float[] vector)
    {
        int byteLength = vector.Length * sizeof(float);
        byte[] bytes = new byte[byteLength];
        Buffer.BlockCopy(vector, 0, bytes, 0, byteLength);
        raw.sqlite3_bind_blob(stmt, index, bytes);
    }

    public static void ExecuteStatement(sqlite3_stmt stmt)
    {
        try
        {
            int rc = raw.sqlite3_step(stmt);
            HandleError(rc);
        }
        finally
        {
            FinalizeStatement(stmt);
        }
    }

    public static void AddBlobToStatement(sqlite3_stmt stmt, int index, byte[] data)
    {
        int rc = raw.sqlite3_bind_blob(stmt, index, data);
        HandleError(rc);
    }

    public static void AddGuidToStatement(sqlite3_stmt stmt, int index, Guid guid)
    {
        string text = guid.ToString();
        AddTextToStatement(stmt, index, text);
    }

    public static void AddTextToStatement(sqlite3_stmt stmt, int index, string text)
    {
        int rc = raw.sqlite3_bind_text(stmt, index, text);
        HandleError(rc);
    }

    public static void EnableExtensionsLoad(sqlite3 db)
    {
        const int ON_OFF = 1;
        int result = raw.sqlite3_enable_load_extension(db, ON_OFF);
        HandleError(result);
    }

    public static bool TableExists(sqlite3 db, string tableName)
    {
        const string sql = """
            SELECT name from sqlite_master WHERE type='table' AND name=?1 LIMIT 1;
            """;
        int rc = raw.sqlite3_prepare_v2(db, sql, out sqlite3_stmt stmt);
        try
        {
            HandleDbError(rc, db);
            rc = raw.sqlite3_bind_text(stmt, 1, tableName);
            HandleDbError(rc, db);
            rc = raw.sqlite3_step(stmt);
            HandleDbError(rc, db);
            if (rc == raw.SQLITE_ROW)
            {
                return true;
            }

            return false;
        }
        finally
        {
            raw.sqlite3_finalize(stmt);
        }
    }

    public static void LoadExtension(DatabaseSettings settings, sqlite3 db)
    {
        LoadExtension(db, settings.VectorExtensionPath);
    }

    public static void LoadExtension(sqlite3 db, string extensionPath)
    {
        utf8z filePathPointerWrapper = utf8z.FromString(extensionPath);
        utf8z nullPointerWrapper = utf8z.FromIntPtr(IntPtr.Zero);
        int result = raw.sqlite3_load_extension(
            db,
            filePathPointerWrapper,
            nullPointerWrapper,
            out utf8z errorMessage
        );
        HandleError(result, errorMessage);
    }

    private static void HandleError(int result, utf8z errorPointerWrapper)
    {
        if (!IsError(result))
        {
            return;
        }

        string error = PointerToString(errorPointerWrapper);
        throw new Exception($"SQLite error {result}: {error}");
    }

    private static void HandleError(int result, sqlite3? db = null)
    {
        if (!IsError(result))
        {
            return;
        }

        if (db != null)
        {
            // throws exception
            HandleDbError(result, db);
        }

        string error = ToErrorMessage(result);
        throw new Exception($"SQLite error {result}: {error}");
    }

    private static void HandleDbError(int result, sqlite3 db)
    {
        if (!IsError(result))
        {
            return;
        }

        string error = ToErrorMessage(db);
        throw new Exception($"SQLite error {result}: {error}");
    }

    public static bool IsError(int result)
    {
        return !(result == raw.SQLITE_OK || result == raw.SQLITE_ROW || result == raw.SQLITE_DONE);
    }

    private static string ToErrorMessage(int rc)
    {
        utf8z pointer = raw.sqlite3_errstr(rc);
        return PointerToString(pointer);
    }

    private static utf8z NullPointerWrapper()
    {
        return utf8z.FromIntPtr(IntPtr.Zero);
    }

    private static string ToErrorMessage(sqlite3 db)
    {
        utf8z pointer = raw.sqlite3_errmsg(db);
        return PointerToString(pointer);
    }

    private static string PointerToString(utf8z pointerWrapper)
    {
        return pointerWrapper.utf8_to_string();
    }
}
