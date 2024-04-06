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
            throw new NotImplementedException();
        }

        public void CreateTable()
        {
            _db.SaveData(
                @"CREATE TABLE IF NOT EXISTS Seat(
                    ID INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL ,
                    RoomID INTEGER  NOT NULL,
                    Name TEXT,
                    Rank TEXT,
                    Type TEXT,
                    FOREIGN KEY (RoomID) REFERENCES Room (ID)
                )"
            );
        }

        public SeatModel GetItemFromId(int id)
        {
            throw new NotImplementedException();
        }

        public bool ItemToDb(SeatModel item)
        {
            throw new NotImplementedException();
        }

        public bool UpdateItem(SeatModel item)
        {
            throw new NotImplementedException();
        }
    }
}