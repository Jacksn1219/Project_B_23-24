namespace Models
{
    public class InputMenu
    {
        string introduction;
        bool exit;
        List<InputMenuOption> menuoptions = new List<InputMenuOption>();
        int row;

        public int GetMenuOptionsCount() { return this.menuoptions.Count; }

        public InputMenu(string introduction = "", bool exit = false, int row = 1)
        {
            this.introduction = introduction;
            this.exit = exit;
            this.row = row;
        }

        //Add item to menu option list
        public void Add(string Name, Action<string> Act, bool? isTaken = null) //Adding the menu options created in the menu calling part
        {
            this.menuoptions.Add(new InputMenuOption(Name, Act, isTaken));
        }

        //Remove item from menu option list
        public void Remove()
        {
            this.menuoptions.Remove(this.menuoptions[this.menuoptions.Count - 1]);
        }

        //Print menu to screen
        public void Draw()
        {
            Console.Clear();
            Console.WriteLine(this.introduction);
            for (int i = 0; i <= this.menuoptions.Count; i++)
            {
                if (i == cursor)
                {
                    Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine((i == this.menuoptions.Count) ? (this.exit) ? "> Exit" : "> Back" : $"> {this.menuoptions[i].Name}");
                    Console.ResetColor();
                }
                else
                {
                    if (i == this.menuoptions.Count)
                    {
                        if (this.exit) { Console.WriteLine("  Exit"); }
                        else { Console.WriteLine("  Back"); }
                    }
                    else { Console.WriteLine($"  {this.menuoptions[i].Name}"); }
                }
            };
        }

        // Main loop, getting users menuchoice
        private int cursor = 0;
        public void UseMenu(int row = 1)
        {
            Console.CursorVisible = false;
            //ConsoleColor DefaultFront = ConsoleColor.White;
            //ConsoleColor DefaultBack = ConsoleColor.Black;
            cursor = 0;

            ConsoleKey userInput = ConsoleKey.Delete;
            //Main loop
            while (userInput != ConsoleKey.Q)
            {
                Draw();

                //Getting User choice
                userInput = Console.ReadKey().Key;
                switch (userInput)
                {
                    case ConsoleKey.UpArrow or ConsoleKey.W:
                        if (cursor > 0) { cursor--; }
                        break;
                    case ConsoleKey.DownArrow or ConsoleKey.S:
                        if (cursor < this.menuoptions.Count) { cursor++; }
                        break;
                    case ConsoleKey.Enter:
                        if (cursor == this.menuoptions.Count && this.exit == true)
                        {
                            Console.Clear();
                            Environment.Exit(0);
                        }
                        else if (cursor == this.menuoptions.Count && this.exit == false)
                        {
                            Console.Clear();
                            return;
                        }
                        else { this.menuoptions[cursor].Act(""); }
                        break;
                    default: continue;
                }
            }
        }

        class InputMenuOption
        {
            public string Name;
            public Action<string> Act;
            public bool? isTaken;
            public InputMenuOption(string Name, Action<string> Act, bool? isTaken)
            {
                this.Name = Name;
                this.Act = Act;
                this.isTaken = isTaken;
            }
        }
    }
}