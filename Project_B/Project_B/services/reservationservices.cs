using System.Data.SQLite;
using DataAccessLibrary;
using DataAccessLibrary.logic;

namespace Project_B.services
{
    public class ReservationService
    {
        private readonly ReservationFactory _rf;
        public ReservationService(ReservationFactory rf)
        {
            _rf = rf;
        }

        public void DisplayAvailableRooms()
        {
            Console.WriteLine("Available Rooms:");
            Console.WriteLine("1. Room 1");
            Console.WriteLine("2. Room 2");
            Console.WriteLine("3. Room 3");
        }

        public void DisplayRoomLayout(RoomModel room)
        {
            RoomLayoutService model = new RoomLayoutService(room, room.Seats);
            model.drawLayout(room);
        }

        public void ReserveSeats(List<int> seatNumbers, int userAge, string fullName, string email, string phoneNumber)
        {
            try
            {
                // Fetch pegiAge of the movie associated with the selected room
                int pegiAge = 0;//GetMoviePegiAgeForRoom();

                // Check age requirement
                if (userAge < pegiAge)
                {
                    Console.WriteLine($"Warning: You are below the recommended age of {pegiAge} for this movie.");
                }

                // Check age requirement
                int ageRequirement = 18;  // Example age requirement, modify as needed
                if (userAge < ageRequirement)
                {
                    Console.WriteLine($"Warning: You are below the age requirement of {ageRequirement}.");
                    return;
                }

                foreach (var seatNumber in seatNumbers)
                {
                    //ReserveSeat(seatNumber, fullName, email, phoneNumber); // Corrected the placement of this method call
                    Console.WriteLine("Seat reserved successfully!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reserving SeatModels: {ex.Message}");
            }
        }


        private int GetMoviePegiAgeForRoom(MovieModel mov)
        {
            // Fetch pegiAge of the movie associated with the selected room from the database
            // This is a placeholder; you'd replace this with actual database fetching logic
            return (int)mov.PegiAge;
        }


        public List<SeatModel> GetAvailableSeats(RoomModel room)
        {
            // Fetch available SeatModels for the selected room from the database
            // This is a placeholder; you'd replace this with actual database fetching logic
            var availableSeatModels = new List<SeatModel>
            {
                // Sample SeatModels; replace with actual fetched data
                new SeatModel("A1", "1", "Normaal"),
                new SeatModel("A2", "1", "Normaal"),
                // ... add more SeatModels
            };

            return availableSeatModels;
        }
    }
}
