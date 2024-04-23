using System.Text.Json;
using Models;
using System.Collections.Generic;
using System.Xml.Linq;
using DataAccessLibrary;
using DataAccessLibrary.logic;

namespace Project_B
{
    class Program
    {
        public static void Main()
        {
            //----- Welkom scherm -----//
            List<Action> welcomeList = new List<Action>
            {
                () => {Universal.WriteColor("                    █████ █████", ConsoleColor.Cyan); Universal.WriteColor($"                              ", ConsoleColor.Gray); Universal.WriteColor(" ██████████", ConsoleColor.Cyan); Universal.WriteColor($"                            \n", ConsoleColor.Gray);},
                () => {Universal.WriteColor("                   ░░███ ░░███ ", ConsoleColor.Cyan); Universal.WriteColor($"                              ", ConsoleColor.Gray); Universal.WriteColor("░░███░░░░░█", ConsoleColor.Cyan); Universal.WriteColor($"                            \n", ConsoleColor.Gray);},
                () => {Universal.WriteColor("                    ░░███ ███  ", ConsoleColor.Cyan); Universal.WriteColor($"  ██████  █████ ████ ████████ ", ConsoleColor.Gray); Universal.WriteColor(" ░███  █ ░ ", ConsoleColor.Cyan); Universal.WriteColor($" █████ ████  ██████   █████ \n", ConsoleColor.Gray);},
                () => {Universal.WriteColor("                     ░░█████   ", ConsoleColor.Cyan); Universal.WriteColor($" ███░░███░░███ ░███ ░░███░░███", ConsoleColor.Gray); Universal.WriteColor(" ░██████   ", ConsoleColor.Cyan); Universal.WriteColor($"░░███ ░███  ███░░███ ███░░  \n", ConsoleColor.Gray);},
                () => {Universal.WriteColor("                      ░░███    ", ConsoleColor.Cyan); Universal.WriteColor($"░███ ░███ ░███ ░███  ░███ ░░░ ", ConsoleColor.Gray); Universal.WriteColor(" ░███░░█   ", ConsoleColor.Cyan); Universal.WriteColor($" ░███ ░███ ░███████ ░░█████ \n", ConsoleColor.Gray);},
                () => {Universal.WriteColor("                       ░███    ", ConsoleColor.Cyan); Universal.WriteColor($"░███ ░███ ░███ ░███  ░███     ", ConsoleColor.Gray); Universal.WriteColor(" ░███ ░   █", ConsoleColor.Cyan); Universal.WriteColor($" ░███ ░███ ░███░░░   ░░░░███\n", ConsoleColor.Gray);},
                () => {Universal.WriteColor("                       █████   ", ConsoleColor.Cyan); Universal.WriteColor($"░░██████  ░░████████ █████    ", ConsoleColor.Gray); Universal.WriteColor(" ██████████", ConsoleColor.Cyan); Universal.WriteColor($" ░░███████ ░░██████  ██████ \n", ConsoleColor.Gray);},
                () => {Universal.WriteColor("                      ░░░░░    ", ConsoleColor.Cyan); Universal.WriteColor($" ░░░░░░    ░░░░░░░░ ░░░░░     ", ConsoleColor.Gray); Universal.WriteColor("░░░░░░░░░░ ", ConsoleColor.Cyan); Universal.WriteColor($"  ░░░░░███  ░░░░░░  ░░░░░░  \n", ConsoleColor.Gray);},
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
            Thread.Sleep(400);
            Console.Write($"\n\n\n\n\n                                               Loading data...");
            Console.SetCursorPosition(Console.CursorLeft - 15, Console.CursorTop);

            //----- Setup starting data -----//
            Universal.setupDatabase();

            ConsoleKey key;
            do
            {
                Console.Write("Press <Any> key to continue...");
                Thread.Sleep(700);
                Console.SetCursorPosition(Console.CursorLeft - 30, Console.CursorTop);
                if(Console.KeyAvailable) break;
                Console.Write("                              ");
                Thread.Sleep(700);
                Console.SetCursorPosition(Console.CursorLeft - 30, Console.CursorTop);
            } while (!Console.KeyAvailable);

            key = Console.ReadKey(true).Key;
            Console.CursorVisible = true;

            // ------ Klant menu met menu opties ------//
            InputMenu klantMenu = new InputMenu("useLambda");
            klantMenu.Add("Movies", (x) =>
            {
                //Show all movies that are in the timetable and load timetable from only the selected movie
            });
            klantMenu.Add("Schedule", (x) =>
            {
                //Show the timetable and the book ticket
            });

            /*klantMenu.Add("Timetable", (x) =>
            {
                Movie movie1 = new Movie(1, "KUNG FU PANDA 4", 1, 12, "", "", 120); //Film 1 wordt toegevoegd
                Movie movie2 = new Movie(2, "DUNE: PART TWO", 1, 16, "", "", 150);  //Film 2 wordt toegevoegd

                Room room1 = new Room(1, "Room_1", 150, 6); //Room 1 heeft 150 plekken
                Room room2 = new Room(2, "Room_2", 300, 6); //Room 2 heeft 300 plekken
                Room room3 = new Room(3, "Room_3", 500, 6); //Room 3 heeft 500 plekken

                Timetable timetable = new Timetable();

                // Toevoegen van films aan de timetable
                timetable.AddMovie(new DateTime(2024, 3, 24, 12, 0, 0), movie1, room1); // Film 1 start om 12:00 uur in zaal 1
                timetable.AddMovie(new DateTime(2024, 3, 24, 15, 0, 0), movie2, room2); // Film 2 start om 15:00 uur in zaal 2

                // Tonen van de timetable
                timetable.DisplayTimetable();
                Console.ReadLine();
            });*/

            // ------ Medewerker menu met menu opties ------//
            InputMenu medewerkerMenu = new InputMenu("useLambda");
            medewerkerMenu.Add("Planning", (x) =>
            {
                //Inplannen film en aanpassen wat er geplanned is en Kunnen zien notities klanten
            });
            medewerkerMenu.Add("Reservaties", (x) =>
            {
                //Zie gemaakte reservaties voor timetable films
            });
            medewerkerMenu.Add("Historie", (x) =>
            {
                //Zie verkoop per film, week en maand en kunnen filteren per verkoop hoeveelheid
            });
            medewerkerMenu.Add("Aanmaken", (x) =>
            {
                //Aanmaken nieuwe film, acteur, regiseur, zaal.
            });
            medewerkerMenu.Add("Layout creator", (x) =>
            {
                Layout.MakeNewLayout();
            });
            medewerkerMenu.Add("Edit layout item", (x) =>
            {
                List<RoomModel> roomList = new List<RoomModel>();
                try
                {
                    SeatModelFactory seatModelFactory = new SeatModelFactory(Universal.Db);
                    RoomFactory roomFactory = new RoomFactory(Universal.Db, seatModelFactory);
                    int i = 1;
                    RoomModel? room = null;
                    do
                    {
                        room = roomFactory.GetItemFromId(i);
                        if (room != null) roomList.Add(room);
                        i++;
                    } while (room != null);
                } catch { }

                List<SeatModel> seatList = new List<SeatModel>();
                try
                {
                    SeatModelFactory seatModelFactory = new SeatModelFactory(Universal.Db);

                    int i = 1;
                    SeatModel? seat = new SeatModel();
                    while (seat != null)
                    {
                        seat = seatModelFactory.GetItemFromId(i);
                        if (seat != null) seatList.Add(seat);
                        i++;
                    }
                } catch { }
                List<List<SeatModel>> layouts = new List<List<SeatModel>>();
                foreach (SeatModel seat in seatList)
                {
                    if (seat == null) continue;
                    else if (seat.RoomID > layouts.Count) layouts.Add(new List<SeatModel> { seat });
                    else layouts[(seat.RoomID ?? 0) - 1].Add(seat);
                }

                InputMenu selectRoom = new InputMenu("useLambda");
                foreach (RoomModel room in roomList) //getRoomFromDatabase() - Aymane
                {
                    selectRoom.Add($"{room.Name}", (x) => {
                    room.AddSeatModels(layouts[(room.ID ?? 2) - 1].ToArray());
                    Layout.editLayout(room);
                    });
                }
                selectRoom.UseMenu(() => Universal.printAsTitle("Select room to edit"));
            });

            /*
            medewerkerMenu.Add("Setup Database", (x) =>
            {
                //Opzet Sqlite database
                SQLite.SetupProjectB();
                Console.ReadLine();
            });
            medewerkerMenu.Add("Test Author", (x) =>
            {
                Author testAuthor = new Author(1, "John", "Not succesfull", 25);
                Console.WriteLine($"{testAuthor.Name} - {testAuthor.Age} :\n{testAuthor.Description}");
                Console.ReadLine();
            });
            medewerkerMenu.Add("Timetable", (x) =>
            {
                Movie movie1 = new Movie(1, "KUNG FU PANDA 4", 1, 12, "", "", 120); //Film 1 wordt toegevoegd
                Movie movie2 = new Movie(2, "DUNE: PART TWO", 1, 16, "", "", 150);  //Film 2 wordt toegevoegd

                Room room1 = new Room(1, "Room_1", 150, 6); //Room 1 heeft 150 plekken
                Room room2 = new Room(2, "Room_2", 300, 6); //Room 2 heeft 300 plekken
                Room room3 = new Room(3, "Room_3", 500, 6); //Room 3 heeft 500 plekken

                Timetable timetable = new Timetable();

                // Toevoegen van films aan de timetable
                timetable.AddMovie(new DateTime(2024, 3, 24, 12, 0, 0), movie1, room1); // Film 1 start om 12:00 uur in zaal 1
                timetable.AddMovie(new DateTime(2024, 3, 24, 15, 0, 0), movie2, room2); // Film 2 start om 15:00 uur in zaal 2

                // Tonen van de timetable
                timetable.DisplayTimetable();
                Console.ReadLine();
            });*/
            medewerkerMenu.Add("Reserve Seat", (x) =>
            {
                // Ask for user's age
                Console.Write("Enter your age: ");
                if (int.TryParse(Console.ReadLine(), out int userAge))
                { }
                // Display available rooms
                /*ReservationService.DisplayAvailableRooms();

                //Ask user to choose a room

                Console.Write("Choose a room (1, 2, or 3): ");
                if (int.TryParse(Console.ReadLine(), out int selectedRoomID) && selectedRoomID >= 1 && selectedRoomID <= 3)
                {
                    // Display available rooms
                    ReservationService.DisplayAvailableRooms();

                    // Ask user to choose a room
                    Console.Write("Choose a room (1, 2, or 3): ");
                    if (int.TryParse(Console.ReadLine(), out int selectedRoomID) && selectedRoomID >= 1 && selectedRoomID <= 3)
                    {
                        Console.WriteLine($"SeatModel ID: {SeatModel.ID}, Room ID: {SeatModel.RoomID}, Name: {SeatModel.Name}, Rank: {SeatModel.Rank}, Type: {SeatModel.Type}");
                    }

                    // Ask user to choose SeatModels
                    Console.WriteLine("Enter SeatModel numbers to reserve (comma-separated):");
                    var input = Console.ReadLine();
                    var SeatModelNumbers = new List<int>();

                    foreach (var SeatModelNumber in input.Split(','))
                    {
                        if (int.TryParse(SeatModelNumber.Trim(), out int num))
                        {
                            SeatModelNumbers.Add(num);
                        }
                    }

                    // Reserve SeatModels
                    ReservationService.ReserveSeatModels(selectedRoomID, SeatModelNumbers, userAge);
                }*/
                else
                {
                    Console.WriteLine("Invalid room selection.");
                }

                Console.ReadLine();
            });
            
            InputMenu menu = new InputMenu("useLambda", true);
            menu.Add("Klant", (x) => { klantMenu.UseMenu(() => Universal.printAsTitle("Klant Menu")); });
            menu.Add("Medewerker", (x) =>
            {
                Console.Write("| Inlog |\nWachtwoord: ");
                string? userInput = Console.ReadLine();
                if (userInput == "w817") medewerkerMenu.UseMenu(() => Universal.printAsTitle("Medewerker Menu"));
                else
                {
                    Universal.ChangeColour(ConsoleColor.Red);
                    Console.WriteLine("Onjuist wachtwoord !");
                    Console.ReadLine();
                }
            });
            menu.UseMenu(() => Universal.printAsTitle("Main Menu"));
        }
    }
}

/*
 * Unit tests Inputmenu
 * Als klant wil ik zien welke stoelen al bezet zijn zodat ik niet per ongeluk een al gereserveerde stoel pak
 * Als administratie wil ik de gereserveerde stoelen terugzien, zodat ik de klanten naar hun stoel kan begeleiden
 * Als administratie wil ik graag zien hoe vol een zaal is, zodat ik kan zien of de desbetreffende film een grotere zaal nodig heeft of niet zo populair is
 * Als administratie wil ik een nieuwe film toevoegen, zodat we telkens de nieuwste films kunnen laten zien.
 * Als administratie wil ik slecht lopende films verwijderen, zodat we geen films laten zien die niet populair zijn.
 * Als administratie wil ik films kunnen aanpassen, zodat als ik een fout maak ik de film niet opnieuw aan moet maken.
 * Als medewerker wil ik in kunnen loggen, zodat niet iedereen administratorrechten heeft
*/
