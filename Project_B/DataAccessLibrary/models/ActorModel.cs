namespace DataAccessLibrary;

public class ActorModel : IDbItem
{
    public int? ID { get; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Age { get; set; }
    public ActorModel(int id, string name, string description, int age)
    {
        ID = id;
        Name = name;
        Description = description;
        Age = age;
    }
}
