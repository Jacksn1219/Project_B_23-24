using DataAccessLibrary.logic;
using Models;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.SQLite;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DataAccessLibrary;

public class Layout
{
    private readonly RoomFactory _rf;
    private readonly SeatFactory _sf;
    public Layout(RoomFactory rf, SeatFactory sf)
    {
        _rf = rf;
        _sf = sf;
    }
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
    /// TEMPORARY FUNCTION : Made to upload a new Layout to the database.
    /// </summary>
    /// <param name="layout"></param>
    /// <param name="room"></param>
    public void upload_to_database(RoomModel room)
    {
        _rf.ItemToDb(room);
        /*
        SQLiteConnection sqlite_conn;
        sqlite_conn = CreateConnection();

        try
        {
            ExcecuteQuerry(sqlite_conn, $@"INSERT INTO Room(
                    Name,
                    Capacity,
                    RowWidth
                ) VALUES (
                    '{room.Name}',
                    {room.Capacity},
                    {room.RowWidth}
                ); ");
            foreach (SeatModel seat in seats)
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
        */
    }
    public static void ChangeColour(ConsoleColor colour) => Console.ForegroundColor = colour;
    public static void drawLayout(List<SeatModel> layout, RoomModel room)
    {
        Console.Clear();
        Console.ResetColor();

        //List<Seat> layout = getSeatsFromDatabase();
        string alfabet = "abcdefghijklmnopqrstuvwxyz";
        for (int i = 1; i < room.RowWidth + 1; i++) { Console.Write("  " + alfabet[i - 1]); }

        int Row = 1;
        Console.Write("\n" + Row);
        for (int i = 0; i < layout.Count; i++)
        {
            Console.ForegroundColor = layout[i].Type switch
            {
                "Normaal" => ConsoleColor.Blue,
                "Extra Beenruimte" => ConsoleColor.DarkYellow,
                "Love Seat" => ConsoleColor.Magenta,
                _ => ConsoleColor.Gray
            };
            if (i % room.RowWidth == 0) { Row++; Console.Write("\n" + Row); }
            else if (layout[i].Rank == " ") { Console.Write("   "); }
            else { Console.Write($" []"); }
        }
        Console.Write($"[{room.RowWidth - 8 / 2}Screen{room.RowWidth - 8 / 2}]");
        Console.ResetColor();
    }
    public static void selectSeat(List<SeatModel> layout, RoomModel room)
    {
        //List<Seat> layout = getSeatsFromDatabase(); - Aymane
        //drawLayout(layout, room);

        InputMenu seatSelectionMenu = new InputMenu($"  [   Screen   ]", false, room.RowWidth ?? 0);
        foreach (SeatModel seat in layout)
        {
            string seatName = seat.Type == " " ? "   " : $" []";
            seatSelectionMenu.Add($"{seat.Type[0]}", (x) =>
            {
                SeatModel selectedSeat = seat;
                Console.Clear();
                //ShowSeatInfo(selectedSeat); - Jelle
                Console.WriteLine("Not yet implemented - ShowSeatInfo");
                Console.ReadLine();
            });
        }
        seatSelectionMenu.UseMenu();
    }
    public static void WriteColor(string toPrint, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.Write(toPrint);
        Console.ForegroundColor = ConsoleColor.White;
    }
    public static void editLayout(List<SeatModel> layout, RoomModel room)
    {
        //List<Seat> layout = getSeatsFromDatabase(); - Aymane
        //Room room = getRoomFromDatabase(); - Aymane

        InputMenu seatSelectionMenu = new InputMenu($"  [   Screen   ]", false, room.RowWidth ?? 0);
        string seatName;
        string getType;
        string getRank;
        foreach (SeatModel seat in layout)
        {
            seatName = seat.Type == " " ? "   " : $" []";
            seatSelectionMenu.Add($"{seat.Type[0]}", (x) =>
            {
                getType = seat.Type;
                getRank = seat.Rank;
                SeatModel selectedSeat = seat;
                Console.Clear();

                ConsoleKey userInput = ConsoleKey.Delete;
                while (userInput != ConsoleKey.Q)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.White;
                    switch (getType)
                    {
                        case "Normaal":
                            Console.Write("\n\nType: ");
                            WriteColor("N", ConsoleColor.Blue);
                            Console.Write(" E L");
                            break;
                        case "Extra Beenruimte":
                            Console.Write("\n\nType: N ");
                            WriteColor("E", ConsoleColor.DarkYellow);
                            Console.Write(" L");
                            break;
                        case "Love Seat":
                            Console.Write("\n\nType: N E ");
                            WriteColor("L", ConsoleColor.Magenta);
                            break;
                        default:
                            Console.Write("\n\nType: N E L");
                            break;
                    };
                    switch (getRank)
                    {
                        case "1":
                            Console.Write($"\nRank: ");
                            WriteColor(getRank, ConsoleColor.DarkCyan);
                            Console.Write($" 2 3");
                            break;
                        case "2":
                            Console.Write($"\nRank: 1 ");
                            WriteColor(getRank, ConsoleColor.DarkCyan);
                            Console.Write($" 3");
                            break;
                        case "3":
                            Console.Write($"\nRank: 1 2 ");
                            WriteColor(getRank, ConsoleColor.DarkCyan);
                            break;
                        default:
                            Console.Write("\nRank: 1 2 3");
                            break;
                    };
                    Console.Write("\n\nDruk op een van de volgende toetsen om het nieuwe type te selecteren (");
                    WriteColor("N", ConsoleColor.Blue);
                    Console.Write(", ");
                    WriteColor("E", ConsoleColor.DarkYellow);
                    Console.Write(", ");
                    WriteColor("L", ConsoleColor.Magenta);
                    Console.Write(", 1, 2, 3, Enter, Spatiebalk)\n\n");

                    Console.Write("Uitleg:\n  (");
                    WriteColor("N", ConsoleColor.Blue);
                    Console.Write(") = Normaal                (1) = Betaal niveau 1        (Spatiebalk) = Lege plek instellen\n  (");
                    WriteColor("E", ConsoleColor.DarkYellow);
                    Console.Write(") = Extra beenruimte       (2) = Betaal niveau 2        (Enter) = goedkeuren aanpassing\n  (");
                    WriteColor("L", ConsoleColor.Magenta);
                    Console.Write(") = Love Seat              (3) = Betaal niveau 3");

                    //Getting User choice
                    userInput = Console.ReadKey().Key;

                    if (new List<ConsoleKey> { ConsoleKey.N, ConsoleKey.E, ConsoleKey.L, ConsoleKey.Spacebar, ConsoleKey.NumPad1, ConsoleKey.NumPad2, ConsoleKey.NumPad3, ConsoleKey.D1, ConsoleKey.D2, ConsoleKey.D3 }.Contains(userInput))
                    {
                        try
                        {
                            getType = userInput switch
                            {
                                ConsoleKey.Spacebar => " ",
                                ConsoleKey.N => "Normaal",
                                ConsoleKey.E => "Extra Beenruimte",
                                ConsoleKey.L => "Love Seat",
                                _ => throw new NotImplementedException()
                            };
                        }
                        catch { }
                        try
                        {
                            getRank = userInput switch
                            {
                                ConsoleKey.Spacebar => " ",
                                ConsoleKey.D1 or ConsoleKey.NumPad1 => "1",
                                ConsoleKey.D2 or ConsoleKey.NumPad2 => "2",
                                ConsoleKey.D3 or ConsoleKey.NumPad3 => "3",
                                _ => throw new NotImplementedException()
                            };
                        }
                        catch { }
                    }
                    else if (userInput == ConsoleKey.Enter)
                    {
                        if (getType == "0" || getRank == "0") Console.WriteLine("Not all required fields are filled in...");
                        else
                        {
                            seatSelectionMenu.Edit(Int32.Parse(seat.Name), $"{getType[0]}");
                            layout[Int32.Parse(selectedSeat.Name)].Type = getType;
                            layout[Int32.Parse(selectedSeat.Name)].Rank = getRank;
                            userInput = ConsoleKey.Q;
                        }
                    }
                };
            }, null, Int32.Parse(seat.Name));
        }
        seatSelectionMenu.UseMenu();
    }
    public void MakeNewLayout()
    {
        //Getting the correct room ID
        /*int Room_ID = getRoomsFromDatabase().Count;*/

        List<SeatModel> seats = new List<SeatModel>();
        /*Room currentRoom = new Room(Room_ID, $"Room{Room_ID}", seats.Count)*/
        RoomModel currentRoom = new RoomModel("Room1", seats.Count, 6);

        Console.Clear();
        Console.WriteLine("  [   screen   ]");
        //Console.WriteLine("Press one of these:\nN = Normaal\nE = Extra beenruimte\nL = Love Seat\nRank: (1, 2, 3)\nBackspace\nEnter = 1x dan automatisch\nQ = goedkeuren volgorde.\n");

        string getType = "0";
        string getRank = "0";

        ConsoleKey userInput = ConsoleKey.Delete;
        while (userInput != ConsoleKey.Q)
        {
            Console.ForegroundColor = ConsoleColor.White;
            switch (getType)
            {
                case "Normaal":
                    Console.Write("\n\nType: ");
                    WriteColor("N", ConsoleColor.Blue);
                    Console.Write(" E L");
                    break;
                case "Extra Beenruimte":
                    Console.Write("\n\nType: N ");
                    WriteColor("E", ConsoleColor.DarkYellow);
                    Console.Write(" L");
                    break;
                case "Love Seat":
                    Console.Write("\n\nType: N E ");
                    WriteColor("L", ConsoleColor.Magenta);
                    break;
                default:
                    Console.Write("\n\nType: N E L");
                    break;
            };
            switch (getRank)
            {
                case "1":
                    Console.Write($"\nRank: ");
                    WriteColor(getRank, ConsoleColor.DarkCyan);
                    Console.Write($" 2 3");
                    break;
                case "2":
                    Console.Write($"\nRank: 1 ");
                    WriteColor(getRank, ConsoleColor.DarkCyan);
                    Console.Write($" 3");
                    break;
                case "3":
                    Console.Write($"\nRank: 1 2 ");
                    WriteColor(getRank, ConsoleColor.DarkCyan);
                    break;
                default:
                    Console.Write("\nRank: 1 2 3");
                    break;
            };
            Console.Write("\n\nDruk op een van de volgende toetsen (");
            WriteColor("N", ConsoleColor.Blue);
            Console.Write(", ");
            WriteColor("E", ConsoleColor.DarkYellow);
            Console.Write(", ");
            WriteColor("L", ConsoleColor.Magenta);
            Console.Write(", 1, 2, 3, Enter, Backspace, ");
            WriteColor("Q", ConsoleColor.Red);
            Console.Write(", Spatiebalk, A)\n\n");

            /*Console.Write("Stoel types:\n (");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("N");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(") = Normaal\n (");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write("E");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(") = Extra beenruimte\n (");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("L");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(") = Love Seat \n\nBetaal niveaus:\n (1) = niveau 1\n (2) = niveau 2\n (3) = niveau 3 \n\nAnders:\n (Enter) = 1x dan automatisch\n (Backspace) \n (");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Q");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(") = goedkeuren volgorde \n");*/

            //Console.Write("Stoel types:                 Betaal niveaus:              Anders:\n  (");
            Console.Write("Uitleg:\n  (");
            WriteColor("N", ConsoleColor.Blue);
            Console.Write(") = Normaal                (1) = Betaal niveau 1        (Enter) = 1x dan automatisch        (Spatiebalk) = Lege plek instellen\n  (");
            WriteColor("E", ConsoleColor.DarkYellow);
            Console.Write(") = Extra beenruimte       (2) = Betaal niveau 2        (Backspace)                         (A) = Ingestelde stoel toevoegen\n  (");
            WriteColor("L", ConsoleColor.Magenta);
            Console.Write(") = Love Seat              (3) = Betaal niveau 3        (");
            WriteColor("Q", ConsoleColor.Red);
            Console.Write(") = goedkeuren volgorde \n");

            //Getting User choice
            userInput = Console.ReadKey().Key;

            Console.Clear();
            if (userInput == ConsoleKey.Backspace && seats.Count > 0) seats.RemoveAt(seats.Count - 1);
            else if (userInput == ConsoleKey.Enter && currentRoom.RowWidth == 1)
            {
                currentRoom.RowWidth = seats.Count;
            }
            else if (userInput == ConsoleKey.A)
            {
                if (getType == "0" || getRank == "0") Console.WriteLine("Not all required fields are filled in...");
                //new Seat(seats.Count, Room_ID, $"{seats.Where(s => s.RoomID == 1).Count()}", getRank, getType),
                else seats.Add(new SeatModel($"test", getRank, getType));
            }
            else
            {
                try
                {
                    getType = userInput switch
                    {
                        ConsoleKey.Spacebar => " ",
                        ConsoleKey.N => "Normaal",
                        ConsoleKey.E => "Extra Beenruimte",
                        ConsoleKey.L => "Love Seat",
                        _ => throw new NotImplementedException()
                    };
                }
                catch { }
                try
                {
                    getRank = userInput switch
                    {
                        ConsoleKey.Spacebar => " ",
                        ConsoleKey.D1 or ConsoleKey.NumPad1 => "1",
                        ConsoleKey.D2 or ConsoleKey.NumPad2 => "2",
                        ConsoleKey.D3 or ConsoleKey.NumPad3 => "3",
                        _ => throw new NotImplementedException()
                    };
                }
                catch { }
            }
            //Console.WriteLine("Press one of these:\nN = Normaal\nE = Extra beenruimte\nL = Love Seat\nRank: (1, 2, 3)\nBackspace\nEnter = 1x dan automatisch\nQ = goedkeuren volgorde.\n");
            Console.WriteLine("  [   Screen   ]");

            for (int i = 0; i < seats.Count; i++)
            {
                Console.ForegroundColor = seats[i].Type switch
                {
                    "Normaal" => ConsoleColor.Blue,
                    "Extra Beenruimte" => ConsoleColor.DarkYellow,
                    "Love Seat" => ConsoleColor.Magenta,
                    _ => ConsoleColor.Gray
                };
                if (i == 0 || currentRoom.RowWidth == 1) Console.Write(seats[i].Type == " " ? " []" : " " + seats[i].Type[0] + seats[i].Rank);
                else Console.Write((i % currentRoom.RowWidth == 0) ? (seats[i].Type == " ") ? "\n []" : "\n " + seats[i].Type[0] + seats[i].Rank : (seats[i].Type == " ") ? " []" : " " + seats[i].Type[0] + seats[i].Rank);
            }
        }
        //Adding the seats to the database
        currentRoom.AddSeats(seats.ToArray());

        upload_to_database(currentRoom);

        Console.WriteLine("\n\nList<Seat> layout = new List<Seat> {");
        foreach (SeatModel seat in currentRoom.Seats)
        {
            Console.WriteLine($"new Seat({seat.ID}, {currentRoom.ID}, \"{seat.Name}\", \"{seat.Rank}\", \"{seat.Type}\"),");
        }
        Console.WriteLine("};");
        Console.ReadLine();

        selectSeat(seats, currentRoom);
    }
}
