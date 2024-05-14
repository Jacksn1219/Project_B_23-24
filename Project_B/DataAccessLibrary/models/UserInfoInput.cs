using System;
using DataAccessLibrary;

namespace Project_B.services
{
    public static class UserInfoInput
    {
        public static CustomerModel GetUserInfo()
        {
            string fullName;
            while (true)
            {
                Console.Write("Enter your full name: ");
                fullName = Console.ReadLine() ?? "";
                if (IsValidFullName(fullName))
                {
                    break;  // Exit the loop if a valid full name is entered
                }
                else
                {
                    Console.WriteLine("Please enter a valid full name.");
                }
            }

            Console.Write("Enter your email: ");
            string email = Console.ReadLine();

            string phoneNumber;
            while (true)
            {
                Console.Write("Enter your phone number (starting with 0 and max 10 digits): ");
                phoneNumber = Console.ReadLine() ?? "";
                if (IsValidPhoneNumber(phoneNumber))
                {
                    break;  // Exit the loop if a valid phone number is entered
                }
                else
                {
                    Console.WriteLine("Please enter a valid phone number starting with 0 and max 10 digits.");
                }
            }
            // todo add age check and ask for mail subscription
            return new CustomerModel(fullName, 0, email, phoneNumber, false);
        }

        private static bool IsValidFullName(string fullName)
        {
            return !string.IsNullOrWhiteSpace(fullName) && fullName.Replace(" ", "").All(char.IsLetter);
        }

        private static bool IsValidPhoneNumber(string phoneNumber)
        {
            // Phone number must start with '0' and have a maximum length of 10 characters
            return phoneNumber.StartsWith("0") && phoneNumber.Length == 10 && phoneNumber.All(char.IsDigit);
        }
    }
}
