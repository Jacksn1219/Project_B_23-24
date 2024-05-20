using System;
using DataAccessLibrary;

namespace Project_B.services
{
    public static class UserInfoInput
    {
        public static (string fullName, string email, string phoneNumber, string note) GetUserInfo()
        {
            string fullName;
            while (true)
            {
                Console.Write("Enter your full name: ");
                fullName = Console.ReadLine() ?? "";
                if (fullName.IsValidFullName())
                {
                    break;  // Exit the loop if a valid full name is entered
                }
                else
                {
                    Console.WriteLine("Please enter a valid full name.");
                }
            }
            
            string email;
            while (true){
                Console.Write("Enter your email: ");
                email = Console.ReadLine() ?? "";
                if(email.IsValidEmail()) break;
                System.Console.WriteLine("invalid email. ");
            }
            

            string phoneNumber;
            while (true)
            {
                Console.Write("Enter your phone number (starting with 0 and max 10 digits): ");
                phoneNumber = Console.ReadLine() ?? "";
                if (phoneNumber.IsValidPhoneNumber())
                {
                    break;  // Exit the loop if a valid phone number is entered
                }
                else
                {
                    Console.WriteLine("Please enter a valid phone number starting with 0 and max 10 digits.");
                }
            }

            Console.WriteLine("In case of allergies or special needs that the cinema needs to know about");
            Console.WriteLine("Please write them here:");
            string userinput = Console.ReadLine() ?? "";
            Console.Clear();
            Console.WriteLine("Thank you, YourEyes will do their utmost best to accompany your needs. Here is what you entered: ");
            Console.WriteLine(userinput);

            return (fullName, email, phoneNumber, userinput);
        }
    }
}
