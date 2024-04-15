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
            VALUES ($1,$2,$3);",
            new Dictionary<string, dynamic?>(){
                {"$1", item.Name},
                {"$2", item.Age},
                {"$3", item.Description}
            }
        );
        return item.ID > 0;
    }

    public void CreateTable()
    {
        _db.SaveData(
            @"CREATE TABLE IF NOT EXSISTS Director(
                ID INTEGER PRIMARY KEY AUTOINCREMENT UNIQUE NOT NULL,
                Name TEXT NOT NULL,
                Description TEXT NOT NULL,
                Age INTEGER NOT NULL
            )"
        );
    }

    public DirectorModel GetItemFromId(int id)
    {
        return _db.ReadData<DirectorModel>(
            @"SELECT * FROM Director
            WHERE ID = $1",
            new Dictionary<string, dynamic?>(){
                {"$1", id}
            }).First();
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
        return _db.SaveData(
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
    }
}