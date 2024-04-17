using System.Text.Json;
using DataAccessLibrary;
using DataAccessLibrary.logic;
using Models;

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
            menu.UseMenu();





            RoomModel room1 = new RoomModel("Room_1", 150); //RoomModel 1 heeft 150 plekken
            RoomModel room2 = new RoomModel("Room_2", 300); //RoomModel 2 heeft 300 plekken
            RoomModel room3 = new RoomModel("Room_3", 500); //RoomModel 3 heeft 500 plekken

            Timetable timetable = new Timetable();

            // Toevoegen van films aan de timetable
            timetable.AddMovie(new DateTime(2024, 3, 24, 12, 0, 0), movie1, room1); // Film 1 start om 12:00 uur in zaal 1
            timetable.AddMovie(new DateTime(2024, 3, 24, 15, 0, 0), movie2, room2); // Film 2 start om 15:00 uur in zaal 2

            // Tonen van de timetable
            timetable.DisplayTimetable();
        }
    }
}
