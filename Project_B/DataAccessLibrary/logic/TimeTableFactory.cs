using System.Globalization;
using DataAccessLibrary;
using DataAccessLibrary.logic;
using DataAccessLibrary.models;
using Newtonsoft.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

public class TimeTableFactory : IDbItemFactory<TimeTableModel>
{
    private readonly DataAccess _db;
    private readonly MovieFactory _mf;
    private readonly RoomFactory _rf;
    private readonly Serilog.Core.Logger _logger;
    public TimeTableFactory(DataAccess db, MovieFactory mf, RoomFactory rf, Serilog.Core.Logger logger)
    {
        _db = db;
        _mf = mf;
        _rf = rf;
        _logger = logger;
        CreateTable();
    }
    public bool CreateItem(TimeTableModel item, int deepcopyLv = 99)
    {
        if (deepcopyLv < 0) return true;
        if (item.ID != null) throw new InvalidDataException("the timetable is already in the db.");
        if (!item.IsChanged) return true;
        bool dontClose = _db.IsOpen;
        try
        {
            _db.OpenConnection();
            bool result = RelatedItemsToDb(item, deepcopyLv - 1);
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
        catch (Exception ex)
        {
            _logger.Warning(ex, "failed to create a timetable");
            return false;
        }
        finally
        {
            if (!dontClose) _db.CloseConnection();
        }
    }

    public void CreateTable()
    {
        try
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
        catch (Exception ex)
        {
            _logger.Fatal(ex, "failed to create table timetable");
            throw;
        }
    }

    public TimeTableModel? GetItemFromId(int id, int deepcopyLv = 0)
    {
        if (deepcopyLv < 0) return null;
        try
        {
            var toReturn = _db.ReadData<TimeTableModel>(
                @"SELECT * FROM TimeTable
                WHERE ID = $1 AND StartDate > $2",
                new Dictionary<string, dynamic?>(){
                    {"$1", id},
                    {"$2", DateTime.MinValue.ToString(CultureInfo.InvariantCulture)},
                }
            ).First();
            getRelatedItemsFromDb(toReturn, deepcopyLv - 1);
            return toReturn;
        }
        catch (Exception ex)
        {
            _logger.Warning(ex, $"failed to get timetable with ID {id}");
            return null;
        }
    }

    public TimeTableModel[] GetItems(int count, int page = 1, int deepcopyLv = 0)
    {
        if (deepcopyLv < 0) return Array.Empty<TimeTableModel>();
        bool dontClose = _db.IsOpen;
        try
        {
            _db.OpenConnection();
            TimeTableModel[] tts = _db.ReadData<TimeTableModel>(
                $"SELECT * FROM TimeTable WHERE StartDate > $1 LIMIT {count} OFFSET {count * page - count}",
                new Dictionary<string, dynamic?>()
                {
                    {"$1", DateTime.MinValue.ToString(CultureInfo.InvariantCulture)},
                }
            ).OrderBy(x => x.StartDate).ToArray();
            if (deepcopyLv < 1) return tts;
            foreach (TimeTableModel tt in tts)
            {
                tt.Movie = _mf.GetItemFromId(tt.MovieID ?? 0, deepcopyLv - 1);
                tt.Room = _rf.GetItemFromId(tt.RoomID ?? 0, deepcopyLv - 1);
            }
            return tts;
        }
        catch (Exception ex)
        {
            _logger.Warning(ex, "failed to get timetables");
            return Array.Empty<TimeTableModel>();
        }
        finally
        {
            if (!dontClose) _db.CloseConnection();
        }
    }

    public bool ItemToDb(TimeTableModel item, int deepcopyLv = 99)
    {
        if (deepcopyLv < 0) return true;
        if (!item.IsChanged) return true;
        if (!item.Exists) return CreateItem(item, deepcopyLv);
        return UpdateItem(item, deepcopyLv);
    }
    public bool UpdateItem(TimeTableModel item, int deepcopyLv = 99)
    {
        if (deepcopyLv < 0) return true;
        if (item.ID == null) throw new InvalidDataException("timetable is not in the db.");
        if (!item.IsChanged) return true;
        bool dontClose = _db.IsOpen;
        try
        {
            bool result = RelatedItemsToDb(item, deepcopyLv - 1);
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
        catch (Exception ex)
        {
            _logger.Warning(ex, $"failed to update timetable with ID {item.ID}");
            return false;
        }
        finally
        {
            if (!dontClose) _db.CloseConnection();
        }
    }
    private bool RelatedItemsToDb(TimeTableModel item, int deepcopyLv)
    {
        bool dontClose = _db.IsOpen;
        try
        {
            _db.OpenConnection();
            if (item.Movie != null)
            {
                _mf.ItemToDb(item.Movie, deepcopyLv);
                item.MovieID = item.Movie.ID;
            }
            if (item.Room != null)
            {
                _rf.ItemToDb(item.Room, deepcopyLv);
                item.RoomID = item.Room.ID;
            }
            return true;
        }
        catch (Exception ex)
        {
            _logger.Warning(ex, $"failed to add room and/or movie of timetable with ID {item.ID}");
            return false;
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
            item.Movie = _mf.GetItemFromId(item.MovieID ?? 0, deepcopyLv);
            item.Room = _rf.GetItemFromId(item.RoomID ?? 0, deepcopyLv);
            SeatModel[] reservedSeat = GetReservedSeats(item);
            foreach (SeatModel seat in reservedSeat)
            {
                item.Room.Seats[Convert.ToInt16(seat.Name)].IsReserved = true;
            }
        }
        catch (Exception ex)
        {
            _logger.Warning(ex, $"failed to get room and/or movie of timetable with ID {item.ID}");
        }
        finally
        {
            if (!dontClose) _db.CloseConnection();
        }

    }

    public bool ItemsToDb(List<TimeTableModel> items, int deepcopyLv = 99)
    {
        if (deepcopyLv < 0) return true;
        foreach (var item in items)
        {
            ItemToDb(item, deepcopyLv);
        }
        return true;
    }
    public SeatModel[]? GetReservedSeats(TimeTableModel tt)
    {
        try
        {
            return _db.ReadData<SeatModel>(@"SELECT Seat.ID, Seat.Name, Seat.Type, Seat.Rank FROM Seat
                INNER JOIN ReservedSeat ON ReservedSeat.SeatID = Seat.ID
                INNER JOIN Reservation ON Reservation.ID = ReservedSeat.ReservationID
                INNER JOIN TimeTable ON Reservation.TimeTableID = TimeTable.ID
                INNER JOIN Room ON Room.ID = Seat.RoomID
                WHERE Room.ID = $1 AND Reservation.TimeTableID = $2",
                new Dictionary<string, dynamic?>(){
                    {"$1", tt.RoomID},
                    {"$2", tt.ID}
                }
            );
        }
        catch (Exception ex)
        {
            _logger.Warning(ex, $"failed to get reserved seats of timetable with ID {tt.ID}");
            return null;
        }
    }
    public TimeTableModel[]? GetTimeTablesFromDate(DateOnly date)
    {
        try
        {
            return _db.ReadData<TimeTableModel>
            (
                @"SELECT * FROM TimeTable
                WHERE StartDate >= $1 AND StartDate <= $2",
                new Dictionary<string, dynamic?>(){
                    {"$1", date.ToString(CultureInfo.InvariantCulture)},
                    {"$2", date.AddDays(1).ToString(CultureInfo.InvariantCulture)}
                }
            ).OrderBy(x => x.StartDate).ToArray();
        }
        catch (Exception ex)
        {
            _logger.Warning(ex, $"failed to get the timetables on date {date}");
            return null;
        }
    }
    /// <summary>
    /// gets all TimeTables that play in between <paramref name="startDate"/> & <paramref name="endDate"/>
    /// </summary>
    /// <param name="startDate">the movie has to play after this date</param>
    /// <param name="endDate">the movie has to have started before this date</param>
    /// <returns></returns>
    public TimeTableModel[] GetTimeTablesBetweenDates(DateTime startDate, DateTime endDate)
    {
        return _db.ReadData<TimeTableModel>
        (
            @"SELECT * FROM TimeTable
            WHERE EndDate >= $1 AND StartDate <= $2",
            new Dictionary<string, dynamic?>(){
                {"$1", startDate.ToString(CultureInfo.InvariantCulture)},
                {"$2", endDate.ToString(CultureInfo.InvariantCulture)}
            }
        ).OrderBy(x => x.StartDate).ToArray();
    }
    /// <summary>
    /// gets all TimeTables that play in the room with ID: <para>roomID</para>,
    /// and between <paramref name="startDate"/> & <paramref name="endDate"/>
    /// </summary>
    /// <param name="roomID">the ID of the room the timetable will be playing in</param>
    /// <param name="startDate">the movie has to play after this date</param>
    /// <param name="endDate">the movie has to have started before this date</param>
    /// <returns></returns>
    public TimeTableModel[] GetTimeTablesInRoomBetweenDates(int roomID, DateTime startDate, DateTime endDate)
    {
        return _db.ReadData<TimeTableModel>
        (
            @"SELECT * FROM TimeTable
            WHERE EndDate >= $1 AND StartDate <= $2 AND RoomID = $3",
            new Dictionary<string, dynamic?>(){
                {"$1", startDate.ToString(CultureInfo.InvariantCulture)},
                {"$2", endDate.ToString(CultureInfo.InvariantCulture)},
                {"$3", roomID}
            }
        ).OrderBy(x => x.StartDate).ToArray();
    }
    public void RemoveFromDB(int id)
    {
        RemoveFromDB(GetItemFromId(id));
    }
    public void RemoveFromDB(TimeTableModel timetable)
    {
        try {
            timetable.StartDate = DateTime.MinValue.ToString(CultureInfo.InvariantCulture);
            timetable.EndDate = DateTime.MinValue.ToString(CultureInfo.InvariantCulture);
            this.ItemToDb(timetable);
        }
        catch (Exception ex)
        {
            _logger.Warning(ex, $"failed to remove the timetable with ID: {timetable.ID}");
        }
    }
}
