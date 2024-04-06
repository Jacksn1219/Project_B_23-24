using DataAccessLibrary;

namespace DataAccessLibrary.logic
{
    public class SeatFactory : IDbItemFactory<SeatModel>
    {
        private readonly DataAccess _db;
        public SeatFactory(DataAccess db)
        {
            _db = db;
        }
        public bool CreateItem(SeatModel item)
        {
            throw new NotImplementedException();
        }

        public void CreateTable()
        {
            throw new NotImplementedException();
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