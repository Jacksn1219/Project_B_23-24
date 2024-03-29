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
    public string? Name { get; set; }
    public string? Description { get; set; }
    public PEGIAge PegiAge { get; set; }

}