namespace DataAccessLibrary;

public class Seat
{
    public int ID { get; }
    public int RoomID { get; }
    public string Name { get; set; }
    public string Rank { get; set; }
    public string Type { get; set; }
    public Seat(int id, int roomID, string name, string rank, string type)
    {
        ID = id;
        RoomID = roomID;
        Name = name;
        Rank = rank;
        Type = type;
    }
}
