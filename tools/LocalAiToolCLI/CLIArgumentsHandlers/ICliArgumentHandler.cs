namespace LocalAiToolCLI.CLIArgumentsHandlers;

public interface ICliArgumentHandler
{
    public bool CanHandle(string[] arguments);
    public Task Handle(string[] arguments);
}
