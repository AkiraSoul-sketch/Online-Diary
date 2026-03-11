namespace LocalAiToolCLI.SettingsManagementContext;

public sealed class GenerativeModelSettings
{
    public required string BaseFolderPath
    {
        get;
        init { field = value; }
    }
}
