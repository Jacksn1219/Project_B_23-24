using DataAccessLibrary.models.interfaces;

namespace DataAccessLibrary;

public class SeatModel : DbItem
{
    private string _name;
    private string _rank;
    private string _type;

    public override int? ID { get; internal set; }
    internal int? RoomID { get; set; }
    public RoomModel? Room;
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
    public string Type
    {
        get => _type;
        set
        {
            _type = value;
            IsChanged = true;
        }
    }
    /// <summary>
    /// parameterless ctor to please the jsonserialiser gods
    /// </summary>
    public SeatModel()
    {

    }
    public SeatModel(string name, string rank, string type)
    {
        ID = null;
        RoomID = null;
        Name = name;
        Rank = rank;
        Type = type;
    }
    public SeatModel(string name, string rank, string type, RoomModel room)
    {
        ID = null;
        RoomID = room.ID;
        Room = room;
        Name = name;
        Rank = rank;
        Type = type;
    }
}
