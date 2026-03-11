using LocalAiToolCLI.CLIArgumentsHandlers;
using LocalAiToolCLI.DependencyInjectionModule;
using LocalAiToolCLI.LoggingManagement;
using LocalAiToolCLI.OnStartup;
using Microsoft.Extensions.DependencyInjection;

namespace LocalAiToolCliY
{
    class Program
    {
        static async Task Main(string[] args)
        {
            IServiceProvider services = InitializeServices();
            ProcessOnStartupActions(services);
            args = ["-a", "Как установить penpot в докере?"];
            await ProcessCliCommands(services, args);
        }

        private static IServiceProvider InitializeServices()
        {
            ServiceCollection services = new();
            services.AddSettings();
            services.AddInfrastructure();
            services.AddOnStartupActions();
            services.AddCliHandlers();
            return services.BuildServiceProvider();
        }

        private static void ProcessOnStartupActions(IServiceProvider services)
        {
            IEnumerable<OnStartupDelegate> actions = services.GetServices<OnStartupDelegate>();
            foreach (OnStartupDelegate action in actions)
            {
                action(services);
            }
        }

        private static async Task ProcessCliCommands(IServiceProvider services, string[] args)
        {
            if (args.Length == 0)
            {
                "Аргументы команды не предоставлены".PrintWarningMessage();
                return;
            }

            await using AsyncServiceScope scope = services.CreateAsyncScope();
            IEnumerable<ICliArgumentHandler> cliHandlers =
                scope.ServiceProvider.GetServices<ICliArgumentHandler>();
            bool handled = false;
            foreach (ICliArgumentHandler handler in cliHandlers)
            {
                if (handler.CanHandle(args))
                {
                    await handler.Handle(args);
                    handled = true;
                }
            }

            if (!handled)
            {
                "Команда не распознана.".PrintSystemMessage();
            }
        }
    }
}
