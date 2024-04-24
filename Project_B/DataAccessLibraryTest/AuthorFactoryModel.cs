using DataAccessLibrary;
using DataAccessLibrary.logic;

namespace DataAccessLibraryTest
{
    [TestClass]
    public class DirectorFactoryTest
    {
        private const string TestDbPath = "directorTest.db";
        private readonly DataAccess _db;
        private readonly DirectorFactory _df;
        public DirectorFactoryTest()
        {
            try
            {
                File.Delete(TestDbPath);
            }
            catch (Exception ex)
            {
                System.Console.WriteLine($"cannot delete testdb {TestDbPath}: {ex.Message}");
            }

            _db = new SQliteDataAccess($"Data Source={TestDbPath}; Version = 3; New = True; Compress = True;");
            _df = new DirectorFactory(_db);
        }
        [TestMethod]
        public void TestCreateDirector()
        {
            var dir = new DirectorModel(
                "John Badham", "dead", 99
            );
            Assert.IsTrue(_df.ItemToDb(dir));
            Assert.IsNotNull(dir.ID);
        }
        [TestMethod]
        public void TestGetDirector()
        {
            var dir = new DirectorModel(
                "getme", "now", 10
            );
            Assert.IsTrue(_df.ItemToDb(dir));
            var newDir = _df.GetItemFromId(dir.ID ?? 1);
            Assert.AreEqual(dir.Name, newDir.Name);
        }
        [TestMethod]
        public void TestUpdateDirector()
        {
            var dir = new DirectorModel(
                "update", "me", 33
            );
            Assert.IsTrue(_df.ItemToDb(dir));
            dir.Name = "updated";
            Assert.IsTrue(_df.ItemToDb(dir));
            var newDir = _df.GetItemFromId(dir.ID ?? 1);
            Assert.AreEqual(dir.Name, newDir.Name);
        }
    }
}