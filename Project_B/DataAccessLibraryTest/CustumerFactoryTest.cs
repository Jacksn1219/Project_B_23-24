using DataAccessLibrary;
using DataAccessLibrary.logic;

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

            _db = new SQliteDataAccess(TestDbPath);
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
            CustomerModel cust = _cf.GetItemFromId(1);
            Assert.IsNotNull(cust);
        }
    }
}

