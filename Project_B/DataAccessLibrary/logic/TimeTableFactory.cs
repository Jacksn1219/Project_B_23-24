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
        CreateTable();
    }
    public bool CreateItem(TimeTableModel item)
    {
        throw new NotImplementedException();
    }

    public void CreateTable()
    {
        _db.SaveData(
            @"CREATE TABLE IF NOT EXISTS TimeTable(
                ID INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL ,
                MovieID INTEGER NOT NULL,
                RoomID INTEGER NOT NULL,
                StartDate TEXT NOT NULL,
                EndDate TEXT NOT NULL,
                FOREIGN KEY (MovieID) REFERENCES Movie (ID),
                FOREIGN KEY (RoomID) REFERENCES Room (ID)
            )"
        );
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
