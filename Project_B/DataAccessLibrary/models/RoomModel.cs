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
    private List<SeatModel> _seats;
    public List<SeatModel> Seats { get { return _seats; } private set { _seats = value; } }
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
    public bool AddSeatModel(SeatModel SeatModel)
    {
        return AddSeatModels(new SeatModel[] { SeatModel });
    }
    public bool AddSeatModels(SeatModel[] SeatModels)
    {
        if (this.Seats == null) this.Seats = new List<SeatModel>();
        if (this.Seats.Count + SeatModels.Length > Capacity) return false;
        this.Seats.AddRange(SeatModels);
        return true;
    }
}
