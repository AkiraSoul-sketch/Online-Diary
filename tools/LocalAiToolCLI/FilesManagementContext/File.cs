namespace LocalAiToolCLI.FilesManagementContext;

public sealed class LocalFile
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public required string Path { get; init; }
}
