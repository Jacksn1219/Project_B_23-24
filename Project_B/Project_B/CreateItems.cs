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

namespace Project_B
{
    public class CreateItems
    {
        private readonly ActorFactory _af;
        private readonly DirectorFactory _df;
        private readonly MovieFactory _mf;
        public CreateItems(ActorFactory af, DirectorFactory df, MovieFactory mf)
        {
            _af = af;
            _df = df;
            _mf = mf;
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
            _df.CreateItem(new DirectorModel("Martin Scorsese", "", 81));
            _df.CreateItem(new DirectorModel("David Fincher", "", 61));
            
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
            _af.CreateItem(new ActorModel("Dwayne Johnson", "The Rock", 51));
            _af.CreateItem(new ActorModel("Kevin Hart", "Side Rock", 44));
            _af.CreateItem(new ActorModel("Levi", "Something", 33));
            _af.CreateItem(new ActorModel("Someone", "Something else", 46));

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
                    // Test actors
                    //actorFactory.CreateItem(new ActorModel("Dwayne Johnson", "The Rock", 51));
                    //actorFactory.CreateItem(new ActorModel("Kevin Hart", "Side Rock", 44));
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
                    //_mf.AddRelatedActors(movieToEdit);

                    // Menu to chose director
                    InputMenu actorMenu = new InputMenu(Universal.centerToScreen("Choose an actor:"), null);
                    foreach (ActorModel actor in actorList)
                    {
                        actorMenu.Add(actor.Name, (x) => { movieToEdit.Actors.Add(actor); });
                    }

                    // Deleting chosen actor
                    foreach (ActorModel actor in movieToEdit.Actors) actorMenu.Remove(actor.Name);

                    actorMenu.UseMenu();

                    addOrRemoveTitle = Universal.centerToScreen($"Current actors are: ");
                    foreach (ActorModel actor in movieToEdit.Actors) { addOrRemoveTitle += "\n" + actor.Name; }
                    addOrRemove.editIntro(addOrRemoveTitle + "\n" + Universal.centerToScreen("What would you like to do?"));

                    // Menu to select more actors
                    InputMenu anotherActorMenu = new InputMenu(Universal.centerToScreen("Do you want to add another actor?\nIf not, click Back..."), null);
                    anotherActorMenu.Add("Yes", (x) =>
                    {
                        if (actorMenu.GetMenuOptionsCount() > 0)
                        {
                            actorMenu.UseMenu();
                            foreach (ActorModel actor in actorList)
                            {
                                actorMenu.Remove(actor.Name);
                                anotherActorMenu.UseMenu();
                            }
                            if (actorMenu.GetMenuOptionsCount() == 0)
                            {
                                anotherActorMenu.Remove("Yes");
                                anotherActorMenu.Add("No more actors", (x) => { });
                            }
                        }
                    });
                    anotherActorMenu.UseMenu();
                });
                addOrRemove.Add("Remove an actor", (x) =>
                {
                    //Get actors -> .FindIndex((x) => x.ID == movieToEdit.DirectorID)
                    //_mf.AddRelatedActors(movieToEdit);

                    // Menu to chose director
                    InputMenu actorMenu = new InputMenu(Universal.centerToScreen("Choose an actor:"), null);
                    foreach (ActorModel actor in movieToEdit.Actors)
                    {
                        actorMenu.Add(actor.Name, (x) => {
                            movieToEdit.removeActor(actor);
                            actorMenu.Remove(actor.Name);
                        });
                    }
                    actorMenu.UseMenu();

                    // Deleting chosen actor
                    
                    addOrRemoveTitle = Universal.centerToScreen($"Current actors are: ");
                    foreach (ActorModel actor in movieToEdit.Actors) { addOrRemoveTitle += "\n" + actor.Name; }
                    addOrRemove.editIntro(addOrRemoveTitle + "\n" + Universal.centerToScreen("What would you like to do?"));

                    // Menu to select more actors
                    InputMenu anotherActorMenu = new InputMenu(Universal.centerToScreen("Do you want to remove another actor?\nIf not, click Back..."), null);
                    anotherActorMenu.Add("Yes", (x) =>
                    {
                        if (actorMenu.GetMenuOptionsCount() > 0)
                        {
                            actorMenu.UseMenu();
                            foreach (ActorModel actor in movieToEdit.Actors) actorMenu.Remove(actor.Name);
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
    }
}
