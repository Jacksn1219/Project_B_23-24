using System.CodeDom;
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
    private readonly Serilog.Core.Logger _logger;
    /// <summary>
    /// the constructor for the movie factory
    /// </summary>
    /// <param name="db">the database connection</param>
    public MovieFactory(DataAccess db, DirectorFactory df, ActorFactory af, Serilog.Core.Logger logger)
    {
        _db = db;
        _df = df;
        _af = af;
        _logger = logger;
        CreateTable();
    }
    /// <summary>
    /// creates the movie table 
    /// </summary>
    public void CreateTable()
    {
        bool dontClose = _db.IsOpen;
        try
        {

            _db.OpenConnection();
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
        catch(Exception ex){
            _logger.Fatal(ex, "failed to create table Movie and/or ActorInMovie");
            throw;
        }
        finally
        {
            if (!dontClose) _db.CloseConnection();
        }
    }
    /// <summary>
    /// Get a MovieModel from the ID
    /// </summary>
    /// <param name="id">the ID of the Movie</param>
    /// <returns>the first movie returned from the query</returns>
    public MovieModel? GetItemFromId(int id, int deepcopyLv = 0)
    {
        if(deepcopyLv < 0) return null;
        try
        {
            
            var toReturn = _db.ReadData<MovieModel>(
            @"SELECT * FROM Movie
            WHERE ID=$1",
            new Dictionary<string, dynamic?>()
            {
                {"$1", id}
            }
            ).First();
            getRelatedItemsFromDb(toReturn, deepcopyLv - 1);
            return toReturn;
        }
        catch (Exception ex)
        {
            _logger.Warning(ex, $"failed to get Movie with ID {id}");
            return null;
        }
    }

    /// <summary>
    /// updates or creates the movie in the db
    /// </summary>
    /// <param name="item">the movie to update or create</param>
    /// <returns>true if succesfull, else false</returns>
    public bool ItemToDb(MovieModel item,int deepcopyLv = 99 )
    {
        if(deepcopyLv < 0) return true;
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
        if (!item.IsChanged && (actorsChanged || directorchanged) && deepcopyLv > 0) return RelatedItemsToDb(item, deepcopyLv - 1);
        if (!item.IsChanged) return true;
        if (item.ID == null) return CreateItem(item, deepcopyLv);
        return UpdateItem(item, deepcopyLv);

    }
    /// <summary>
    /// creates a new movie in the db
    /// </summary>
    /// <param name="item">the movie to create</param>
    /// <returns>true if successfull, else false</returns>
    /// <exception cref="InvalidOperationException">when ID is not null</exception>
    public bool CreateItem(MovieModel item, int deepcopyLv = 99)
    {
        if (deepcopyLv < 0) return true;
        bool dontClose = _db.IsOpen;
        try
        {
            if (item.ID != null) throw new InvalidOperationException("this movie already exists in the db");
            if (!item.IsChanged) return true;
            if(!DependingItemsToDb(item, deepcopyLv - 1)) return false;
            _db.OpenConnection();
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
            bool result = RelatedItemsToDb(item, deepcopyLv - 1);
            if (!result) return result;
            if (item.ID > 0) item.IsChanged = false;
            return item.ID > 0;
        }
        catch (Exception ex){
            _logger.Warning(ex, "failed to create a movie");
            return false;
        }
        finally
        {
            if (!dontClose) _db.CloseConnection();
        }
    }
    /// <summary>
    /// updates the movie
    /// </summary>
    /// <param name="item">the movie to update</param>
    /// <returns>true if succesfull, else false</returns>
    /// <exception cref="InvalidOperationException">if ID of the movie is null</exception>
    public bool UpdateItem(MovieModel item, int deepcopyLv = 99)
    {
        if(deepcopyLv < 0) return true;
        bool dontClose = _db.IsOpen;
        try
        {
            if (item.ID == null) throw new InvalidOperationException("cannot update a movie without an ID.");
            if (!item.IsChanged) return true;
            _db.OpenConnection();
            if (!DependingItemsToDb(item, deepcopyLv - 1)) return false;
            bool toReturn = _db.SaveData(
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
            ) && RelatedItemsToDb(item, deepcopyLv - 1);
            if (toReturn) item.IsChanged = false;
            return toReturn;
        }
        catch(Exception ex)
        {
            _logger.Warning(ex, $"failed to update movie with ID {item.ID}");
            return false;
        }
        finally
        {
            if (!dontClose) _db.CloseConnection();
        }
    }
    private bool DependingItemsToDb(MovieModel item, int deepcopyLv){
        if (item.DirectorID == null) return true;
        if (item.Director != null)
        {
            if(!_df.ItemToDb(item.Director, deepcopyLv)) return false;
            item.DirectorID = item.Director.ID;
        }
        return true;
    }
    private bool RelatedItemsToDb(MovieModel item, int deepcopyLv)
    {
        if(deepcopyLv < 0) return true;
        bool dontClose = _db.IsOpen;
        try
        {
            foreach (ActorModel actor in item.Actors)
            {
                try
                {
                    //try add actor
                    _af.ItemToDb(actor, deepcopyLv);
                    //check if actorInMovie already exists
                    if (_db.ReadData<ActorModel>
                    (
                        @"SELECT ID FROM ActorInMovie
                        WHERE MovieID = $1, ActorID = $2",
                        new(){
                            {"$1", item.ID},
                            {"$2", actor.ID}
                        }
                    ).Length > 0) continue;
                    //add actorinmovie
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
        catch (Exception ex)
        {
            _logger.Warning(ex, $"failed to add Actors and/or ActorsInMovies form movie with ID {item.ID}");
            return false;
        }
        finally
        {
            if (!dontClose) _db.CloseConnection();
        }
    }

    public MovieModel[] GetItems(int count, int page = 1, int deepcopyLv = 0)
    {
        bool dontClose = _db.IsOpen;
        try
        {
            if (deepcopyLv < 0) return Array.Empty<MovieModel>();
            _db.OpenConnection();
            var movies = _db.ReadData<MovieModel>(
                    $"SELECT * FROM Movie LIMIT {count} OFFSET {count * page - count}"
                );
            //return only movies when deepcopyLv is less than 1.
            if (deepcopyLv < 1) return movies;
            foreach (MovieModel mov in movies)
            {
                getRelatedItemsFromDb(mov, deepcopyLv - 1);
            }
            return movies;
        }
        catch (Exception ex)
        {
            _logger.Warning(ex, "failed to get movies");
            return Array.Empty<MovieModel>();
        }
        finally
        {
            if (!dontClose) _db.CloseConnection();
        }
    }
    public void getRelatedItemsFromDb(MovieModel item, int deepcopyLv)
    {
        bool dontClose = _db.IsOpen;
        try
        {
            _db.OpenConnection();
            if (deepcopyLv < 0) return;
            if (item.DirectorID != null)
            {
                item.Director = _df.GetItemFromId(item.DirectorID ?? 0);
            }
            item.Actors = GetActorsFromMovie(item).ToList();
            return;
        }
        catch (Exception ex)
        {
            _logger.Warning(ex, $"failed to get related items of movie with ID {item.ID}");
        }
        finally
        {
            if (!dontClose) _db.CloseConnection();
        }
    }
    private ActorModel[] GetActorsFromMovie(MovieModel movie)
    {
        try
        {
            return _db.ReadData<ActorModel>(
                @"SELECT Actor.ID, Actor.Name, Actor.Description, Actor.Age FROM Actor
                INNER JOIN ActorInMovie ON Actor.ID = ActorInMovie.ActorID
                INNER JOIN Movie ON ActorInMovie.MovieID = Movie.ID
                WHERE Movie.ID = $1",
                new Dictionary<string, dynamic?>{
                    { "$1", movie.ID }
                }
            );
        }
        catch(Exception ex)
        {
            _logger.Warning(ex, $"Failed to get actors of Movie with ID {movie.ID}");
            return Array.Empty<ActorModel>();
        }
    }

    public bool ItemsToDb(List<MovieModel> items, int deepcopyLv = 99)
    {
        if (deepcopyLv < 0) return true;
        foreach (var item in items)
        {
            ItemToDb(item, deepcopyLv);
        }
        return true;
    }
}