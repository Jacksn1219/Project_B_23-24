using DataAccessLibrary.models.interfaces;

namespace DataAccessLibrary;

public class ActorModel : DbItem
{
    private string name;
    private string description;
    private int age;

    public override int? ID { get; internal set; }
    public string Name
    {
        get => name;
        set
        {
            name = value;
            IsChanged = true;
        }
    }
    public string Description
    {
        get => description;
        set
        {
            description = value;
            IsChanged = true;
        }
    }
    public int Age
    {
        get => age;
        set
        {
            age = value;
            IsChanged = true;
        }
    }
    public ActorModel(int id, string name, string description, int age)
    {
        ID = id;
        Name = name;
        Description = description;
        Age = age;
    }
}
