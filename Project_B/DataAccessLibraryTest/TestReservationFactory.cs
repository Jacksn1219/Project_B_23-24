using DataAccessLibrary;
using DataAccessLibrary.logic;
using DataAccessLibrary.models;

namespace DataAccessLibraryTest
{
    [TestClass]
    public class TestReservationFactory
    {
        public const string TestDbPath = "ReservationTest.db";
        private DataAccess? _db;
        private readonly ReservationFactory _rf;
        private readonly SeatFactory _sf;
        private readonly CustomerFactory _cf;
        private readonly TimeTableModel testTimeTable;
        public TestReservationFactory()
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
            _cf = new CustomerFactory(_db);
            _rf = new ReservationFactory(_db, _cf, _sf);
            var df = new DirectorFactory(_db);
            var af = new ActorFactory(_db);
            var mf = new MovieFactory(_db, df, af);
            var rf = new RoomFactory(_db, _sf);
            var tf = new TimeTableFactory(_db, mf, rf);
            SeatModel seat = new SeatModel("seat1", "1", "1");
            RoomModel room = new RoomModel("room1", 10, 1, new List<SeatModel>() { seat });
            MovieModel mov = new MovieModel("movie1", "descr1", 3, 300, "genre");
            testTimeTable = new TimeTableModel(
                room, mov, DateTime.Now
            );
            tf.ItemToDb(testTimeTable);

        }
        [TestMethod]
        public void TestAddReservation()
        {
            CustomerModel cust = new CustomerModel(
                "someone", 21, "email@mail.mail", "123456789", true
            );
            ReservationModel reservation = new ReservationModel(
                cust, testTimeTable, testTimeTable.Room.Seats, "hi"
            );
            Assert.IsTrue(_rf.ItemToDb(reservation));
            Assert.IsTrue(reservation.Exists);
        }
        [TestMethod]
        public void TestGetReservation()
        {
            CustomerModel cust = new CustomerModel(
                "someone else", 12, "someone@mail.mail", "12344321", true
            );
            ReservationModel reservation = new ReservationModel(
                cust, testTimeTable, testTimeTable.Room.Seats, "I do not like u"
            );
            Assert.IsTrue(_rf.ItemToDb(reservation));
            var newReservation = _rf.GetItemFromId(reservation.ID ?? 0);
            Assert.AreEqual(newReservation.Note, reservation.Note);
            Assert.AreEqual(newReservation.CustomerID, reservation.CustomerID);
        }
        [TestMethod]
        public void TestUpdateReservation()
        {
            CustomerModel cust = new CustomerModel(
                "him", 12, "him@mail.mail", "321654987", false
            );
            ReservationModel reservation = new ReservationModel(
                cust, testTimeTable, testTimeTable.Room.Seats, "I do like u"
            );
            Assert.IsTrue(_rf.ItemToDb(reservation));
            reservation.Customer.Name = "her";
            reservation.Note = "hello there";
            Assert.IsTrue(_rf.ItemToDb(reservation));
            var newRes = _rf.GetItemFromId(reservation.ID ?? 0);
            Assert.AreEqual(reservation.Note, newRes.Note);
            var newCust = _cf.GetItemFromId(newRes.CustomerID ?? 0);
            Assert.AreEqual(reservation.Customer.Name, newCust.Name);
        }
    }
}