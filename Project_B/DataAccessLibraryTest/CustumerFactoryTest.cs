using DataAccessLibrary;
using DataAccessLibrary.logic;
using Serilog;

namespace DataAccessLibraryTest
{
    [TestClass]
    public class CustumerFactoryTest
    {
        public const string TestDbPath = "customertest.db";
        private DataAccess? _db;
        private CustomerFactory? _cf;
        public CustumerFactoryTest()
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
            _cf = new CustomerFactory(_db);
        }
        [TestMethod]
        public void TestCustomerWithInvalidEmail()
        {
            try
            {
                CustomerModel crusty = new CustomerModel
                (
                    "Jelle de Kok", 19, "123.ikdachthet.ni", "12345", false
                );
                throw new Exception("");
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex.Message.Equals("the email has an invalid value."));
            }
        }
        [TestMethod]
        public void TestCreateCustomer()
        {
            Assert.IsTrue(_cf != null);
            CustomerModel crusty = new CustomerModel
            (
                "tester", 1, "test@gmail.com", "123456789", false
            );
            Assert.IsTrue(_cf.CreateItem(crusty));
            try
            {
                _cf.CreateItem(crusty);
                Assert.Fail("the item can be added twice");
            }
            catch { }
        }
        [TestMethod]
        public void TestGetCustomer()
        {
            Assert.IsTrue(_cf != null);
            CustomerModel cust = new CustomerModel
            (
                "tester2", 1, "test2@gmail.com", "0987654321", false
            );
            _cf.CreateItem(cust);
            Assert.IsNotNull(cust.ID);
            CustomerModel custReturned = _cf.GetItemFromId(cust.ID ?? 1);
            Assert.AreEqual(cust.Email, custReturned.Email);
        }
        [TestMethod]
        public void TestUpdateCustomer()
        {
            Assert.IsTrue(_cf != null);
            CustomerModel cust = new CustomerModel
            (
                "tester3", 1, "test3@gmail.com", "112233445", true
            );
            _cf.CreateItem(cust);
            Assert.IsNotNull(cust.ID);
            cust.Age = 80;
            Assert.IsTrue(_cf.UpdateItem(cust));
        }
    }
}

