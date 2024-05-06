using DataAccessLibrary.models;
using DataAccessLibrary.models.interfaces;

namespace DataAccessLibrary;

public class ReservationModel : DbItem
{
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
    public readonly List<SeatModel> ReservedSeatModels = new List<SeatModel>();
    private string? _note;
    public ReservationModel() { }
    public ReservationModel(CustomerModel customer, TimeTableModel timeTable, List<SeatModel> SeatModels, string? note = null)
    : this(null, customer.ID, timeTable.ID, note)
    {
        Customer = customer;
        TimeTable = timeTable;
        ReservedSeatModels.AddRange(SeatModels);
    }
    internal ReservationModel(int? id, int? customerId, int? timetableId, string? note)
    {
        ID = id;
        CustomerID = customerId;
        TimeTableID = timetableId;
        Note = note;
    }
}
