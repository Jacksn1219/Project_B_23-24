using System.Diagnostics;

namespace Project_B.menu_s;

public static class StartupMenu
{
    public static void UseMenu(Action dataToLoad)
    {
        ShowWelcomeMessage();

        Thread.Sleep(400);
        //Universal.WriteColor("\n\n                                         To navigate the menus you can only use:\n", ConsoleColor.Blue);
        //Console.WriteLine("                                          [w], [s], [a], [d], [enter], [escape]\n\n\n\n\n\n\n\n\n");
        Console.Write($"\n\n\n\n\n                                                    Loading data...");

        Universal.WriteColor("\n\n\n\n\n\n\n\n\n                                  [", ConsoleColor.Blue);
        Console.Write(" Use arrow keys, w, s, a, d, Enter, Esc to navigate menu's ");
        Universal.WriteColor("]", ConsoleColor.Blue);

        Console.SetCursorPosition(45, Console.CursorTop - 9);

        //----- Setup starting data -----//
        dataToLoad.Invoke();

        Universal.PressAnyKeyWaiter();

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