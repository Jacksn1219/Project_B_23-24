namespace DataAccessLibrary;

public class Layout
{
    public static int getRowWidth(List<Seat> layout)
    {
        List<string> layoutRanks = new List<string>();
        foreach (Seat seat in layout) layoutRanks.Add(seat.Rank);

        string[] joinedLayout = String.Join("", layoutRanks).Split("\n");
        int rowWidth = joinedLayout.Max(x => x.Length);
        return rowWidth;
    }
    public static void drawLayout(List<Seat> layout)
    {
        Console.Clear();
        Console.ResetColor();

        //List<Seat> layout = getSeatsFromDatabase();
        int rowWidth = getRowWidth(layout);
        string alfabet = "abcdefghijklmnopqrstuvwxyz";
        for (int i = 1; i < rowWidth + 1; i++) { Console.Write("  " + alfabet[i - 1]); }

        int Row = 1;
        Console.Write("\n" + Row);
        foreach (Seat seat in layout)
        {
            Console.ForegroundColor = seat.Rank switch
            {
                "1" => ConsoleColor.Blue,
                "2" => ConsoleColor.DarkYellow,
                "3" => ConsoleColor.DarkRed,
                _ => ConsoleColor.Gray
            };
            if (seat.Rank == "\n") { Row++; Console.Write("\n" + Row); }
            else if (seat.Rank == " ") { Console.Write("   "); }
            else { Console.Write($" []"); }
        }
        Console.ResetColor();
    }
    public static void selectSeat(List<Seat> layout)
    {
        //List<Seat> layout = getSeatsFromDatabase(); - Aymane
        drawLayout(layout);

        int rowWidth = getRowWidth(layout);
        string alfabet = "abcdefghijklmnopqrstuvwxyz";

        Console.WriteLine("\n\nGive a location in a form like ('a1' or 'd5')");
        string chosenSeat = Console.ReadLine();
        if (chosenSeat.Length != 2) chosenSeat = "00";
        while (chosenSeat.Length != 2 && !alfabet.Contains(chosenSeat[0]) && ((int)chosenSeat[1] >= rowWidth || (int)chosenSeat[1] < 0))
        {
            Console.WriteLine("That is not a valid input, give a location in a form like ('a1' or 'd5')");
            chosenSeat = Console.ReadLine();
        }
        string seatLocation = $"{(alfabet.IndexOf(chosenSeat[0]) + 1) * chosenSeat[1]}";
        Seat selectedSeat = layout.FirstOrDefault(t => t.Name == seatLocation);

        //ShowSeatInfo(selectedSeat); - Jelle
        Console.WriteLine("Not yet implemented - ShowSeatInfo");
        Console.ReadLine();
    }
    public static void MakeNewLayout()
    {
        List<Seat> seats = new List<Seat>();

        Console.WriteLine("Press one of these (1, 2, 3, Enter, Q):\n");

        ConsoleKey userInput = ConsoleKey.Delete;
        while (userInput != ConsoleKey.Q)
        {
            //Getting User choice
            userInput = Console.ReadKey().Key;
            Console.Clear();
            if (userInput == ConsoleKey.Backspace) seats.RemoveAt(seats.Count - 1);
            else try
                {
                    seats.Add(userInput switch
                    {
                        ConsoleKey.Spacebar => new Seat(seats.Count, 1, "", " ", " "),
                        ConsoleKey.D1 or ConsoleKey.NumPad1 => new Seat(seats.Count, 1, $"{seats.Where(s => s.RoomID == 1).Count()}", "1", "Regular"),
                        ConsoleKey.D2 or ConsoleKey.NumPad2 => new Seat(seats.Count, 1, $"{seats.Where(s => s.RoomID == 1).Count()}", "2", "Regular"),
                        ConsoleKey.D3 or ConsoleKey.NumPad3 => new Seat(seats.Count, 1, $"{seats.Where(s => s.RoomID == 1).Count()}", "3", "Regular"),
                        ConsoleKey.Enter => new Seat(seats.Count, 1, "", "\n", " "),
                        _ => throw new NotImplementedException()
                    });
                }
                catch { }
            foreach (Seat seat in seats)
            {
                Console.ForegroundColor = seat.Rank switch
                {
                    "1" => ConsoleColor.Blue,
                    "2" => ConsoleColor.DarkYellow,
                    "3" => ConsoleColor.DarkRed,
                    _ => ConsoleColor.Gray
                };
                Console.Write(" " + seat.Rank);
            }
        }
        //Adding the seats to the database
        //SQLite.addSeatToDatabase(seats);
        selectSeat(seats);
        //drawLayout(seats);
    }
}
