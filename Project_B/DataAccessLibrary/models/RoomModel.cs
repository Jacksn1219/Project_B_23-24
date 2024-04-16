using DataAccessLibrary.models.interfaces;

namespace DataAccessLibrary;

public class RoomModel : DbItem
{
    public override int? ID { get; internal set; }
    private string _name;
    public string Name
    {
        get => _name;
        set
        {
            _name = value;
            IsChanged = true;
        }
    }

    private int _capacity;
    public int Capacity
    {
        get => _capacity;
        set
        {
            _capacity = value;
            IsChanged = true;
        }
    }
    internal List<SeatModel> Seats;
    public RoomModel(string name, int capacity)
    : this(name, capacity, new List<SeatModel>())
    { }
    /// <summary>
    /// parameterless ctor to please the json serialiser gods
    /// </summary>
    public RoomModel()
    {

    }
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
