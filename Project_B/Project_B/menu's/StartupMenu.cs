namespace Project_B.menu_s;

public static class StartupMenu
{
    public static void UseMenu(Action dataToLoad)
    {
        ShowWelcomeMessage();

        Thread.Sleep(400);
        Console.Write($"\n\n\n\n\n                                               Loading data...");
        Console.SetCursorPosition(Console.CursorLeft - 15, Console.CursorTop);

        //----- Setup starting data -----//
        dataToLoad.Invoke();

        ConsoleKey key;
        do
        {
            Console.Write("Press <Any> key to continue...");
            Thread.Sleep(700);
            Console.SetCursorPosition(Console.CursorLeft - 30, Console.CursorTop);
            if (Console.KeyAvailable) break;
            Console.Write("                              ");
            Thread.Sleep(700);
            Console.SetCursorPosition(Console.CursorLeft - 30, Console.CursorTop);
        } while (!Console.KeyAvailable);

        key = Console.ReadKey(true).Key;
        Console.CursorVisible = true;
    }
    private static void ShowWelcomeMessage()
    {
        //----- Welkom scherm -----//
        List<Action> welcomeList = new List<Action>
        {
            () => {Universal.WriteColor("                    █████ █████", ConsoleColor.Blue); Universal.WriteColor($"                              ", ConsoleColor.Gray); Universal.WriteColor(" ██████████", ConsoleColor.Blue); Universal.WriteColor($"                            \n", ConsoleColor.Gray);},
            () => {Universal.WriteColor("                   ░░███ ░░███ ", ConsoleColor.Blue); Universal.WriteColor($"                              ", ConsoleColor.Gray); Universal.WriteColor("░░███░░░░░█", ConsoleColor.Blue); Universal.WriteColor($"                            \n", ConsoleColor.Gray);},
            () => {Universal.WriteColor("                    ░░███ ███  ", ConsoleColor.Blue); Universal.WriteColor($"  ██████  █████ ████ ████████ ", ConsoleColor.Gray); Universal.WriteColor(" ░███  █ ░ ", ConsoleColor.Blue); Universal.WriteColor($" █████ ████  ██████   █████ \n", ConsoleColor.Gray);},
            () => {Universal.WriteColor("                     ░░█████   ", ConsoleColor.Blue); Universal.WriteColor($" ███░░███░░███ ░███ ░░███░░███", ConsoleColor.Gray); Universal.WriteColor(" ░██████   ", ConsoleColor.Blue); Universal.WriteColor($"░░███ ░███  ███░░███ ███░░  \n", ConsoleColor.Gray);},
            () => {Universal.WriteColor("                      ░░███    ", ConsoleColor.Blue); Universal.WriteColor($"░███ ░███ ░███ ░███  ░███ ░░░ ", ConsoleColor.Gray); Universal.WriteColor(" ░███░░█   ", ConsoleColor.Blue); Universal.WriteColor($" ░███ ░███ ░███████ ░░█████ \n", ConsoleColor.Gray);},
            () => {Universal.WriteColor("                       ░███    ", ConsoleColor.Blue); Universal.WriteColor($"░███ ░███ ░███ ░███  ░███     ", ConsoleColor.Gray); Universal.WriteColor(" ░███ ░   █", ConsoleColor.Blue); Universal.WriteColor($" ░███ ░███ ░███░░░   ░░░░███\n", ConsoleColor.Gray);},
            () => {Universal.WriteColor("                       █████   ", ConsoleColor.Blue); Universal.WriteColor($"░░██████  ░░████████ █████    ", ConsoleColor.Gray); Universal.WriteColor(" ██████████", ConsoleColor.Blue); Universal.WriteColor($" ░░███████ ░░██████  ██████ \n", ConsoleColor.Gray);},
            () => {Universal.WriteColor("                      ░░░░░    ", ConsoleColor.Blue); Universal.WriteColor($" ░░░░░░    ░░░░░░░░ ░░░░░     ", ConsoleColor.Gray); Universal.WriteColor("░░░░░░░░░░ ", ConsoleColor.Blue); Universal.WriteColor($"  ░░░░░███  ░░░░░░  ░░░░░░  \n", ConsoleColor.Gray);},
            () => {Universal.WriteColor($"                                                                          ███ ░███                  \n", ConsoleColor.Gray);},
            () => {Universal.WriteColor($"                                                                         ░░██████                   \n", ConsoleColor.Gray);},
            () => {Universal.WriteColor($"                                                                          ░░░░░░                    ", ConsoleColor.Gray); }
        };
        Console.CursorVisible = false;
        Console.WriteLine("\n\n\n");
        for (int i = 0; i < welcomeList.Count(); i++)
        {
            welcomeList[i]();
            Thread.Sleep(100);
        }
    }
}