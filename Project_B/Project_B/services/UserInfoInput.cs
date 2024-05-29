using System;
using System.Data.SQLite;
using DataAccessLibrary.models;
using DataAccessLibrary;
using Models;


namespace Project_B.services
{
    
    public static class UserInfoInput
    {
        public static (string fullName, int age, string email, string phoneNumber, string userinput) GetUserInfo(TimeTableModel tt)
        {
            // Get initial user info
            // Call the InitialUserInfo method and pass the movie instance as an argument

            // var userInfo = UserInfoInput.InitialUserInfo(movie);
            var (fullName, age, email, phoneNumber, userinput) = InitialUserInfo(tt.Movie);

            // Display initial info
            DisplayUserInfo(fullName, age, email, phoneNumber, userinput);

            // Confirm or change info
            Console.WriteLine("\nDo you want to confirm this information? (Yes/No)");
            string choice = Universal.takeUserInput("Type...")?.ToLower() ?? "";

            if (choice == "no")
            {
                // Change info if desired
                (fullName, age, email, phoneNumber, userinput) = UserInfoEditor.EditUserInfo(fullName, age, email, phoneNumber, userinput);
            }

            // Return user info
            return (fullName, age, email, phoneNumber, userinput);
        }

        public static (string fullName, int age, string email, string phoneNumber, string userinput) InitialUserInfo(MovieModel movie)
        {
            string fullName;
            while (true)
            {
                Console.Write("Enter your full name: ");
                fullName = Universal.takeUserInput("Type...") ?? "";
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
                string? input = Universal.takeUserInput("Type...");
                if (input.IsValidEmail())
                {
                    email = input ?? "";
                    break;
                }
                else
                {
                    System.Console.WriteLine("Please enter a valid email.");
                }
            }

            int age;
            while (true)
            {
                Console.Write("Enter your age: ");
                string input = Universal.takeUserInput("Type...");

                if (int.TryParse(input, out age) && age > 0 && age < 100)
                {

                    

                    if ( (int)movie.PegiAge > age)
                    {
                        Universal.WriteColor($"\nWarning: This movie is not suitable for a {age}-year-old. Do you want to continue? (Y/N)", ConsoleColor.Red);
                        string choice = Universal.takeUserInput("Type...")?.ToLower() ?? "";
                        if (choice == "y" || choice == "yes")
                        {
                            // User wants to continue
                            System.Console.WriteLine("\nContinuing reservation\n");
                            break;
                        }
                        else if (choice == "n" || choice == "no")
                        {
                            System.Console.WriteLine("\nRservation canceled");
                        }
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    Console.WriteLine("Please enter a valid age.");
                }
            }


            string phoneNumber;
            while (true)
            {
                Console.Write("Enter your phone number (starting with 0 and max 10 digits): ");
                phoneNumber = Universal.takeUserInput("Type...") ?? "";
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
            string userinput = Universal.takeUserInput("Type...") ?? "";
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

        public static void DisplayUserInfo(string fullName, int age, string email, string phoneNumber, string userinput)
        {
            Console.Clear();
            Console.WriteLine("Your entered information:\n");
            Console.WriteLine($"Full Name: {fullName}");
            Console.WriteLine($"Age: {age}");
            Console.WriteLine($"Email: {email}");
            Console.WriteLine($"Phone Number: {phoneNumber}");
            Console.WriteLine($"User Input: {userinput}");
        }
    }
}
