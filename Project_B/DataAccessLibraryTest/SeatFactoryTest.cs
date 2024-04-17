using System.Text.Json;
using DataAccessLibrary;
using DataAccessLibrary.logic;

namespace DataAccessLibraryTest
{
    [TestClass]
    public class SeatFactoryTest
    {
        private readonly SeatFactory _sf;
        public const string TestDbPath = "seatTest.db";
        private DataAccess? _db;
        private RoomModel _room;
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
            var rf = new RoomFactory(_db, _sf);
            _room = new RoomModel(
                    "test", 10
                );
            rf.ItemToDb(
                _room
            );
        }
        [TestMethod]
        public void AddSeatToDBTest()
        {
            SeatModel zetel = new SeatModel
            (
                "zetel 1", "Godly", "zetel", _room
            );
            Assert.IsTrue(_sf.ItemToDb(zetel));
            Assert.IsTrue(zetel.Exists);
        }
        [TestMethod]
        public void GetSeatFromDbTest()
        {
            SeatModel zetel = new(
                "zetel 2", "ok", "zetel", _room
            );
            Assert.IsTrue(_sf.ItemToDb(zetel));
            var newZetel = _sf.GetItemFromId(zetel.ID ?? 1);
            Assert.AreEqual(zetel.Name, newZetel.Name);
        }
        [TestMethod]
        public void UpdateSeatToDBTest()
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
    }
}