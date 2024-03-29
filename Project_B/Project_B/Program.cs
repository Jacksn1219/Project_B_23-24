using DataAccessLibrary;
using Models;

namespace Project_B
{
    class Program
    {
        public static void drawLayout(List<Seat> layout)
        {
            Console.Clear();
            Console.ResetColor();

            List<string> layoutRanks = new List<string>();
            foreach (Seat seat in layout) layoutRanks.Add(seat.Rank);

            string[] joinedLayout = String.Join("", layoutRanks).Split("\n");
            int rowWidth = joinedLayout.Max(x => x.Length);
            string alfabet = "abcdefghijklmnopqrstuvwxyz";
            for (int i = 1; i < rowWidth+1; i++) { Console.Write("  " + alfabet[i-1]); }

            int Row = 1;
            Console.Write("\n" + Row);
            foreach (Seat seat in layout)
            {
                Console.ForegroundColor = seat.Rank switch
                {
                    "1" => ConsoleColor.Blue,
                    "2" => ConsoleColor.DarkYellow,
                    "3" => ConsoleColor.DarkRed,
                    _=> ConsoleColor.Gray
                };
                if (seat.Rank == "\n") { Row++; Console.Write("\n" + Row); }
                else { Console.Write($" []"); }
            }
            Console.ResetColor();
        }
        public static void Main()
        {

            InputMenu menu = new InputMenu("| Main menu |", true);
            menu.Add("Setup Database", (x) =>
            {
                //Opzet Sqlite database
                SQLite.SetupProjectB();
                Console.ReadLine();
            });
            menu.Add("Test Author", (x) =>
            {
                Author testAuthor = new Author(1, "John", "Not succesfull", 25);
                Console.WriteLine($"{testAuthor.Name} - {testAuthor.Age} :\n{testAuthor.Description}");
                Console.ReadLine();
            });
            menu.Add("Make layout", (x) =>
            {
                List<Seat> seats = new List<Seat>();
                //Dictionary<int, string[]> tempDict = new Dictionary<int, string[]>();  //{ rank, type }
                                                                                       //tempDict.Add(1, new string[] { "1", "Regular" });

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
                                ConsoleKey.D1 or ConsoleKey.NumPad1 => new Seat(seats.Count, 1, "", "1", "Regular"),
                                ConsoleKey.D2 or ConsoleKey.NumPad2 => new Seat(seats.Count, 1, "", "2", "Regular"),
                                ConsoleKey.D3 or ConsoleKey.NumPad3 => new Seat(seats.Count, 1, "", "3", "Regular"),
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
                            _=> ConsoleColor.Gray
                        };
                        Console.Write(" " + seat.Rank);
                    }
                    //Console.Write(tempDict[tempDict.Count-1][0]);
                }
                drawLayout(seats);
                //Console.Clear();
                //foreach (KeyValuePair<int, string[]> item in tempDict) Console.Write($"{{{item.Key} , new string[] {{{item.Value[0]}, {item.Value[1]}}}");
                Console.ReadLine();
            });

            menu.UseMenu();

            Movie movie1 = new Movie(1, "KUNG FU PANDA 4", 1, 12, "", "", 120); //Film 1 wordt toegevoegd
            Movie movie2 = new Movie(2, "DUNE: PART TWO", 1, 16, "", "", 150);  //Film 2 wordt toegevoegd

            Room room1 = new Room(1, "Room_1", 150); //Room 1 heeft 150 plekken
            Room room2 = new Room(2, "Room_2", 300); //Room 2 heeft 300 plekken
            Room room3 = new Room(3, "Room_3", 500); //Room 3 heeft 500 plekken

            //Seat testSeat = new Seat(1, 1, "", "1", "Regular");

            /*
            Als klant wil ik de stoelen in een zaal zien omdat ik wil weten waar ik kan zitten. -Chris

            Als klant wil ik zien wat voor type stoel een bepaalde stoel is, zodat ik mijn favoriete type kan kiezen.(Love-seat, regular, deluxe) -Chris
            */

            Timetable timetable = new Timetable();

            // Toevoegen van films aan de timetable
            timetable.AddMovie(new DateTime(2024, 3, 24, 12, 0, 0), movie1, room1); // Film 1 start om 12:00 uur in zaal 1
            timetable.AddMovie(new DateTime(2024, 3, 24, 15, 0, 0), movie2, room2); // Film 2 start om 15:00 uur in zaal 2

            // Tonen van de timetable
            timetable.DisplayTimetable();
        }
    }
}
