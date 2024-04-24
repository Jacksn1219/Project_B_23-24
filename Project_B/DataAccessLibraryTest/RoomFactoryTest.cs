using DataAccessLibrary;
using DataAccessLibrary.logic;

namespace DataAccessLibraryTest
{
    [TestClass]
    public class RoomFactoryTest
    {
        public const string TestDbPath = "roomTest.db";
        private readonly DataAccess _db;
        private readonly RoomFactory _rf;
        private readonly SeatFactory _sf;
        public RoomFactoryTest()
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
            _rf = new RoomFactory(_db, _sf);
        }
        [TestMethod]
        public void TestAddRoomNoChairsTest()
        {
            RoomModel room = new("room", 10, 3);
            Assert.IsTrue(_rf.ItemToDb(room));
            Assert.IsTrue(room.Exists);
        }
        [TestMethod]
        public void TestAddRoomWithChairsTest()
        {
            RoomModel room = new(
                "roomier room", 11, 4
            );
            room.AddSeats(new SeatModel[]{
                new SeatModel("myseat1", "1", "cool", room),
                new SeatModel("myseat2", "2", "not cool", room)
            });
            Assert.IsTrue(_rf.ItemToDb(room));
            Assert.IsTrue(room.Exists);
            Assert.IsTrue(room.Seats[0].Exists);
        }
        [TestMethod]
        public void TestToManyChairsError()
        {
            RoomModel room = new("fakeroom", 1, 1);
            Assert.IsFalse(
                room.AddSeats
                (
                    new SeatModel[]
                    {
                            new SeatModel("myseat1", "1", "cool", room),
                            new SeatModel("myseat2", "2", "not cool", room)
                    }
                )
            );
        }
        [TestMethod]
        public void TestGetRoomDb()
        {
            RoomModel room = new("roomest room", 13, 2);
            Assert.IsTrue(_rf.ItemToDb(room));
            var newRoom = _rf.GetItemFromId(room.ID ?? 1);
            Assert.AreEqual(room.Name, newRoom.Name);
        }
        [TestMethod]
        public void TestUpdateRoomNoSeats()
        {
            RoomModel room = new("what's rooming on", 3, 1);
            Assert.IsTrue(_rf.ItemToDb(room));
            room.Capacity = 100;
            Assert.IsTrue(_rf.ItemToDb(room));
            var newRoom = _rf.GetItemFromId(room.ID ?? 0);
            Assert.AreEqual(room.Capacity, newRoom.Capacity);
        }
        [TestMethod]
        public void TestUpdateRoomWithSeats()
        {
            RoomModel room = new RoomModel("lastroomtest", 3, 1);
            room.AddSeats(
                new SeatModel[]{
                    new SeatModel("1", "1", "1"),
                    new SeatModel("2", "2", "2"),
                    new SeatModel("3", "3", "3")
                });
            Assert.IsTrue(_rf.ItemToDb(room));
            room.Seats[0].Name = "hello there";
            Assert.IsTrue(_rf.ItemToDb(room));
            var newSeat = _sf.GetItemFromId(room.Seats[0].ID ?? 0);
            Assert.AreEqual(room.Seats[0].Name, newSeat.Name);

        }
    }
}