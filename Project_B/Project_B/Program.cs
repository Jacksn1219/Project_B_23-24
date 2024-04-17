using System.Text.Json;
using DataAccessLibrary;
using DataAccessLibrary.logic;
using Models;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Project_B
{
    class Program
    {
        private const string DbPath = "database.db";
        public static void Main()
        {
            //start of app
            //create database connection
            DataAccess db = new SQliteDataAccess($"Data Source={DbPath}; Version = 3; New = True; Compress = True;");
            //create factories to add DbItems to the db
            DirectorFactory df = new(db);
            ActorFactory af = new(db);
            MovieFactory mf = new(db, df, af);
            SeatFactory sf = new(db);
            RoomFactory roomf = new(db, sf);
            TimeTableFactory tf = new(db, mf, roomf);
            CustomerFactory cf = new(db);
            ReservationFactory resf = new(db, cf, sf);


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
                // Ask for user's age
                Console.Write("Enter your age: ");
                if (int.TryParse(Console.ReadLine(), out int userAge))
                { }
                // Display available rooms
                //ReservationService.DisplayAvailableRooms();

                /* Ask user to choose a room

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
                    //ReservationService.ReserveSeats(selectedRoomID, seatNumbers, userAge);
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
            */
            });

            menu.UseMenu();
        }
    }
}
