using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using DataAccessLibrary;
using DataAccessLibrary.logic;
using Models;
using Project_B.services;
using Serilog;

namespace Project_B
{
    class Program
    {
        private const string DbPath = "database.db";
        public static void Main()
        {
            //start of app
            //create logger
            using var logger = new LoggerConfiguration()
                .WriteTo.File("logs/dbErrors.txt", rollingInterval: RollingInterval.Day, retainedFileCountLimit: 7)
                .CreateLogger();
            //create database connection
            DataAccess db = new SQliteDataAccess($"Data Source={DbPath}; Version = 3; New = True; Compress = True;", logger);
            //create factories to add DbItems to the db
            DirectorFactory df = new(db);
            ActorFactory af = new(db);
            MovieFactory mf = new(db, df, af);
            SeatFactory sf = new(db);
            RoomFactory roomf = new(db, sf);
            TimeTableFactory tf = new(db, mf, roomf);
            CustomerFactory cf = new(db);
            ReservationFactory resf = new(db, cf, sf, tf);


            InputMenu menu = new InputMenu("| Main menu |", true);
            /*menu.Add("Setup Database", (x) =>
            {
                //Opzet Sqlite database
                //SQLite.SetupProjectB();
                Console.ReadLine();
            });
            menu.Add("Test Author", (x) =>
            {
                ActorModel testAuthor = new ActorModel("John", "Not succesfull", 25);
                Console.WriteLine($"{testAuthor.Name} - {testAuthor.Age} :\n{testAuthor.Description}");
                Console.ReadLine();
            });*/
            menu.Add("Layout creator", (x) =>
            {
                Layout l = new Layout(roomf, sf);
                l.MakeNewLayout();
                /*
                Als klant wil ik de stoelen in een zaal zien omdat ik wil weten waar ik kan zitten. -Chris
                Als klant wil ik zien wat voor type stoel een bepaalde stoel is, zodat ik mijn favoriete type kan kiezen.(Love-seat, regular, deluxe) -Chris
                */
            });
            menu.Add("Edit layout item", (x) =>
            {
                List<SeatModel> layout1 = new List<SeatModel>{
                    new SeatModel("0", " ", " "),
                    new SeatModel("1", " ", " "),
                    new SeatModel("2", "1", "Normaal"),
                    new SeatModel("3", "1", "Normaal"),
                    new SeatModel("4", "1", "Normaal"),
                    new SeatModel("5", "1", "Normaal"),
                    new SeatModel("6", "1", "Normaal"),
                    new SeatModel( "7", "1", "Normaal"),
                    new SeatModel("8", "1", "Normaal"),
                    new SeatModel( "9", "1", "Normaal"),
                    new SeatModel("10", " ", " "),
                    new SeatModel("11", " ", " "),
                    new SeatModel("12", " ", " "),
                    new SeatModel("13", " ", " ")
                };
                List<RoomModel> roomList = new List<RoomModel> { new RoomModel("Room1", layout1.Count, 6) };
                InputMenu selectRoom = new InputMenu("| Select room to edit |");
                foreach (RoomModel room in roomList/*getRoomFromDatabase() - Aymane*/)
                {
                    selectRoom.Add($"{room.Name}", (x) => DataAccessLibrary.Layout.editLayout(layout1/*getLayoutFromDatabase() - Aymane*/, room));
                }
                selectRoom.UseMenu();
            });
            menu.Add("Timetable", (x) =>
            {
                MovieModel movie1 = new MovieModel("KUNG FU PANDA 4", "", 12, 120, "horor"); //Film 1 wordt toegevoegd
                MovieModel movie2 = new MovieModel("DUNE: PART TWO", "", 16, 150, "horor");  //Film 2 wordt toegevoegd

                RoomModel room1 = new RoomModel("Room_1", 150, 6); //Room 1 heeft 150 plekken
                RoomModel room2 = new RoomModel("Room_2", 300, 6); //Room 2 heeft 300 plekken
                RoomModel room3 = new RoomModel("Room_3", 500, 6); //Room 3 heeft 500 plekken

                Timetable timetable = new Timetable();

                // Toevoegen van films aan de timetable
                timetable.AddMovie(new DateTime(2024, 3, 24, 12, 0, 0), movie1, room1); // Film 1 start om 12:00 uur in zaal 1
                timetable.AddMovie(new DateTime(2024, 3, 24, 15, 0, 0), movie2, room2); // Film 2 start om 15:00 uur in zaal 2

                // Tonen van de timetable
                timetable.DisplayTimetable();
                Console.ReadLine();
            });

            menu.Add("Reserve Seats", (x) =>
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
            menu.Add("Test SeeActors", (x) => // Als klant wil ik de acteurs van een film bekijken
            {
                List<ActorModel> authors = new List<ActorModel>();
                authors.Add(new ActorModel("Jack Black", "Plays Po", 43));
                authors.Add(new ActorModel("Jackie Chan", "Plays Monkey", 57));
                authors.Add(new ActorModel("Ada Wong", "Plays Viper", 27));
                authors.Add(new ActorModel("Jada Pinket Smith", "Plays Tigress", 41));
                MovieModel movietje = new MovieModel("KUNG FU PANDA 4", "everybody was kung fu fighting", 12, 120, "Horror");
                movietje.Director = new DirectorModel("Jaycey", "Director from netherlands", 20);
                movietje.Actors.AddRange(authors);
                Console.WriteLine(movietje.SeeActors());
                Console.ReadLine();
            });
            menu.Add("Test SeeDirector", (x) => // Als klant wil ik de regisseur van een film zien
            {
                List<DirectorModel> directors = new List<DirectorModel>();
                directors.Add(new DirectorModel("Christopher Nolan", "Famous movie director known for several blockbuster movies such as Oppenheimer, Interstellar, Inception and many more", 53));
                MovieModel interStellar = new MovieModel("Interstellar", "While the earth no longer has the resources to supply the human race, a group of astronauts go to beyond the milky way to find a possible future planet for mankind", 12, 190, "Sci-Fi");
                Console.WriteLine(interStellar.SeeDirector(directors));
                Console.ReadLine();
            });
            menu.Add("Test SeeDescription", (x) => // Als klant wil ik de omschrijving (leeftijd + genre) van een film zien
            {
                MovieModel interStellar = new MovieModel("Interstellar", "While the earth no longer has the resources to supply the human race, a group of astronauts go to beyond the milky way to find a possible future planet for mankind", 12, 190, "Sci-Fi");
                Console.WriteLine(interStellar.SeeDescription());
                Console.ReadLine();
            });
            menu.Add("set prices", (x) =>
            {
                var prices = SeatPriceCalculator.GetCurrentPrices();
                SeatPriceCalculator.WritePrices();
                System.Console.WriteLine("\nChange Prices? (Y/N)");
                char input = Console.ReadKey().KeyChar;
                if (input.Equals('Y') || input.Equals('y'))
                {
                    bool changing = true;
                    while (changing)
                    {
                        System.Console.WriteLine("type price to change: (Q to quit)");
                        string response = Console.ReadLine() ?? "";
                        switch (response.ToLower())
                        {
                            case "price tier i" or "tier i" or "i" or "1":
                                Console.WriteLine("type new price:");
                                response = Console.ReadLine() ?? "";
                                prices.PriceTierI = decimal.Parse(response);
                                break;
                            case "price tier ii" or "tier ii" or "ii" or "2":
                                Console.WriteLine("type new price:");
                                response = Console.ReadLine() ?? "";
                                prices.PriceTierII = decimal.Parse(response);
                                break;
                            case "price tier iii" or "tier iii" or "iii" or "3":
                                Console.WriteLine("type new price:");
                                response = Console.ReadLine() ?? "";
                                prices.PriceTierIII = decimal.Parse(response);
                                break;
                            case "extra space" or "extra" or "space":
                                Console.WriteLine("type new price:");
                                response = Console.ReadLine() ?? "";
                                prices.ExtraSpace = decimal.Parse(response);
                                break;
                            case "loveseat" or "love" or "love seat":
                                Console.WriteLine("type new price:");
                                response = Console.ReadLine() ?? "";
                                prices.LoveSeat = decimal.Parse(response);
                                break;
                            case "q":
                                changing = false;
                                break;
                        }
                    }

                }
                SeatPriceCalculator.UpdatePrice(prices);
                SeatPriceCalculator.WritePrices();
                Console.ReadLine();

            });
            menu.Add("get seat PRICE info", (x) =>
            {
                SeatModel seat = new SeatModel("naam", "II", "loveseat");
                Console.WriteLine(SeatPriceCalculator.ShowCalculation(seat));
                Console.ReadLine();
            });
            menu.UseMenu();


            static bool IsValidEmail(string email)
            {
                return Regex.IsMatch(email, @"^[a-zA-Z0-9._%+-]+@(gmail\.com|hotmail\.com|outlook\.com|hotmail\.nl)$");
            }

            static bool IsValidPhoneNumber(string phoneNumber)
            {
                // Phone number must start with '0' and have a maximum length of 10 characters
                return phoneNumber.StartsWith("0") && phoneNumber.Length <= 10 && phoneNumber.All(char.IsDigit);
            }



            menu.UseMenu();
        }
    }
}
