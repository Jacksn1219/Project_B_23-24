namespace DataAccessLibrary;

public class DirectorModel
{
    public int ID { get; }
    public string Name { get; set; }
    public string Discription { get; set; }
    public int Age { get; set; }
    public DirectorModel(int id, string name, string discription, int age)
    {
        ID = id;
        Name = name;
        Discription = discription;
        Age = age;
    }
}
