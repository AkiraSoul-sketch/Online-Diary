namespace LocalAiToolCLI.FilesManagementContext;

public sealed class LocalFile
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public required string Path { get; init; }

    public string ReadContents()
    {
        if (Name == "общая_цветовая_схема.md")
        {
            int a = 0;
        }

        using StreamReader reader = File.OpenText(Path);
        string text = reader.ReadToEnd();
        return text;
    }
}
