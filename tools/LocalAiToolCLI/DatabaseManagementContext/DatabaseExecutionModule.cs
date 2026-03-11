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

        public static void ExecuteQuery() { }
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

        public DatabaseQuery CreateQuery(string sql)
        {
            int rc = raw.sqlite3_prepare_v2(instance.Db, sql, out sqlite3_stmt statement);
            if (DatabaseInstance.IsError(rc))
            {
                DatabaseInstance.HandleError(rc);
            }

            return new DatabaseQuery(statement);
        }

        public void InitializeVectorsForTable(string table, string column)
        {
            const string sql = """
                    SELECT vector_init(?1, ?2, 'type=FLOAT32,dimension=1024');
                """;
            using DatabaseQuery query = instance
                .CreateQuery(sql)
                .AddStringParameter(1, table)
                .AddStringParameter(2, column);
            query.ExecuteAction();
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
