using DataAccessLibrary;


namespace DataAccessLibrary.logic
{
    public class RoomFactory : IDbItemFactory<RoomModel>
    {
        private readonly DataAccess _db;
        private readonly SeatFactory _sf;
        private readonly Serilog.Core.Logger _logger;

        public RoomFactory(DataAccess db, SeatFactory sf, Serilog.Core.Logger logger)
        {
            _db = db;
            _sf = sf;
            _logger = logger;
            CreateTable();
        }

        public bool CreateItem(RoomModel item, int deepcopyLv = 99)
        {
            if(deepcopyLv < 0) return true;
            if (item.ID != null) throw new InvalidDataException("the room is already in the db.");
            if (!item.IsChanged) return true;
            bool dontClose = _db.IsOpen;
            try
            {
                _db.OpenConnection();
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
                bool result = RelatedItemsToDb(item, deepcopyLv - 1);
                if (item.ID > 0) item.IsChanged = false;
                return item.ID > 0 && result;
            }
            catch(Exception ex)
            {
                _logger.Warning(ex, "failed to create a Room");
                return false;
            }
            finally
            {
                if (!dontClose) _db.CloseConnection();
            }
        }

        public void CreateTable()
        {
            try
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
            catch (Exception ex){
                _logger.Fatal(ex, "failed to add room to database");
                throw;
            }
        }

        public RoomModel? GetItemFromId(int id, int deepcopyLv = 0)
        {
            if(deepcopyLv < 0) return null;
            try
            {
                RoomModel toReturn = _db.ReadData<RoomModel>(
                    @"SELECT * FROM Room
                    WHERE ID = $1",
                    new Dictionary<string, dynamic?>(){
                        {"$1", id},
                    }
                ).First();
                getRelatedItemsFromDb(toReturn, deepcopyLv - 1);
                return toReturn;
            }
            catch (Exception ex) 
            { 
                _logger.Warning(ex, $"failed to get Room with ID {id}");
                return null; 
            }
        }

        public RoomModel[] GetItems(int count, int page = 1, int deepcopyLv = 0)
        {
            if (deepcopyLv < 0) return Array.Empty<RoomModel>();
            bool dontClose = _db.IsOpen;
            try
            {
                _db.OpenConnection();
                RoomModel[] rooms = _db.ReadData<RoomModel>(
                    $"SELECT * FROM Room LIMIT {count} OFFSET {count * page - count}"
                );
                if (deepcopyLv == 0) return rooms;
                foreach (RoomModel room in rooms)
                {
                    getRelatedItemsFromDb(room, deepcopyLv - 1);
                }
                return rooms;
            }
            catch (Exception ex){
                _logger.Warning(ex, "failed to get rooms");
                return Array.Empty<RoomModel>();
            }
            finally
            {
                if (!dontClose) _db.CloseConnection();
            }
        }

        public bool ItemToDb(RoomModel item, int deepcopyLv = 99)
        {
            if(deepcopyLv < 0) return true;
            bool SeatModelsChanged = false;
            foreach (var SeatModel in item.Seats)
            {
                if (SeatModel.IsChanged)
                {
                    SeatModelsChanged = true;
                    break;
                }
            }
            if (!item.IsChanged && SeatModelsChanged) return RelatedItemsToDb(item, deepcopyLv - 1);
            if (!item.IsChanged) return true;
            if (item.ID == null) return CreateItem(item, deepcopyLv);
            return UpdateItem(item, deepcopyLv);
        }
        public bool UpdateItem(RoomModel item, int deepcopyLv = 99)
        {
            if (deepcopyLv < 0) return true;
            if (item.ID == null) throw new InvalidDataException("the id of the room is null. the item cannot be updated.");
            if (!item.IsChanged) return true;
            bool dontClose = _db.IsOpen;
            try
            {
                _db.OpenConnection();
                bool result = RelatedItemsToDb(item, deepcopyLv - 1);
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
            catch(Exception ex){
                _logger.Warning(ex, $"failed to updat Room with ID {item.ID}");
                return false;
            }
            finally
            {
                if (!dontClose) _db.CloseConnection();
            }
        }

        public bool RelatedItemsToDb(RoomModel item, int deepcopyLv)
        {
            if(deepcopyLv < 0) return true;
            bool dontClose = _db.IsOpen;
            try
            {
                _db.OpenConnection();
                foreach (SeatModel seat in item.Seats)
                {
                    if (!seat.IsChanged) continue;
                    seat.RoomID = item.ID;
                    _sf.ItemToDb(seat, deepcopyLv);
                }
                return true;
            }
            catch(Exception ex){
                _logger.Warning(ex, $"failed to add seats of room with ID {item.ID}");
                return false;
            }
            finally
            {
                if (!dontClose) _db.CloseConnection();
            }
        }
        public void getRelatedItemsFromDb(RoomModel item, int deepcopyLv = 0)
        {
            try{
                if (deepcopyLv < 0) return;
                item.AddSeatModels(
                    _db.ReadData<SeatModel>(
                        $"SELECT * FROM Seat WHERE Seat.RoomID = {item.ID}"
                    )
                );
                item.IsChanged = false;
            }
            catch(Exception ex)
            {
                _logger.Warning(ex, $"failed to get seats of Room with ID {item.ID}");
            }
        }

        public bool ItemsToDb(List<RoomModel> items, int deepcopyLv = 99)
        {
            if(deepcopyLv < 0) return true;
            foreach (var item in items)
            {
                ItemToDb(item, deepcopyLv);
            }
            return true;
        }
    }
}