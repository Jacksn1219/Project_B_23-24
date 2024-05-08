using System.Globalization;
using DataAccessLibrary;
using DataAccessLibrary.logic;
using DataAccessLibrary.models;

namespace DataAccessLibraryTest
{
    [TestClass]
    public class TimeTableFactoryTest
    {
        private readonly TimeTableFactory _tf;
        private readonly DataAccess _db;
        private readonly MovieFactory _mf;
        private readonly RoomFactory _rf;
        public const string TestDbPath = "timetableTest.db";
        public TimeTableFactoryTest()
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
            var af = new ActorFactory(_db);
            var df = new DirectorFactory(_db);
            var sf = new SeatModelFactory(_db);
            _mf = new MovieFactory(_db, df, af);
            _rf = new RoomFactory(_db, sf);
            _tf = new TimeTableFactory(_db, _mf, _rf);
        }
        [TestMethod]
        public void TestAddTimeTable()
        {
            RoomModel room = new RoomModel(
                "a room", 10, 1
            );
            room.AddSeatModel(new SeatModel("a SeatModel", "1", "1"));
            MovieModel mov = new(
                "a movie", "disc", 3, 300, "boring"
            );
            TimeTableModel timet = new(
                room, mov, DateTime.Now
            );
            Assert.IsTrue(_tf.ItemToDb(timet));
            Assert.IsTrue(timet.Exists);
            Assert.IsTrue(timet.Exists);
            Assert.IsTrue(timet.Room.SeatModels[0].Exists);
        }
        [TestMethod]
        public void TestGetTimeTable()
        {
            RoomModel room = new RoomModel(
                "room2", 99, 2
            );
            MovieModel mov = new(
                "mama mia", "here we go again", 3, 999, "pain"
            );
            TimeTableModel tt = new(
                room, mov, DateTime.Now.AddMonths(1)
            );
            Assert.IsTrue(_tf.ItemToDb(tt));
            var newTt = _tf.GetItemFromId(tt.ID ?? 0);
            Assert.AreEqual(newTt.StartDate, tt.StartDate);
        }
        [TestMethod]
        public void TestUpdateTimeTable()
        {
            RoomModel room = new RoomModel(
                "lastRoom", 9, 9
            );
            MovieModel mov = new(
                "aliens", "are real, source: trust me bro", 18, 13, "pain"
            );
            TimeTableModel tt = new(
                room, mov, DateTime.Now.AddMonths(-1)
            );
            Assert.IsTrue(_tf.ItemToDb(tt));
            tt.Room.AddSeatModel(new SeatModel("my throne", "", ""));
            tt.StartDate = DateTime.Now.AddDays(2).ToString(CultureInfo.InvariantCulture);
            Assert.IsTrue(_tf.ItemToDb(tt));
            var newTt = _tf.GetItemFromId(tt.ID ?? 0);
            Assert.AreEqual(newTt.StartDate, tt.StartDate);
            Assert.IsTrue(tt.Room.SeatModels[0].Exists);
        }
    }
}