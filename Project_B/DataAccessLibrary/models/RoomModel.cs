using DataAccessLibrary.models.interfaces;

namespace DataAccessLibrary;

public class RoomModel : DbItem
{
    public override int? ID { get; }
    public string Name { get; set; }
    public int Capacity { get; set; }
    internal List<SeatModel> Seats;
    public RoomModel(string name, int capacity)
    : this(name, capacity, new List<SeatModel>())
    { }
    public RoomModel(string name, int capacity, List<SeatModel> seats)
    {
        Name = name;
        Capacity = capacity;
        Seats = seats;
    }
    public bool AddSeat(SeatModel seat)
    {
        return AddSeats(new SeatModel[] { seat });
    }
    public bool AddSeats(SeatModel[] seats)
    {
        if (this.Seats.Count + seats.Length > Capacity) return false;
        this.Seats.AddRange(seats);
        return true;
    }
}
