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
            menu.Add("Test DateTime", (x) =>
            {
                string StartDateData = new DateTime(2024, 3, 24, 12, 0, 0).ToString("yyyy-MM-dd HH:mm:ss");
                DateTime testDateTimeParse = DateTime.Parse(StartDateData);
                Console.WriteLine(StartDateData);
                Console.WriteLine(testDateTimeParse);
                Console.ReadLine();
            });

            menu.UseMenu();
        }
    }
}
