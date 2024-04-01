namespace DataAccessLibrary;

public class Timetable
{
    private Dictionary<DateTime, KeyValuePair<Movie, Room>> schedule = new Dictionary<DateTime, KeyValuePair<Movie, Room>>(); // Een dictionary om de planning van films op te slaan met datum als sleutel en een KeyValuePair van film en zaal als waarde

    public void AddMovie(DateTime startTime, Movie movie, Room room)
    {
        schedule[startTime] = new KeyValuePair<Movie, Room>(movie, room); // Voeg een film toe aan de timetable met de starttijd als sleutel en een KeyValuePair van de film en zaal als waarde
    }

    public void DisplayTimetable()
    {
        Console.WriteLine("Timetable:");
        foreach (var item in schedule)
        {
            DateTime endTime = item.Key.AddMinutes(item.Value.Key.DurationInMin); // Bereken de eindtijd van de film
            Console.WriteLine($"Movie: {item.Value.Key.Title}, Room: {item.Value.Value.ID}, Start Time: {item.Key}, End Time: {endTime}"); // Toon de film, zaal, starttijd en eindtijd van de film
        }
    }
}