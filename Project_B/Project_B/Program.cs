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

            //geen ID
            MovieModel movie1 = new MovieModel("KUNG FU PANDA 4", "everybody was kung fu fighting", 12, 120, "Horror"); //Film 1 wordt lokaal toegevoegd
            MovieModel movie2 = new MovieModel("DUNE: PART TWO", "I don't like sand. It's coarse and rough and irritating and it gets everywhere.", 16, 150, "Kids");  //Film 2 wordt lokaal toegevoegd

            InputMenu menu = new InputMenu("| Main menu |", true);
            menu.Add("Test Author", (x) =>
            {
                ActorModel testAuthor = new ActorModel("John", "Not succesfull", 25);
                Console.WriteLine($"{testAuthor.Name} - {testAuthor.Age} :\n{testAuthor.Description}");
                Console.ReadLine();
            });
            menu.Add("Layout creator", (x) =>
            {
                DataAccessLibrary.Layout.MakeNewLayout();
                /*
                Als klant wil ik de stoelen in een zaal zien omdat ik wil weten waar ik kan zitten. -Chris
                Als klant wil ik zien wat voor type stoel een bepaalde stoel is, zodat ik mijn favoriete type kan kiezen.(Love-seat, regular, deluxe) -Chris
                */
            });
            menu.Add("Edit layout item", (x) =>
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
                foreach (Room room in roomList/*getRoomFromDatabase() - Aymane*/)
                {
                    selectRoom.Add($"{room.Name}", (x) => DataAccessLibrary.Layout.editLayout(layout1/*getLayoutFromDatabase() - Aymane*/, room));
                }
                selectRoom.UseMenu();
            });
            menu.Add("Timetable", (x) =>
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
            });

            menu.UseMenu();
        }
    }
}
