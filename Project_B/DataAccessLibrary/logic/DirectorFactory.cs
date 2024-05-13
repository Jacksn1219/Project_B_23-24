using DataAccessLibrary;

public class DirectorFactory : IDbItemFactory<DirectorModel>
{
    private readonly DataAccess _db;
    public DirectorFactory(DataAccess db)
    {
        _db = db;
        CreateTable();
    }

    public bool CreateItem(DirectorModel item)
    {
        if (item.ID != null) throw new InvalidOperationException("this director already exists in the db");
        if (!item.IsChanged) return true;
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

    public void CreateTable()
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

    public DirectorModel GetItemFromId(int id, int deepcopyLv = 0)
    {
        try
        {
            return _db.ReadData<DirectorModel>(
            @"SELECT * FROM Director
            WHERE ID = $1",
            new Dictionary<string, dynamic?>(){
                {"$1", id}
            }).First();
        } catch (Exception) { return null; }
    }

    public DirectorModel[] GetItems(int count, int page = 1, int deepcopyLv = 0)
    {
        if (deepcopyLv < 0) return new DirectorModel[0];
        return _db.ReadData<DirectorModel>(
                $"SELECT * FROM Director LIMIT {count} OFFSET {count * page - count}"
            );
    }

    public bool ItemsToDb(List<DirectorModel> items)
    {
        foreach (var item in items)
        {
            ItemToDb(item);
        }
        return true;
    }

    public bool ItemToDb(DirectorModel item)
    {
        if (!item.IsChanged) return true;
        if (item.ID != null) return UpdateItem(item);
        return CreateItem(item);
    }

    public bool UpdateItem(DirectorModel item)
    {
        if (item.ID == null) throw new InvalidOperationException("cannot update a director without an ID.");
        if (!item.IsChanged) return true;
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
}