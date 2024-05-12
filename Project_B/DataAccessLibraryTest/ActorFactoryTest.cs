using DataAccessLibrary;
using DataAccessLibrary.logic;
using Serilog;

namespace DataAccessLibraryTest
{
    [TestClass]
    public class ActorFactoryTest
    {
        public const string TestDbPath = "actortest.db";
        private DataAccess? _db;
        private ActorFactory _af;
        public ActorFactoryTest()
        {
            try
            {
                File.Delete(TestDbPath);
            }
            catch (Exception ex)
            {
                System.Console.WriteLine($"cannot delete testdb {TestDbPath}: {ex.Message}");
            }
            using var logger = new LoggerConfiguration()
                .WriteTo.File("logs/dbErrors.txt", rollingInterval: RollingInterval.Day, retainedFileCountLimit: 7)
                .CreateLogger();

            _db = new SQliteDataAccess($"Data Source={TestDbPath}; Version = 3; New = True; Compress = True;", logger);
            _af = new ActorFactory(_db);
        }
        [TestMethod]
        public void AddActorTest()
        {
            var actor = new ActorModel(
                "jan", "jansen", 51
            );
            Assert.IsTrue(_af.ItemToDb(actor));
            Assert.IsNotNull(actor.ID);
        }
        [TestMethod]
        public void GetActorTest()
        {
            var actor = new ActorModel(
                "wappie", "", 3
            );
            Assert.IsTrue(_af.ItemToDb(actor));
            ActorModel actReturned = _af.GetItemFromId(actor.ID ?? 1);
            Assert.AreEqual(actor.Name, actReturned.Name);
        }
        [TestMethod]
        public void UpdateActorTest()
        {
            var actor = new ActorModel(
                "Dwane", "The rock Jhonson", 40
            );
            Assert.IsTrue(_af.ItemToDb(actor));
            actor.Age = 0;
            Assert.IsTrue(_af.ItemToDb(actor));
            ActorModel actReturned = _af.GetItemFromId(actor.ID ?? 1);
            Assert.AreEqual(actor.Age, actReturned.Age);
        }
    }
}