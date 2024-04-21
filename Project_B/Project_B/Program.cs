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
                    new Movie(0, "Rocky", 0, 14, "Much action and good plot", "Action", 120)
                };
                List<TimeTable> timeTableList = new List<TimeTable> { new TimeTable(0, 0, 1, "2024-3-24 12:00:00", "2024-3-24 14:00:00")};
                InputMenuTT selectTimeTable = new InputMenuTT("| Select TimeTable |");
                Console.ReadLine();
            });

            menu.UseMenu();
        }
    }
}
