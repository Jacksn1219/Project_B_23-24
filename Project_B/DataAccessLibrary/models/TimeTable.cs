namespace DataAccessLibrary;

public class TimeTable
{
    public int ID { get; }
    public string Name { get; }
    public int MovieID { get; }
    public int RoomID { get; }
    public string StartDate { get; set; }
    public string EndDate { get; set; }
    public TimeTable(int id, string name, int movieID, int roomID, string startDate, string endDate)
    {
        ID = id;
        Name = name;
        MovieID = movieID;
        RoomID = roomID;
        StartDate = startDate;
        EndDate = endDate;
    }
}
