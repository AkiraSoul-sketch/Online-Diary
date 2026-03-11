using LocalAiToolCLI.DatabaseManagementContext;
using LocalAiToolCLI.EmbeddingContext;
using LocalAiToolCLI.FilesManagementContext;
using LocalAiToolCLI.GenerativeModelContext;
using LocalAiToolCLI.LoggingManagement;

namespace LocalAiToolCLI.CLIArgumentsHandlers;

public sealed class AskHandler : ICliArgumentHandler
{
    public AskHandler(
        EmbeddingModel embeddingModel,
        DatabaseInstance databaseInstance,
        GenerativeModel generativeModel
    )
    {
        _embeddings = embeddingModel;
        _database = databaseInstance;
        _generative = generativeModel;
    }

    private readonly EmbeddingModel _embeddings;
    private readonly DatabaseInstance _database;
    private readonly GenerativeModel _generative;
    private const string keyword = "--ask";
    private const string alias = "-a";

    public bool CanHandle(string[] arguments)
    {
        StringComparison comparison = StringComparison.OrdinalIgnoreCase;

        if (arguments.Length < 2)
        {
            return false;
        }

        string firstArg = arguments[0];
        return firstArg.Equals(keyword, comparison) || firstArg.Equals(alias, comparison);
    }

    public async Task Handle(string[] arguments)
    {
        string input = string.Join(" ", arguments.AsSpan().Slice(1));
        using (_database)
        {
            _database.InitializeVectorsForTable("documents", "embedding");
            using (_embeddings)
            {
                LocalFile? relevant = _database.SearchRelevantFile(_embeddings, input);
                if (relevant is null)
                {
                    "По запросу не было найдено подходящей информации.".PrintWarningMessage();
                    Console.EmptyLine();
                    return;
                }

                using (_generative)
                {
                    string text = relevant.ReadContents();
                    _generative.RunInference(input, text);
                }
            }
        }
    }
}
