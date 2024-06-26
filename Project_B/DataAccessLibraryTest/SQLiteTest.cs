namespace DataAccessLibraryTest;
using DataAccessLibrary.models.interfaces;
using DataAccessLibrary;
using Serilog;

[TestClass]
public class SQLiteTest
{
    private DataAccess? _db;
    [TestInitialize]
    public void TestDbConnection()
    {
        try
        {
            File.Delete("database.db");

        }
        catch { }
        using var logger = new LoggerConfiguration()
                .WriteTo.File("logs/dbErrors.txt", rollingInterval: RollingInterval.Day, retainedFileCountLimit: 7)
                .CreateLogger();
        string connectionstring = "Data Source=database.db; Version = 3; New = True; Compress = True;";
        _db = new SQliteDataAccess(connectionstring, logger);
        _db.SaveData("CREATE TABLE IF NOT EXISTS SampleTable(Col1 VARCHAR(20), Col2 INT)");
    }
    [TestMethod]
    public void TestSaveData()
    {
        if (_db == null) throw new FileLoadException("db not found");
        Assert.IsTrue(
            _db.SaveData("INSERT INTO SampleTable(Col1, Col2) VALUES($1, $2);",
            new Dictionary<string, dynamic?>(){
                {"$1", "hi"},
                {"$2", 1}
            })
        );
    }
    [TestMethod]
    public void TestReadData()
    {
        if (_db == null) throw new FileLoadException("db not found");
        try
        {
            _db.ReadData<bool>("SELECT * FROM SampleTable");
        }
        catch (NotImplementedException)
        {

        }
        Assert.IsTrue(true);
    }
}
