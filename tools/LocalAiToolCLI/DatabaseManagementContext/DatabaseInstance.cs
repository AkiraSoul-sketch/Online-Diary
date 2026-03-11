using SQLitePCL;

namespace LocalAiToolCLI.DatabaseManagementContext;

public sealed class DatabaseInstance
{
    public DatabaseInstance() { }

    public static bool IsError(int rc)
    {
        return !(rc == raw.SQLITE_OK || rc == raw.SQLITE_DONE || rc == raw.SQLITE_ROW);
    }
}
