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
            if (!item.IsChanged) return true;
            item.ID = _db.CreateData(
                @"INSERT INTO Customer(
                    Name,
                    Email,
                    Age,
                    PhoneNumber,
                    IsSubscribed
                )
                VALUES($1,$2,$3,$4,$5)",
                new Dictionary<string, dynamic?>(){
                    {"$1", item.Name},
                    {"$2", item.Email},
                    {"$3", item.Age},
                    {"$4", item.PhoneNumber},
                    {"$5", item.IsSubscribed}
                }
            );
            if (item.ID > 0) item.IsChanged = false;
            return item.ID > 0;
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

        public CustomerModel GetItemFromId(int id, int deepcopyLv = 0)
        {
            return _db.ReadData<CustomerModel>(
                @"SELECT * FROM Customer
                WHERE ID = $1",
                new Dictionary<string, dynamic?>(){
                    {"$1", id},
                }
            ).First();
        }

        public CustomerModel[] GetItems(int count, int page = 1, int deepcopyLv = 0)
        {
            if (deepcopyLv < 0) return new CustomerModel[0];
            return _db.ReadData<CustomerModel>(
                $"SELECT * FROM Customer LIMIT {count} OFFSET {count * page - count}"
            );
        }

        public bool ItemsToDb(List<CustomerModel> items)
        {
            foreach (var item in items)
            {
                ItemToDb(item);
            }
            return true;
        }

        public bool ItemToDb(CustomerModel item)
        {
            if (!item.IsChanged) return true;
            if (!item.Exists) return CreateItem(item);
            return UpdateItem(item);
        }

        public bool UpdateItem(CustomerModel item)
        {
            if (item.ID == null) throw new InvalidDataException("the ID of the Customer is null. the Customer cannot be updated.");
            if (!item.IsChanged) return true;
            bool toReturn = _db.SaveData(
                @"UPDATE Customer
                SET Name = $1,
                    Age = $2,
                    Email = $3,
                    PhoneNumber = $4,
                    IsSubscribed = $5
                WHERE ID = $6;",
                new Dictionary<string, dynamic?>(){
                    {"$1", item.Name},
                    {"$2", item.Age},
                    {"$3", item.Email},
                    {"$4", item.PhoneNumber},
                    {"$5", item.IsSubscribed},
                    {"$6", item.ID}
                }
            );
            if (toReturn) item.IsChanged = false;
            return toReturn;
        }
    }
}