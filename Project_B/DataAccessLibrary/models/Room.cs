namespace DataAccessLibrary;

public class Room
{
    public int ID { get; }
    public string Name { get; set; }
    public int Capacity { get; set;}
    public Room(int id, string name, int capacity)
    {
        ID = id;
        Name = name;
        Capacity = capacity;
    }
}
