namespace DataAccessLibrary;

public class CustomerModel : DbItem
{
    public override int? ID { get; }
    public string Name { get; set; }
    public int Age { get; set; }
    private string _email;
    public string Email
    {
        get
        {
            return _email;
        }
        set
        {
            if (IsValidEmail(value)) _email = value;
            else throw new ArgumentException("the email has an invalid value.");
        }
    }
    public string PhoneNumber { get; set; }
    public int IsSubscribed { get; set; }
    public CustomerModel(int id, string name, int age, string email, string phoneNumber, int isSubscribed)
    {
        ID = id;
        Name = name;
        Age = age;
        Email = email;
        PhoneNumber = phoneNumber;
        IsSubscribed = isSubscribed;
    }
    /// <summary>
    /// basic check if mail is valid. (totaly not stolen from the internet)
    /// </summary>
    /// <param name="email">ur email</param>
    /// <returns>true if probably valid, else false</returns>
    private bool IsValidEmail(string email)
    {
        var trimmedEmail = email.Trim();

        if (trimmedEmail.EndsWith("."))
        {
            return false;
        }
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == trimmedEmail;
        }
        catch
        {
            return false;
        }
    }
}
