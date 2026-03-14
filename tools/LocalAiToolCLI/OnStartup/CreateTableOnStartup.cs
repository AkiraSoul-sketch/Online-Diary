using LocalAiToolCLI.DatabaseManagementContext;
using LocalAiToolCLI.LoggingManagement;
using Microsoft.Extensions.DependencyInjection;

namespace LocalAiToolCLI.OnStartup;

public static class CreateTableOnStartup
{
    extension(OnStartupDelegate)
    {
        public static void AddCreateDocumentsTableOnStartup(IServiceCollection services)
        {
            services.AddSingleton<OnStartupDelegate>(
                (sp) =>
                {
                    using IServiceScope scope = sp.CreateScope();
                    using DatabaseInstance instance =
                        scope.ServiceProvider.GetRequiredService<DatabaseInstance>();
                    if (IsDocumentsTableCreated(instance))
                    {
                        return;
                    }

                    StartTableCreationProcedure(instance);
                    Console.EmptyLine();
                }
            );
        }

        private static bool IsDocumentsTableCreated(DatabaseInstance instance)
        {
            "Проверка на существование таблицы 'documents' в БД.".PrintSystemMessage();
            const string sql = """
                SELECT COUNT(*) FROM sqlite_master WHERE type='table' and name='documents' LIMIT 1;
                """;
            using DatabaseQuery query = instance.CreateQuery(sql);
            bool exists = query.ExecuteExistsByCount();
            if (exists)
            {
                "Таблица 'documents' существует. Пропуск процедуры создания.".PrintSuccessMessage();
            }
            else
            {
                "Таблица 'documents' не существует. Начало процедуры создания.".PrintSuccessMessage();
            }

            return exists;
        }

        private static void StartTableCreationProcedure(DatabaseInstance instance)
        {
            "Для продолжения использования инструмента, необходимо создание таблицы 'documents'.".PrintSystemMessage();
            "Чтобы продолжить создание таблицы 'documents', нажмите [Y/y]. В противном случае, нажмите [N/n]".PrintSystemMessage();
            HandleUserContinueInput();
            "Начало транзакции".PrintSystemMessage();
            using DatabaseTransaction transaction = instance.BeginTransaction();
            "Транзакция создана".PrintSystemMessage();
            const string sql = """
                CREATE TABLE documents (
                    id UUID PRIMARY KEY,
                    file_path TEXT NOT NULL,
                    file_name TEXT NOT NULL,
                    embedding BLOB
                )
                """;
            instance.ExecuteNonQuery(sql);
            "Выполнена процедура создания таблицы 'documents':".PrintSuccessMessage();
            sql.PrintSystemMessage();
            transaction.Commit();
            "Транзакция была успешна выполнена".PrintSuccessMessage();
        }

        private static void HandleUserContinueInput()
        {
            while (true)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey();
                char keyChar = char.ToUpperInvariant(keyInfo.KeyChar);
                if (keyChar == 'N')
                {
                    string message =
                        "Было принято решение отменить создание таблицы 'documents'. Закрытие приложения.";
                    throw new ApplicationException(message);
                }
                if (keyChar == 'Y')
                    break;
            }
        }
    }
}
