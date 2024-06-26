﻿using System.Globalization;
using DataAccessLibrary;
using DataAccessLibrary.logic;
using DataAccessLibrary.models;
using Models;

namespace Project_B;
public static class Universal
{
    public static string MenuFollower = "";

    /// <summary>
    /// the datetime format of the user based on his culture
    /// </summary>
    public static string UserDateTimeFormat { get => CultureInfo.CurrentUICulture.DateTimeFormat.ShortDatePattern + " " + CultureInfo.CurrentUICulture.DateTimeFormat.ShortTimePattern; }

    /// <summary>
    /// Asks the user to fill in a date & time until a valid datetime format is filled in.
    /// </summary>
    /// <returns>the DateTime the user filled in</returns>
    public static DateTime GetDateTimeFromUser()
    {
        while (true)
        {
            try
            {
                Console.WriteLine($"Fill in the date & time (in this format: {UserDateTimeFormat})");
                return DateTime.Parse(Console.ReadLine() ?? "", CultureInfo.CurrentUICulture);
            }
            catch
            {
                System.Console.WriteLine(ChangeColour(ConsoleColor.Red) + "Invalid datetime format!" + ChangeColour(ConsoleColor.White));
            }

        }
    }

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

    // public static void IsValidPegiAge(MovieModel pegiage)
    // {
    //     int convertToInt = (int)PEGIAge.PEGI18;
    //     int convertToInt2 = (int)PEGIAge.PEGI16;
    //     int convertToInt3 = (int)PEGIAge.PEGI12;
    //     int convertToInt4 = (int)PEGIAge.PEGI7;
    //     int convertToInt5 = (int)PEGIAge.PEGI3;
    // }

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
    public static string WriteBackgroundColor(string toPrint, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.Write(toPrint);
        Console.ForegroundColor = ConsoleColor.White;
        return "";
    }
    public static string? takeUserInput(string question)
    {
        Console.CursorVisible = true;

        // Setting colors
        Console.BackgroundColor = ConsoleColor.DarkGray;
        Console.ForegroundColor = ConsoleColor.White;

        // Writing the hovering message
        Console.Write(" " + question);
        for (int i = question.Length; i < 29; i++) Console.Write(" ");
        Console.SetCursorPosition(Console.CursorLeft - 29, Console.CursorTop);

        // Waiting for the user input and if receifed redraw an empty input field
        while (!Console.KeyAvailable) { }
        Console.Write("                             ");
        Console.SetCursorPosition(Console.CursorLeft - 29, Console.CursorTop);
        (int, int) tempMouseLocation = (Console.CursorLeft - 1, Console.CursorTop);

        // Userinput
        string? userInput = ""; //Console.ReadLine() ??
        ConsoleKeyInfo temp = default(ConsoleKeyInfo);
        do
        {
            while (!Console.KeyAvailable) { }
            Console.SetCursorPosition(Console.CursorLeft - (userInput.Length), Console.CursorTop);
            temp = Console.ReadKey(true);
            if (temp.Key == ConsoleKey.Backspace && userInput.Length > 0)
            {
                List<char> tempUserInput = userInput.ToList();
                Console.Write("                             ");
                if (userInput.Length > 29) for (int i = 29; i < userInput.Length; i++) { Console.Write(" "); }
                Console.SetCursorPosition(Console.CursorLeft - 29, Console.CursorTop);
                if (userInput.Length > 29) Console.SetCursorPosition(Console.CursorLeft - (userInput.Length - 29), Console.CursorTop);
                tempUserInput.RemoveAt(tempUserInput.Count - 1);
                userInput = String.Join("", tempUserInput);
            }
            else if (temp.Key == ConsoleKey.Escape)
            {
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;
                Console.CursorVisible = false;
                return null;
            }
            else if (temp.Key != ConsoleKey.Backspace && temp.Key != ConsoleKey.Escape && Char.IsAscii(temp.KeyChar))
            {
                if (temp.KeyChar == '\0' && temp.Modifiers == 0) userInput += "'";
                else if (temp.KeyChar == '\0') userInput += "\"";
                else userInput += $"{temp.KeyChar}";
            }
            Console.Write(userInput);
        } while (temp.Key != ConsoleKey.Enter && temp.Key != ConsoleKey.Escape);

        // "\r" weghalen. I know no better solution ;D
        List<char> tempList = userInput.ToList();
        tempList.RemoveAt(tempList.Count - 1);
        //tempList.RemoveAt(tempList.Count - 1);
        userInput = String.Join("", tempList);

        if (userInput == "")
        {
            Console.SetCursorPosition(tempMouseLocation.Item1, tempMouseLocation.Item2);
            userInput = takeUserInput("You have to type something...");
            Console.CursorVisible = false;
            if (userInput == null) return null;
        }

        // Reseting the color
        Console.BackgroundColor = ConsoleColor.Black;
        Console.CursorVisible = false;
        return userInput;
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
    public static void showReservedSeats(SeatFactory _sf, CustomerFactory _cf, ReservationFactory _rf, ReservationService rs, TimeTableFactory _ttf)
    {
        ReservationModel[] reservationList = _rf.GetItems(100, 1, 99);
        //ReservationModel[] reservationList = _rf.GetReservationsAfterDate(100, DateTime.Now, 1, 99);
        List<(int, SeatModel)> reservesSeatList = new();
        foreach (ReservationModel reservation in reservationList)
        {
            foreach (SeatModel seat in reservation.ReservedSeats)
                reservesSeatList.Add((reservation.ID ?? 0, seat));
        }
        InputMenu showReservedSeatMenu = new InputMenu("Select a seat to show");
        foreach ((int, SeatModel) seat in reservesSeatList)
        {
            TimeTableModel? timetable = _ttf.GetItemFromId(_rf.GetItemFromId(seat.Item1, 99).TimeTableID ?? 1);
            if (timetable != null)
                showReservedSeatMenu.Add($"{timetable.DateTimeStartDate} | {seat.Item2.RoomID} | {seat.Item2.Name}", (x) => { rs.GetReservationById(seat.Item1); });
            //else showReservedSeatMenu.Add($" | {seat.Item2.RoomID} | {seat.Item2.Name}", (x) => { rs.GetReservationById(seat.Item1); Console.ReadLine(); });
        }
        showReservedSeatMenu.UseMenu((title) => printAsTitle(title));
    }
    public static void PressAnyKeyWaiter()
    {
        ConsoleKey key;
        do
        {
            Console.Write("Press <Any> key to continue...");
            Thread.Sleep(700);
            Console.SetCursorPosition(Console.CursorLeft - 30, Console.CursorTop);
            if (Console.KeyAvailable) break;
            Console.Write("                              ");
            Thread.Sleep(700);
            Console.SetCursorPosition(Console.CursorLeft - 30, Console.CursorTop);
        } while (!Console.KeyAvailable);
        key = Console.ReadKey(true).Key;
    }
}