using System;

namespace Project_B.services
{
    public static class UserInfoInput
    {
        public static (string fullName, string email, string phoneNumber, string userinput) GetUserInfo()
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

            Console.WriteLine("In case of allergies or special needs that the cinema needs to know about");
            Console.WriteLine("Please write them here:");
            string userinput = Console.ReadLine();
            Console.Clear();
            Console.WriteLine("Thank you, YourEyes will do their utmost best to accompany your needs. Here is what you entered: ");
            Console.WriteLine(userinput);

            return (fullName, email, phoneNumber, userinput);
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
