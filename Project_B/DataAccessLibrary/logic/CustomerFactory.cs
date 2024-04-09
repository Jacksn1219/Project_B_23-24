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
            if (item.Exists) throw new InvalidDataException("this Customer already exists in the db.");
            return _db.SaveData(
                @"INSERT INTO Customer(
                    Name,
                    Email,
                    Age
                )
                VALUES($1,$2,$3)",
                new Dictionary<string, dynamic?>(){
                    {"$1", item.Name},
                    {"$2", item.Email},
                    {"$3", item.Age}
                }
            );
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
            return _db.ReadData<CustomerModel>(
                @"SELECT * FROM Customer
                WHERE ID = $1",
                new Dictionary<string, dynamic?>(){
                    {"$1", id},
                }
            ).First();
        }

        public bool ItemToDb(CustomerModel item)
        {
            if (!item.Exists) return CreateItem(item);
            return UpdateItem(item);
        }

        public bool UpdateItem(CustomerModel item)
        {
            if (item.ID == null) throw new InvalidDataException("the ID of the Customer is null. the Customer cannot be updated.");
            return _db.SaveData(
                @"UPDATE Customer
                SET Name = $1,
                    Age = $2,
                    Email = $3
                WHERE ID = $4;",
                new Dictionary<string, dynamic?>(){
                    {"$1", item.Name},
                    {"$2", item.Age},
                    {"$3", item.Email}
                }
            );
        }
    }
}