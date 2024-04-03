namespace DataAccessLibrary;

public class Room
{
    public int ID { get; }
    public string Name { get; set; }
    public int Capacity { get; set; }
    public int RowWidth { get; set; }
    public Room(int id, string name, int capacity, int rowWidth = 1)
    {
        ID = id;
        Name = name;
        Capacity = capacity;
        RowWidth = rowWidth;
    }
}
