﻿using System.Data.SQLite;

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

    private int? _id;
    /// <summary>
    /// the db Id of the movie. must be positive and should be readonly for external classes.
    /// </summary>
    public int? ID
    {
        get { return _id; }
        set
        {
            if (value == null || value >= 0)
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
    /// <summary>
    /// the duration in minutes
    /// </summary>
    public int DurationInMin { get; set; }
    /// <summary>
    /// the ID of the director of the movie
    /// </summary>
    public int? DirectorId { get; set; }
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
        DirectorId = directorId;
        Genre = genre;
    }
    public MovieModel(string name, string description, int pegiAge, int durationInMin, int directorId, string genre)
    : this(null, name, description, pegiAge, durationInMin, directorId, genre) { }
}