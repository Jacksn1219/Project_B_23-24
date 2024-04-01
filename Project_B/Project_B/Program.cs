using DataAccessLibrary;
using Models;

namespace Project_B
{
    class Program
    {
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
                AuthorModel testAuthor = new AuthorModel(1, "John", "Not succesfull", 25);
                Console.WriteLine($"{testAuthor.Name} - {testAuthor.Age} :\n{testAuthor.Description}");
                Console.ReadLine();
            });
            menu.Add("Test DateTime", (x) =>
            {
                string StartDateData = new DateTime(2024, 3, 24, 12, 0, 0).ToString("yyyy-MM-dd HH:mm:ss");
                DateTime testDateTimeParse = DateTime.Parse(StartDateData);
                Console.WriteLine(StartDateData);
                Console.WriteLine(testDateTimeParse);
                Console.ReadLine();
            });

            //menu.UseMenu();
            MovieModel movie1 = new MovieModel("KUNG FU PANDA 4", "", 12, 120, 1); //Film 1 wordt toegevoegd
            MovieModel movie2 = new MovieModel("DUNE: PART TWO", "", 16, 150, 1);  //Film 2 wordt toegevoegd

            RoomModel room1 = new RoomModel(1, "Room_1", 150); //RoomModel 1 heeft 150 plekken
            RoomModel room2 = new RoomModel(2, "Room_2", 300); //RoomModel 2 heeft 300 plekken
            RoomModel room3 = new RoomModel(3, "Room_3", 500); //RoomModel 3 heeft 500 plekken

            Timetable timetable = new Timetable();

            // Toevoegen van films aan de timetable
            timetable.AddMovie(new DateTime(2024, 3, 24, 12, 0, 0), movie1, room1); // Film 1 start om 12:00 uur in zaal 1
            timetable.AddMovie(new DateTime(2024, 3, 24, 15, 0, 0), movie2, room2); // Film 2 start om 15:00 uur in zaal 2

            // Tonen van de timetable
            timetable.DisplayTimetable();
        }
    }
}
