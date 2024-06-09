using DataAccessLibrary;

namespace DataAccessLibrary.logic
{
    public class CustomerFactory : IDbItemFactory<CustomerModel>
    {
        private readonly DataAccess _db;
        private readonly Serilog.Core.Logger _logger;
        public CustomerFactory(DataAccess db, Serilog.Core.Logger logger)
        {
            _db = db;
            _logger = logger;
            CreateTable();
        }

        public bool CreateItem(CustomerModel item, int deepcopyLv = 99)
        {
            if (deepcopyLv < 0) return true;
            if (item.Exists) throw new InvalidDataException("this Customer already exists in the db.");
            if (!item.IsChanged) return true;
            try
            {
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
            catch (Exception ex)
            {
                _logger.Warning(ex, "failed to create a customer");
                return false;
            }
        }

        public void CreateTable()
        {
            try
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
            catch(Exception ex)
            {
                _logger.Fatal(ex, "failed to create table customer");
                throw;
            }
        }

        public CustomerModel? GetItemFromId(int id, int deepcopyLv = 0)
        {
            if (deepcopyLv < 0) return null;
            try
            {
                return _db.ReadData<CustomerModel>(
                    @"SELECT * FROM Customer
                    WHERE ID = $1",
                    new Dictionary<string, dynamic?>(){
                        {"$1", id},
                    }
                ).First();
            }
            catch(Exception ex)
            {
                _logger.Warning(ex, $"failed to get customer with ID {id}");
                return null;
            }
        }
        /// <summary>
        /// gets the customer with the email (email is unique). 
        /// empty customer if no customer found. 
        /// null if error occured.
        /// </summary>
        /// <param name="email">the email, NO validity checks</param>
        /// <param name="deepcopyLv"> if les than 0 return null</param>
        /// <exception cref="InvalidDataException">if no customer returned from email</exception>
        /// <returns>the customer</returns>
        public CustomerModel? GetCustomerFromEmail(string email, int deepcopyLv = 0)
        {
            if (deepcopyLv < 0) return null;
            try
            {
                return _db.ReadData<CustomerModel>(
                    @"SELECT * FROM Customer
                    WHERE Email = $1",
                    new Dictionary<string, dynamic?>(){
                        {"$1", email},
                    }
                ).First();
            }
            catch(InvalidOperationException ex)
            {
                //bad email
                _logger.Information(ex, $"failed to get customer with email {email}");
                throw new InvalidDataException($"no customer is returned from E-mail {email}", ex);
            }
            catch(Exception ex)
            {
                //better start panicing
                _logger.Warning(ex, $"failed to get customer with email {email}");
                return null;
            }
        }

        public CustomerModel[] GetItems(int count, int page = 1, int deepcopyLv = 0)
        {
            if (deepcopyLv < 0) return Array.Empty<CustomerModel>();
            try
            {
                return _db.ReadData<CustomerModel>(
                    $"SELECT * FROM Customer LIMIT {count} OFFSET {count * page - count}"
                ).OrderBy(x => x.Name).ToArray();
            }
            catch (Exception ex)
            {
                _logger.Warning(ex, "failed to get customers");
                return Array.Empty<CustomerModel>();
            }
        }

        public bool ItemsToDb(List<CustomerModel> items, int deepcopyLv = 99)
        {
            if(deepcopyLv < 0) return true;
            foreach (var item in items)
            {
                ItemToDb(item, deepcopyLv);
            }
            return true;
        }

        public bool ItemToDb(CustomerModel item, int deepcopyLv = 99)
        {
            if (deepcopyLv < 0) return true;
            if (!item.IsChanged) return true;
            if (!item.Exists) return CreateItem(item, deepcopyLv);
            return UpdateItem(item, deepcopyLv);
        }

        public bool UpdateItem(CustomerModel item, int deepcopyLv = 99)
        {
            if (deepcopyLv < 0) return true;
            if (item.ID == null) throw new InvalidDataException("the ID of the Customer is null. the Customer cannot be updated.");
            if (!item.IsChanged) return true;
            try
            {
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
            catch(Exception ex)
            {
                _logger.Warning(ex, $"failed to update customer with ID {item.ID}");
                return false;
            }
        }
    }
}