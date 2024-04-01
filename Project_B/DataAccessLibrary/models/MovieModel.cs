using System.Data.SQLite;

namespace DataAccessLibrary;
public enum PEGIAge
{
    PEGI3 = 3,
    PEGI7 = 7,
    PEGI12 = 12,
    PEGI16 = 16,
    PEGI18 = 18

}

public class MovieModel : IDbItem
{

    private int _id;
    /// <summary>
    /// the db Id of the movie. must be positive and should be readonly for external classes.
    /// </summary>
    public int Id
    {
        get { return _id; }
        set
        {
            if (value >= 0)
            {
                _id = value;
            }
            else throw new InvalidDataException("the unique Id cannot be below 0");
        }
    }
    /// <summary>
    /// the title of the movie
    /// </summary>
    public string? Name { get; set; }
    /// <summary>
    /// the description of the movie
    /// </summary>
    public string? Description { get; set; }
    /// <summary>
    /// the pegi age of the movie. valid numerics -> 4, 7, 12, 16 and 18
    /// </summary>
    public PEGIAge PegiAge { get; set; }

}