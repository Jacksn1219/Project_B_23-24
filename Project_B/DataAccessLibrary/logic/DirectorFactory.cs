using DataAccessLibrary;

public class DirectorFactory : IDbItemFactory<DirectorModel>
{
    private readonly DataAccess _db;
    private readonly Serilog.Core.Logger _logger;
    public DirectorFactory(DataAccess db, Serilog.Core.Logger logger)
    {
        _db = db;
        _logger = logger;
        CreateTable();
    }

    public bool CreateItem(DirectorModel item, int deepcopyLv = 99)
    {
        if (deepcopyLv < 0) return true;
        if (item.ID != null) throw new InvalidOperationException("this director already exists in the db");
        if (!item.IsChanged) return true;
        try
        {
            item.ID = _db.CreateData(
                @"INSERT INTO Director(
                    Name,
                    Age,
                    Description
                )
                VALUES ($1,$2,$3)",
                new Dictionary<string, dynamic?>(){
                    {"$1", item.Name},
                    {"$2", item.Age},
                    {"$3", item.Description}
                }
            );
            if (item.ID > 0) item.IsChanged = false;
            return item.ID > 0;
        }
        catch(Exception ex)
        {
            _logger.Warning(ex, "failed to create a director");
            return false;
        }
    }

    public void CreateTable()
    {
       try
       {
            _db.SaveData(
                @"CREATE TABLE IF NOT EXISTS Director(
                    ID INTEGER PRIMARY KEY AUTOINCREMENT UNIQUE NOT NULL,
                    Name TEXT NOT NULL,
                    Description TEXT NOT NULL,
                    Age INTEGER NOT NULL
                )"
            );
       }
       catch(Exception ex){
        _logger.Fatal(ex, "failed to create table Director");
        throw;
       }
    }

    public DirectorModel? GetItemFromId(int id, int deepcopyLv = 0)
    {
        if(deepcopyLv < 0) return null;
        try
        {
            return _db.ReadData<DirectorModel>(
            @"SELECT * FROM Director
            WHERE ID = $1",
            new Dictionary<string, dynamic?>(){
                {"$1", id}
            }).First();
        } catch (Exception ex) 
        {
            _logger.Warning(ex, $"failed to get Director with ID {id}");
            return null;
        }
    }

    public DirectorModel[] GetItems(int count, int page = 1, int deepcopyLv = 0)
    {
        if (deepcopyLv < 0) return Array.Empty<DirectorModel>();
        try
        {
            return _db.ReadData<DirectorModel>(
                $"SELECT * FROM Director LIMIT {count} OFFSET {count * page - count}"
            );
        }
        catch(Exception ex)
        {
            _logger.Warning(ex, "failed to get directors");
            return Array.Empty<DirectorModel>();
        }
    }

    public bool ItemsToDb(List<DirectorModel> items, int deepcopyLv = 99)
    {
        if(deepcopyLv < 0) return true;
        foreach (var item in items)
        {
            ItemToDb(item, deepcopyLv);
        }
        return true;
    }

    public bool ItemToDb(DirectorModel item, int deepcopyLv = 99)
    {
        if (deepcopyLv < 0) return true;
        if (!item.IsChanged) return true;
        if (item.ID != null) return UpdateItem(item);
        return CreateItem(item);
    }

    public bool UpdateItem(DirectorModel item, int deepcopyLv = 99)
    {
        if (deepcopyLv >= 0)
        {
            if (item.ID == null) throw new InvalidOperationException("cannot update a director without an ID.");
            if (!item.IsChanged) return true;
           try
           {
            bool toReturn = _db.SaveData(
                @"UPDATE Director
                SET Name = $1,
                    Age = $2,
                    Description = $3
                WHERE ID = $4;",
                new Dictionary<string, dynamic?>(){
                    {"$1", item.Name},
                    {"$2", item.Age},
                    {"$3", item.Description},
                    {"$4", item.ID}
                }
            );
            if (toReturn) item.IsChanged = false;
            return toReturn;
           }
           catch(Exception ex)
           {
                _logger.Warning(ex, $"failed to update director with ID {item.ID}");
                return false;
           }
        }

        return true;
    }
}