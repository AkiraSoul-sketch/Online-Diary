using SQLitePCL;

namespace LocalAiToolCLI.DatabaseManagementContext;

public delegate T? QueryResultMap<T>(sqlite3_stmt statement);
