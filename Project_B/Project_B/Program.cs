using DataAccessLibrary;
using Models;
using Project_B.Services;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Xml.Linq;

namespace Project_B
{
    class Program
    {
        public static string printAsTitle(string input)
        {
            input = input.Trim();
            string toAdd = "";
            for (int i = 0; i < Console.WindowWidth / 2/4 - (input.Length+6)/2-1; i++) toAdd += "-";
            Layout.WriteColor(toAdd + "== ", ConsoleColor.Cyan);
            Console.Write(input);
            Layout.WriteColor(" ==" + toAdd, ConsoleColor.Cyan);
            return ""; // (toAdd + input + toAdd).Substring(0, 119);
        }
        public static string centerToScreen(string input)
        {
            string toAdd = "";
            for (int i = 0; i < Console.WindowWidth / 2/ 4 - (input.Length + 6) / 2 - 1 + 4; i++) toAdd += " ";
            return toAdd + input + toAdd;
        }
        public static void Main()
        {
            List<Action> welcomeList = new List<Action>
            {
                () => {Layout.WriteColor("                    █████ █████", ConsoleColor.Cyan); Layout.WriteColor($"                              ", ConsoleColor.Gray); Layout.WriteColor(" ██████████", ConsoleColor.Cyan); Layout.WriteColor($"                            \n", ConsoleColor.Gray);},
                () => {Layout.WriteColor("                   ░░███ ░░███ ", ConsoleColor.Cyan); Layout.WriteColor($"                              ", ConsoleColor.Gray); Layout.WriteColor("░░███░░░░░█", ConsoleColor.Cyan); Layout.WriteColor($"                            \n", ConsoleColor.Gray);},
                () => {Layout.WriteColor("                    ░░███ ███  ", ConsoleColor.Cyan); Layout.WriteColor($"  ██████  █████ ████ ████████ ", ConsoleColor.Gray); Layout.WriteColor(" ░███  █ ░ ", ConsoleColor.Cyan); Layout.WriteColor($" █████ ████  ██████   █████ \n", ConsoleColor.Gray);},
                () => {Layout.WriteColor("                     ░░█████   ", ConsoleColor.Cyan); Layout.WriteColor($" ███░░███░░███ ░███ ░░███░░███", ConsoleColor.Gray); Layout.WriteColor(" ░██████   ", ConsoleColor.Cyan); Layout.WriteColor($"░░███ ░███  ███░░███ ███░░  \n", ConsoleColor.Gray);},
                () => {Layout.WriteColor("                      ░░███    ", ConsoleColor.Cyan); Layout.WriteColor($"░███ ░███ ░███ ░███  ░███ ░░░ ", ConsoleColor.Gray); Layout.WriteColor(" ░███░░█   ", ConsoleColor.Cyan); Layout.WriteColor($" ░███ ░███ ░███████ ░░█████ \n", ConsoleColor.Gray);},
                () => {Layout.WriteColor("                       ░███    ", ConsoleColor.Cyan); Layout.WriteColor($"░███ ░███ ░███ ░███  ░███     ", ConsoleColor.Gray); Layout.WriteColor(" ░███ ░   █", ConsoleColor.Cyan); Layout.WriteColor($" ░███ ░███ ░███░░░   ░░░░███\n", ConsoleColor.Gray);},
                () => {Layout.WriteColor("                       █████   ", ConsoleColor.Cyan); Layout.WriteColor($"░░██████  ░░████████ █████    ", ConsoleColor.Gray); Layout.WriteColor(" ██████████", ConsoleColor.Cyan); Layout.WriteColor($" ░░███████ ░░██████  ██████ \n", ConsoleColor.Gray);},
                () => {Layout.WriteColor("                      ░░░░░    ", ConsoleColor.Cyan); Layout.WriteColor($" ░░░░░░    ░░░░░░░░ ░░░░░     ", ConsoleColor.Gray); Layout.WriteColor("░░░░░░░░░░ ", ConsoleColor.Cyan); Layout.WriteColor($"  ░░░░░███  ░░░░░░  ░░░░░░  \n", ConsoleColor.Gray);},
                () => {Layout.WriteColor($"                                                                          ███ ░███                  \n", ConsoleColor.Gray);},
                () => {Layout.WriteColor($"                                                                         ░░██████                   \n", ConsoleColor.Gray);},
                () => {Layout.WriteColor($"                                                                          ░░░░░░                    ", ConsoleColor.Gray); }
            };
            Console.CursorVisible = false;
            Console.WriteLine("\n\n\n");
            for (int i = 0; i < welcomeList.Count(); i++)
            {
                welcomeList[i]();
                Thread.Sleep(100);
            }
            Thread.Sleep(400);
            Console.Write("\n\n\n\n\n                                               Loading data...");
            Console.SetCursorPosition(Console.CursorLeft - 15, Console.CursorTop);

            // setup starting data
            SQLite.SetupProjectB();

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

            // Key is available - read it
            key = Console.ReadKey(true).Key;


            /*for (int i = 0; i < "Press <Any> key to continue...".Count();i++)
            {
                Console.Write("Press <Any> key to continue..."[i]);
                Thread.Sleep(100);
            }*/
            //Console.ReadLine();
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

            /*medewerkerMenu.Add("Setup Database", (x) =>
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
            medewerkerMenu.Add("Layout creator", (x) =>
            {
                DataAccessLibrary.Layout.MakeNewLayout();
            });
            medewerkerMenu.Add("Edit layout item", (x) =>
            {
                List<Seat> layout1 = new List<Seat>{
                        new Seat(0, 1, "0", " ", " "),
                        new Seat(1, 1, "1", " ", " "),
                        new Seat(2, 1, "2", "1", "Normaal"),
                        new Seat(3, 1, "3", "1", "Normaal"),
                        new Seat(4, 1, "4", "1", "Normaal"),
                        new Seat(5, 1, "5", "1", "Normaal"),
                        new Seat(6, 1, "6", "1", "Normaal"),
                        new Seat(7, 1, "7", "1", "Normaal"),
                        new Seat(8, 1, "8", "1", "Normaal"),
                        new Seat(9, 1, "9", "1", "Normaal"),
                        new Seat(10, 1, "10", " ", " "),
                        new Seat(11, 1, "11", " ", " "),
                        new Seat(12, 1, "12", " ", " "),
                        new Seat(13, 1, "13", " ", " ")
                };
                List<Room> roomList = new List<Room> { new Room(1, "Room1", layout1.Count, 6) };
                InputMenu selectRoom = new InputMenu("| Select room to edit |");
                foreach (Room room in roomList) //getRoomFromDatabase() - Aymane
                {
                    selectRoom.Add($"{room.Name}", (x) => DataAccessLibrary.Layout.editLayout(layout1 getLayoutFromDatabase() - Aymane, room));
                }
                selectRoom.UseMenu();
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
            medewerkerMenu.Add("Reserve Seats", (x) =>
            {
                // Ask for user's age
                Console.Write("Enter your age: ");
                if (int.TryParse(Console.ReadLine(), out int userAge))
                {
                    // Display available rooms
                    ReservationService.DisplayAvailableRooms();

                    // Ask user to choose a room
                    Console.Write("Choose a room (1, 2, or 3): ");
                    if (int.TryParse(Console.ReadLine(), out int selectedRoomID) && selectedRoomID >= 1 && selectedRoomID <= 3)
                    {
                        // Display the layout of the selected room
                        ReservationService.DisplayRoomLayout(selectedRoomID);

                        // Display available seats
                        var availableSeats = ReservationService.GetAvailableSeats(selectedRoomID);
                        Console.WriteLine("Available Seats:");
                        foreach (var seat in availableSeats)
                        {
                            Console.WriteLine($"Seat ID: {seat.ID}, Room ID: {seat.RoomID}, Name: {seat.Name}, Rank: {seat.Rank}, Type: {seat.Type}");
                        }

                        // Ask user to choose seats
                        Console.WriteLine("Enter seat numbers to reserve (comma-separated):");
                        var input = Console.ReadLine();
                        var seatNumbers = new List<int>();

                        foreach (var seatNumber in input.Split(','))
                        {
                            if (int.TryParse(seatNumber.Trim(), out int num))
                            {
                                seatNumbers.Add(num);
                            }
                        }

                        // Reserve seats
                        ReservationService.ReserveSeats(selectedRoomID, seatNumbers, userAge);
                    }
                    else
                    {
                        Console.WriteLine("Invalid room selection.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid age input.");
                }

                Console.ReadLine();
            });
            
            InputMenu menu = new InputMenu("useLambda", true);
            menu.Add("Klant", (x) => { klantMenu.UseMenu(() => printAsTitle("Klant Menu")); });
            menu.Add("Medewerker", (x) =>
            {
                Console.Write("| Inlog |\nWachtwoord: ");
                string userInput = Console.ReadLine();
                if (userInput == "w817") medewerkerMenu.UseMenu(() => printAsTitle("Medewerker Menu"));
                else
                {
                    Layout.ChangeColour(ConsoleColor.Red);
                    Console.WriteLine("Onjuist wachtwoord !");
                    Console.ReadLine();
                }
            });
            menu.UseMenu(() => printAsTitle("Main Menu"));
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