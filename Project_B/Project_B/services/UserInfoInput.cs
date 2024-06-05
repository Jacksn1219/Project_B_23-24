using System;
using System.Data.SQLite;
using DataAccessLibrary.models;
using DataAccessLibrary;
using Models;


namespace Project_B.services
{

    public static class UserInfoInput
    {
        public static (CustomerModel?, string) GetUserInfo(TimeTableModel tt)
        {
            // Get initial user info
            // Call the InitialUserInfo method and pass the movie instance as an argument

            // var userInfo = UserInfoInput.InitialUserInfo(movie);
            if (tt.Movie == null) return (null, "");
            (CustomerModel? customer, string note) userInfo = InitialUserInfo(tt.Movie);
            if (userInfo.customer == null) return (null, "");

            // Display initial info
            DisplayUserInfo((userInfo.customer, userInfo.note)); // cannot use userInfo because of silly nullReference


            // Confirm or change info
            ConsoleKeyInfo key;

            bool continueReservation = false;

            while (!continueReservation)
            {
                Console.WriteLine("\nDo you want to confirm this information? (Y/N)");
                key = System.Console.ReadKey();
                if (key.KeyChar == 'y' || key.KeyChar == 'Y')
                {
                    continueReservation = true;
                }
                if (key.KeyChar == 'n' || key.KeyChar == 'N')
                {
                    // Change info if desired
                    userInfo = EditUserInfo((userInfo.customer, userInfo.note), (int)tt.Movie.PegiAge);
                }
                else
                {
                    System.Console.WriteLine("Invalid input.");
                }
            }

            // Return user info
            return userInfo;
        }

        public static (CustomerModel?, string) InitialUserInfo(MovieModel movie)
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



                    if ((int)movie.PegiAge > age)
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
                            return (null, "");
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
            //get note
            Console.WriteLine("In case of allergies or special needs that the cinema needs to know about");
            Console.WriteLine("Please write them here:");
            string userinput = Universal.takeUserInput("Type...") ?? "";
            //ask for rights to send email
            System.Console.WriteLine("Can YourEyes send you mail? (Y/N)");
            char resp = Console.ReadKey().KeyChar;
            bool canSendMail = resp.Equals('Y') || resp.Equals('y');
            Console.Clear();
            Console.WriteLine("Thank you, YourEyes will do their utmost best to accompany your needs. Here is what you entered: ");
            Console.WriteLine(userinput);

            return (new CustomerModel(fullName, age, email, phoneNumber, canSendMail), userinput);
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

        public static void DisplayUserInfo((CustomerModel customer, string note) info)
        {
            Console.Clear();
            Console.WriteLine("Your entered information:\n");
            Console.WriteLine($"Full Name: {info.customer.Name}");
            Console.WriteLine($"Age: {info.customer.Age}");
            Console.WriteLine($"Email: {info.customer.Email}");
            Console.WriteLine($"Phone Number: {info.customer.PhoneNumber}");
            string msg = info.customer.IsSubscribed ? "Subscribed" : "not subscribed";
            Console.WriteLine($"Subscribed to commercial mails: {msg}");
            Console.WriteLine($"Extra info: {info.note}");
        }
        public static (CustomerModel, string) EditUserInfo((CustomerModel customer, string note) options, int minAge)
        {
            // Loop until user confirms changes
            InputMenu menu = new InputMenu("Edit customer info:");
            menu.Add
            (
                new Dictionary<string, Action<string>>()
                {
                    {
                        "Full Name", (x) =>
                        {
                            while(true)
                            {
                                var resp = Universal.takeUserInput("New full name:");
                                if(resp.IsValidFullName())
                                {
                                    options.customer.Name = resp;
                                    DisplayUserInfo(options);
                                    Console.ReadKey();
                                    return;
                                }
                                else Console.WriteLine(Universal.ChangeColour(ConsoleColor.Red) + "Invalid name. Please try again." + Universal.ChangeColour(ConsoleColor.White));

                            }
                        }
                    },
                    {
                        "Email", (x) =>
                        {
                            while(true)
                            {
                                var resp = Universal.takeUserInput("New email:");
                                if(resp.IsValidEmail())
                                {
                                    options.customer.Email = resp;
                                    DisplayUserInfo(options);
                                    Console.ReadKey();
                                    return;
                                }
                                else Console.WriteLine(Universal.ChangeColour(ConsoleColor.Red) + "Invalid Email. Please try again." + Universal.ChangeColour(ConsoleColor.White));

                            }
                        }
                    },
                    {
                        "Phone number", (x) =>
                        {
                            while(true)
                            {
                                var resp = Universal.takeUserInput("New phone number:");
                                if(resp.IsValidPhoneNumber())
                                {
                                    options.customer.PhoneNumber = resp;
                                    DisplayUserInfo(options);
                                    Console.ReadKey();
                                    return;
                                }
                                else Console.WriteLine(Universal.ChangeColour(ConsoleColor.Red) + "Invalid phone number. Please try again." + Universal.ChangeColour(ConsoleColor.White));

                            }
                        }
                    },
                    {
                        "Age", (x) =>
                        {
                            while(true)
                            {
                                var resp = Universal.takeUserInput("New phone number:");
                                if(int.TryParse(resp, out int age))
                                {
                                    if(age < 0 || age > 110)
                                    {
                                        Console.WriteLine(Universal.ChangeColour(ConsoleColor.Red) + "Age to high or low. Please try again." + Universal.ChangeColour(ConsoleColor.White));
                                    }
                                    else if(age < minAge)
                                    {
                                        Universal.WriteColor($"\nWarning: This movie is not suitable for a {age}-year-old.", ConsoleColor.Red);
                                        options.customer.Age = age;
                                        break;
                                    }
                                    else {options.customer.Age = age; break;}
                                }
                                else Console.WriteLine(Universal.ChangeColour(ConsoleColor.Red) + "Invalid input. Please try again." + Universal.ChangeColour(ConsoleColor.White));
                            }
                            DisplayUserInfo(options);
                            Console.ReadKey();
                            return;
                        }
                    },
                    {
                        "Note", (x) =>
                        {
                            options.note = Universal.takeUserInput("New note: ");
                            DisplayUserInfo(options);
                            Console.ReadKey();
                            return;
                        }
                    },
                    {
                        "Allowence to send commercial emails", (x) =>
                        {
                            System.Console.WriteLine("do you want commercial emails from YourEyes? (Y/N)");
                            char result = Console.ReadKey().KeyChar;
                            options.customer.IsSubscribed = result.Equals('y') || result.Equals('Y');
                            DisplayUserInfo(options);
                            Console.ReadKey();
                            return;
                        }
                    },
                    // {
                    //     "Confirm", (x) => 
                    //     { 
                            
                    //     }
                    // }
                }
            );
            menu.UseMenu();
            return options;
        }
    }
}
