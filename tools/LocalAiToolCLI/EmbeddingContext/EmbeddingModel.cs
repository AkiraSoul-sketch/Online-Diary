using LocalAiToolCLI.SettingsManagementContext;
using Microsoft.Extensions.Options;

namespace LocalAiToolCLI.EmbeddingContext;

public sealed class EmbeddingModel
{
    public EmbeddingModel()
    {
        
    }

    private readonly IOptions<EmbeddingModelSettings> _settings;
    private string Path => _settings.Value.TokenizerPath;
    private bool _disposed = false;
}