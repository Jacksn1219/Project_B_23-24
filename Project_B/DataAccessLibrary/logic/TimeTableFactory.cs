using DataAccessLibrary;
using DataAccessLibrary.logic;
using DataAccessLibrary.models;
using Newtonsoft.Json;

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
        bool result = RelatedItemsToDb(item);
        if (!result) return false;
        item.ID = _db.CreateData(
            @"INSERT INTO TimeTable(
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
                {"$3", item.StartDate},
                {"$4", item.EndDate}
            }
        );
        if (item.ID > 0) item.IsChanged = false;
        return item.ID > 0;
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

    public TimeTableModel GetItemFromId(int id, int deepcopyLv = 0)
    {
        try
        {
            return _db.ReadData<TimeTableModel>(
        @"SELECT * FROM Room
        WHERE ID = $1",
        new Dictionary<string, dynamic?>(){
            {"$1", id},
        }
        ).First();
        }
        catch { return null; }
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
        bool result = RelatedItemsToDb(item);
        if (!result) return result;
        result = _db.SaveData(
            @"UPDATE TimeTable
            SET RoomID = $1,
                MovieID = $2,
                StartDate = $3,
                EndDate = $4
            WHERE ID = $5",
            new Dictionary<string, dynamic?>(){
                {"$1", item.RoomID},
                {"$2", item.MovieID},
                {"$3", item.StartDate},
                {"$4", item.EndDate},
                {"$5", item.ID}
            }
        );
        item.IsChanged = !result;
        return result;
    }
    private bool RelatedItemsToDb(TimeTableModel item)
    {
        if (item.Movie != null)
        {
            _mf.ItemToDb(item.Movie);
            item.MovieID = item.Movie.ID;
        }
        if (item.Room != null)
        {
            _rf.ItemToDb(item.Room);
            item.RoomID = item.Room.ID;
        }
        return true;
    }
}
