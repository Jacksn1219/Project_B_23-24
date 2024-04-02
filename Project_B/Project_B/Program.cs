using DataAccessLibrary;
using Models;
using System;
using System.IO;
using System.Text;
using System.Data.SQLite;

namespace Project_B
{
    class Program
    {
        private const string databaseConnectionString = @"Data Source=Project_B\Project_B\database.db;";

        public static void Main()
        {
            InputMenu menu = new InputMenu("| Main menu |", true);
            menu.Add("Setup Database", (x) =>
            {
                // Setup SQLite database
                SQLite.SetupProjectB();
                Console.ReadLine();
            });
            menu.Add("Generate C# Models", (x) =>
            {
                using (SQLiteConnection conn = new SQLiteConnection(databaseConnectionString))
                {
                    conn.Open();
                    CSharpModelGenerator.GenerateModels(conn);
                }
                Console.WriteLine("C# models generated successfully.");
            });
            menu.Add("Test Author", (x) =>
            {
                Author testAuthor = new Author(1, "John", "Not successful", 25);
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
