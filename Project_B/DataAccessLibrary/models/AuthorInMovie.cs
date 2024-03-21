namespace DataAccessLibrary;

public class AuthorInMovie
{
    public int ID { get; }
    public int AuthorID { get; set; }
    public int MovieID { get; set; }
    public AuthorInMovie(int id, int authorID, int movieID)
    {
        ID = id;
        AuthorID = authorID;
        MovieID = movieID;
    }
}
