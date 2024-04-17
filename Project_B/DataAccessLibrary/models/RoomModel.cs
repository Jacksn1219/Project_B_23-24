using DataAccessLibrary.models.interfaces;

namespace DataAccessLibrary;

public class RoomModel : DbItem
{
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
    public readonly List<SeatModel> Seats;
    private int? _rowWidth;
    public int? RowWidth
    {
        get
        {
            return _rowWidth;
        }
        set
        {
            _rowWidth = value;
            IsChanged = true;
        }
    }
    public RoomModel(string name, int capacity, int rowWidth)
    : this(name, capacity, rowWidth, new List<SeatModel>())
    { }
    /// <summary>
    /// parameterless ctor to please the json serialiser gods
    /// </summary>
    public RoomModel()
    {

    }
    public RoomModel(string name, int capacity, int rowWidth, List<SeatModel> seats)
    {
        Name = name;
        Capacity = capacity;
        Seats = seats;
        RowWidth = rowWidth;
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
