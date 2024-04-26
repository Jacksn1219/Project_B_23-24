namespace DataAccessLibrary;

public class TimeTable
{
    public int ID { get; }
    public int MovieID { get; }
    public int RoomID { get; }
    public string StartDate { get; set; }
    public string EndDate { get; set; }
    public TimeTable(int id, int movieID, int roomID, string startDate, string endDate)
    {
        ID = id;
        MovieID = movieID;
        RoomID = roomID;
        StartDate = startDate;
        EndDate = endDate;
    }
}
