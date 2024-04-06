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
        }

        public bool CreateItem(ReservationModel item)
        {
            throw new NotImplementedException();
        }

        public void CreateTable()
        {
            throw new NotImplementedException();
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