namespace DataAccessLibrary;

public class Movie
{
    public int ID { get; }
    public int DirectorID { get; }
    public int pegiAge { get; set; }
    public string Discription { get; set; }
    public string Genre { get; set; }
    public int DurationInSec { get; set; }
    public Movie(int id, int directorID, int pegiAge, string discription, string genre, int durationInSec)
    {
        ID = id;
        DirectorID = directorID;
        this.pegiAge = pegiAge;
        Discription = discription;
        Genre = genre;
        DurationInSec = durationInSec;
    }
}
