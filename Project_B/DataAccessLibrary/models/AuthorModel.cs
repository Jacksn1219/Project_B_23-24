namespace DataAccessLibrary;

public class AuthorModel
{
    public int ID { get; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Age { get; set; }
    public AuthorModel(int id, string name, string description, int age)
    {
        ID = id;
        Name = name;
        Description = description;
        Age = age;
    }
}
