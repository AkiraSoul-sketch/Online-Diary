namespace LocalAiToolCLI.SettingsManagementContext;

public sealed class DatabaseSettings
{
    public required string UsingDatabasePath { get; init; }
    public required string OriginalDatabasePath { get; init; }
    public string[]? ExtensionFiles { get; set; }
}
