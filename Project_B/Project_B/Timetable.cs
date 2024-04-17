using DataAccessLibrary.models;

namespace DataAccessLibrary;

public class Timetable
{
    //private Dictionary<DateTime, KeyValuePair<MovieModel, RoomModel>> schedule = new Dictionary<DateTime, KeyValuePair<MovieModel, RoomModel>>(); // Een dictionary om de planning van films op te slaan met datum als sleutel en een KeyValuePair van film en zaal als waarde
    private readonly List<TimeTableModel> timetable = new List<TimeTableModel>();
    public void AddMovie(DateTime startTime, MovieModel movie, RoomModel room)
    {
        timetable.Add(new TimeTableModel(room, movie, startTime));
        //schedule[startTime] = new KeyValuePair<MovieModel, RoomModel>(movie, room); // Voeg een film toe aan de timetable met de starttijd als sleutel en een KeyValuePair van de film en zaal als waarde
    }

    public void DisplayTimetable()
    {
        Console.WriteLine("Timetable:");
        foreach (TimeTableModel item in timetable)
        {

            Console.WriteLine($"Movie: {item.Movie.Name}, RoomModel: {item.Room.Name}, Start Time: {item.StartDate}, End Time: {item.EndDate}"); // Toon de film, zaal, starttijd en eindtijd van de film
        }
    }
}