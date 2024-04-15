using DataAccessLibrary;

namespace DataAccessLibrary.logic
{
    public class SeatFactory : IDbItemFactory<SeatModel>
    {
        private readonly DataAccess _db;
        public SeatFactory(DataAccess db)
        {
            _db = db;
            CreateTable();
        }
        public bool CreateItem(SeatModel item)
        {
            if (item.ID != null) throw new InvalidDataException("the seat is already in the db.");
            if (!item.IsChanged) return true;
            item.ID = _db.CreateData(
                @"INSERT INTO Seat(
                    Name,
                    Rank,
                    Type,
                    RoomID
                )
                VALUES($1,$2,$3)",
                new Dictionary<string, dynamic?>(){
                    {"$1", item.Name},
                    {"$2", item.Rank},
                    {"$3", item.Type},
                    {"$4", item.RoomID}
                }
            );
            return item.ID > 0;
        }

        public void CreateTable()
        {
            _db.SaveData(
                @"CREATE TABLE IF NOT EXISTS Seat(
                    ID INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL ,
                    RoomID INTEGER NOT NULL,
                    Name TEXT,
                    Rank TEXT,
                    Type TEXT,
                    FOREIGN KEY (RoomID) REFERENCES Room (ID)
                )"
            );
        }

        public SeatModel GetItemFromId(int id)
        {
            return _db.ReadData<SeatModel>(
                @"SELECT * FROM Seat
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
            if (item.ID == null) throw new InvalidDataException("the seat has no ID therefore it cannot be updated.");
            if (!item.IsChanged) return true;
            return _db.SaveData(
                @"UPDATE Seat
                SET RoomID = $1
                    Name = $2
                    Type = $3
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
        }
    }
}