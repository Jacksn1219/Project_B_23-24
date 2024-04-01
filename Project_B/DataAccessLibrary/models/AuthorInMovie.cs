namespace DataAccessLibrary;

public class AuthorInMovie
{// waarchijnlijk ook niet nodig
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
