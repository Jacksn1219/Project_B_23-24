namespace DataAccessLibrary;

public class RoomModel
{
    public int ID { get; }
    public string Name { get; set; }
    public int Capacity { get; set; }
    public RoomModel(int id, string name, int capacity)
    {
        ID = id;
        Name = name;
        Capacity = capacity;
    }
}
