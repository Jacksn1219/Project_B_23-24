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
            menu.Add("Test SeeActors", (x) =>
            {
                List<AuthorModel> authors = new List<AuthorModel>();
                authors.Add(new AuthorModel(1, "Jack Black", "Plays Po", 43));
                authors.Add(new AuthorModel(2, "Jackie Chan", "Plays Monkey", 57));
                authors.Add(new AuthorModel(3, "Ada Wong", "Plays Viper", 27));
                authors.Add(new AuthorModel(4, "Jada Pinket Smith", "Plays Tigress", 41));
                MovieModel movietje = new MovieModel("KUNG FU PANDA 4", "everybody was kung fu fighting", 12, 120, 1, "Horror");
                movietje.AuthorsInMovie.AddRange(authors);
                Console.WriteLine(movietje.SeeActors());
                Console.ReadLine();
            });
            menu.Add("Test SeeDirector", (x) =>
            {
                List<DirectorModel> directors = new List<DirectorModel>();
                directors.Add(new DirectorModel(1, "Christopher Nolan", "Famous movie director known for several blockbuster movies such as Oppenheimer, Interstellar, Inception and many more", 53));
                MovieModel interStellar = new MovieModel("Interstellar", "While the earth no longer has the resources to supply the human race, a group of astronauts go to beyond the milky way to find a possible future planet for mankind", 12, 190, 1, "Sci-Fi");
                Console.WriteLine(interStellar.SeeDirector(directors));
                Console.ReadLine();
            });
            menu.Add("Test SeeDescription", (x) =>
            {
                MovieModel interStellar = new MovieModel("Interstellar", "While the earth no longer has the resources to supply the human race, a group of astronauts go to beyond the milky way to find a possible future planet for mankind", 12, 190, 1, "Sci-Fi");
                Console.WriteLine(interStellar.SeeDescription());
                Console.ReadLine();
            });
            menu.UseMenu();


            //start of app
            DataAccess db = new SQliteDataAccess("epic connection string");
            MovieFactory movieFactory = new(db);
            movieFactory.CreateTable(); // creates the movie table, if movie is missing in the db.
            //geen ID
            MovieModel movie1 = new MovieModel("KUNG FU PANDA 4", "everybody was kung fu fighting", 12, 120, 1, "Horror"); //Film 1 wordt lokaal toegevoegd
            MovieModel movie2 = new MovieModel("DUNE: PART TWO", "I don't like sand. It's coarse and rough and irritating and it gets everywhere.", 16, 150, 1, "Kids");  //Film 2 wordt lokaal toegevoegd
            //wel ID
            MovieModel movie3 = movieFactory.GetItemFromId(1); //Film 3 wordt uit de database gehaald
            //movie3.ID = 1;  <- not possible to set the ID
            movie3.DurationInMin += 1;
            bool success = movieFactory.UpdateItem(movie3); //will be true
            movieFactory.UpdateItem(movie1); // will crash
            success = movieFactory.CreateItem(movie1); // do this instead
            //if you are not sure or want to play it safe
            success = movieFactory.ItemToDb(movie2); // will be true



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
