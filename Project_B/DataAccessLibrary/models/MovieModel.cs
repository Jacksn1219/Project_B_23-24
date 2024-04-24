using System.Data.SQLite;
using DataAccessLibrary.models.interfaces;
using System.Text;
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
    private string? _description;
    private PEGIAge _pegiAge;
    private int _durationInMin;
    /// <summary>
    /// the title of the movie
    /// </summary>
    private string? _name;
    public string? Name
    {
        get => _name;
        set
        {
            _name = value;
            IsChanged = true;
        }
    }
    /// <summary>
    /// the description of the movie
    /// </summary>
    public string? Description
    {
        get => _description;
        set
        {
            _description = value;
            IsChanged = true;
        }
    }
    /// <summary>
    /// the pegi age of the movie. valid numerics -> 4, 7, 12, 16 and 18
    /// </summary>
    public PEGIAge PegiAge
    {
        get => _pegiAge;
        set
        {
            _pegiAge = value;
            IsChanged = true;
        }
    }
    /// <summary>
    /// the duration in minutes
    /// </summary>
    public int DurationInMin
    {
        get => _durationInMin;
        set
        {
            _durationInMin = value;
            IsChanged = true;
        }
    }
    /// <summary>
    /// the ID of the director of the movie
    /// </summary>
    public int? DirectorID { get; set; }
    public DirectorModel? Director;
    private string? _genre;
    public string? Genre
    {
        get => _genre;
        set
        {
            _genre = value;
            IsChanged = true;
        }
    }
    public List<ActorModel> Actors = new();
    internal MovieModel(int? id, string name, string description, int pegiAge, int durationInMin, int? directorId, string genre)
    : this(id, name, description, (PEGIAge)pegiAge, durationInMin, directorId, genre) { }
    internal MovieModel(int? id, string name, string description, PEGIAge pegiAge, int durationInMin, int? directorId, string genre)
    {
        ID = id;
        Name = name;
        Description = description;
        PegiAge = pegiAge;
        DurationInMin = durationInMin;
        DirectorID = directorId;
        Genre = genre;
    }
    /// <summary>
    /// parameterless ctor to please the JsonSerializer gods
    /// </summary>
    public MovieModel()
    {

    }
    public MovieModel(string name, string description, int pegiAge, int durationInMin, string genre)
    : this(null, name, description, pegiAge, durationInMin, null, genre) { }
    public MovieModel(string name, string description, int pegiAge, int durationInMin, string genre, DirectorModel dir, List<ActorModel> actors)
    : this(null, name, description, pegiAge, durationInMin, dir.ID, genre)
    {
        Director = dir;
        Actors = actors;
    }
    public string SeeActors()
    {
        StringBuilder sb = new();
        sb.Append("All actors in this movie:\n");
        foreach (ActorModel actor in Actors)
        {
            sb.Append($"{actor.Name}\n");
        }
        return sb.ToString();
    }
    public string SeeDirector(List<DirectorModel> directors)
    {
        foreach (DirectorModel director in directors)
        {
            if (DirectorID == director.ID)
            {
                return $"The director of the movie {Name}: {director.Name}";
            }
        }
        return $"No director found for this {Name}";
    }
    public string SeeDescription()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append($"The minimum age for this movie is: {PegiAge}\n");
        sb.Append($"The genre of this movie is: {Genre}");
        sb.Append($"\nDescription of this movie: \n{Description}");
        return sb.ToString();
    }
}