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
        }

        public bool CreateItem(RoomModel item)
        {
            throw new NotImplementedException();
        }

        public void CreateTable()
        {
            throw new NotImplementedException();
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