namespace DataAccessLibrary;

public class ReservationModel : IDbItem
{
    private int? _id;
    /// <summary>
    /// the db Id of the Reservation. must be positive and should be readonly for external classes.
    /// </summary>
    public int? ID
    {
        get { return _id; }
        set
        {
            if (value == null || value >= 0)
            {
                _id = value;
            }
            else throw new InvalidDataException("the unique Id cannot be below 0");
        }
    }
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
