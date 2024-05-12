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
        bool dontClose = _db.IsOpen;
        try
        {
            _db.OpenConnection();
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
        finally
        {
            if (!dontClose) _db.CloseConnection();
        }
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
        return _db.ReadData<TimeTableModel>(
            @"SELECT * FROM TimeTable
            WHERE ID = $1",
            new Dictionary<string, dynamic?>(){
                {"$1", id},
            }
        ).First();
    }

    public TimeTableModel[] GetItems(int count, int page = 1, int deepcopyLv = 0)
    {
        if (deepcopyLv < 0) return new TimeTableModel[0];
        bool dontClose = _db.IsOpen;
        try
        {
            _db.OpenConnection();
            TimeTableModel[] tts = _db.ReadData<TimeTableModel>(
                $"SELECT * FROM TimeTable LIMIT {count} OFFSET {count * page - count}"
            );
            if (deepcopyLv < 1) return tts;
            foreach (TimeTableModel tt in tts)
            {
                tt.Movie = _mf.GetItemFromId(tt.MovieID ?? 0, deepcopyLv - 1);
            }
            return tts;
        }
        finally
        {
            if (!dontClose) _db.CloseConnection();
        }
    }

    public bool ItemToDb(TimeTableModel item)
    {
        if (!item.IsChanged) return true;
        if (!item.Exists) return CreateItem(item);
        return UpdateItem(item);
    }
    public bool UpdateItem(TimeTableModel item)
    {
        if (item.ID == null) throw new InvalidDataException("timetable is not in the db.");
        if (!item.IsChanged) return true;
        bool dontClose = _db.IsOpen;
        try
        {
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
        finally
        {
            if (!dontClose) _db.CloseConnection();
        }
    }
    private bool RelatedItemsToDb(TimeTableModel item)
    {
        bool dontClose = _db.IsOpen;
        try
        {
            _db.OpenConnection();
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
        finally
        {
            if (!dontClose) _db.CloseConnection();
        }
    }
    public void getRelatedItemsFromDb(TimeTableModel item, int deepcopyLv = 0)
    {
        if (deepcopyLv < 0) return;
        bool dontClose = _db.IsOpen;
        try
        {
            _db.OpenConnection();
            item.Movie = _mf.GetItemFromId(item.MovieID ?? 0);
            item.Room = _rf.GetItemFromId(item.RoomID ?? 0);
        }
        finally
        {
            if (!dontClose) _db.CloseConnection();
        }

    }

    public bool ItemsToDb(List<TimeTableModel> items)
    {
        foreach (var item in items)
        {
            ItemToDb(item);
        }
        return true;
    }
}
