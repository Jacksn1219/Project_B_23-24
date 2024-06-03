using DataAccessLibrary.models.interfaces;

namespace DataAccessLibrary;

public class ActorInMovieModel : DbItem
{
    public required int AuthorID { get; set; }
    public required int MovieID { get; set; }
    public required decimal Price { get; set; }
}
