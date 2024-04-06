using DataAccessLibrary;

namespace DataAccessLibrary.logic
{
    public class ReservationFactory : IDbItemFactory<ReservationModel>
    {
        private readonly DataAccess _db;
        private readonly CustomerFactory _cf;
        private readonly SeatFactory _sf; // do not know if this factory is needed
        public ReservationFactory(DataAccess db, CustomerFactory cf, SeatFactory sf)
        {
            _db = db;
            _cf = cf;
            _sf = sf;
            CreateTable();
        }

        public bool CreateItem(ReservationModel item)
        {
            throw new NotImplementedException();
        }

        public void CreateTable()
        {
            _db.SaveData(
                @"CREATE TABLE IF NOT EXISTS Reservation(
                    ID INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL ,
                    CostumerID INTEGER NOT NULL,
                    TimeTableID INTEGER NOT NULL,
                    Note TEXT,
                    FOREIGN KEY (CostumerID) REFERENCES Costumer (ID),
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
            throw new NotImplementedException();
        }

        public bool ItemToDb(ReservationModel item)
        {
            throw new NotImplementedException();
        }

        public bool UpdateItem(ReservationModel item)
        {
            throw new NotImplementedException();
        }
    }
}