using DataAccessLibrary.logic;
using DataAccessLibrary;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Linq;
using DataAccessLibrary.models;

namespace Project_B
{
    public class CreateItems
    {
        private readonly ActorFactory _af;
        private readonly DirectorFactory _df;
        private readonly MovieFactory _mf;
        private readonly TimeTableFactory _ttf;
        public CreateItems(ActorFactory af, DirectorFactory df, MovieFactory mf, TimeTableFactory ttf)
        {
            _af = af;
            _df = df;
            _mf = mf;
            _ttf = ttf;
        }
        public void CreateNewMovie()
        {
            // Name //
            Console.WriteLine("What is the name of the movie?");
            string Name = Console.ReadLine() ?? "";

            // Discription //
            Console.WriteLine("What is the discription of the movie?");
            string Discription = Console.ReadLine() ?? "";

            // pegiAge //
            int pegiAge = 0;
            Console.WriteLine("What is the PEGIage of the movie? (4, 7, 12, 16, 18)");
            int.TryParse(Console.ReadLine(), out pegiAge);
            List<int> possiblePegiAges = new List<int> { 4, 7, 12, 16, 18 };
            while (!possiblePegiAges.Contains(pegiAge))
            {
                try
                {
                    Universal.WriteColor("Invalid number, try again!", ConsoleColor.Red);
                    Console.WriteLine("What is the PEGIage of the movie? (4, 7, 12, 16, 18)");
                    int.TryParse(Console.ReadLine(), out pegiAge);
                }
                catch { }
            }

            // Duration in minutes //
            int Duration = 0;
            Console.WriteLine("What is the duration of the movie? (more than 0)");
            int.TryParse(Console.ReadLine(), out Duration);
            while (Duration == 0)
            {
                Universal.WriteColor("Invalid number, try again!", ConsoleColor.Red);
                Console.WriteLine("What is the duration of the movie? (more than 0)");
                int.TryParse(Console.ReadLine(), out Duration);
            }

            // genre //
            Console.WriteLine("What is the genre of the movie?");
            string Genre = Console.ReadLine() ?? "";

            // Director //
            DirectorModel Director = new DirectorModel("", "", 0);

            List<DirectorModel> directorList = new List<DirectorModel>();
            // Test directors
            //_df.CreateItem(new DirectorModel("Martin Scorsese", "", 81));
            //_df.CreateItem(new DirectorModel("David Fincher", "", 61));

            // Get directors from database
            try
            {
                int i = 1;
                DirectorModel? director = new DirectorModel("", "", 0);
                while (director != null)
                {
                    director = _df.GetItemFromId(i);
                    if (director != null) directorList.Add(director);
                    i++;
                }
            }
            catch { }

            // Menu to chose director
            InputMenu directorMenu = new InputMenu(Universal.centerToScreen("Choose a director:"), null);
            foreach (DirectorModel director in directorList)
            {
                directorMenu.Add(director.Name, (x) => { Director = director; });
            }
            if (directorMenu.GetMenuOptionsCount() > 0) directorMenu.UseMenu();

            // Actors //
            List<ActorModel> Actors = new List<ActorModel>();

            List<ActorModel> actorList = new List<ActorModel>();
            // Test actors
            //_af.CreateItem(new ActorModel("Dwayne Johnson", "The Rock", 51));
            //_af.CreateItem(new ActorModel("Kevin Hart", "Side Rock", 44));

            // Get directors from database
            try
            {
                int i = 1;
                ActorModel? actor = new ActorModel("", "", 0);
                while (actor != null)
                {
                    actor = _af.GetItemFromId(i);
                    if (actor != null) actorList.Add(actor);
                    i++;
                }
            }
            catch { }

            // Menu to chose director
            InputMenu actorMenu = new InputMenu(Universal.centerToScreen("Choose an actor:"), null);
            foreach (ActorModel actor in actorList)
            {
                actorMenu.Add(actor.Name, (x) => { Actors.Add(actor); });
            }
            actorMenu.UseMenu();

            // Deleting chosen actor
            foreach (ActorModel actor in Actors) actorMenu.Remove(actor.Name);

            // Menu to select more actors
            InputMenu anotherActorMenu = new InputMenu(Universal.centerToScreen("Do you want to add another actor?\nIf not, click Back..."));
            anotherActorMenu.Add("Yes", (x) =>
            {
                if (actorMenu.GetMenuOptionsCount() > 0)
                {
                    actorMenu.UseMenu();
                    foreach (ActorModel actor in Actors) try { actorMenu.Remove(actor.Name); } catch { }
                }
                if (actorMenu.GetMenuOptionsCount() == 0)
                {
                    anotherActorMenu.Remove("Yes");
                    anotherActorMenu.Add("No more actors", (x) => { });
                }
            });
            anotherActorMenu.UseMenu();

            MovieModel newMovie = new MovieModel(Name, Discription, pegiAge, Duration, Genre, Director, Actors);
            _mf.CreateItem(newMovie);
        }
        public void ChangeMovie()
        {
            List<MovieModel> movieList = new List<MovieModel>();
            try
            {
                int page = 1;
                while (true)
                {
                    var movs = _mf.GetItems(100, page, 6);
                    movieList.AddRange(movs);
                    page++;
                    if (movs.Length < 100) break;
                }
            }
            catch { }

            //movie to edit
            MovieModel movieToEdit = new MovieModel("", "", 4, 120, "");

            // Menu to chose Movie
            InputMenu movieMenu = new InputMenu(Universal.centerToScreen("Select movie to edit:"), null);
            foreach (MovieModel movie in movieList)
            {
                movieMenu.Add(movie.Name ?? "", (x) => { movieToEdit = movie; });
            }
            movieMenu.UseMenu();

            InputMenu whatToEditMenu = new InputMenu(Universal.centerToScreen("Select what you want to edit:"), null);
            whatToEditMenu.Add("Name", (x) =>
            {
                Console.WriteLine($"Current Name = {movieToEdit.Name}" + "\n" + "What is the new name of the movie?");
                string Name = Console.ReadLine() ?? movieToEdit.Name ?? "";
                movieToEdit.editName(Name);
            });
            whatToEditMenu.Add("Description", (x) =>
            {
                Console.WriteLine($"Current Description = {movieToEdit.Description}" + "\n" + "What is the new discription of the movie?");
                string Description = Console.ReadLine() ?? movieToEdit.Description ?? "";
                movieToEdit.editDescription(Description);
            });
            whatToEditMenu.Add("pegiAge", (x) =>
            {

                int pegiAge = 0;
                Console.WriteLine($"Current pegiAge = {movieToEdit.PegiAge}" + "\n" + "What is the new PEGIage of the movie? (4, 7, 12, 16, 18)");
                int.TryParse(Console.ReadLine(), out pegiAge);
                List<int> possiblePegiAges = new List<int> { 4, 7, 12, 16, 18 };
                while (!possiblePegiAges.Contains(pegiAge))
                {
                    try
                    {
                        Universal.WriteColor("Invalid number, try again!", ConsoleColor.Red);
                        Console.WriteLine($"Current pegiAge = {movieToEdit.PegiAge}" + "\n" + "What is the PEGIage of the movie? (4, 7, 12, 16, 18)");
                        int.TryParse(Console.ReadLine(), out pegiAge);
                    }
                    catch { }
                }
                movieToEdit.editpegiAge(pegiAge);
            });
            whatToEditMenu.Add("Duration", (x) =>
            {
                int Duration = 0;
                Console.WriteLine($"Current duration = {movieToEdit.DurationInMin}" + "\n" + "What is the new duration of the movie? (more than 0)");
                int.TryParse(Console.ReadLine(), out Duration);
                while (Duration == 0)
                {
                    Universal.WriteColor("Invalid number, try again!", ConsoleColor.Red);
                    Console.WriteLine($"Current duration = {movieToEdit.DurationInMin}" + "\n" + "What is the new duration of the movie? (more than 0)");
                    int.TryParse(Console.ReadLine(), out Duration);
                }
                movieToEdit.editDuration(Duration);
            });
            whatToEditMenu.Add("Genre", (x) =>
            {
                Console.WriteLine($"Current genre = {movieToEdit.Genre}" + "\n" + "What is the new genre of the movie?");
                string Genre = Console.ReadLine() ?? "";
                movieToEdit.editGenre(Genre);
            });
            whatToEditMenu.Add("Director", (x) =>
            {
                DirectorModel Director = new DirectorModel("", "", 0);

                List<DirectorModel> directorList = new List<DirectorModel>();
                // Test directors
                //_df.CreateItem(new DirectorModel("Martin Scorsese", "", 81));
                //_df.CreateItem(new DirectorModel("David Fincher", "", 61));
                // Get directors from database
                try
                {
                    int i = 1;
                    DirectorModel? director = new DirectorModel("", "", 0);
                    while (director != null)
                    {
                        director = _df.GetItemFromId(i);
                        if (director != null) directorList.Add(director);
                        i++;
                    }
                }
                catch { }

                //Get directors -> .FindIndex((x) => x.ID == movieToEdit.DirectorID)
                movieToEdit.Director = _df.GetItemFromId(movieToEdit.DirectorID ?? 1);

                // Menu to chose director
                InputMenu directorMenu = new InputMenu(Universal.centerToScreen($"Current director = {movieToEdit.Director.Name}") + "\n" + Universal.centerToScreen("Choose a new director:"), null);
                foreach (DirectorModel director in directorList)
                {
                    if (movieToEdit.Director.Name == director.Name) continue;
                    directorMenu.Add(director.Name, (x) => { Director = director; });
                }
                if (directorMenu.GetMenuOptionsCount() > 0) directorMenu.UseMenu();
                movieToEdit.editDirector(Director);
            });
            whatToEditMenu.Add("Actors", (x) =>
            {
                string addOrRemoveTitle = Universal.centerToScreen($"Current actors are: ");
                foreach (ActorModel actor in movieToEdit.Actors) { addOrRemoveTitle += "\n" + actor.Name; }
                InputMenu addOrRemove = new InputMenu(addOrRemoveTitle + "\n" + Universal.centerToScreen("What would you like to do?"));
                addOrRemove.Add("Add an actor", (x) =>
                {
                    List<ActorModel> actorList = new List<ActorModel>();

                    // Get directors from database
                    try
                    {
                        int i = 1;
                        ActorModel? actor = new ActorModel("", "", 0);
                        while (actor != null)
                        {
                            actor = _af.GetItemFromId(i);
                            if (actor != null) actorList.Add(actor);
                            i++;
                        }
                    }
                    catch { }

                    //Get actors -> .FindIndex((x) => x.ID == movieToEdit.DirectorID)
                    _mf.AddRelatedActors(movieToEdit);

                    // Menu to chose director
                    InputMenu actorMenu = new InputMenu(Universal.centerToScreen("Choose an actor:"), null);
                    foreach (ActorModel actor in actorList)
                    {
                        actorMenu.Add(actor.Name, (x) => { movieToEdit.Actors.Add(actor); });
                    }

                    // Deleting chosen actor
                    foreach (ActorModel actor in movieToEdit.Actors) actorMenu.Remove(actor.Name);

                    actorMenu.UseMenu();

                    // Menu to select more actors
                    InputMenu anotherActorMenu = new InputMenu(Universal.centerToScreen("Do you want to add another actor?\nIf not, click Back..."), null);
                    anotherActorMenu.Add("Yes", (x) =>
                    {
                        if (actorMenu.GetMenuOptionsCount() > 0)
                        {
                            actorMenu.UseMenu();
                            foreach (ActorModel actor in actorList) try { actorMenu.Remove(actor.Name); } catch { }
                        }
                        if (actorMenu.GetMenuOptionsCount() == 0)
                        {
                            anotherActorMenu.Remove("Yes");
                            anotherActorMenu.Add("No more actors", (x) => { });
                        }
                    });
                    anotherActorMenu.UseMenu();
                });
                addOrRemove.Add("Remove an actor", (x) =>
                {
                    //Get actors -> .FindIndex((x) => x.ID == movieToEdit.DirectorID)
                    _mf.AddRelatedActors(movieToEdit);

                    // Menu to chose director
                    InputMenu actorMenu = new InputMenu(Universal.centerToScreen("Choose an actor:"), null);
                    foreach (ActorModel actor in movieToEdit.Actors)
                    {
                        actorMenu.Add(actor.Name, (x) => {
                            movieToEdit.Actors.Remove(actor); // Remove Actor function -> yet to be...     Line 372 / CreateItems.cs
                            actorMenu.Remove(actor.Name);
                        });
                    }
                    actorMenu.UseMenu();

                    // Deleting chosen actor
                    foreach (ActorModel actor in movieToEdit.Actors) actorMenu.Remove(actor.Name);

                    // Menu to select more actors
                    InputMenu anotherActorMenu = new InputMenu(Universal.centerToScreen("Do you want to remove another actor?\nIf not, click Back..."), null);
                    anotherActorMenu.Add("Yes", (x) =>
                    {
                        if (actorMenu.GetMenuOptionsCount() > 0)
                        {
                            actorMenu.UseMenu();
                            //foreach (ActorModel actor in movieToEdit.Actors) try { actorMenu.Remove(actor.Name); } catch { }
                        }
                        if (actorMenu.GetMenuOptionsCount() == 0)
                        {
                            anotherActorMenu.Remove("Yes");
                            anotherActorMenu.Add("No more actors", (x) => { });
                        }
                    });
                    anotherActorMenu.UseMenu();
                });
                addOrRemove.UseMenu();
            });
            whatToEditMenu.UseMenu();
            _mf.ItemToDb(movieToEdit);
        }

        public void CreateTimeTable()
        {
            // Select a movie from the database
            List<MovieModel> movieList = new List<MovieModel>();
            try
            {
                int page = 1;
                while (true)
                {
                    var movs = _mf.GetItems(100, page, 6);
                    movieList.AddRange(movs);
                    page++;
                    if (movs.Length < 100) break;
                }
            }
            catch { }

            MovieModel selectedMovie = new MovieModel("", "", 4, 120, "");
            InputMenu movieMenu = new InputMenu(Universal.centerToScreen("Select a movie:"), null);
            foreach (MovieModel movie in movieList)
            {
                movieMenu.Add(movie.Name ?? "", (x) => { selectedMovie = movie; });
            }
            movieMenu.UseMenu();

            // Select a room
            RoomModel selectedRoom = new RoomModel(); // Assuming you have a RoomModel class

            Console.WriteLine("Enter the room number:");
            int roomNumber = int.Parse(Console.ReadLine() ?? "0");

            // Assuming you have a method to get RoomModel by room number
            try
            {
                selectedRoom = _ttf.getRelatedItemsFromDb(roomNumber); // Implement this method in _ttf
            }
            catch
            {
                Console.WriteLine("Invalid room number. Please try again.");
                return;
            }

            // Set start date and end date
            Console.WriteLine("Enter the start date (yyyy-MM-dd HH:mm):");
            DateTime startDate;
            while (!DateTime.TryParse(Console.ReadLine(), out startDate))
            {
                Console.WriteLine("Invalid date format. Please enter the start date (yyyy-MM-dd HH:mm):");
            }

            Console.WriteLine("Enter the end date (yyyy-MM-dd HH:mm):");
            DateTime endDate;
            while (!DateTime.TryParse(Console.ReadLine(), out endDate))
            {
                Console.WriteLine("Invalid date format. Please enter the end date (yyyy-MM-dd HH:mm):");
            }

            // Create new timetable
            TimeTableModel newTimeTable = new TimeTableModel(selectedRoom, selectedMovie, startDate, endDate);
            _ttf.CreateItem(newTimeTable);
        }


        public void EditTimeTable()
        {
            // Fetch existing timetables from the database
            // List<TimeTableModel> timeTableList = new List<TimeTableModel>();
            // try
            // {
            //     int page = 1;
            //     while (true)
            //     {
            //         var tts = _ttf.GetItems(100, page, 6);
            //         timeTableList.AddRange(tts);
            //         page++;
            //         if (tts.Length < 100) break;
            //     }
            // }
            // catch { }

            // TimeTableModel selectedTimeTable = new TimeTableModel(0, 0, DateTime.Now, DateTime.Now);
            // InputMenu timeTableMenu = new InputMenu(Universal.centerToScreen("Select a timetable to edit:"), null);
            // foreach (TimeTableModel tt in timeTableList)
            // {
            //     string movieName = _mf.GetItemFromId(tt.MovieID)?.Name ?? "Unknown Movie";
            //     timeTableMenu.Add($"{movieName} in Room {tt.RoomID} from {tt.StartDate} to {tt.EndDate}", (x) => { selectedTimeTable = tt; });
            // }
            // timeTableMenu.UseMenu();

            // InputMenu editMenu = new InputMenu(Universal.centerToScreen("Select what you want to edit:"), null);
            // editMenu.Add("Movie", (x) =>
            // {
            //     List<MovieModel> movieList = new List<MovieModel>();
            //     try
            //     {
            //         int page = 1;
            //         while (true)
            //         {
            //             var movs = _mf.GetItems(100, page, 6);
            //             movieList.AddRange(movs);
            //             page++;
            //             if (movs.Length < 100) break;
            //         }
            //     }
            //     catch { }

            //     MovieModel selectedMovie = new MovieModel("", "", 4, 120, "");
            //     InputMenu movieMenu = new InputMenu(Universal.centerToScreen("Select a new movie:"), null);
            //     foreach (MovieModel movie in movieList)
            //     {
            //         movieMenu.Add(movie.Name ?? "", (x) => { selectedMovie = movie; });
            //     }
            //     movieMenu.UseMenu();

            //     selectedTimeTable.MovieID = selectedMovie.ID;
            // });
            // editMenu.Add("Room", (x) =>
            // {
            //     Console.WriteLine($"Current Room = {selectedTimeTable.RoomID}\nEnter the new room number:");
            //     int roomNumber = int.Parse(Console.ReadLine() ?? selectedTimeTable.RoomID.ToString());
            //     selectedTimeTable.RoomID = roomNumber;
            // });
            // editMenu.Add("Start Date", (x) =>
            // {
            //     Console.WriteLine($"Current Start Date = {selectedTimeTable.StartDate}\nEnter the new start date (yyyy-MM-dd HH:mm):");
            //     DateTime startDate = DateTime.Parse(Console.ReadLine() ?? selectedTimeTable.StartDate.ToString());
            //     selectedTimeTable.StartDate = startDate;
            // });
            // editMenu.Add("End Date", (x) =>
            // {
            //     Console.WriteLine($"Current End Date = {selectedTimeTable.EndDate}\nEnter the new end date (yyyy-MM-dd HH:mm):");
            //     DateTime endDate = DateTime.Parse(Console.ReadLine() ?? selectedTimeTable.EndDate.ToString());
            //     selectedTimeTable.EndDate = endDate;
            // });
            // editMenu.UseMenu();

            // _ttf.UpdateItem(selectedTimeTable);
        }
    }
}
