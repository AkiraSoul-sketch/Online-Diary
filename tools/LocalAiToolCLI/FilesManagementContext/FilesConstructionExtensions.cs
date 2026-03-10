namespace LocalAiToolCLI.FilesManagementContext;

public static class FilesConstructionExtensions
{
    extension(LocalFile)
    {
        public static LocalFile[] GetFromDirectoryRoot(string directoryRootPath, string fileExtensions, string[]? filterNames = null)
        {
            DirectoryInfo directory = new(directoryRootPath);
            EnumerationOptions options = new() { RecurseSubdirectories = true };
            IEnumerable<FileInfo> filesSequence = directory.EnumerateFiles(fileExtensions);
            if (filterNames != null && filterNames.Length > 0)
            {
                filesSequence = filesSequence.Where(f => !filterNames.Any(fn => f.Name.Contains(fn, StringComparison.OrdinalIgnoreCase)));
            }

            return [.. filesSequence.Select(FromFileInfo)];
        }

        public static LocalFile FromFileInfo(FileInfo fileInfo)
        {                        
            return new LocalFile()
            {
                Id = Guid.NewGuid(),
                Name = fileInfo.Name,
                Path = fileInfo.FullName
            };
        }
    }
}
