using System;
using System.Collections.Generic;
using System.Data.SQLite;
using DataAccessLibrary;
using Models;
using Project_B.Services;

namespace Project_B.services
{
    public static class ReservationServices
    {
        public static readonly SQLiteConnection sqlite_conn = Layout.CreateConnection();

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

        public static void ReserveSeats(int roomID, List<int> seatNumbers, int userAge, string fullName, string email, string phoneNumber)
        {
            try
            {
                // Fetch pegiAge of the movie associated with the selected room
                int pegiAge = GetMoviePegiAgeForRoom(roomID);

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
                    ReserveSeat(seatNumber, fullName, email, phoneNumber); // Corrected the placement of this method call
                    Console.WriteLine("Seat reserved successfully!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reserving SeatModels: {ex.Message}");
            }
        }


        private static int GetMoviePegiAgeForRoom(int roomID)
        {
            // Fetch pegiAge of the movie associated with the selected room from the database
            // This is a placeholder; you'd replace this with actual database fetching logic
            int pegiAge = 0;  // Placeholder for fetched pegiAge

            try
            {
                using var cmd = new SQLiteCommand($"SELECT pegiAge FROM Movie WHERE RoomID = {roomID}", sqlite_conn);
                using var reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    pegiAge = reader.GetInt32(0);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching pegiAge for room {roomID}: {ex.Message}");
            }

            return pegiAge;
        }


        public static List<SeatModel> GetAvailableSeats(int roomID)
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

        private static void ReserveSeat(int seatNumber, string fullName, string email, string phoneNumber)
        {
            // Update the seat status in the database to "Reserved" and associate with user information
            try
            {
                using var cmd = new SQLiteCommand($"UPDATE Seat SET Status = 'Reserved', FullName = '{fullName}', Email = '{email}', PhoneNumber = '{phoneNumber}' WHERE SeatID = {seatNumber}", sqlite_conn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating SeatModel {seatNumber}: {ex.Message}");
            }
        }

        public static void DisplayAvailableMovies()
        {
            Console.WriteLine("Available Movies:");
            Console.WriteLine("1. Movie A");
            Console.WriteLine("2. Movie B");
        }

    }
}
