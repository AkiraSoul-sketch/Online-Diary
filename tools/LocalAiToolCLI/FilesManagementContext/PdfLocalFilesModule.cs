using System.Text;
using UglyToad.PdfPig;
using UglyToad.PdfPig.Content;

namespace LocalAiToolCLI.FilesManagementContext;

public static class PdfLocalFilesModule
{
    extension(LocalFile file)
    {
        public string ExtractPdfText()
        {
            using PdfDocument doc = PdfDocument.Open(file.Path);
            StringBuilder sb = new();
            foreach (Page page in doc.GetPages())
            {
                IEnumerable<Word> words = page.GetWords();
                sb.AppendLine(string.Join(' ', words.Select(w => w.Text)));
            }

            return sb.ToString();
        }

        public bool IsPdfFile()
        {
            return file.Name.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase);
        }
    }
}
