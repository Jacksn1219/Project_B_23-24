using System;
using System.Collections.Generic;
using System.Data.SQLite;
using DataAccessLibrary;
using Models;

namespace Project_B.Services
{
    public static class ReservationService
    {
        private static readonly SQLiteConnection sqlite_conn = Layout.CreateConnection();

        public static void DisplayAvailableRooms()
        {
            Console.WriteLine("Available Rooms:");
            Console.WriteLine("1. Room 1");
            Console.WriteLine("2. Room 2");
            Console.WriteLine("3. Room 3");
        }

        public static void DisplayRoomLayout(int roomID)
        {
            // Fetch and display the layout of the selected room
            // This is a placeholder; you'd replace this with actual database fetching logic

            // For now, let's use the example layout creation logic from Layout class
            var seats = new List<SeatModel>(); // Placeholder for fetched seats from the database
            var room = new RoomModel($"Room{roomID}", 10, 10); // Example room with RowWidth 10
            Layout.drawLayout(seats, room);
        }

        public static void ReserveSeats(int roomID, List<int> seatNumbers, int userAge)
        {
            // Check age requirement
            int ageRequirement = 18;  // Example age requirement, modify as needed
            if (userAge < ageRequirement)
            {
                Console.WriteLine($"Warning: You are below the age requirement of {ageRequirement}.");
                return;
            }

            try
            {
                foreach (var seatNumber in seatNumbers)
                {
                    ReserveSeat(seatNumber); // Corrected the placement of this method call
                    Console.WriteLine("Seat reserved successfully!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reserving seats: {ex.Message}");
            }
        }

        public static List<SeatModel> GetAvailableSeats(int roomID)
        {
            // Fetch available seats for the selected room from the database
            // This is a placeholder; you'd replace this with actual database fetching logic
            var availableSeats = new List<SeatModel>
            {
                // Sample seats; replace with actual fetched data
                new SeatModel("A1", "1", "Normaal"),
                new SeatModel("A2", "1", "Normaal"),
                // ... add more seats
            };

            return availableSeats;
        }

        private static void ReserveSeat(int seatNumber)
        {
            // Update the seat status in the database to "Reserved"
            // This is a placeholder; you'd replace this with actual database update logic
            try
            {
                using var cmd = new SQLiteCommand($"UPDATE Seat SET Status = 'Reserved' WHERE SeatID = {seatNumber}", sqlite_conn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating seat {seatNumber}: {ex.Message}");
            }
        }
    }
}
