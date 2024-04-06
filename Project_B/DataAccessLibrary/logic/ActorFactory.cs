using DataAccessLibrary;

namespace DataAccessLibrary.logic
{
    public class ActorFactory : IDbItemFactory<ActorModel>
    {
        private readonly DataAccess _db;
        public ActorFactory(DataAccess db)
        {
            _db = db;
            CreateTable();
        }
        public bool CreateItem(ActorModel item)
        {
            throw new NotImplementedException();
        }

        public void CreateTable()
        {
            _db.SaveData(
                @"CREATE TABLE IF NOT EXISTS Actor(
                    ID INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
                    Name TEXT NOT NULL,
                    Description TEXT,
                    Age INTEGER NOT NULL
                )"
            );
        }

        public ActorModel GetItemFromId(int id)
        {
            throw new NotImplementedException();
        }

        public bool ItemToDb(ActorModel item)
        {
            throw new NotImplementedException();
        }

        public bool UpdateItem(ActorModel item)
        {
            throw new NotImplementedException();
        }
    }
}