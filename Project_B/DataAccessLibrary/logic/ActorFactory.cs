using DataAccessLibrary;

namespace DataAccessLibrary.logic
{
    public class ActorFactory : IDbItemFactory<ActorModel>
    {
        private readonly DataAccess _db;
        private readonly Serilog.Core.Logger _logger;
        public ActorFactory(DataAccess db, Serilog.Core.Logger logger)
        {
            _db = db;
            _logger = logger;
            CreateTable();
        }
        public bool CreateItem(ActorModel item, int deepcopyLv = 99)
        {
            if(deepcopyLv < 0) return true;
            if (item.Exists) throw new InvalidDataException("this Actor already exists in the db.");
            if (!item.IsChanged) return true;
            try
            {
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
            catch(Exception ex)
            {
                _logger.Warning(ex, "failed to create am actor");
                return false;
            }
        }

        public void CreateTable()
        {
            try
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
            catch(Exception ex)
            {
                _logger.Fatal(ex, "failed to create table actor");
                throw;
            }
        }

        public ActorModel? GetItemFromId(int id, int deepcopyLv = 0)
        {
            if(deepcopyLv < 0) return null;
            try
            {
                return _db.ReadData<ActorModel>(
                @"SELECT * FROM Actor
                WHERE ID = $1",
                new Dictionary<string, dynamic?>(){
                    {"$1", id}
                }
            ).First();
            }catch (Exception ex) 
            {
                _logger.Warning(ex, $"failed to get actor with ID {id}");
                return null; 
            }
        }

        public ActorModel[] GetItems(int count, int page = 1, int deepcopyLv = 0)
        {
            if (deepcopyLv < 0) return Array.Empty<ActorModel>();
            try
            {
                return _db.ReadData<ActorModel>(
                    $"SELECT * FROM Actor LIMIT {count} OFFSET {count * page - count}"
                );
            }
            catch (Exception ex)
            {
                _logger.Warning(ex, "failed to get actors");
                return Array.Empty<ActorModel>();
            }
        }

        public bool ItemsToDb(List<ActorModel> items, int deepcopyLv = 99)
        {
            if(deepcopyLv < 1) return true;
            foreach (var item in items)
            {
                ItemToDb(item, deepcopyLv);
            }
            return true;
        }

        public bool ItemToDb(ActorModel item, int deepcopyLv = 99)
        {
            if(deepcopyLv < 0) return true;
            if (!item.IsChanged) return true;
            if (!item.Exists) return CreateItem(item, deepcopyLv);
            return UpdateItem(item, deepcopyLv);
        }

        public bool UpdateItem(ActorModel item, int deepcopyLv = 99)
        {
            if(deepcopyLv < 0) return true;
            if (!item.Exists) throw new InvalidDataException("this Actor's ID is null. this actor cannot be updated.");
            if (!item.IsChanged) return true;
            try
            {
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
            catch (Exception ex)
            {
                _logger.Warning(ex, $"failed to add actor with ID {item.ID}");
                return false;
            }
        }
    }
}