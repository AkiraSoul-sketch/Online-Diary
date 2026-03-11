using LocalAiToolCLI.LoggingManagement;
using SQLitePCL;

namespace LocalAiToolCLI.DatabaseManagementContext;

public static class DatabaseErrorsModule
{
    extension(DatabaseInstance)
    {
        public static void HandleError(utf8z errorMessage)
        {
            string error = errorMessage.utf8_to_string();
            $"Ошибка работы с SQLite: {error}".PrintErrorMessage();
            throw new ApplicationException($"Database error: {error}");
        }

        public static void HandleError(int rc)
        {
            if (!IsError(rc))
            {
                return;
            }

            utf8z errorPointer = raw.sqlite3_errstr(rc);
            string error = errorPointer.utf8_to_string();
            $"Ошибка работы с SQLite: {error}".PrintErrorMessage();
            throw new ApplicationException($"Database error: {error}");
        }

        public static void HandleError(int rc, utf8z errorMessage)
        {
            if (!IsError(rc))
            {
                return;
            }

            string error = errorMessage.utf8_to_string();
            $"Ошибка работы с SQLite: {error}".PrintErrorMessage();
            throw new ApplicationException($"Database error: {error}");
        }

        public static bool IsError(int errorCode)
        {
            return (
                    raw.SQLITE_OK == errorCode
                    || raw.SQLITE_DONE == errorCode
                    || raw.SQLITE_ROW == errorCode
                ) == false;
        }
    }
}
