using DataAccessLibrary;

namespace DataAccessLibrary.logic
{
    public class SeatModelFactory : IDbItemFactory<SeatModel>
    {
        private readonly DataAccess _db;
        public SeatModelFactory(DataAccess db)
        {
            _db = db;
            CreateTable();
        }
        public bool CreateItem(SeatModel item)
        {
            if (item.ID != null) throw new InvalidDataException("the SeatModel is already in the db.");
            if (!item.IsChanged) return true;
            item.ID = _db.CreateData(
                @"INSERT INTO SeatModel(
                    Name,
                    Rank,
                    Type,
                    RoomID
                )
                VALUES($1,$2,$3,$4)",
                new Dictionary<string, dynamic?>(){
                    {"$1", item.Name},
                    {"$2", item.Rank},
                    {"$3", item.Type},
                    {"$4", item.RoomID}
                }
            );
            if (item.ID > 0) item.IsChanged = false;
            return item.ID > 0;
        }
        public bool CreateItem(SeatModel[] item)
        {
            foreach (SeatModel seat in item)
            {
                if ( !CreateItem(seat) ) return false;
            }
            return true;
        }

        public void CreateTable()
        {
            _db.SaveData(
                @"CREATE TABLE IF NOT EXISTS SeatModel(
                    ID INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL ,
                    RoomID INTEGER NOT NULL,
                    Name TEXT NOT NULL,
                    Rank TEXT,
                    Type TEXT,
                    FOREIGN KEY (RoomID) REFERENCES Room (ID)
                )"
            );
        }

        public SeatModel GetItemFromId(int id, int deepcopyLv = 0)
        {
            return _db.ReadData<SeatModel>(
                @"SELECT * FROM SeatModel
                WHERE ID = $1",
                new Dictionary<string, dynamic?>(){
                    {"$1", id},
                }
            ).First();
        }

        public bool ItemToDb(SeatModel item)
        {
            if (!item.IsChanged) return true;
            if (item.ID == null) return CreateItem(item);
            return UpdateItem(item);
        }

        public bool UpdateItem(SeatModel item)
        {
            if (item.ID == null) throw new InvalidDataException("the SeatModel has no ID therefore it cannot be updated.");
            if (!item.IsChanged) return true;
            bool toReturn = _db.SaveData(
                @"UPDATE SeatModel
                SET RoomID = $1,
                    Name = $2,
                    Type = $3,
                    Rank = $4
                WHERE ID = $5",
                new Dictionary<string, dynamic?>(){
                    {"$1", item.RoomID },
                    {"$2", item.Name },
                    {"$3", item.Type },
                    {"$4", item.Rank },
                    {"$5", item.ID }
                }
            );
            item.IsChanged = !toReturn;
            return toReturn;
        }
    }
}