namespace DataAccessLibrary;

public class ReservationModel : DbItem
{
    /// <summary>
    /// the db Id of the Reservation. must be positive and should be readonly for external classes.
    /// </summary>
    public override int? ID { get; }
    public int CostumerID { get; }
    public int TimeTableID { get; }
    public string Note { get; set; }
    public ReservationModel(int id, int costumerID, int timeTableID, string note)
    {
        ID = id;
        CostumerID = costumerID;
        TimeTableID = timeTableID;
        Note = note;
    }
}
