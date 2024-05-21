using System;
using Models;

namespace Project_B.services
{
    public static class UserInfoEditor
    {
        public static (string fullName, int age, string email, string phoneNumber, string userinput) EditUserInfo(string fullName, int age, string email, string phoneNumber, string userinput)
        {
            // Loop until user confirms changes
            while (true)
            {
                // Display options menu
                var option = DisplayEditMenu();

                // Perform selected action
                switch (option)
                {
                    case 0:
                        Console.Write("Enter your full name: ");
                        fullName = Console.ReadLine() ?? "";
                        break;
                    case 1:
                        Console.Write("Enter your email: ");
                        email = Console.ReadLine();
                        break;
                    case 2:
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
                        break;
                    case 3:
                        Console.Write("Enter your phone number (starting with 0 and max 10 digits): ");
                        phoneNumber = Console.ReadLine() ?? "";
                        break;
                    case 4:
                        Console.WriteLine("In case of allergies or special needs that the cinema needs to know about");
                        Console.WriteLine("Please write them here:");
                        userinput = Console.ReadLine();
                        break;
                    case 5:
                        // Confirm changes
                        Console.WriteLine("Changes confirmed.");
                        return (fullName, age, email, phoneNumber, userinput);
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

        private static int DisplayEditMenu()
        {
            var editMenu = new InputMenu("Select information to edit:");
            editMenu.Add("Full Name", _ => { });
            editMenu.Add("Email", _ => { });
            editMenu.Add("Age", _ => { });
            editMenu.Add("Phone Number", _ => { });
            editMenu.Add("Additional Info", _ => { });
            editMenu.Add("Confirm Changes", _ => { });
            editMenu.UseMenu();
            return editMenu.GetMenuOptionsCount() - 1;
        }
    }
}
