using DataAccessLibrary.models.interfaces;

namespace DataAccessLibrary;

public class SeatModel : DbItem
{
    private string _name;
    private string _rank;
    private string _type;

    public override int? ID { get; internal set; }
    internal int? RoomID { get; }
    public string Name
    {
        get => _name;
        set
        {
            _name = value;
            IsChanged = true;
        }
    }
    public string Rank
    {
        get => _rank;
        set { _rank = value; IsChanged = true; }
    }
    public string Type { get => _type; set { _type = value; IsChanged = true; } }
    public SeatModel(int id, int roomID, string name, string rank, string type)
    {
        ID = id;
        RoomID = roomID;
        Name = name;
        Rank = rank;
        Type = type;
    }
}
