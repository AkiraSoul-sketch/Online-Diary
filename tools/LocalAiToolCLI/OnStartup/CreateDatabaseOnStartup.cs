using LocalAiToolCLI.DatabaseManagementContext;
using LocalAiToolCLI.LoggingManagement;
using LocalAiToolCLI.SettingsManagementContext;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace LocalAiToolCLI.OnStartup;

public static class CreateDatabaseOnStartupExtension
{
    extension(OnStartupDelegate)
    {
        public static void AddCreateDatabaseOnStartup(IServiceCollection services)
        {
            services.AddSingleton<OnStartupDelegate>(
                async (sp) =>
                {
                    using IServiceScope scope = sp.CreateScope();
                    using DatabaseInstance db =
                        scope.ServiceProvider.GetRequiredService<DatabaseInstance>();
                    IOptions<DatabaseSettings> settings = scope.ServiceProvider.GetRequiredService<
                        IOptions<DatabaseSettings>
                    >();
                    if (!UsingDatabaseExists(db))
                    {
                        ProcessDatabaseCreationProcedure(db);
                    }
                    Console.EmptyLine();
                }
            );
        }

        private static bool UsingDatabaseExists(DatabaseInstance instance)
        {
            $"Проверка наличия базы данных: {instance.DbPath}".PrintSystemMessage();
            bool exists = instance.Exists();
            if (!exists)
            {
                $"Файл базы данных: {instance.DbPath} не существует.".PrintWarningMessage();
            }
            else
            {
                $"Файл базы данных: {instance.DbPath} существует.".PrintSuccessMessage();
            }
            return exists;
        }

        private static void ProcessDatabaseCreationProcedure(DatabaseInstance instance)
        {
            $"Файл базы данных: '{instance.DbPath}' не найден.".PrintSystemMessage();
            $"Необходимо создать базу данных для дальнейшей работы с инструментом".PrintSystemMessage();
            "Для продолжения процедуры нажмите на клавишу [Y/y]. В противном случае, нажмите [N/n]".PrintSystemMessage();
            HandleUserInput();
            $"Создание файла базы данных на основе оригинального: '{instance.OrigDbPath}'".PrintSystemMessage();
            instance.Generate();
            $"База данных: '{instance.DbPath}' была успешно создана".PrintSuccessMessage();
        }

        private static void HandleUserInput()
        {
            while (true)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey();
                char pressedChar = char.ToUpperInvariant(keyInfo.KeyChar);
                if (pressedChar == 'N')
                {
                    string error = "Отмена операции создания базы данных.";
                    error.PrintErrorMessage();
                    throw new ApplicationException(error);
                }

                if (pressedChar == 'Y')
                {
                    break;
                }
            }
        }
    }
}
