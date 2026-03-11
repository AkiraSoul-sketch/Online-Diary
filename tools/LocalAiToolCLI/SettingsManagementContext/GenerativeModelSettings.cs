namespace LocalAiToolCLI.SettingsManagementContext;

public sealed class GenerativeModelSettings
{
    private static readonly string BasePath = AppDomain.CurrentDomain.BaseDirectory;
    public string ModelPath { get; private set; } = string.Empty;
    public required string BaseFolderPath
    {
        get;
        init
        {
            field = value;
            ModelPath = Path.Combine(BasePath, value);
        }
    }
}
