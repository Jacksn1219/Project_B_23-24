using DataAccessLibrary;

namespace DataAccessLibrary.logic
{
    public class RoomFactory : IDbItemFactory<RoomModel>
    {
        private readonly DataAccess _db;
        private readonly SeatFactory _sf;

        public RoomFactory(DataAccess db, SeatFactory sf)
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
                    Capacity
                )
                VALUES ($1,$2);",
                new Dictionary<string, dynamic?>(){
                    {"$1", item.Name},
                    {"$2", item.Capacity}
                }
            );
            return item.ID > 0 && RelatedItemsToDb(item);
        }

        public void CreateTable()
        {
            _db.SaveData(
                @"CREATE TABLE IF NOT EXISTS Room(
                    ID INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL ,
                    Name TEXT NOT NULL,
                    Capacity INTEGER NOT NULL
                )"
            );
        }

        public RoomModel GetItemFromId(int id)
        {
            return _db.ReadData<RoomModel>(
                @"SELECT * FROM Room
                WHERE ID = $1",
                new Dictionary<string, dynamic?>(){
                    {"$1", id},
                }
            ).First();
        }

        public bool ItemToDb(RoomModel item)
        {
            if (!item.IsChanged) return true;
            if (item.ID == null) return CreateItem(item);
            return UpdateItem(item);
        }

        public bool UpdateItem(RoomModel item)
        {
            if (item.ID == null) throw new InvalidDataException("the id of the room is null. the item cannot be updated.");
            if (!item.IsChanged) return true;
            return _db.SaveData(
                @"UPDATE Room
                SET Name = $1
                    Capacity = $2
                where ID = $3",
                new Dictionary<string, dynamic?>(){
                    {"$1", item.Name},
                    {"$2", item.Capacity},
                    {"$3", item.ID}
                }
            ) && RelatedItemsToDb(item);
        }

        private bool RelatedItemsToDb(RoomModel item)
        {
            foreach (SeatModel seat in item.Seats)
            {
                _sf.ItemToDb(seat);
            }
            return true;
        }
    }
}