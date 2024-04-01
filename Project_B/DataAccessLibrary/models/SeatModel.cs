namespace DataAccessLibrary;

public class SeatModel
{
    public int ID { get; }
    public int RoomID { get; set; }
    public string Name { get; set; }
    public string Rank { get; set; }
    public string Type { get; set; }
    public SeatModel(int id, int roomID, string name, string rank, string type)
    {
        ID = id;
        RoomID = roomID;
        Name = name;
        Rank = rank;
        Type = type;
    }
}
