using System.Text;
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
    public List<SeatModel> ReservedSeats = new List<SeatModel>();
    private string? _note;
    public ReservationModel() { }
    public ReservationModel(CustomerModel customer, TimeTableModel timeTable, List<SeatModel> SeatModels, string? note = null)
    : this(null, customer.ID, timeTable.ID, note)
    {
        Customer = customer;
        TimeTable = timeTable;
        ReservedSeats.AddRange(SeatModels);
    }
    internal ReservationModel(int? id, int? customerId, int? timetableId, string? note)
    {
        ID = id;
        CustomerID = customerId;
        TimeTableID = timetableId;
        Note = note;
    }
    public override string ToString()
    {
        StringBuilder sb = new();
        sb.AppendLine($"Nummer: {ID}");
        if(Customer != null) sb.AppendLine($"Customer: \n{Customer.ToString()}");
        if(TimeTable != null) sb.AppendLine($"Movie at time: \n{TimeTable.ToString()}");
        if (ReservedSeats.Count > 0){
            sb.AppendLine("Reserved seats: ");
        }
        foreach (SeatModel seatModel in ReservedSeats)
        {
            sb.AppendLine($"{seatModel.ToString()}");
        }
        return  sb.ToString();
    }
}
