using DataAccessLibrary;

public class MovieFactory : IDbItemFactory<MovieModel>
{
    /// <summary>
    /// The db acccess for the class
    /// </summary>
    private readonly DataAccess _db;
    /// <summary>
    /// the constructor for the movie factory
    /// </summary>
    /// <param name="db">the database connection</param>
    public MovieFactory(DataAccess db)
    {
        _db = db;
    }
    /// <summary>
    /// creates the movie table 
    /// </summary>
    public void CreateTable()
    {
        _db.SaveData(
            @"CREATE TABLE IF NOT EXISTS Movie(
            ID INTEGER PRIMARY KEY AUTOINCREMENT UNIQUE NOT NULL ,
            Name TEXT NOT NULL,
            DirectorID INTEGER NOT NULL,
            pegiAge INTEGER NOT NULL,
            Description TEXT,
            Genre TEXT  NOT NULL,
            DurationInMin INTEGER  NOT NULL,
            FOREIGN KEY (DirectorID) REFERENCES Director (ID)
            )"
        );
    }
    /// <summary>
    /// Get a MovieModel from the ID
    /// </summary>
    /// <param name="id">the ID of the Movie</param>
    /// <returns>the first movie returned from the query</returns>
    public MovieModel GetItemFromId(int id)
    {
        return _db.ReadData<MovieModel>(
            @"SELECT * FROM Movie
            WHERE ID=$1",
            new Dictionary<string, dynamic?>()
            {
                {"$1", id}
            }
            ).First();
    }
    /// <summary>
    /// updates or creates the movie in the db
    /// </summary>
    /// <param name="movie">the movie to update or create</param>
    /// <returns>true if succesfull, else false</returns>
    public bool ItemToDb(MovieModel movie)
    {
        if (movie.ID == null) return CreateItem(movie);
        return UpdateItem(movie);

    }
    /// <summary>
    /// creates a new movie in the db
    /// </summary>
    /// <param name="movie">the movie to create</param>
    /// <returns>true if successfull, else false</returns>
    /// <exception cref="InvalidOperationException">when ID is not null</exception>
    public bool CreateItem(MovieModel movie)
    {
        if (movie.ID != null) throw new InvalidOperationException("this movie already exists in the db");
        return _db.SaveData(
            @"INSERT INTO Movie(
                Name,
                DirectorID,
                pegiAge,
                Description,
                Genre,
                DurationInMin
            )
            VALUES ($1,$2,$3,$4,$5,$6);",
            new Dictionary<string, dynamic?>(){
                {"$1", movie.Name},
                {"$2", movie.DirectorId},
                {"$3", (int)movie.PegiAge},
                {"$4", movie.Description},
                {"$5", movie.Genre},
                {"$6", movie.DurationInMin}
            }
        );
    }
    /// <summary>
    /// updates the movie
    /// </summary>
    /// <param name="movie">the movie to update</param>
    /// <returns>true if succesfull, else false</returns>
    /// <exception cref="InvalidOperationException">if ID of the movie is null</exception>
    public bool UpdateItem(MovieModel movie)
    {
        if (movie.ID == null) throw new InvalidOperationException("cannot update a movie without an ID.");
        return _db.SaveData(
            @"UPDATE Movie
            SET Name = $1,
                DirectorID = $2,
                pegiAge = $3,
                Description = $4,
                Genre = $5,
                DurationInMin = $6
            WHERE ID = $7;",
            new Dictionary<string, dynamic?>(){
                {"$1", movie.Name},
                {"$2", movie.DirectorId},
                {"$3", (int)movie.PegiAge},
                {"$4", movie.Description},
                {"$5", movie.Genre},
                {"$6", movie.DurationInMin},
                {"$7", movie.ID}
            }
        );
    }
}