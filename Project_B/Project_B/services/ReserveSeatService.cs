using DataAccessLibrary;
using Models;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;

namespace Project_B.services
{
    public class ReserveSeatService
    {
        private readonly UserSeatSelection _uss;
        public ReserveSeatService(UserSeatSelection uss)
        {
            _uss = uss;
        }
        public SeatModel? ReserveSeatForUser()
        {
            SeatModel? selectedSeat = _uss.SelectSeatForUser();
            if (selectedSeat != null)
            {
                // Handle seat selection logic here
                Console.WriteLine($"You've selected seat {selectedSeat.Name}. Please provide your information.");

                // Here you can prompt the user for their information and handle it accordingly
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

                // Now you can use this information to reserve the seat or perform other actions
                // ReservationServices.ReserveSeat(selectedSeat, fullName, email, phoneNumber);

                return selectedSeat; // Return the selected seat model
            }
            else
            {
                // Handle case when no seat is selected
                Console.WriteLine("No seat selected.");
                return null; // Return null to indicate failure
            }
        }

        static bool IsValidFullName(string fullName)
        {
            return !string.IsNullOrWhiteSpace(fullName) && fullName.Replace(" ", "").All(char.IsLetter);
        }

        static bool IsValidPhoneNumber(string phoneNumber)
        {
            // Phone number must start with '0' and have a maximum length of 10 characters
            return phoneNumber.StartsWith("0") && phoneNumber.Length == 10 && phoneNumber.All(char.IsDigit);
        }
    }
}