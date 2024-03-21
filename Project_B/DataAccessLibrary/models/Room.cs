namespace DataAccessLibrary;

public class Room
{
    public int ID { get; }
    public string Name { get; set; }
    public Room(int id, string name)
    {
        ID = id;
        Name = name;
    }
}
