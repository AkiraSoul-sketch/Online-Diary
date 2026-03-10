namespace AI_Local_Tool_Console.Settings;

public sealed class GenerativeModelSettings
{
    private static readonly string DEFAULT_EXECUTION_PATH = AppDomain.CurrentDomain.BaseDirectory;
    private string? _modelFolder;

    public required string ModelFolder
    {
        get =>
            _modelFolder ?? throw new InvalidOperationException("Model folder is not configured.");
        set => _modelFolder = System.IO.Path.Combine(DEFAULT_EXECUTION_PATH, value);
    }

    public string Path => System.IO.Path.Combine(ModelFolder, "tokenizer.onnx");
}
