using DataAccessLibrary.models.interfaces;

namespace DataAccessLibrary;

public class CustomerModel : DbItem
{
    public string Name
    {
        get => _name;
        set
        {
            _name = value;
            IsChanged = true;
        }
    }
    public int Age
    {
        get => _age;
        set
        {
            _age = value;
            IsChanged = true;
        }
    }
    private string? _email;
    private string _name;
    private int _age;
    private string _phoneNumber;
    private bool _isSubscribed;

    public string? Email
    {
        get
        {
            return _email;
        }
        set
        {
            if (IsValidEmail(value)) _email = value;
            else throw new ArgumentException("the email has an invalid value.");
            IsChanged = true;
        }
    }
    public string PhoneNumber
    {
        get => _phoneNumber;
        set
        {
            _phoneNumber = value;
            IsChanged = true;
        }
    }
    public bool IsSubscribed
    {
        get => _isSubscribed;
        set
        {
            _isSubscribed = value;
            IsChanged = true;
        }
    }
    public CustomerModel(string name, int age, string email, string phoneNumber, bool isSubscribed)
    : this(null, name, age, email, phoneNumber, isSubscribed)
    { }
    internal CustomerModel(int? id, string name, int age, string email, string phoneNumber, bool isSubscribed)
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
    private static bool IsValidEmail(string? email)
    {
        if (email == null) return true;
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
