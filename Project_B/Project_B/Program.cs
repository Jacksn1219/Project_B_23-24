using DataAccessLibrary;
using Models;
using System.Collections.Generic;
using System.Xml.Linq;
using TimeTablemodels;

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
                foreach (Room room in roomList/*getRoomFromDatabase() - Aymane*/) {
                    selectRoom.Add($"{room.Name}", (x) => DataAccessLibrary.Layout.editLayout(layout1/*getLayoutFromDatabase() - Aymane*/, room));
                }
                selectRoom.UseMenu();
            });
            menu.Add("Timetable", (x) =>
            {
                List<Movie> movieLayout1 = new List<Movie>{
                    new Movie(1, "Rocky", 1, 14, "Much action and good plot", "Action", 120),
                    new Movie(2, "Indiana Jones and The Lost Ark", 1, 16, "Wereldreiziger en archeoloog Indiana Jones wordt net voor de Tweede Wereldoorlog door de Amerikaanse overheid ingehuurd om een religieus relikwie op te sporen, voordat dit artefact in de handen van de Nazi's valt.", "Adventure, Action", 150),
                    new Movie(3, "Gone In 60 Seconds", 1, 18, "Randall Raines, een ex-autodief, leidt al een aantal jaren een rustig leven. Maar als zijn broer zich in de nesten werkt moet Randall zijn oude activiteiten weer oppakken om zijn broeder te redden.", "Action, Adventure, Thriller, Drama, Horror, Detective", 180),
                    new Movie(4, "Interstellar", 1, 12, "Terwijl de aarde niet meer in staat is om de mensheid in haar levensbehoefte te voorzien, gaat een groep ontdekkingsreizigers, ver buiten het melkwegstelsel, op zoek naar een toekomst voor de mens achter de sterren.", "drama, adventure, and speculative fiction", 90),
                    new Movie(5, "Cars 2", 1, 6, "Racecar Lightning McQueen en Mater besluiten mee te doen aan de World Grand Prix. Mater raakt onderweg echter betrokken bij spionage.", "action, comedy, and spy thriller", 120)
                };
                List<TimeTable> timeTableList = new List<TimeTable> { new TimeTable(1, 1, 1, "2024-3-24 12:00:00", "2024-3-24 14:00:00")};
                foreach (TimeTable timeTable in timeTableList)
                {
                    IEnumerable<Movie> query = movieLayout1.Where(movie => movie.ID == timeTable.MovieID);
                    foreach (Movie movie in query)
                    {
                        Console.WriteLine($"Movie: {movie.Title}");
                    }
                }
                Console.ReadLine();
            });

            menu.UseMenu();
        }
    }
}
