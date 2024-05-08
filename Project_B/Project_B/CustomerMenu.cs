using Project_B;
using Models;
using Project_B.services;
using System.Text.RegularExpressions;
public static class CustomerMenu
{
    public static InputMenu klantMenu;
    static CustomerMenu()
    {
        // ------ Klant menu met menu opties ------//
        klantMenu = new InputMenu("useLambda");
        /*klantMenu.Add("Movies", (x) =>
        {
            //Show all movies that are in the timetable and load timetable from only the selected movie
        });
        klantMenu.Add("Schedule", (x) =>
        {
            //Show the timetable and the book ticket
        });*/
        try
        {
            klantMenu.Add("Reserve Seat", (x) =>
            {
                int selectedMovieID;

                while (true)
                {
                    // Display available movies
                    ReservationServices.DisplayAvailableMovies();

                    // Ask user to choose a movie
                    Console.Write("Choose a movie (1 or 2): ");
                    if (int.TryParse(Console.ReadLine(), out selectedMovieID) && (selectedMovieID == 1 || selectedMovieID == 2))
                    {
                        break;  // Exit the loop if a valid movie is selected
                    }
                    else
                    {
                        Console.WriteLine("Invalid movie selection. Please choose either 1 or 2.");
                    }
                }

                // Ask for user's information
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

                // Ask for user's age
                int userAge;
                while (true)
                {
                    Console.Write("Enter your age: ");
                    if (int.TryParse(Console.ReadLine(), out userAge) && userAge > 0 && userAge <= 100)
                    {
                        break;  // Exit the loop if a valid age is entered
                    }
                    else
                    {
                        Console.WriteLine("Please enter a valid age between 1 and 100.");
                    }
                }

                // Ask for user's email
                string email;
                while (true)
                {
                    Console.Write("Enter your email: ");
                    email = Console.ReadLine() ?? "";
                    if (IsValidEmail(email))
                    {
                        break;  // Exit the loop if a valid email is entered
                    }
                    else
                    {
                        Console.WriteLine("Please enter a valid email.");
                    }
                }

                // Ask for user's phone number
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

                // Ask user to choose a room
                int selectedRoomID;
                while (true)
                {
                    Console.Write("Choose a room (1, 2, or 3): ");
                    if (int.TryParse(Console.ReadLine(), out selectedRoomID) && selectedRoomID >= 1 && selectedRoomID <= 3)
                    {
                        break;  // Exit the loop if a valid room is selected
                    }
                    else
                    {
                        Console.WriteLine("Invalid room selection.");
                    }
                }

                // Display the layout of the selected room
                ReservationServices.DisplayRoomLayout(selectedRoomID);

                // Display available seats
                var availableSeats = ReservationServices.GetAvailableSeats(selectedRoomID);
                Console.WriteLine("Available Seats:");
                foreach (var seat in availableSeats)
                {
                    Console.WriteLine($"Seat ID: {seat.ID}, Room ID: {seat.ID}, Name: {seat.Name}, Rank: {seat.Rank}, Type: {seat.Type}");
                }

                // Ask user to choose seats
                Console.WriteLine("Enter seat numbers to reserve (comma-separated):");
                var input = Console.ReadLine() ?? "";
                var seatNumbers = new List<int>();

                foreach (var seatNumber in input.Split(','))
                {
                    if (int.TryParse(seatNumber.Trim(), out int num))
                    {
                        seatNumbers.Add(num);
                    }
                }

                // Reserve seats
                ReservationServices.ReserveSeats(selectedRoomID, seatNumbers, userAge, fullName, email, phoneNumber);

                Console.ReadLine();
            });
        }
        catch (Exception)
        {
        }
    }
    static bool IsValidFullName(string fullName)
    {
        return !string.IsNullOrWhiteSpace(fullName) && fullName.Replace(" ", "").All(char.IsLetter);
    }
    static bool IsValidEmail(string email)
    {
        return Regex.IsMatch(email, @"^[a-zA-Z0-9._%+-]+@(gmail\.com|hotmail\.com|outlook\.com|hotmail\.nl)$");
    }
    static bool IsValidPhoneNumber(string phoneNumber)
    {
        // Phone number must start with '0' and have a maximum length of 10 characters
        return phoneNumber.StartsWith("0") && phoneNumber.Length == 10 && phoneNumber.All(char.IsDigit);
    }
    public static void UseMenu(Action printmenu)
    {
        klantMenu.UseMenu(printmenu);
    }
}