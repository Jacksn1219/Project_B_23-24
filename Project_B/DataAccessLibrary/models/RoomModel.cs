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
    public readonly List<SeatModel> SeatModels;
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
    public RoomModel(string name, int capacity, int rowWidth, List<SeatModel> seatModels)
    {
        Name = name;
        Capacity = capacity;
        SeatModels = seatModels;
        RowWidth = rowWidth;
    }
    public bool AddSeatModel(SeatModel SeatModel)
    {
        return AddSeatModels(new SeatModel[] { SeatModel });
    }
    public bool AddSeatModels(SeatModel[] SeatModels)
    {
        if (this.SeatModels.Count + SeatModels.Length > Capacity) return false;
        this.SeatModels.AddRange(SeatModels);
        return true;
    }
}
