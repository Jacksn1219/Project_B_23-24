using DataAccessLibrary;

namespace DataAccessLibrary.logic
{
    public class CustomerFactory : IDbItemFactory<CustomerModel>
    {
        private readonly DataAccess _db;
        public CustomerFactory(DataAccess db)
        {
            _db = db;
            CreateTable();
        }

        public bool CreateItem(CustomerModel item)
        {
            throw new NotImplementedException();
        }

        public void CreateTable()
        {
            _db.SaveData(
                @"CREATE TABLE IF NOT EXISTS Customer(
                    ID INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL ,
                    Name TEXT NOT NULL,
                    Age INTEGER NOT NULL,
                    Email TEXT UNIQUE NOT NULL,
                    PhoneNumber TEXT NOT NULL,
                    IsSubscribed INTEGER NOT NULL
                )"
            );
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