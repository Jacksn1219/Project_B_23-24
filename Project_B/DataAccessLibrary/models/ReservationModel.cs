using DataAccessLibrary.models;
using DataAccessLibrary.models.interfaces;

namespace DataAccessLibrary;

public class ReservationModel : DbItem
{
    /// <summary>
    /// the db Id of the Reservation. must be positive and should be readonly for external classes.
    /// </summary>
    public override int? ID { get; set; }
    public CustomerModel? Customer;
    public int? CustomerID { get; set; }
    public TimeTableModel? TimeTable;
    public int? TimeTableID { get; set; }
    public string? Note
    {
        get => _note;
        set
        {
            _note = value;
            IsChanged = true;
        }
    }
    public readonly List<SeatModel> ReservedSeats = new List<SeatModel>();
    private string? _note;
    public ReservationModel() { }
    public ReservationModel(CustomerModel customer, TimeTableModel timeTable, List<SeatModel> seats, string? note = null)
    : this(null, customer.ID, timeTable.ID, note)
    {
        Customer = customer;
        TimeTable = timeTable;
        ReservedSeats.AddRange(seats);
    }
    internal ReservationModel(int? id, int? customerId, int? timetableId, string? note)
    {
        ID = id;
        CustomerID = customerId;
        TimeTableID = timetableId;
        Note = note;
    }
}
