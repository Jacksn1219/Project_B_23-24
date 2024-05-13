using System.Text.Json;
using DataAccessLibrary;
using DataAccessLibrary.logic;
using Serilog;

namespace DataAccessLibraryTest
{
    [TestClass]
    public class SeatModelFactoryTest
    {
        private readonly SeatModelFactory _sf;
        public const string TestDbPath = "SeatModelTest.db";
        private SQliteDataAccess? _db;
        private RoomModel _room;
        public SeatModelFactoryTest()
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
            _sf = new SeatFactory(_db);
            var rf = new RoomFactory(_db, _sf);
            _room = new RoomModel(
                    "test", 10, 10
                );
            rf.ItemToDb(
                _room
            );
        }
        [TestMethod]
        public void AddSeatModelToDBTest()
        {
            SeatModel zetel = new SeatModel
            (
                "zetel 1", "Godly", "zetel", _room
            );
            Assert.IsTrue(_sf.ItemToDb(zetel));
            Assert.IsTrue(zetel.Exists);
        }
        [TestMethod]
        public void GetSeatModelFromDbTest()
        {
            SeatModel zetel = new(
                "zetel 2", "ok", "zetel", _room
            );
            Assert.IsTrue(_sf.ItemToDb(zetel));
            var newZetel = _sf.GetItemFromId(zetel.ID ?? 1);
            Assert.AreEqual(zetel.Name, newZetel.Name);
        }
        [TestMethod]
        public void UpdateSeatModelToDBTest()
        {
            SeatModel zetel = new(
                "zetel 3", "spelling error", "zetel", _room
            );
            Assert.IsTrue(_sf.ItemToDb(zetel));
            zetel.Rank = "epic";
            Assert.IsTrue(_sf.UpdateItem(zetel));
            var newZetel = _sf.GetItemFromId(zetel.ID ?? 1);
            Assert.AreEqual(newZetel.Rank, zetel.Rank);
        }
        public void GetAllSeatsFromDbTest()
        {
            List<SeatModel> layout1 = new List<SeatModel>{
                    new SeatModel("0", " ", " "),
                    new SeatModel("1", " ", " "),
                    new SeatModel("2", "1", "Normaal"),
                    new SeatModel("3", "1", "Normaal"),
                    new SeatModel("4", "1", "Normaal"),
                    new SeatModel("5", "1", "Normaal"),
                    new SeatModel("6", "1", "Normaal"),
                    new SeatModel( "7", "1", "Normaal"),
                    new SeatModel("8", "1", "Normaal"),
                    new SeatModel( "9", "1", "Normaal"),
                    new SeatModel("10", " ", " "),
                    new SeatModel("11", " ", " "),
                    new SeatModel("12", " ", " "),
                    new SeatModel("13", " ", " ")
                };
            _sf.ItemsToDb(layout1);
            var result = _sf.GetItems(10);
            Assert.AreEqual(10, result.Length);
        }
    }
}