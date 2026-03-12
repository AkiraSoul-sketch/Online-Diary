using Microsoft.ML.OnnxRuntimeGenAI;

namespace LocalAiToolCLI.LoggingManagement;

public static class StringPrintingExtensions
{
    extension(Console)
    {
        public static void EmptyLine()
        {
            Console.WriteLine();
        }
    }

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

        public void PrintUserInput()
        {
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine(input);
            Console.ResetColor();
        }
    }
}
