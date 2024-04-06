namespace DataAccessLibrary;

public class RoomModel : DbItem
{
    public override int? ID { get; }
    public string Name { get; set; }
    public int Capacity { get; set; }
    public List<SeatModel> Seats;
    public RoomModel(string name, int capacity)
    : this(name, capacity, new List<SeatModel>())
    { }
    public RoomModel(string name, int capacity, List<SeatModel> seats)
    {
        Name = name;
        Capacity = capacity;
        Seats = seats;
    }
}
