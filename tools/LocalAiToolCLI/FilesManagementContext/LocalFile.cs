namespace LocalAiToolCLI.FilesManagementContext;

public sealed class LocalFile
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public required string Path { get; init; }

    public string ReadContents()
    {
        if (this.IsPdfFile())
        {
            return this.ExtractPdfText();
        }

        using StreamReader reader = File.OpenText(Path);
        string text = reader.ReadToEnd();
        return text;
    }
}
