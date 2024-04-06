namespace DataAccessLibrary;

public class ActorInMovie
{// waarchijnlijk ook niet nodig
    public int ID { get; }
    public int AuthorID { get; set; }
    public int MovieID { get; set; }
    public ActorInMovie(int id, int authorID, int movieID)
    {
        ID = id;
        AuthorID = authorID;
        MovieID = movieID;
    }
}
