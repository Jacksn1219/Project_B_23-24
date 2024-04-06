using DataAccessLibrary;

namespace DataAccessLibrary.logic
{
    public class ActorFactory : IDbItemFactory<ActorModel>
    {
        private readonly DataAccess _db;
        public ActorFactory(DataAccess db)
        {
            _db = db;
        }
        public bool CreateItem(ActorModel item)
        {
            throw new NotImplementedException();
        }

        public void CreateTable()
        {
            throw new NotImplementedException();
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