using DataAccessLibrary.models;
using DataAccessLibrary.models.interfaces;

namespace DataAccessLibrary;

public class ReservationModel : DbItem
{
    /// <summary>
    /// the db Id of the Reservation. must be positive and should be readonly for external classes.
    /// </summary>
    public override int? ID { get; internal set; }
    public CustomerModel? Customer;
    internal int CostumerID { get; }
    public TimeTableModel? TimeTable;
    internal int TimeTableID { get; }
    public string Note
    {
        get => note;
        set
        {
            note = value;
            IsChanged = true;
        }
    }
    public List<SeatModel> ReservedSeats = new List<SeatModel>();
    private string note;

    public ReservationModel(int id, int costumerID, int timeTableID, string note)
    {
        ID = id;
        CostumerID = costumerID;
        TimeTableID = timeTableID;
        Note = note;
    }
}
