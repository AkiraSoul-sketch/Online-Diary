namespace LocalAiToolCLI.DatabaseManagementContext;

public static class PathExtensions
{
    private static readonly string ExecutableDirectory = AppDomain.CurrentDomain.BaseDirectory;
    extension(Path)
    {
        public static string CombineWithExecutableDirectory(string path)
        {
            return Path.Combine(ExecutableDirectory, path);
        }

        public static string CombineWithExecutableDirectory(string[] paths)
        {
            return Path.Combine(ExecutableDirectory, Path.Combine(paths));
        }
    }
}
