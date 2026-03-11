using LocalAiToolCLI.SettingsManagementContext;
using Microsoft.Extensions.DependencyInjection;

namespace LocalAiToolCli
{
    class Program
    {
        static void Main(string[] args) { }

        private static IServiceProvider InitializeServices()
        {
            ServiceCollection services = new ServiceCollection();
            services.AddOptions<DatabaseSettings>();
            services.AddOptions<EmbeddingModelSettings>();
            services.AddOptions<GenerativeModelSettings>();
            return services.BuildServiceProvider();
        }
    }
}
