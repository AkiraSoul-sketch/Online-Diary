namespace LocalAiToolCLI.SettingsManagementContext;

public sealed class DatabaseSettings
{
    public required string UsingDatabasePath { get; init; }
    public required string OriginalDatabasePath { get; init; }
    public string[]? ExtensionFiles { get; set; }
}

public sealed class EmbeddingModelSettings
{
    private static readonly string BasePath = AppDomain.CurrentDomain.BaseDirectory;
    public string TokenizerPath { get; private set; } = string.Empty;
    public required string BaseFolderPath { get; init { field = value; TokenizerPath = Path.Combine(BasePath, value); } }
}

public sealed class GenerativeModelSettings
{
    private static readonly string BasePath = AppDomain.CurrentDomain.BaseDirectory;
    public string ModelPath { get; private set; } = string.Empty;
    public required string BaseFolderPath { get; init { field = value; ModelPath = Path.Combine(BasePath, value); } }
}

public static class StringPrintingExtensions
{
    extension(string input)
    {
        public void PrintSystemMessage()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(input);
            Console.ResetColor();
        }

        public void PrintErrorMessage()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(input);
            Console.ResetColor();
        }

        public void PrintWarningMessage()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(input);
            Console.ResetColor();
        }

        public void PrintSuccessMessage()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(input);
            Console.ResetColor();
        }
    }
}