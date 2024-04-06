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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public bool ItemToDb(RoomModel item)
        {
            throw new NotImplementedException();
        }

        public bool UpdateItem(RoomModel item)
        {
            throw new NotImplementedException();
        }
    }
}