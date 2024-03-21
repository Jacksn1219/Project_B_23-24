namespace DataAccessLibrary;

public class Reservation
{
    public int ID { get; }
    public int CostumerID { get; }
    public int TimeTableID { get; }
    public string Note { get; set; }
    public Reservation(int id, int  costumerID, int timeTableID, string note)
    {
        ID = id;
        CostumerID = costumerID;
        TimeTableID = timeTableID;
        Note = note;
    }
}
