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
        if (item.ID != null) throw new InvalidDataException("the timetable is already in the db.");
        if (!item.IsChanged) return true;
        item.ID = _db.CreateData(
            @"INSERT INTO TimeTable
            VALUES(
                MovieID,
                RoomID,
                StartDate,
                EndDate
            )
            VALUES($1,$2,$3,$4)",
            new Dictionary<string, dynamic?>()
            {
                {"$1", item.MovieID},
                {"$2", item.RoomID},
                {"$3", item.StartDate.ToString()},
                {"$4", item.EndDate.ToString()}
            }
        );
        return item.ID > 0 && RelatedItemsToDb(item);
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
        return _db.ReadData<TimeTableModel>(
            @"SELECT * FROM TimeTable
            WHERE ID = $1",
            new Dictionary<string, dynamic?>(){
                {"$1", id},
            }
        ).First();
    }

    public bool ItemToDb(TimeTableModel item)
    {
        if (!item.IsChanged) return true;
        if (item.ID == null) return CreateItem(item);
        return UpdateItem(item);
    }
    public bool UpdateItem(TimeTableModel item)
    {
        if (item.ID == null) throw new InvalidDataException("timetable is not in the db.");
        if (!item.IsChanged) return true;
        return _db.SaveData(
            @"UPDATE TimeTable
            SET RoomID,
                MovieID,
                StartDate,
                EndDate
            VALUES($1,$2,$3,$4)",
            new Dictionary<string, dynamic?>(){
                {"$1", item.RoomID},
                {"$2", item.MovieID},
                {"$3", item.StartDate.ToString()},
                {"$4", item.EndDate.ToString()}
            }
        ) && RelatedItemsToDb(item);
    }
    private bool RelatedItemsToDb(TimeTableModel item)
    {
        if (item.Movie != null) _mf.ItemToDb(item.Movie);
        if (item.Room != null) _rf.ItemToDb(item.Room);
        return true;
    }
}
