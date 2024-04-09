using System.Data.SQLite;
using DataAccessLibrary.models.interfaces;

namespace DataAccessLibrary;
public enum PEGIAge
{
    PEGI3 = 3,
    PEGI7 = 7,
    PEGI12 = 12,
    PEGI16 = 16,
    PEGI18 = 18

}

public class MovieModel : DbItem
{
    /// <summary>
    /// the db Id of the movie. must be positive and should be readonly for external classes.
    /// </summary>
    public override int? ID { get; }
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
    /// <summary>
    /// the duration in minutes
    /// </summary>
    public int DurationInMin { get; set; }
    /// <summary>
    /// the ID of the director of the movie
    /// </summary>
    internal int? DirectorID { get; }
    public DirectorModel? Director { get; set; }
    public string Genre { set; get; }
    internal MovieModel(int? id, string name, string description, int pegiAge, int durationInMin, int directorId, string genre)
    : this(id, name, description, (PEGIAge)pegiAge, durationInMin, directorId, genre) { }
    internal MovieModel(int? id, string name, string description, PEGIAge pegiAge, int durationInMin, int directorId, string genre)
    {
        ID = id;
        Name = name;
        Description = description;
        PegiAge = pegiAge;
        DurationInMin = durationInMin;
        DirectorID = directorId;
        Genre = genre;
    }
    public MovieModel(string name, string description, int pegiAge, int durationInMin, int directorId, string genre)
    : this(null, name, description, pegiAge, durationInMin, directorId, genre) { }
}