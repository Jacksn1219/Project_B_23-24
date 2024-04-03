using System.Data.SQLite;

namespace DataAccessLibrary;

public class Layout
{
    /// <summary>
    /// Create folder if it doesnt exsist
    /// </summary>
    /// <param name="folderName"></param>
    public static void setupFolder(string folderName) => System.IO.Directory.CreateDirectory(System.IO.Path.GetFullPath(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\" + folderName)));
    private static string databasePath
    {
        get { setupFolder("DataSource"); return System.IO.Path.GetFullPath(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\DataSource")); }
    }
    /// <summary>
    /// Create database connection
    /// </summary>
    /// <returns>SQLiteConnection object</returns>
    static SQLiteConnection CreateConnection()
    {
        SQLiteConnection sqlite_conn;
        // Create a new database connection:
        sqlite_conn = new SQLiteConnection($"Data Source={databasePath}\\database.db; Version = 3; New = True; Compress = True; ");
        // Open the connection:
        try { sqlite_conn.Open(); }
        catch (Exception) { }
        return sqlite_conn;
    }
    /// <summary>
    /// Create a table by excecuting the querry
    /// </summary>
    /// <param name="conn"></param>
    /// <param name="CreateQuerry"></param>
    /// <exception cref="System.Data.SQLite.SQLiteException"></exception>
    static void ExcecuteQuerry(SQLiteConnection conn, string CreateQuerry)
    {
        SQLiteCommand sqlite_cmd;
        sqlite_cmd = conn.CreateCommand();
        try
        {
            sqlite_cmd.CommandText = CreateQuerry;
            sqlite_cmd.ExecuteNonQuery();
        }
        catch (System.Data.SQLite.SQLiteException)
        {
            throw new System.Data.SQLite.SQLiteException();
        }
    }
    /// <summary>
    /// Made to upload a new Layout to the database.
    /// </summary>
    /// <param name="layout"></param>
    /// <param name="room"></param>
    public static void upload_to_database(List<Seat> seats, Room room)
    {
        SQLiteConnection sqlite_conn;
        sqlite_conn = CreateConnection();

        try
        {
            ExcecuteQuerry(sqlite_conn, $@"INSERT INTO Room(
                    Name,
                    Capacity
                ) VALUES (
                    '{room.Name}',
                    {room.Capacity}
                ); ");
            foreach (Seat seat in seats)
            {
                ExcecuteQuerry(sqlite_conn, $@"INSERT INTO Seat(
                    RoomID,
                    Name,
                    Rank,
                    Type
                ) VALUES (
                    {seat.RoomID},
                    '{seat.Name}',
                    '{seat.Rank}',
                    '{seat.Type}'
                ); ");
            }
        } catch { sqlite_conn.Close(); }
        sqlite_conn.Close();
    }
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
        string chosenSeat = Console.ReadLine() ?? "00";
        if (chosenSeat.Length != 2) chosenSeat = "00";
        while (chosenSeat.Length != 2 && !alfabet.Contains(chosenSeat[0]) && ((int)chosenSeat[1] >= rowWidth || (int)chosenSeat[1] < 0))
        {
            Console.WriteLine("That is not a valid input, give a location in a form like ('a1' or 'd5')");
            chosenSeat = Console.ReadLine() ?? "00";
        }
        string seatLocation = $"{(alfabet.IndexOf(chosenSeat[0]) + 1) * chosenSeat[1]}";
        Seat? selectedSeat = layout.FirstOrDefault(t => t.Name == seatLocation);

        //ShowSeatInfo(selectedSeat); - Jelle
        Console.WriteLine("Not yet implemented - ShowSeatInfo");
        Console.ReadLine();
    }
    public static void MakeNewLayout()
    {
        //getting the correct room ID
        //int Room_ID = getRoomsFromDatabase().Count;

        List<Seat> seats = new List<Seat>();
        //Room currentRoom = new Room(Room_ID, $"Room{Room_ID}", seats.Count)
        Room currentRoom = new Room(1, "Room1", seats.Count);

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
                        //ConsoleKey.D1 or ConsoleKey.NumPad1 => new Seat(seats.Count, Room_ID, $"{seats.Where(s => s.RoomID == 1).Count()}", "1", "Regular"),
                        //ConsoleKey.D2 or ConsoleKey.NumPad2 => new Seat(seats.Count, Room_ID, $"{seats.Where(s => s.RoomID == 1).Count()}", "2", "Regular"),
                        //ConsoleKey.D3 or ConsoleKey.NumPad3 => new Seat(seats.Count, Room_ID, $"{seats.Where(s => s.RoomID == 1).Count()}", "3", "Regular"),
                        //ConsoleKey.Enter => new Seat(seats.Count, Room_ID, "", "\n", " "),
                        ConsoleKey.D1 or ConsoleKey.NumPad1 => new Seat(seats.Count, 1, $"{seats.Where(s => s.RoomID == 1).Count()}", "1", "Regular"),
                        ConsoleKey.D2 or ConsoleKey.NumPad2 => new Seat(seats.Count, 1, $"{seats.Where(s => s.RoomID == 1).Count()}", "2", "Regular"),
                        ConsoleKey.D3 or ConsoleKey.NumPad3 => new Seat(seats.Count, 1, $"{seats.Where(s => s.RoomID == 1).Count()}", "3", "Regular"),
                        ConsoleKey.Enter => new Seat(seats.Count, 1, "", "\n", " "),
                        _ => throw new NotImplementedException()
                    });
                } catch { }
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
        //upload_to_database(seats, new Room(Room_ID, $"Room{Room_ID}", seats.Count));
        upload_to_database(seats, currentRoom);

        selectSeat(seats);
        //drawLayout(seats);
    }
}
