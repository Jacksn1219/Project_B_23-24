namespace DataAccessLibrary;

public class Timetable
{
    private Dictionary<DateTime, KeyValuePair<MovieModel, RoomModel>> schedule = new Dictionary<DateTime, KeyValuePair<MovieModel, RoomModel>>(); // Een dictionary om de planning van films op te slaan met datum als sleutel en een KeyValuePair van film en zaal als waarde

    public void AddMovie(DateTime startTime, MovieModel movie, RoomModel room)
    {
        schedule[startTime] = new KeyValuePair<MovieModel, RoomModel>(movie, room); // Voeg een film toe aan de timetable met de starttijd als sleutel en een KeyValuePair van de film en zaal als waarde
    }

    public void DisplayTimetable()
    {
        Console.WriteLine("Timetable:");
        foreach (var item in schedule)
        {
            DateTime endTime = item.Key.AddMinutes(item.Value.Key.DurationInMin); // Bereken de eindtijd van de film
            Console.WriteLine($"Movie: {item.Value.Key.Name}, RoomModel: {item.Value.Value.ID}, Start Time: {item.Key}, End Time: {endTime}"); // Toon de film, zaal, starttijd en eindtijd van de film
        }
    }
}