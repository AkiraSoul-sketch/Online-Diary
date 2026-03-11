namespace LocalAiToolCLI.SettingsManagementContext;

public sealed class DocumentationFolderSettings
{
    public required string FolderPath { get; init; }
    public string[]? FileExtensions { get; set; }
    public string[]? FilesIgnored { get; set; }
}
