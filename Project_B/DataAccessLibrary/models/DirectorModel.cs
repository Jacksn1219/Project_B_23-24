namespace DataAccessLibrary;

public class DirectorModel : IDbItem
{
    private int? _id;
    public int? ID
    {
        get { return _id; }
        protected set
        {
            if (value != null && value >= 0)
            {
                _id = value;
                return;
            }
            throw new InvalidDataException("ID cannot be below 0");
        }
    }
    public string Name { get; set; }
    public string? Description { get; set; }
    private int _age;
    public int Age
    {
        get { return _age; }
        set
        {
            if (value >= 0)
            {
                _age = value;
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
}
