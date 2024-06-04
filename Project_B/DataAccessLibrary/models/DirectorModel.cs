using DataAccessLibrary.models.interfaces;

namespace DataAccessLibrary;

public class DirectorModel : DbItem
{
    private string _name;
    public string Name
    {
        get => _name;
        set
        {
            _name = value;
            IsChanged = true;
        }
    }
    private string? _description;
    public string? Description
    {
        get => _description;
        set
        {
            _description = value;
            IsChanged = true;
        }
    }
    private int _age;
    public int Age
    {
        get { return _age; }
        set
        {
            if (value >= 0)
            {
                _age = value;
                IsChanged = true;
                return;
            }
            throw new InvalidDataException("The director's age cannot be below 0");
        }
    }
    public DirectorModel(string name, string? description, int age) : this(null, name, description, age) { }
    protected DirectorModel(int? id, string name, string? description, int age)
    {
        Name = name;
        Description = description;
        Age = age;
        ID = id;
    }

    public override string ToString()
    {
        return Name;
    }
}
