using DataAccessLibrary;

namespace DataAccessLibrary.logic
{
    public class ReservationFactory : IDbItemFactory<ReservationModel>
    {
        private readonly DataAccess _db;
        private readonly CustomerFactory _cf;
        private readonly SeatFactory _sf;
        public ReservationFactory(DataAccess db, CustomerFactory cf, SeatFactory sf)
        {
            _db = db;
            _cf = cf;
            _sf = sf;
            CreateTable();
        }

        public bool CreateItem(ReservationModel item)
        {
            if (item.ID != null) throw new InvalidDataException("the reservation is already in the db.");
            if (!item.IsChanged) return true;
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

        public void CreateTable()
        {
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

        public ReservationModel GetItemFromId(int id)
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
            bool seatsChanged = false;
            foreach (var seat in item.ReservedSeats)
            {
                if (seat.IsChanged)
                {
                    seatsChanged = true;
                    break;
                }
            }
            if (!item.IsChanged && item.Exists && (customerChanged || seatsChanged))
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
            foreach (SeatModel seat in item.ReservedSeats)
            {
                if (!seat.Exists) _sf.ItemToDb(seat);
                _db.SaveData(
                    @"INSERT INTO ReservedSeat(
                        SeatID,
                        ReservationID
                    )
                    VALUES($1,$2)",
                    new Dictionary<string, dynamic?>(){
                        {"$1", seat.ID},
                        {"$2", item.ID}
                    }
                );
            }
            return true;
        }
    }

}