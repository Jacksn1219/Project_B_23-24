using System.Text.Json;
using DataAccessLibrary;
using DataAccessLibrary.logic;

namespace DataAccessLibraryTest
{
    [TestClass]
    public class SeatFactoryTest
    {
        private readonly SeatFactory _sf;
        public const string TestDbPath = "actortest.db";
        private DataAccess? _db;
        public SeatFactoryTest()
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
            _sf = new SeatFactory(_db);
        }
        [TestMethod]
        public void AddSeatToDBTest()
        {
            SeatModel zetel = new SeatModel
            (
                d
            );
        }
    }
}