namespace Models
{
    public class InputMenu
    {
        string introduction;
        bool exit;
        List<InputMenuOption> menuoptions = new List<InputMenuOption>();
        int row;

        public int GetMenuOptionsCount() => this.menuoptions.Count;

        public InputMenu(string introduction = "", bool exit = false, int row = 1)
        {
            this.introduction = introduction;
            this.exit = exit;
            this.row = row;
        }

        /// <summary>
        /// Add item to menu option list
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Act"></param>
        /// <param name="isTaken"></param>
        public void Add(string Name, Action<string> Act, bool? isTaken = null) => this.menuoptions.Add(new InputMenuOption(Name, Act, isTaken));

        /// <summary>
        /// Remove item from menu option list
        /// </summary>
        public void Remove() => this.menuoptions.Remove(this.menuoptions[this.menuoptions.Count - 1]);

        /// <summary>
        /// Print menu to screen
        /// </summary>
        public void Draw(int cursor)
        {
            Console.Clear();
            Console.WriteLine(this.introduction);
            for (int i = 0; i <= this.menuoptions.Count; i++)
            {
                if (i == cursor)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine((i == this.menuoptions.Count) ? (this.exit) ? "> Exit" : "> Back" : $"> {this.menuoptions[i].Name}");
                    Console.ResetColor();
                }
                else { Console.WriteLine(i == this.menuoptions.Count ? this.exit ? "  Exit" : "  Back" : $"  {this.menuoptions[i].Name}"); }
            };
        }

        /// <summary>
        /// Activating the menu
        /// </summary>
        /// <param name="row"></param>
        public void UseMenu(int row = 1)
        {
            Console.CursorVisible = false;
            int cursor = 0;
            ConsoleKey userInput = ConsoleKey.Delete;
            //Main loop
            while (userInput != ConsoleKey.Q)
            {
                Draw(cursor);

                //Getting User choice
                userInput = Console.ReadKey().Key;

                if ((userInput == ConsoleKey.UpArrow || userInput == ConsoleKey.W) && cursor > 0) { cursor--; }
                else if ((userInput == ConsoleKey.DownArrow || userInput ==  ConsoleKey.S) && cursor < this.menuoptions.Count) { cursor++; }
                else if (userInput == ConsoleKey.Enter)
                {
                    if (cursor == this.menuoptions.Count)
                    {
                        Console.Clear();
                        if (this.exit) Environment.Exit(0);
                        return;
                    }
                    else { this.menuoptions[cursor].Act(""); }
                }
            }
        }
    }
}