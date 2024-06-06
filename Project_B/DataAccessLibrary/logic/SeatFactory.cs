using DataAccessLibrary;

namespace DataAccessLibrary.logic
{
    public class SeatFactory : IDbItemFactory<SeatModel>
    {
        private readonly DataAccess _db;
        private readonly Serilog.Core.Logger _logger;
        public SeatFactory(DataAccess db, Serilog.Core.Logger logger)
        {
            _db = db;
            _logger = logger;
            CreateTable();
        }
        public bool CreateItem(SeatModel item, int deepcopyLv = 99)
        {
            if(deepcopyLv < 0) return true;
            if (item.ID != null) throw new InvalidDataException("the SeatModel is already in the db.");
            try
            {
                if (!item.IsChanged) return true;
                item.ID = _db.CreateData(
                    @"INSERT INTO Seat(
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
            catch (Exception ex)
            {
                _logger.Warning(ex, "failed to create a seat");
                return false;
            }
        }
        public bool CreateItems(SeatModel[] item, int deepcopyLv = 99)
        {
            if(deepcopyLv < 0) return true;
            foreach (SeatModel seat in item)
            {
                if (!CreateItem(seat, deepcopyLv)) return false;
            }
            return true;
        }

        public void CreateTable()
        {
            try
            {
                _db.SaveData(
                    @"CREATE TABLE IF NOT EXISTS Seat(
                        ID INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL ,
                        RoomID INTEGER NOT NULL,
                        Name TEXT NOT NULL,
                        Rank TEXT,
                        Type TEXT,
                        FOREIGN KEY (RoomID) REFERENCES Room (ID)
                    )"
                );
            }
            catch (Exception ex)
            {
                _logger.Fatal(ex, "failed to create table seat");
                throw;
            }
        }

        public SeatModel? GetItemFromId(int id, int deepcopyLv = 0)
        {
            if(deepcopyLv < 0) return null;
            try
            {
                return _db.ReadData<SeatModel>(
                    @"SELECT * FROM Seat
                    WHERE ID = $1",
                    new Dictionary<string, dynamic?>(){
                        {"$1", id},
                    }
                ).First();
            }
            catch(Exception ex)
            {
                _logger.Warning(ex, $"failed to get seat with ID {id}");
                return null;
            }
        }

        public SeatModel[] GetItems(int count, int page = 1, int deepcopyLv = 0)
        {
            if (deepcopyLv < 0) return Array.Empty<SeatModel>();
            try
            {
                return _db.ReadData<SeatModel>(
                    $"SELECT * FROM Seat LIMIT {count} OFFSET {count * page - count}"
                );
            }
            catch(Exception ex){
                _logger.Warning(ex, "failed to get seats");
                return Array.Empty<SeatModel>();
            }
        }

        public bool ItemsToDb(List<SeatModel> items, int deepcopyLv = 99)
        {
            if(deepcopyLv < 0) return true;
            foreach (var item in items)
            {
                ItemToDb(item, deepcopyLv);
            }
            return true;
        }

        public bool ItemToDb(SeatModel item, int deepcopyLv = 99)
        {
            if(deepcopyLv < 0) return true;
            if (!item.IsChanged) return true;
            if (item.ID == null) return CreateItem(item, deepcopyLv);
            return UpdateItem(item, deepcopyLv);
        }

        public bool UpdateItem(SeatModel item, int deepcopyLv = 99)
        {
            if(deepcopyLv < 0) return true;
            if (item.ID == null) throw new InvalidDataException("the SeatModel has no ID therefore it cannot be updated.");
            if (!item.IsChanged) return true;
            try
            {
                bool toReturn = _db.SaveData(
                    @"UPDATE Seat
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
            catch(Exception ex)
            {
                _logger.Warning(ex, $"failed to update seat with ID {item.ID}");
                return false;
            }
        }
    }
}