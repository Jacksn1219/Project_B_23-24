using DataAccessLibrary;

namespace DataAccessLibrary.logic
{
    public class ActorFactory : IDbItemFactory<ActorModel>
    {
        private readonly DataAccess _db;
        public ActorFactory(DataAccess db)
        {
            _db = db;
            CreateTable();
        }
        public bool CreateItem(ActorModel item)
        {
            if (item.Exists) throw new InvalidDataException("this Actor already exists in the db.");
            if (!item.IsChanged) return true;
            item.ID = _db.CreateData(
                @"INSERT INTO Actor(
                    Name,
                    Description,
                    Age
                ) 
                VALUES ($1,$2,$3)",
                new Dictionary<string, dynamic?>(){
                    {"$1", item.Name},
                    {"$2", item.Description},
                    {"$3", item.Age}
                }
            );
            if (item.ID > 0) item.IsChanged = false;
            return item.ID > 0;
        }

        public void CreateTable()
        {
            _db.SaveData(
                @"CREATE TABLE IF NOT EXISTS Actor(
                    ID INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
                    Name TEXT NOT NULL,
                    Description TEXT,
                    Age INTEGER NOT NULL
                )"
            );
        }

        public ActorModel GetItemFromId(int id, int deepcopyLv = 0)
        {
            return _db.ReadData<ActorModel>(
                @"SELECT * FROM Actor
                WHERE ID = $1",
                new Dictionary<string, dynamic?>(){
                    {"$1", id}
                }
            ).First();
        }

        public bool ItemToDb(ActorModel item)
        {
            if (!item.IsChanged) return true;
            if (!item.Exists) return CreateItem(item);
            return UpdateItem(item);
        }

        public bool UpdateItem(ActorModel item)
        {
            if (!item.Exists) throw new InvalidDataException("this Actor's ID is null. this actor cannot be updated.");
            if (!item.IsChanged) return true;
            bool toReturn = _db.SaveData(
                @"UPDATE Actor
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
}