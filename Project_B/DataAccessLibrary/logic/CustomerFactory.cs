using DataAccessLibrary;

namespace DataAccessLibrary.logic
{
    public class CustomerFactory : IDbItemFactory<CustomerModel>
    {
        private readonly DataAccess _db;
        public CustomerFactory(DataAccess db)
        {
            _db = db;
        }

        public bool CreateItem(CustomerModel item)
        {
            throw new NotImplementedException();
        }

        public void CreateTable()
        {
            throw new NotImplementedException();
        }

        public CustomerModel GetItemFromId(int id)
        {
            throw new NotImplementedException();
        }

        public bool ItemToDb(CustomerModel item)
        {
            throw new NotImplementedException();
        }

        public bool UpdateItem(CustomerModel item)
        {
            throw new NotImplementedException();
        }
    }
}