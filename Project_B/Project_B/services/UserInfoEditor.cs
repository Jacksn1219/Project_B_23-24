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
                DisplayEditMenu(fullName, age, email, phoneNumber, userinput);

            }
        }

        static int DisplayEditMenu(string fullName, int age, string email, string phoneNumber, string userinput)
        {
            var editMenu = new InputMenu("Select information to edit:");
            editMenu.Add("Full Name", _ => 
            { 

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
            });

            editMenu.Add("Email", _ => {
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
             });

            editMenu.Add("Age", _ => {

                while (true)
                {
                    Console.Write("Enter your age: ");
                    if (!int.TryParse(Universal.takeUserInput("Type..."), out age) || age < 0 || age >= 100)
                    {
                        Console.WriteLine("Please enter a valid age.");
                    }
                    else
                    {
                        break;
                    }
                }

            });

            editMenu.Add("Phone Number", _ => {
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
            });

            editMenu.Add("Additional Info", _ => {
                Console.WriteLine("In case of allergies or special needs that the cinema needs to know about");
                Console.WriteLine("Please write them here:");
                string userinput = Universal.takeUserInput("Type...") ?? "";
                Console.Clear();
                Console.WriteLine("Thank you, YourEyes will do their utmost best to accompany your needs. Here is what you entered: ");
                Console.WriteLine(userinput);
            });

            editMenu.Add("Confirm Changes", _ => {
                //var (fullName, age, email, phoneNumber, userinput) = UserInfoInput.InitialUserInfo();
                //DisplayInfo(fullName, age, email, phoneNumber, userinput);
                // Console.Clear();
                // Console.WriteLine("Your entered information:\n");
                // Console.WriteLine($"Full Name: {fullName}");
                // Console.WriteLine($"Age: {age}");
                // Console.WriteLine($"Email: {email}");
                // Console.WriteLine($"Phone Number: {phoneNumber}");
                // Console.WriteLine($"User Input: {userinput}");

            });
            editMenu.UseMenu();
            return editMenu.GetMenuOptionsCount() - 1;
        }

        public static void DisplayInfo(string fullName, int age, string email, string phoneNumber, string userinput)
        {
            Console.Clear();
            Console.WriteLine("Your entered information:\n");
            Console.WriteLine($"Full Name: {fullName}");
            Console.WriteLine($"Age: {age}");
            Console.WriteLine($"Email: {email}");
            Console.WriteLine($"Phone Number: {phoneNumber}");
            Console.WriteLine($"User Input: {userinput}");
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
