namespace AI_Local_Tool_Console;

public static class MyConsole
{
    extension(string message)
    {
        public void PrintAsConsoleSystemMessage()
        {
            PrintSystemMessage(message);
        }

        public void PrintAsWarningMessage()
        {
            PrintWarningMessage(message);
        }

        public void PrintAsConsoleErrorMessage()
        {
            PrintErrorMessage(message);
        }
    }

    public static void PrintWarningMessage(string message)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"[Предупреждение] {message}");
        Console.ForegroundColor = ConsoleColor.White;
    }

    public static void PrintSystemMessage(string message)
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine($"[Система] {message}");
        Console.ForegroundColor = ConsoleColor.White;
    }

    public static void PrintErrorMessage(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"[Ошибка] {message}");
        Console.ForegroundColor = ConsoleColor.White;
    }
}
