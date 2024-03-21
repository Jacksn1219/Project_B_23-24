using DataAccessLibrary;

namespace Project_B
{
    class Program
    {
        public static void Main()
        {
            //Opzet Sqlite database
            //SQLite.SetupProjectB();

            //Test models
            Author testAuthor = new Author(1, "John", "Not succesfull", 25);
            Console.WriteLine($"{testAuthor.Name} - {testAuthor.Age} :\n{testAuthor.Description}");
        }
    }
}
