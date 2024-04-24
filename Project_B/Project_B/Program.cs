using System.Text.Json;
using Models;
using System.Collections.Generic;
using System.Xml.Linq;
using DataAccessLibrary;
using DataAccessLibrary.logic;
using DataAccessLibrary.models;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Project_B.Services;
using System.Text.RegularExpressions;

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
            medewerkerMenu.Add("Edit layout", (x) =>
            {
                Layout.editLayoutPerRoom();
            });
            medewerkerMenu.Add("Select a seat", (x) =>
            {
                Console.WriteLine(Layout.selectSeatPerRoom());
                Console.ReadLine();
            });
            medewerkerMenu.Add("Maak nieuwe film", (x) =>
            {
                CreateItems.CreateNewMovie();
            });
            medewerkerMenu.Add("Pas film aan", (x) =>
            {
                CreateItems.ChangeMovie();
            });
            medewerkerMenu.Add("Reserve Seat", (x) =>
            {
                int selectedMovieID;

                while (true)
                {
                    // Display available movies
                    ReservationServices.DisplayAvailableMovies();

                    // Ask user to choose a movie
                    Console.Write("Choose a movie (1 or 2): ");
                    if (int.TryParse(Console.ReadLine(), out selectedMovieID) && (selectedMovieID == 1 || selectedMovieID == 2))
                    {
                        break;  // Exit the loop if a valid movie is selected
                    }
                    else
                    {
                        Console.WriteLine("Invalid movie selection. Please choose either 1 or 2.");
                    }
                }

                // Ask for user's information
                string fullName;
                while (true)
                {
                    Console.Write("Enter your full name: ");
                    fullName = Console.ReadLine();
                    if (IsValidFullName(fullName))
                    {
                        break;  // Exit the loop if a valid full name is entered
                    }
                    else
                    {
                        Console.WriteLine("Please enter a valid full name.");
                    }
                }

                // Ask for user's age
                int userAge;
                while (true)
                {
                    Console.Write("Enter your age: ");
                    if (int.TryParse(Console.ReadLine(), out userAge) && userAge > 0 && userAge <= 100)
                    {
                        break;  // Exit the loop if a valid age is entered
                    }
                    else
                    {
                        Console.WriteLine("Please enter a valid age between 1 and 100.");
                    }
                }

                // Ask for user's email
                string email;
                while (true)
                {
                    Console.Write("Enter your email: ");
                    email = Console.ReadLine();
                    if (IsValidEmail(email))
                    {
                        break;  // Exit the loop if a valid email is entered
                    }
                    else
                    {
                        Console.WriteLine("Please enter a valid email.");
                    }
                }

                // Ask for user's phone number
                string phoneNumber;
                while (true)
                {
                    Console.Write("Enter your phone number (starting with 0 and max 10 digits): ");
                    phoneNumber = Console.ReadLine();
                    if (IsValidPhoneNumber(phoneNumber))
                    {
                        break;  // Exit the loop if a valid phone number is entered
                    }
                    else
                    {
                        Console.WriteLine("Please enter a valid phone number starting with 0 and max 10 digits.");
                    }
                }

                // Ask user to choose a room
                int selectedRoomID;
                while (true)
                {
                    Console.Write("Choose a room (1, 2, or 3): ");
                    if (int.TryParse(Console.ReadLine(), out selectedRoomID) && selectedRoomID >= 1 && selectedRoomID <= 3)
                    {
                        break;  // Exit the loop if a valid room is selected
                    }
                    else
                    {
                        Console.WriteLine("Invalid room selection.");
                    }
                }

                // Display the layout of the selected room
                ReservationServices.DisplayRoomLayout(selectedRoomID);

                // Display available seats
                var availableSeats = ReservationServices.GetAvailableSeats(selectedRoomID);
                Console.WriteLine("Available Seats:");
                foreach (var seat in availableSeats)
                {
                    Console.WriteLine($"Seat ID: {seat.ID}, Room ID: {seat.ID}, Name: {seat.Name}, Rank: {seat.Rank}, Type: {seat.Type}");
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
                ReservationServices.ReserveSeats(selectedRoomID, seatNumbers, userAge, fullName, email, phoneNumber);

                

                Console.ReadLine();
            });

            static bool IsValidFullName(string fullName)
            {
                return !string.IsNullOrWhiteSpace(fullName) && fullName.Replace(" ", "").All(char.IsLetter);
            }


            static bool IsValidEmail(string email)
            {
                return Regex.IsMatch(email, @"^[a-zA-Z0-9._%+-]+@(gmail\.com|hotmail\.com|outlook\.com|hotmail\.nl)$");
            }

            static bool IsValidPhoneNumber(string phoneNumber)
            {
                // Phone number must start with '0' and have a maximum length of 10 characters
                return phoneNumber.StartsWith("0") && phoneNumber.Length <= 10 && phoneNumber.All(char.IsDigit);
            }

            /*
            medewerkerMenu.Add("Setup Database", (x) =>
            {
                //Opzet Sqlite database
                SQLite.SetupProjectB();
                Console.ReadLine();
            });
            medewerkerMenu.Add("Layout creator", (x) =>
            {
                Layout.MakeNewLayout();
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

            InputMenu menu = new InputMenu("useLambda", true);
            menu.Add("Klant", (x) => { klantMenu.UseMenu(() => Universal.printAsTitle("Klant Menu")); });
            menu.Add("Medewerker", (x) =>
            {
                string fileName = "Medewerker.json";
                JObject? jsonData = (JObject?)JsonConvert.DeserializeObject(File.ReadAllText(Universal.databasePath() + "\\" + fileName));
                string passWord = jsonData["Value"].Value<string>() ?? "";

                Console.Write("| Inlog |\nWachtwoord: ");
                string? userInput = Console.ReadLine();
                if (userInput == passWord) medewerkerMenu.UseMenu(() => Universal.printAsTitle("Medewerker Menu"));
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
 X Als administratie wil ik een nieuwe film toevoegen, zodat we telkens de nieuwste films kunnen laten zien.
 * Als administratie wil ik slecht lopende films verwijderen, zodat we geen films laten zien die niet populair zijn.
 X Als administratie wil ik films kunnen aanpassen, zodat als ik een fout maak ik de film niet opnieuw aan moet maken.
 X Als medewerker wil ik in kunnen loggen, zodat niet iedereen administratorrechten heeft
*/
