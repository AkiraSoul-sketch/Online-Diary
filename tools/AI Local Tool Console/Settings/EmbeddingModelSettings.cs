namespace AI_Local_Tool_Console.Settings;

public sealed class EmbeddingModelSettings
{
    private static readonly string DEFAULT_EXECUTION_PATH = AppDomain.CurrentDomain.BaseDirectory;
    private string? _modelFolder;
    private string? _tokenizerPath;
    private string? _modelPath;
    private string? _customLibraryPath;
    public required string ModelFolder
    {
        get =>
            _modelFolder ?? throw new InvalidOperationException("Model folder is not configured.");
        set => _modelFolder = Path.Combine(DEFAULT_EXECUTION_PATH, value);
    }

    public string CustomLibraryPath
    {
        get
        {
            if (string.IsNullOrWhiteSpace(_customLibraryPath))
            {
                _customLibraryPath = Path.Combine(
                    ModelFolder,
                    "_extensions_pydll.cpython-313-x86_64-linux-gnu.so"
                );
                return _customLibraryPath;
            }

            return _customLibraryPath;
        }
    }

    public string TokenizerPath
    {
        get
        {
            if (string.IsNullOrWhiteSpace(_tokenizerPath))
            {
                _tokenizerPath = Path.Combine(ModelFolder, "tokenizer.onnx");
                return _tokenizerPath;
            }

            return _tokenizerPath;
        }
    }

    public string ModelPath
    {
        get
        {
            if (string.IsNullOrWhiteSpace(_modelPath))
            {
                _modelPath = Path.Combine(ModelFolder, "model.onnx");
                return _modelPath;
            }

            return _modelPath;
        }
    }
}
