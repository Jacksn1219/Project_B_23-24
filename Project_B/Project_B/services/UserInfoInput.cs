using System;

namespace Project_B.services
{
    public static class UserInfoInput
    {
        public static (string fullName, int age, string email, string phoneNumber, string userinput) GetUserInfo()
        {
            // Get initial user info
            var (fullName, age, email, phoneNumber, userinput) = InitialUserInfo();

            // Display initial info
            DisplayUserInfo(fullName, age, email, phoneNumber, userinput);

            // Confirm or change info
            Console.WriteLine("\nDo you want to confirm this information? (Yes/No)");
            string choice = Console.ReadLine()?.ToLower() ?? "";

            if (choice == "no")
            {
                // Change info if desired
                (fullName, age, email, phoneNumber, userinput) = UserInfoEditor.EditUserInfo(fullName, age, email, phoneNumber, userinput);
            }

            // Return user info
            return (fullName, age, email, phoneNumber, userinput);
        }

        private static (string fullName, int age, string email, string phoneNumber, string userinput) InitialUserInfo()
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

            string email;
            while (true)
            {
                Console.Write("Enter your email: ");
                string? input = Console.ReadLine();
                if (input.IsValidEmail())
                {
                    email = input ?? "";
                    break;
                }
            }

            int age;
            while (true)
            {
                Console.Write("Enter your age: ");
                if (!int.TryParse(Console.ReadLine(), out age) || age < 0 || age >= 100)
                {
                    Console.WriteLine("Please enter a valid age.");
                }
                else
                {
                    break;
                }
            }

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
            string userinput = Console.ReadLine() ?? "";
            Console.Clear();
            Console.WriteLine("Thank you, YourEyes will do their utmost best to accompany your needs. Here is what you entered: ");
            Console.WriteLine(userinput);

            return (fullName, age, email, phoneNumber, userinput);
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

        private static void DisplayUserInfo(string fullName, int age, string email, string phoneNumber, string userinput)
        {
            Console.Clear();
            Console.WriteLine("Your entered information:");
            Console.WriteLine($"Full Name: {fullName}");
            Console.WriteLine($"Age: {age}");
            Console.WriteLine($"Email: {email}");
            Console.WriteLine($"Phone Number: {phoneNumber}");
            Console.WriteLine($"User Input: {userinput}");
        }
    }
}
