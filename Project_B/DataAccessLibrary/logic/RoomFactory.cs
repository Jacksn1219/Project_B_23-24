using DataAccessLibrary;

namespace DataAccessLibrary.logic
{
    public class RoomFactory : IDbItemFactory<RoomModel>
    {
        private readonly SQliteDataAccess _db;
        private readonly SeatModelFactory _sf;

        public RoomFactory(SQliteDataAccess db, SeatModelFactory sf)
        {
            _db = db;
            _sf = sf;
            CreateTable();
        }

        public bool CreateItem(RoomModel item)
        {
            if (item.ID != null) throw new InvalidDataException("the room is already in the db.");
            if (!item.IsChanged) return true;
            item.ID = _db.CreateData(
                @"INSERT INTO Room(
                    Name,
                    Capacity,
                    RowWidth
                )
                VALUES ($1,$2,$3)",
                new Dictionary<string, dynamic?>(){
                    {"$1", item.Name},
                    {"$2", item.Capacity},
                    {"$3", item.RowWidth}
                }
            );
            bool result = RelatedItemsToDb(item);
            if (item.ID > 0) item.IsChanged = false;
            return item.ID > 0 && result;
        }

        public void CreateTable()
        {
            _db.SaveData(
                @"CREATE TABLE IF NOT EXISTS Room(
                    ID INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL ,
                    Name TEXT NOT NULL,
                    Capacity INTEGER NOT NULL,
                    RowWidth INTEGER NOT NULL
                )"
            );
        }

        public RoomModel GetItemFromId(int id, int deepcopyLv = 0)
        {
            try
            {
                return _db.ReadData<RoomModel>(
            @"SELECT * FROM Room
            WHERE ID = $1",
            new Dictionary<string, dynamic?>(){
                {"$1", id},
            }
            ).First();
            }
            catch { return null; }
        }

        public bool ItemToDb(RoomModel item)
        {
            bool SeatModelsChanged = false;
            foreach (var SeatModel in item.SeatModels)
            {
                if (SeatModel.IsChanged)
                {
                    SeatModelsChanged = true;
                    break;
                }
            }
            if (!item.IsChanged && SeatModelsChanged) return RelatedItemsToDb(item);
            if (!item.IsChanged) return true;
            if (item.ID == null) return CreateItem(item);
            return UpdateItem(item);
        }
        public bool UpdateItem(RoomModel item)
        {
            if (item.ID == null) throw new InvalidDataException("the id of the room is null. the item cannot be updated.");
            if (!item.IsChanged) return true;
            bool result = RelatedItemsToDb(item);
            if (!result) return result;
            result = _db.SaveData(
                @"UPDATE Room
                SET Name = $1,
                    Capacity = $2,
                    RowWidth = $3
                where ID = $4",
                new Dictionary<string, dynamic?>(){
                    {"$1", item.Name},
                    {"$2", item.Capacity},
                    {"$3", item.RowWidth},
                    {"$4", item.ID}
                }
            );
            if (result) item.IsChanged = false;
            return result;
        }

        private bool RelatedItemsToDb(RoomModel item)
        {
            foreach (SeatModel SeatModel in item.SeatModels)
            {
                if (!SeatModel.IsChanged) continue;
                SeatModel.RoomID = item.ID;
                _sf.ItemToDb(SeatModel);
            }
            return true;
        }
    }
}