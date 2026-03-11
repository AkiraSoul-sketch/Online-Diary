using SQLitePCL;

namespace LocalAiToolCLI.DatabaseManagementContext;

public static class DatabaseExecutionModule
{
    extension(DatabaseInstance)
    {
        public static void ExecuteActionStatic(Func<int> action)
        {
            int rc = action();
            if (DatabaseInstance.IsError(rc))
            {
                DatabaseInstance.HandleError(rc);
            }
        }
    }

    extension(DatabaseInstance instance)
    {
        public void ExecuteNonQuery(string sql)
        {
            int rc = raw.sqlite3_exec(instance.Db, sql, out string errorMsg);
            if (DatabaseInstance.IsError(rc))
            {
                DatabaseInstance.HandleError(rc, utf8z.FromString(errorMsg));
            }
        }

        public void ExecuteAction(Func<int> action)
        {
            int rc = action();
            if (DatabaseInstance.IsError(rc))
            {
                DatabaseInstance.HandleError(rc);
            }
        }

        public void ExecuteAction(Func<DatabaseInstance, int> action)
        {
            int rc = action(instance);
            if (DatabaseInstance.IsError(rc))
            {
                DatabaseInstance.HandleError(rc);
            }
        }
    }
}
