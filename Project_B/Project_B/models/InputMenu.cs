using DataAccessLibrary;
using Project_B;
using System.Linq;

namespace Models
{
    public class InputMenu
    {
        string introduction;
        bool? exit;
        List<InputMenuOption> menuoptions;
        int row;

        /// <summary>
        /// Title (useLambda for use of displayAsTitle()) | exit (null = back after choice, false = Back, true = Exit)
        /// </summary>
        /// <param name="introduction"></param>
        /// <param name="exit"></param>
        /// <param name="row"></param>
        public InputMenu(string introduction = "", bool? exit = false, int row = 1)
        {
            this.introduction = introduction;
            this.exit = exit;
            this.menuoptions = new List<InputMenuOption>();
            this.row = row;
        }

        public void editIntro(string newIntro) => this.introduction = newIntro;

        public int GetMenuOptionsCount() => menuoptions.Count;

        /// <summary>
        /// Add item to menu option list
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Act"></param>
        /// <param name="isTaken"></param>
        public void Add(string Name, Action<string> Act, bool isTaken = false, int? ID = null)
        {
            this.menuoptions.Add(ID == null ? new InputMenuOption(Name, Act, isTaken) : new InputMenuOption(Name, Act, isTaken, ID));
        }
        public void Add(Dictionary<string, Action<string>> toAdd)
        {
            foreach (var item in toAdd) { Add(item.Key, item.Value); }
        }

        /// <summary>
        /// Remove item from menu option list
        /// </summary>
        public void Remove() => this.menuoptions.Remove(this.menuoptions[this.menuoptions.Count - 1]);
        public void Remove(string optionName)
        {
            try { this.menuoptions.Remove(this.menuoptions[this.menuoptions.FindIndex(menuoption => menuoption.Name == optionName)]); }
            catch { }
        }

        public void Edit(int ID, string newName) => this.menuoptions[this.menuoptions.FindIndex((x) => x.ID == ID)].Name = newName;

        /// <summary>
        /// Print menu to screen
        /// </summary>
        public void Draw(int cursor, Action? printMenu)
        {
            //Console.Clear();
            Console.SetCursorPosition(0, 0);
            if (this.introduction == "useLambda" && printMenu != null) printMenu();
            else Console.WriteLine(this.introduction);
            Console.WriteLine();

            for (int i = 0; i <= this.menuoptions.Count; i++)
            {
                if (i % row == 0) Console.Write("\n");
                if (i == cursor)
                {
                    //Console.BackgroundColor = ConsoleColor.Gray;
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write((i == this.menuoptions.Count) ? (this.exit ?? false) ? "\n" + Universal.centerToScreen("Exit") : "\n" + Universal.centerToScreen("Back") : new List<string> { "N", "E", "L", "\n", " " }.Contains($"{this.menuoptions[i].Name}") ? $">{this.menuoptions[i].Name}<" : Universal.centerToScreen($"{this.menuoptions[i].Name}"));
                }
                else
                {
                    try
                    {
                        
                        if (this.menuoptions[i].isTaken == true) { this.menuoptions[i].Name = "X"; }
                    }
                    catch { }
                    try
                    {
                        Console.ForegroundColor = this.menuoptions[i].Name switch
                        {
                            "N" => ConsoleColor.Blue,
                            "E" => ConsoleColor.DarkYellow,
                            "L" => ConsoleColor.Magenta,
                            "X" => ConsoleColor.DarkGray,
                            _ => ConsoleColor.Gray
                        };
                    }
                    catch { }
                    try { if (this.menuoptions[i].isTaken == true) Console.ForegroundColor = ConsoleColor.DarkGray; } catch { Console.ForegroundColor = ConsoleColor.Gray; }
                    Console.Write((i == this.menuoptions.Count) ? (this.exit ?? false) ? "\n" + Universal.centerToScreen("Exit") : "\n" + Universal.centerToScreen("Back") : new List<string> { "N", "E", "L", "X", "\n", " " }.Contains($"{this.menuoptions[i].Name}") ? $" {this.menuoptions[i].Name} " : Universal.centerToScreen($"{this.menuoptions[i].Name}"));
                }
                Console.ResetColor();
            };
        }

        /// <summary>
        /// Activating the menu
        /// </summary>
        /// <param name="row"></param>
        public void UseMenu(Action? printMenu = null)
        {
            Console.CursorVisible = false;
            int cursor = 0;
            if (cursor == 0) while ((this.menuoptions[cursor].Name == " " || this.menuoptions[cursor].Name == "X" || this.menuoptions[cursor].isTaken == true) && cursor < this.menuoptions.Count) cursor++;
            ConsoleKey userInput = ConsoleKey.Delete;
            Console.Clear();
            //Main loop
            while (userInput != ConsoleKey.Q)
            {
                Draw(cursor, printMenu);
                Console.ForegroundColor = ConsoleColor.Black;

                //Getting User choice
                userInput = Console.ReadKey().Key;
                Console.ForegroundColor = ConsoleColor.White;

                if ((userInput == ConsoleKey.UpArrow || userInput == ConsoleKey.W) && cursor > 0)
                {
                    cursor = Math.Max(cursor - row, 0);
                    while (cursor > 0 && (this.menuoptions[cursor].Name == " " || this.menuoptions[cursor].Name == "X" || this.menuoptions[cursor].isTaken == true)) cursor = Math.Max(cursor - row, 0);
                    while ((this.menuoptions[cursor].Name == " " || this.menuoptions[cursor].Name == "X" || this.menuoptions[cursor].isTaken == true) && cursor < this.menuoptions.Count) cursor++;
                }
                else if ((userInput == ConsoleKey.DownArrow || userInput == ConsoleKey.S) && cursor < this.menuoptions.Count)
                {
                    cursor = Math.Min(cursor + row, this.menuoptions.Count);
                    while (cursor < this.menuoptions.Count && (this.menuoptions[cursor].Name == " " || this.menuoptions[cursor].Name == "X" || this.menuoptions[cursor].isTaken == true)) cursor = Math.Min(cursor + row, this.menuoptions.Count);
                }
                else if ((userInput == ConsoleKey.LeftArrow || userInput == ConsoleKey.A) && cursor > 0)
                {
                    if (this.menuoptions[cursor - 1].Name != " ")
                    {
                        cursor--;
                        while (cursor > 0 && (this.menuoptions[cursor].Name == " " || this.menuoptions[cursor].Name == "X" || this.menuoptions[cursor].isTaken == true)) cursor--;
                    }
                }
                else if ((userInput == ConsoleKey.RightArrow || userInput == ConsoleKey.D) && cursor < this.menuoptions.Count)
                {
                    cursor++;
                    while (cursor < this.menuoptions.Count && (this.menuoptions[cursor].Name == " " || this.menuoptions[cursor].Name == "X" || this.menuoptions[cursor].isTaken == true)) cursor++;
                }
                else if (userInput == ConsoleKey.Escape)
                {
                    Console.Clear();
                    Console.WriteLine("\x1b[3J");
                    if (this.exit == true) Environment.Exit(0);
                    return;
                }
                else if (userInput == ConsoleKey.Enter)
                {
                    Console.Clear();
                    if (cursor == this.menuoptions.Count)
                    {
                        Console.Clear();
                        if (this.exit == true) Environment.Exit(0);
                        return;
                    }
                    else
                    {
                        this.menuoptions[cursor].Act("");
                        Console.Clear();
                        if (this.exit == null) return;
                    }
                }
            }
        }
    }
}