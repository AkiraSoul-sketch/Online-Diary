namespace AI_Local_Tool_Console.Settings;

public sealed class DatabaseSettings
{
    private static readonly string DEFAULT_EXECUTION_PATH = AppDomain.CurrentDomain.BaseDirectory;
    private string? _databasePath;
    private string? _originalDatabasePath;
    private string? _vectorExtensionPath;

    public required string DatabasePath
    {
        get =>
            _databasePath
            ?? throw new InvalidOperationException("Database path is not configured.");
        set => _databasePath = Path.Combine(DEFAULT_EXECUTION_PATH, value);
    }
    public required string OriginalDatabasePath
    {
        get =>
            _originalDatabasePath
            ?? throw new InvalidOperationException("Original database path is not configured.");
        set => _originalDatabasePath = Path.Combine(DEFAULT_EXECUTION_PATH, value);
    }
    public required string VectorExtensionPath
    {
        get =>
            _vectorExtensionPath
            ?? throw new InvalidOperationException("Vector extension path is not configured.");
        set => _vectorExtensionPath = Path.Combine(DEFAULT_EXECUTION_PATH, value);
    }
}
