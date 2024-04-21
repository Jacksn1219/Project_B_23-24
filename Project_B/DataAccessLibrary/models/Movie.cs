namespace DataAccessLibrary;

public class Movie
{
    public int ID { get; }
    public string Title { get; set; }
    public int DirectorID { get; }
    public int pegiAge { get; set; }
    public string Discription { get; set; }
    public string Genre { get; set; }
    public int DurationInMin { get; set; }
    public Movie(int id, string title, int directorID, int pegiAge, string discription, string genre, int durationInMin)
    {
        ID = id;
        Title = title;
        DirectorID = directorID;
        this.pegiAge = pegiAge;
        Discription = discription;
        Genre = genre;
        DurationInMin = durationInMin;
    }
}
