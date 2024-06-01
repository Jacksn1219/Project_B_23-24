using DataAccessLibrary.models.interfaces;

namespace DataAccessLibrary;

public class ActorModel : DbItem
{
    private string _name;
    private string _description;
    private int age;
    public string Name
    {
        get => _name;
        set
        {
            _name = value;
            IsChanged = true;
        }
    }
    public string Description
    {
        get => _description;
        set
        {
            _description = value;
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
    public ActorModel(string name, string description, int age)
    {
        Name = name;
        Description = description;
        Age = age;
    }

    public void editName(string name) { Name = name; }
    public void editDescription(string description) { Description = description; }
    public void editAge(int age) { Age = age; }
}
