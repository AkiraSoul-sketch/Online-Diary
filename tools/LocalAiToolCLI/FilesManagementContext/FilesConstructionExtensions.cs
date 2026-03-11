namespace LocalAiToolCLI.FilesManagementContext;

public static class FilesConstructionExtensions
{
    extension(LocalFile)
    {
        public static LocalFile[] SearchForDocumentationFiles(
            string directoryRootPath,
            string[]? fileExtensions,
            string[]? filterNames = null
        )
        {
            LocalFile[] files = GetFromDirectoryRoot(
                directoryRootPath,
                fileExtensions,
                filterNames
            );
            return files;
        }

        private static LocalFile[] GetFromDirectoryRoot(
            string rootPath,
            string[]? extensions,
            string[]? ignored = null
        )
        {
            DirectoryInfo directory = new(rootPath);
            EnumerationOptions options = new() { RecurseSubdirectories = true };
            IEnumerable<FileInfo> files = directory.EnumerateFiles("*", options);

            if (extensions != null && extensions.Length > 0)
            {
                files = files.Where(f =>
                    extensions.Contains(f.Extension, StringComparer.OrdinalIgnoreCase)
                );
            }

            if (ignored != null && ignored.Length > 0)
            {
                files = files.Where(f =>
                    ignored.Any(fn => !f.Name.Contains(fn, StringComparison.OrdinalIgnoreCase))
                );
            }

            return [.. files.Select(FromFileInfo)];
        }

        private static LocalFile FromFileInfo(FileInfo fileInfo)
        {
            return new LocalFile()
            {
                Id = Guid.NewGuid(),
                Name = fileInfo.Name,
                Path = fileInfo.FullName,
            };
        }
    }
}
