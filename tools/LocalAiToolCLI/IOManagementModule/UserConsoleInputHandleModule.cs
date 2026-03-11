namespace LocalAiToolCLI.IOManagementModule;

public static class UserConsoleInputHandleModule
{
    extension(Console)
    {
        public static bool UserPressedContinue()
        {
            while (true)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey();
                char keyChar = char.ToUpperInvariant(keyInfo.KeyChar);
                if (keyChar == 'N')
                {
                    return false;
                }
                if (keyChar == 'Y')
                    return true;
            }
        }
    }
}
