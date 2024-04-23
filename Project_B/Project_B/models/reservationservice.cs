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
            var SeatModels = new List<SeatModel>(); // Placeholder for fetched SeatModels from the database
            var room = new RoomModel($"Room{roomID}", 10, 10); // Example room with RowWidth 10
            Layout.drawLayout(SeatModels, room);
        }

        public static void ReserveSeatModels(int roomID, List<int> SeatModelNumbers, int userAge)
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
                foreach (var SeatModelNumber in SeatModelNumbers)
                {
                    ReserveSeatModel(SeatModelNumber); // Corrected the placement of this method call
                    Console.WriteLine("SeatModel reserved successfully!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reserving SeatModels: {ex.Message}");
            }
        }

        public static List<SeatModel> GetAvailableSeatModels(int roomID)
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

        private static void ReserveSeatModel(int SeatModelNumber)
        {
            // Update the SeatModel status in the database to "Reserved"
            // This is a placeholder; you'd replace this with actual database update logic
            try
            {
                using var cmd = new SQLiteCommand($"UPDATE SeatModel SET Status = 'Reserved' WHERE SeatModelID = {SeatModelNumber}", sqlite_conn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating SeatModel {SeatModelNumber}: {ex.Message}");
            }
        }
    }
}
