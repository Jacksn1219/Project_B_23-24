using DataAccessLibrary;
using DataAccessLibrary.logic;

public class MovieFactory : IDbItemFactory<MovieModel>
{
    /// <summary>
    /// The db acccess for the class
    /// </summary>
    private readonly DataAccess _db;
    private readonly DirectorFactory _df;
    private readonly ActorFactory _af;
    /// <summary>
    /// the constructor for the movie factory
    /// </summary>
    /// <param name="db">the database connection</param>
    public MovieFactory(DataAccess db, DirectorFactory df, ActorFactory af)
    {
        _db = db;
        _df = df;
        _af = af;
        CreateTable();
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
            DirectorID INTEGER,
            pegiAge INTEGER NOT NULL,
            Description TEXT,
            Genre TEXT NOT NULL,
            DurationInMin INTEGER  NOT NULL,
            FOREIGN KEY (DirectorID) REFERENCES Director (ID)
            )"
        );
        _db.SaveData(
            @"CREATE TABLE IF NOT EXISTS ActorInMovie(
                ID INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
                ActorID INTEGER NOT NULL,
                MovieID INTEGER NOT NULL,
                FOREIGN KEY (ActorID) REFERENCES Actor (ID),
                FOREIGN KEY (MovieID) REFERENCES Movie (ID)
            )"
        );
    }
    /// <summary>
    /// Get a MovieModel from the ID
    /// </summary>
    /// <param name="id">the ID of the Movie</param>
    /// <returns>the first movie returned from the query</returns>
    public MovieModel? GetItemFromId(int id)
    {
        try
        {
            return _db.ReadData<MovieModel>(
            @"SELECT * FROM Movie
            WHERE ID=$1",
            new Dictionary<string, dynamic?>()
            {
                {"$1", id}
            }
            ).First();
        } catch { return null; }
    }
    /// <summary>
    /// updates or creates the movie in the db
    /// </summary>
    /// <param name="item">the movie to update or create</param>
    /// <returns>true if succesfull, else false</returns>
    public bool ItemToDb(MovieModel item)
    {

        bool directorchanged = item.Director != null && item.Director.IsChanged;
        bool actorsChanged = false;
        foreach (var act in item.Actors)
        {
            if (act.IsChanged)
            {
                actorsChanged = true;
                break;
            }
        }
        if (!item.IsChanged && (actorsChanged || directorchanged)) return RelatedItemsToDb(item);
        if (!item.IsChanged) return true;
        if (item.ID == null) return CreateItem(item);
        return UpdateItem(item);

    }
    /// <summary>
    /// creates a new movie in the db
    /// </summary>
    /// <param name="item">the movie to create</param>
    /// <returns>true if successfull, else false</returns>
    /// <exception cref="InvalidOperationException">when ID is not null</exception>
    public bool CreateItem(MovieModel item)
    {
        if (item.ID != null) throw new InvalidOperationException("this movie already exists in the db");
        if (!item.IsChanged) return true;
        bool result = RelatedItemsToDb(item);
        if (!result) return result;
        item.ID = _db.CreateData(
            @"INSERT INTO Movie(
                Name,
                DirectorID,
                pegiAge,
                Description,
                Genre,
                DurationInMin
            )
            VALUES ($1,$2,$3,$4,$5,$6)",
            new Dictionary<string, dynamic?>(){
                {"$1", item.Name},
                {"$2", item.DirectorID},
                {"$3", item.PegiAge},
                {"$4", item.Description},
                {"$5", item.Genre},
                {"$6", item.DurationInMin}
            }
        );
        if (item.ID > 0) item.IsChanged = true;
        return item.ID > 0;
    }
    /// <summary>
    /// updates the movie
    /// </summary>
    /// <param name="item">the movie to update</param>
    /// <returns>true if succesfull, else false</returns>
    /// <exception cref="InvalidOperationException">if ID of the movie is null</exception>
    public bool UpdateItem(MovieModel item)
    {
        if (item.ID == null) throw new InvalidOperationException("cannot update a movie without an ID.");
        if (!item.IsChanged) return true;
        bool toReturn = RelatedItemsToDb(item)
            && _db.SaveData(
            @"UPDATE Movie
            SET Name = $1,
                DirectorID = $2,
                pegiAge = $3,
                Description = $4,
                Genre = $5,
                DurationInMin = $6
            WHERE ID = $7;",
            new Dictionary<string, dynamic?>(){
                {"$1", item.Name},
                {"$2", item.DirectorID},
                {"$3", (int)item.PegiAge},
                {"$4", item.Description},
                {"$5", item.Genre},
                {"$6", item.DurationInMin},
                {"$7", item.ID}
            }
        );
        if (toReturn) item.IsChanged = false;
        return toReturn;
    }
    private bool RelatedItemsToDb(MovieModel item)
    {
        if (item.Director != null)
        {
            _df.ItemToDb(item.Director);
            item.DirectorID = item.Director.ID;
        }
        foreach (ActorModel actor in item.Actors)
        {
            try
            {
                // need a check for already existing actors of the movie...
                _af.ItemToDb(actor);
                var x = _db.SaveData(
                    @"INSERT INTO ActorInMovie(
                        ActorID,
                        MovieID
                    )
                    VALUES($1,$2)",
                    new Dictionary<string, dynamic?>(){
                        {"$1", actor.ID},
                        {"$2", item.ID}
                    }
                );
            }
            catch (Exception ex)
            {// to be replaced with logger?
                Console.WriteLine($"Failed to add a actor to db. ex: {ex.Message}");
            }

        }
        return true;
    }
}