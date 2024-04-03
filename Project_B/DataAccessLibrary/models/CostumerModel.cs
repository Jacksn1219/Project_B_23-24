﻿namespace DataAccessLibrary;

public class CostumerModel
{
    public int ID { get; }
    public string Name { get; set; }
    public int Age { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public int IsSubscribed { get; set; }
    public CostumerModel(int id, string name, int age, string email, string phoneNumber, int isSubscribed)
    {
        ID = id;
        Name = name;
        Age = age;
        Email = email;
        PhoneNumber = phoneNumber;
        IsSubscribed = isSubscribed;
    }
}