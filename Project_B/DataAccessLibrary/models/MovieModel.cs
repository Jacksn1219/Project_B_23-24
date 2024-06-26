﻿using System.Data.SQLite;
using DataAccessLibrary.models.interfaces;
using System.Text;
namespace DataAccessLibrary;
public enum PEGIAge
{
    PEGI4 = 4,
    PEGI7 = 7,
    PEGI12 = 12,
    PEGI16 = 16,
    PEGI18 = 18
}
public enum Genre
{
    Action_and_adventure,
    Animation,
    Comedy,
    Devotional,
    Drama,
    Historical,
    Horror,
    Science_fiction,
    Western,
    Kids,
    Violence
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
    private Genre _genre;
    public Genre Genre
    {
        get => _genre;
        set
        {
            _genre = value;
            IsChanged = true;
        }
    }
    public List<ActorModel> Actors = new();
    private bool _isRemoved;
    public bool IsRemoved
    {
        get => _isRemoved;
        set
        {
            _isRemoved = value;
            IsChanged = true;
        }
    }
    internal MovieModel(int? id, string name, string description, int pegiAge, int durationInMin, int? directorId, Genre genre)
    : this(id, name, description, (PEGIAge)pegiAge, durationInMin, directorId, genre) { }
    internal MovieModel(int? id, string name, string description, PEGIAge pegiAge, int durationInMin, int? directorId, Genre genre)
    {
        ID = id;
        Name = name;
        Description = description;
        PegiAge = pegiAge;
        DurationInMin = durationInMin;
        DirectorID = directorId;
        Genre = genre;
        IsRemoved = false;
    }
    /// <summary>
    /// parameterless ctor to please the JsonSerializer gods
    /// </summary>
    public MovieModel()
    {

    }
    public MovieModel(string name, string description, int pegiAge, int durationInMin, Genre genre)
    : this(null, name, description, pegiAge, durationInMin, null, genre) { }
    public MovieModel(string name, string description, int pegiAge, int durationInMin, Genre genre, DirectorModel dir, List<ActorModel> actors)
    : this(null, name, description, pegiAge, durationInMin, dir.ID, genre)
    {
        Director = dir;
        Actors = actors;
    }

    public void editName(string newName) => this.Name = newName;
    public void editDescription(string newDescription) => this.Description = newDescription;
    public void editpegiAge(int newpegiAge) => this.PegiAge = (PEGIAge)newpegiAge;
    public void editDuration(int newDuration) => this.DurationInMin = newDuration;
    public void editGenre(Genre newGenre) => this.Genre = newGenre;
    public void editDirector(DirectorModel newDirector) => this.Director = newDirector;
    public void addActor(ActorModel newActor)
    {
        List<int?> IDList = Actors.Select(x => (int?)x.ID).ToList();
        if(!IDList.Contains(newActor.ID)) this.Actors.Add(newActor);
    }
    public void removeActor(ActorModel newActors) => this.Actors.Remove(newActors);
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