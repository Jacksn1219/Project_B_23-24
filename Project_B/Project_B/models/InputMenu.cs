﻿using DataAccessLibrary;
using Project_B;

namespace Models
{
    public class InputMenu
    {
        string introduction;
        bool exit;
        List<InputMenuOption> menuoptions = new List<InputMenuOption>();
        int row;

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
        public void Add(string Name, Action<string> Act, bool? isTaken = null, int? ID = null) => this.menuoptions.Add(ID == null ? new InputMenuOption(Name, Act, isTaken) : new InputMenuOption(Name, Act, isTaken));

        /// <summary>
        /// Remove item from menu option list
        /// </summary>
        public void Remove() => this.menuoptions.Remove(this.menuoptions[this.menuoptions.Count - 1]);

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
                    Console.ForegroundColor = ConsoleColor.Cyan;
                }
                else
                {
                    try
                    {
                        Console.ForegroundColor = this.menuoptions[i].Name switch
                        {
                            "N" => ConsoleColor.Blue,
                            "E" => ConsoleColor.DarkYellow,
                            "L" => ConsoleColor.Magenta,
                            _ => ConsoleColor.Gray
                        };
                    } catch { }
                }
                Console.Write((i == this.menuoptions.Count) ? this.exit ? "\n" + Universal.centerToScreen("Exit") : "\n" + Universal.centerToScreen("Back") : new List<string> { "N", "E", "L", "\n", " " }.Contains($"{this.menuoptions[i].Name}") ? $" {this.menuoptions[i].Name} " : Universal.centerToScreen($"{this.menuoptions[i].Name}"));
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
            ConsoleKey userInput = ConsoleKey.Delete;
            Console.Clear();
            //Main loop
            while (userInput != ConsoleKey.Q)
            {
                Draw(cursor, printMenu);

                //Getting User choice
                userInput = Console.ReadKey().Key;

                if ((userInput == ConsoleKey.UpArrow || userInput == ConsoleKey.W) && cursor > 0)
                {
                    cursor = Math.Max(cursor - row, 0);
                    while (cursor > 0 && this.menuoptions[cursor].Name == " ") cursor = Math.Max(cursor - row, 0);
                    while (this.menuoptions[cursor].Name == " " && cursor < this.menuoptions.Count) cursor++;
                }
                else if ((userInput == ConsoleKey.DownArrow || userInput == ConsoleKey.S) && cursor < this.menuoptions.Count)
                {
                    cursor = Math.Min(cursor + row, this.menuoptions.Count);
                    while (cursor < this.menuoptions.Count && this.menuoptions[cursor].Name == " ") cursor = Math.Min(cursor + row, this.menuoptions.Count);
                }
                else if ((userInput == ConsoleKey.LeftArrow || userInput == ConsoleKey.A) && cursor > 0)
                {
                    if (this.menuoptions[cursor - 1].Name != " ")
                    {
                        cursor--;
                        while (cursor > 0 && this.menuoptions[cursor].Name == " ") cursor--;
                    }
                }
                else if ((userInput == ConsoleKey.RightArrow || userInput == ConsoleKey.D) && cursor < this.menuoptions.Count)
                {
                    cursor++;
                    while (cursor < this.menuoptions.Count && this.menuoptions[cursor].Name == " ") cursor++;
                }
                else if (userInput == ConsoleKey.Enter)
                {
                    Console.Clear();
                    if (cursor == this.menuoptions.Count)
                    {
                        Console.Clear();
                        if (this.exit) Environment.Exit(0);
                        return;
                    }
                    else {
                        this.menuoptions[cursor].Act("");
                        Console.Clear();
                    }
                }
            }
        }
    }
}