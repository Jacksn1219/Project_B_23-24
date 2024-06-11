using Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Project_B.menu_s
{
    public static class AdminMenu
    {
        public static bool IsLogedIn = false;
        public static void UseMenu(Dictionary<string, Action<string>> menuItems)
        {
            if (LogIn())
            {
                // ------ Medewerker menu met menu opties ------//
                //create menu
                InputMenu medewerkerMenu = new InputMenu("Admin menu");
                medewerkerMenu.Add(menuItems);
                medewerkerMenu.UseMenu((title) => Universal.printAsTitle(title));
            }
            IsLogedIn = false;
        }
        private static bool LogIn()
        {
            if (IsLogedIn) return IsLogedIn;
            string fileName = "Medewerker.json";
            JObject? jsonData = (JObject?)JsonConvert.DeserializeObject(File.ReadAllText(Universal.datafolderPath + "\\" + fileName));
            string passWord;
            if (jsonData != null)
            {
                var x = jsonData["Value"] ?? "";
                passWord = x.Value<string>() ?? ""; ;
            }
            else passWord = "";
            while (!IsLogedIn)
            {
                Universal.printAsTitle("Login");
                Console.Write("\n");
                string userInput = Universal.takeUserInput("Password") ?? "";
                if (userInput.Equals(passWord)) IsLogedIn = true;
                else
                {
                    Universal.ChangeColour(ConsoleColor.Red);
                    Console.WriteLine("Invalid password!\nPress Q to quit.\nPress any oter button to continue.");
                    Universal.ChangeColour(ConsoleColor.White);
                    char key = Console.ReadKey().KeyChar;
                    System.Console.WriteLine(); // enter
                    if (key.Equals('q') || key.Equals('Q')) return false;
                }
            }
            return true;
        }
    }
}