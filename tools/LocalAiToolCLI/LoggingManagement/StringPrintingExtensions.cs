namespace LocalAiToolCLI.LoggingManagement;

public static class StringPrintingExtensions
{
    extension(string input)
    {
        public void PrintSystemMessage()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(input);
            Console.ResetColor();
        }

        public void PrintErrorMessage()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(input);
            Console.ResetColor();
        }

        public void PrintWarningMessage()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(input);
            Console.ResetColor();
        }

        public void PrintSuccessMessage()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(input);
            Console.ResetColor();
        }
    }
}
