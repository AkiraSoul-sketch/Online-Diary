using SQLitePCL;

namespace LocalAiToolCLI.DatabaseManagementContext;

public static class DatabaseQueryMappingModule
{
    extension(sqlite3_stmt statement)
    {
        public string GetString(int index)
        {
            return raw.sqlite3_column_text(statement, index).utf8_to_string();
        }

        public string GetGuid(int index)
        {
            return Guid.Parse(statement.GetString(index)).ToString();
        }

        public double GetDouble(int index)
        {
            return raw.sqlite3_column_double(statement, index);
        }
    }

    extension<T>(QueryResultMap<T?>)
    {
        public static QueryResultMap<T?> Create(Func<sqlite3_stmt, T?> mapFunction)
        {
            return new QueryResultMap<T?>(mapFunction);
        }
    }
}
