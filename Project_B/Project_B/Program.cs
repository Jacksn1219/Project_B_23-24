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
                Author testAuthor = new Author(1, "John", "Not succesfull", 25);
                Console.WriteLine($"{testAuthor.Name} - {testAuthor.Age} :\n{testAuthor.Description}");
                Console.ReadLine();
            });

            menu.UseMenu();

            Movie movie1 = new Movie("KUNG FU PANDA 4", 120); //Film 1 wordt toegevoegd
            Movie movie2 = new Movie("DUNE: PART TWO", 150);  //Film 2 wordt toegevoegd

            Room room1 = new Room(1, 150); //Room 1 heeft 150 plekken
            Room room2 = new Room(2, 300); //Room 2 heeft 300 plekken
            Room room3 = new Room(3, 500); //Room 3 heeft 500 plekken

            Timetable timetable = new Timetable();

            // Toevoegen van films aan de timetable
            timetable.AddMovie(new DateTime(2024, 3, 24, 12, 0, 0), movie1, room1); // Film 1 start om 12:00 uur in zaal 1
            timetable.AddMovie(new DateTime(2024, 3, 24, 15, 0, 0), movie2, room2); // Film 2 start om 15:00 uur in zaal 2

            // Tonen van de timetable
            timetable.DisplayTimetable();
        }
    }
}
