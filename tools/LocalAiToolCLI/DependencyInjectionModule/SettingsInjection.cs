using LocalAiToolCLI.CLIArgumentsHandlers;
using LocalAiToolCLI.DatabaseManagementContext;
using LocalAiToolCLI.EmbeddingContext;
using LocalAiToolCLI.GenerativeModelContext;
using LocalAiToolCLI.LoggingManagement;
using LocalAiToolCLI.OnStartup;
using LocalAiToolCLI.SettingsManagementContext;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace LocalAiToolCLI.DependencyInjectionModule;

public static class SettingsInjection
{
    extension(IServiceCollection services)
    {
        public void AddSettings()
        {
            ConfigurationManager manager = new();
            manager.AddJsonFile("appsettings.json");
            DatabaseSettings? database = manager
                .GetSection(nameof(DatabaseSettings))
                .Get<DatabaseSettings>();
            EmbeddingModelSettings? embeddings = manager
                .GetSection(nameof(EmbeddingModelSettings))
                .Get<EmbeddingModelSettings>();
            GenerativeModelSettings? genAi = manager
                .GetSection(nameof(GenerativeModelSettings))
                .Get<GenerativeModelSettings>();
            DocumentationFolderSettings? docs = manager
                .GetSection(nameof(DocumentationFolderSettings))
                .Get<DocumentationFolderSettings>();

            if (docs == null)
            {
                string error =
                    $"Секция: {nameof(DocumentationFolderSettings)} отсутствует в конфигурации.";
                error.PrintErrorMessage();
                throw new ApplicationException(error);
            }

            if (database == null)
            {
                string error = $"Секция: {nameof(DatabaseSettings)} отсутствует в конфигурации.";
                error.PrintErrorMessage();
                throw new ApplicationException(error);
            }

            if (embeddings == null)
            {
                string error =
                    $"Секция: {nameof(EmbeddingModelSettings)} отсутствует в конфигурации.";
                error.PrintErrorMessage();
                throw new ApplicationException(error);
            }

            if (genAi == null)
            {
                string error =
                    $"Секция: {nameof(GenerativeModelSettings)} отсутствует в конфигурации.";
                error.PrintErrorMessage();
                throw new ApplicationException(error);
            }

            IOptions<DatabaseSettings> dbOptions = Options.Create(database);
            IOptions<EmbeddingModelSettings> embeddingOptions = Options.Create(embeddings);
            IOptions<GenerativeModelSettings> genAiSettings = Options.Create(genAi);
            IOptions<DocumentationFolderSettings> docSettings = Options.Create(docs);

            services.AddSingleton(dbOptions);
            services.AddSingleton(embeddingOptions);
            services.AddSingleton(genAiSettings);
            services.AddSingleton(docSettings);
        }
    }
}

public static class InfrastructureInjection
{
    extension(IServiceCollection services)
    {
        public void AddInfrastructure()
        {
            services.AddScoped<DatabaseInstance>();
            services.AddScoped<EmbeddingModel>();
            services.AddScoped<GenerativeModel>();
        }
    }
}

public static class OnStartupActionsInjection
{
    extension(IServiceCollection services)
    {
        public void AddOnStartupActions()
        {
            OnStartupDelegate.AddSettingsValidation(services);
            OnStartupDelegate.AddCreateDatabaseOnStartup(services);
            OnStartupDelegate.AddCreateDocumentsTableOnStartup(services);
            OnStartupDelegate.AddDocumentsTableInitializationOnStartup(services);
        }
    }
}

public static class CliHandlersInjection
{
    extension(IServiceCollection services)
    {
        public void AddCliHandlers()
        {
            services.AddScoped<ICliArgumentHandler, AskHandler>();
        }
    }
}
