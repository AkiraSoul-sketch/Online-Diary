namespace LocalAiToolCLI.SettingsManagementContext;

public sealed class EmbeddingModelSettings
{
    private static readonly string BasePath = AppDomain.CurrentDomain.BaseDirectory;
    public required string TokenizerPath
    {
        get => field;
        init { field = Path.Combine(BasePath, value); }
    }

    public required string ModelPath
    {
        get => field;
        init { field = Path.Combine(BasePath, value); }
    }
}
