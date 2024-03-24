namespace DataAccessLibrary;

public class Author
{
    public int ID { get; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Age { get; set; }
    public Author(int id, string name, string description, int age)
    {
        ID = id;
        Name = name;
        Description = description;
        Age = age;
    }
}
