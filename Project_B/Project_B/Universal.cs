﻿using DataAccessLibrary;
using DataAccessLibrary.logic;
using Models;

namespace Project_B;
public static class Universal
{
    /// <summary>
    /// basic check if mail is valid. (totaly not stolen from the internet)
    /// </summary>
    /// <param name="email">ur email</param>
    /// <returns>true if probably valid, else false</returns>
    public static bool IsValidEmail(this string? email)
    {
        if (email == null) return true;
        var trimmedEmail = email.Trim();

        if (trimmedEmail.EndsWith("."))
        {
            return false;
        }
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == trimmedEmail;
        }
        catch
        {
            return false;
        }
    }
    /// <summary>
    /// checks if the string is a valid full name
    /// </summary>
    /// <param name="fullName">the name</param>
    /// <returns>true if valid, else false</returns>
    public static bool IsValidFullName(this string? fullName)
    {
        if (fullName == null) return false;
        return !string.IsNullOrWhiteSpace(fullName) && fullName.Replace(" ", "").All(char.IsLetter);
    }


    public static bool IsStartDate(this string? date, int mins, out DateTime? toReturn)
    {
        toReturn = null;
        if (DateTime.TryParse(date, out DateTime startDate))
        {
            var endDate = startDate.AddMinutes(mins);
            if (startDate.Date > DateTime.Now.Date &&
                startDate.TimeOfDay >= new TimeSpan(10, 0, 0) &&
                endDate.TimeOfDay <= new TimeSpan(22, 0, 0))
            {
                toReturn = startDate;
                return true;
            }
            else
            {
                Console.WriteLine(Universal.WriteColor("The start date must be in the future, between 10:00 and 22:00, and the movie must end by 22:00. Please enter a valid date (yyyy-MM-dd HH:mm):", ConsoleColor.Red));
            }
        }
        else
        {
            Console.WriteLine(Universal.WriteColor("Invalid date format. Please enter the start date (yyyy-MM-dd HH:mm):", ConsoleColor.Red));
        }
        return false;
    }

    /// <summary>
    /// checks if the phonenumber is valid
    /// </summary>
    /// <param name="phoneNumber">the phonenumber as string</param>
    /// <returns>true if valid, else false.</returns>
    public static bool IsValidPhoneNumber(this string? phoneNumber)
    {
        // Phone number must start with '0' and have a maximum length of 10 characters
        if (phoneNumber == null) return false;
        return phoneNumber.StartsWith("0") && phoneNumber.Length == 10 && phoneNumber.All(char.IsDigit);
    }

    public static string printAsTitle(string input)
    {
        input = input.Trim();
        string toAdd = "";
        for (int i = 0; i < Console.WindowWidth / 2 / 4 - (input.Length + 6) / 2 - 1; i++) toAdd += "-";
        WriteColor(toAdd + "== ", ConsoleColor.Blue);
        Console.Write(input);
        WriteColor(" ==" + toAdd, ConsoleColor.Blue);
        return ""; // (toAdd + input + toAdd).Substring(0, 119);
    }
    public static string centerToScreen(string input)
    {
        string toAdd = "";
        for (int i = 0; i < Console.WindowWidth / 2 / 4 - (input.Length + 6) / 2 - 1 + 4; i++) toAdd += " ";
        return toAdd + input + toAdd;
    }
    public static string ChangeColour(ConsoleColor colour)
    {
        Console.ForegroundColor = colour;
        return "";
    }
    public static string WriteColor(string toPrint, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.Write(toPrint);
        Console.ForegroundColor = ConsoleColor.White;
        return "";
    }
    private static void setupFolder(string folderName) => System.IO.Directory.CreateDirectory(System.IO.Path.GetFullPath(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\" + folderName)));
    public static string datafolderPath
    {
        get
        {
            setupFolder("DataSource");
            return System.IO.Path.GetFullPath(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\DataSource"));
        }
    }
    public static void showReservedSeats(SeatFactory _sf, CustomerFactory _cf, ReservationFactory _rf)
    {

        List<ReservationModel> reservationList = new List<ReservationModel>();
        try
        {
            int i = 1;
            ReservationModel? reservation = new ReservationModel();
            while (reservation != null)
            {
                reservation = _rf.GetItemFromId(i);
                if (reservation != null) reservationList.Add(reservation);
                i++;
            }
        }
        catch { }

        List<SeatModel> reservesSeatList = new List<SeatModel>();
        try
        {
            int i = 1;
            SeatModel? seat = new SeatModel();
            while (seat != null)
            {
                seat = _sf.GetItemFromId(reservationList[i].ID ?? 1, 1);
                if (seat != null) reservesSeatList.Add(seat);
                i++;
            }
        }
        catch { }

        InputMenu showReservedSeatMenu = new InputMenu("useLambda");
        foreach (SeatModel seat in reservesSeatList)
        {
            showReservedSeatMenu.Add($"| {seat.Room} | {seat.Name}", (x) => { Console.WriteLine(seat.ToString()); });
        }
        showReservedSeatMenu.UseMenu(() => printAsTitle("Select a seat to show"));
    }
}