namespace DataAccessLibrary;

public class Costumer
{
    public int ID { get; }
    public string Name { get; set; }
    public int Age { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public int IsSubscribed { get; set; }
    public Costumer(int id, string name, int age, string email, string phoneNumber, int isSubscribed)
    {
        ID = id;
        Name = name;
        Age = age;
        Email = email;
        PhoneNumber = phoneNumber;
        IsSubscribed = isSubscribed;
    }
}
