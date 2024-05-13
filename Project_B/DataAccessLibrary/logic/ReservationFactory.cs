using DataAccessLibrary;

namespace DataAccessLibrary.logic
{
    public class ReservationFactory : IDbItemFactory<ReservationModel>
    {
        private readonly DataAccess _db;
        private readonly CustomerFactory _cf;
        private readonly SeatFactory _sf;
        private readonly TimeTableFactory _tf;
        public ReservationFactory(DataAccess db, CustomerFactory cf, SeatFactory sf, TimeTableFactory tf)
        {
            _db = db;
            _cf = cf;
            _sf = sf;
            _tf = tf;
            CreateTable();
        }

        public bool CreateItem(ReservationModel item)
        {
            if (item.ID != null) throw new InvalidDataException("the reservation is already in the db.");
            if (!item.IsChanged) return true;
            bool dontClose = _db.IsOpen;
            try
            {
                _db.OpenConnection();
                //add reservation
                bool result = RelatedItemsItemDependsOnToDb(item);
                if (!result) return false;
                item.ID = _db.CreateData(
                    @"INSERT INTO Reservation(
                        CustomerID,
                        TimeTableID,
                        Note
                    )
                    VALUES($1,$2,$3)",
                    new Dictionary<string, dynamic?>(){
                        {"$1", item.CustomerID },
                        {"$2", item.TimeTableID},
                        {"$3", item.Note}
                    }
                );
                result = RelatedItemsDependingOnItemToDb(item);
                return item.ID > 0 && result;
            }
            finally
            {
                if (!dontClose) _db.CloseConnection();
            }
        }

        public void CreateTable()
        {
            bool dontClose = _db.IsOpen;
            try
            {
                _db.OpenConnection();
                _db.SaveData(
                    @"CREATE TABLE IF NOT EXISTS Reservation(
                        ID INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL ,
                        CustomerID INTEGER NOT NULL,
                        TimeTableID INTEGER NOT NULL,
                        Note TEXT,
                        FOREIGN KEY (CustomerID) REFERENCES Customer (ID),
                        FOREIGN KEY (TimeTableID) REFERENCES TimeTable (ID)
                    )"
                );
                _db.SaveData(
                    @"CREATE TABLE IF NOT EXISTS ReservedSeat(
                        ID INTEGER PRIMARY KEY  AUTOINCREMENT  NOT NULL ,
                        SeatID INTEGER  NOT NULL,
                        ReservationID INTEGER  NOT NULL,
                        FOREIGN KEY (SeatID) REFERENCES Seat (ID),
                        FOREIGN KEY (ReservationID) REFERENCES Reservation (ID)
                    )"
                );
            }
            finally
            {
                if (!dontClose) _db.CloseConnection();
            }
        }

        public ReservationModel GetItemFromId(int id, int deepcopyLv = 0)
        {
            return _db.ReadData<ReservationModel>(
                @"SELECT * FROM Reservation
                WHERE ID = $1",
                new Dictionary<string, dynamic?>(){
                    {"$1", id},
                }
            ).First();
        }

        public bool ItemToDb(ReservationModel item)
        {
            bool customerChanged = item.Customer != null && item.Customer.IsChanged;
            bool SeatModelsChanged = false;
            foreach (var SeatModel in item.ReservedSeats)
            {
                if (SeatModel.IsChanged)
                {
                    SeatModelsChanged = true;
                    break;
                }
            }
            if (!item.IsChanged && item.Exists && (customerChanged || SeatModelsChanged))
            {
                bool result = RelatedItemsItemDependsOnToDb(item);
                if (!result) return result;
                return RelatedItemsDependingOnItemToDb(item);
            }

            if (!item.IsChanged) return true;
            if (item.ID == null) return CreateItem(item);
            else return UpdateItem(item);
        }

        public bool UpdateItem(ReservationModel item)
        {
            if (item.ID == null) throw new InvalidDataException("the Reservation does not have a value and cannot be updated.");
            if (!item.IsChanged) return true;
            bool dontClose = _db.IsOpen;
            try
            {
                _db.OpenConnection();
                bool result = RelatedItemsItemDependsOnToDb(item);
                if (!result) { return result; }
                result = _db.SaveData(
                    @"UPDATE Reservation
                    SET CustomerID = $1,
                        TimeTableID = $2,
                        Note = $3
                    WHERE ID = $4",
                    new Dictionary<string, dynamic?>(){
                        {"$1", item.CustomerID},
                        {"$2", item.TimeTableID},
                        {"$3", item.Note},
                        {"$4", item.ID}
                    }
                );
                if (result)
                {
                    item.IsChanged = false;
                    result = RelatedItemsDependingOnItemToDb(item);
                }
                return result;
            }
            finally
            {
                if (!dontClose) _db.CloseConnection();
            }
        }
        private bool RelatedItemsItemDependsOnToDb(ReservationModel item)
        {
            // update/add the customer
            if (item.Customer != null)
            {
                if (item.Customer.IsChanged) _cf.ItemToDb(item.Customer);
                item.CustomerID = item.Customer.ID;
            }
            return true;
        }
        private bool RelatedItemsDependingOnItemToDb(ReservationModel item)
        {
            foreach (SeatModel SeatModel in item.ReservedSeats)
            {
                if (!SeatModel.Exists) _sf.ItemToDb(SeatModel);
                _db.SaveData(
                    @"INSERT INTO ReservedSeatModel(
                        SeatModelID,
                        ReservationID
                    )
                    VALUES($1,$2)",
                    new Dictionary<string, dynamic?>(){
                        {"$1", SeatModel.ID},
                        {"$2", item.ID}
                    }
                );
            }
            return true;
        }

        public ReservationModel[] GetItems(int count, int page = 1, int deepcopyLv = 0)
        {
            if (deepcopyLv < 0) return new ReservationModel[0];
            bool dontClose = _db.IsOpen;
            try
            {
                _db.OpenConnection();
                ReservationModel[] result = _db.ReadData<ReservationModel>(
                    $"SELECT * FROM Reservation LIMIT {count} OFFSET {count * page - count}"
                );
                if (deepcopyLv < 1) return result;
                foreach (ReservationModel item in result)
                {
                    getRelatedItemsFromDb(item, deepcopyLv - 1);
                }
                return result;
            }
            finally
            {
                if (!dontClose) _db.CloseConnection();
            }
        }
        public void getRelatedItemsFromDb(ReservationModel item, int deepcopyLv = 0)
        {
            bool dontClose = _db.IsOpen;
            try
            {
                if (deepcopyLv < 0) return;
                _db.OpenConnection();
                if (item.TimeTableID != null)
                {
                    item.TimeTable = _tf.GetItemFromId(item.TimeTableID ?? 0, deepcopyLv);
                }
                item.ReservedSeats.AddRange(GetReservedSeatsFromDb(item));
                return;
            }
            finally
            {
                if (!dontClose) _db.CloseConnection();
            }
        }

        private SeatModel[] GetReservedSeatsFromDb(ReservationModel item)
        {
            return _db.ReadData<SeatModel>(
                @"SELECT Seat.ID, Seat.Name, Seat.Rank, Seat.Type FROM Seat
                INNER JOIN ReservedSeat ON ReservedSeat.SeatID = Seat.ID
                INNER JOIN Reservation ON Reservation.ID = ReservedSeat.ReservationID
                WHERE Reservation.ID = $1",
                new Dictionary<string, dynamic?>(){
                    {"$1", item.ID}
                }
            );
        }

        public bool ItemsToDb(List<ReservationModel> items)
        {
            foreach (var item in items)
            {
                ItemToDb(item);
            }
            return true;
        }
    }

}