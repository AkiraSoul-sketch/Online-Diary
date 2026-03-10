namespace LocalAiToolCLI.FilesManagementContext;

public sealed class File
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public required string Path { get; init; }
}

public static class FilesConstructionExtensions
{
    public static File[] GetFromDirectoryRoot(string directoryRootPath)
    {
        DirectoryInfo directory = new(directoryRootPath);
        
    }
}
