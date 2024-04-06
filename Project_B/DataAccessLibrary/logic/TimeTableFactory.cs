using DataAccessLibrary;
using DataAccessLibrary.logic;
using DataAccessLibrary.models;

public class TimeTableFactory : IDbItemFactory<TimeTableModel>
{
    private readonly DataAccess _db;
    private readonly MovieFactory _mf;
    private readonly RoomFactory _rf;
    public TimeTableFactory(DataAccess db, MovieFactory mf, RoomFactory rf)
    {
        _db = db;
        _mf = mf;
        _rf = rf;
    }
    public bool CreateItem(TimeTableModel item)
    {
        throw new NotImplementedException();
    }

    public void CreateTable()
    {
        throw new NotImplementedException();
    }

    public TimeTableModel GetItemFromId(int id)
    {
        throw new NotImplementedException();
    }

    public bool ItemToDb(TimeTableModel item)
    {
        throw new NotImplementedException();
    }

    public bool UpdateItem(TimeTableModel item)
    {
        throw new NotImplementedException();
    }
}
