using DataAccessLibrary;
using DataAccessLibrary.logic;
using DataAccessLibrary.models;
using Models;
using Project_B;
using Project_B.services;

public class ReservationService
{
    private readonly ReservationFactory _rf;
    private readonly MovieFactory _mf;
    private readonly TimeTableFactory _tf;
    public ReservationService(ReservationFactory rf, MovieFactory mf, TimeTableFactory tf)
    {
        _rf = rf;
        _mf = mf;
        _tf = tf;
    }
    //add methodes to get and add reservations
    public void CreateReservation(RoomFactory roomFactory)
    {
        //select timetable day
        DateOnly? day = GetWeekDay();
        if (day == null) return;


        //select timetable
        TimeTableModel? tt = null;

        tt = SelectTimeTableInDay(day ?? DateOnly.MaxValue);
        if (tt == null)
        {
            System.Console.WriteLine("failed to get timetable");
            Console.ReadLine();
            return;
        }

        //get reserved seats,
        
        // Loop until the user is done selecting seats
        List<SeatModel> selectedSeats = new List<SeatModel>();
        while (true)
        {
            // Select a seat
            var seat = RoomLayoutService.selectSeatModel(tt.Room);
            if (seat == null)
            {
                // No more seats available
                break;
            }

            // Add the selected seat to the list of selected seats
            selectedSeats.Add(seat);
            tt.Room.Seats[seat.ID-1 ?? 0].IsReserved = true;

            // Prompt the user if they want to select another seat
            System.Console.WriteLine("Select another seat? (Y/N)");
            ConsoleKeyInfo keyInfo = System.Console.ReadKey();
            if (keyInfo.KeyChar == 'y' || keyInfo.KeyChar == 'Y')
            {
                // User wants to select another seat
                continue;
            }
            else if (keyInfo.KeyChar == 'n' || keyInfo.KeyChar == 'N')
            {
                // User is done selecting seats
                break;
            }
            else
            {
                // Invalid input, ask again
                System.Console.WriteLine("\nInvalid input. Please enter 'Y' or 'N'.");
                continue;
            }
        }
        if (selectedSeats.Count() == 0) return;
    
        
        //get seats

        //select seats to reserve
        //ook in layout -> select Seatperroom of selectseatmodel
        if (tt != null)
        {
            if (tt.Room == null || tt.Room.Seats == null || tt.Room.Seats.Count < 1)
            {
                _tf.getRelatedItemsFromDb(tt, 89);
            }
        }
        else
        {
            // Handle the case when tt is null, if needed
            throw new ArgumentNullException(nameof(tt), "The tt object is null.");
        }
        if (tt.Room == null) { return; }

        //fill in user data
        var user = UserInfoInput.GetUserInfo(tt);
        CustomerModel cust = new CustomerModel(user.fullName, user.age, user.email, user.phoneNumber, true);

        System.Console.WriteLine(SeatPriceCalculator.ShowCalculation(selectedSeats));
        System.Console.WriteLine($"\nCreate reservation? \n {Universal.WriteColor("Once created, the reservation can NOT be cancelled!", ConsoleColor.Red)}(Y/N)");
        ConsoleKeyInfo key = System.Console.ReadKey();
        if (key.KeyChar == 'y' || key.KeyChar == 'Y')
        {
            // create reservation
            ReservationModel res = new ReservationModel(cust, tt, selectedSeats, user.userinput);
            _rf.ItemToDb(res);
            //print number
            MailService.SendEmail(res.Customer.Email, res.TimeTable.Movie.Name, res.Customer.Name, res.ID, res.TimeTable.StartDate, res.TimeTable.EndDate, res.ReservedSeats);
            Console.Clear();
            System.Console.WriteLine($"\nReservation is created!\nYour reservation number is: {res.ID}\nAn E-mail has been sent with your confirmation details!");
        }
        else System.Console.WriteLine("\nCanceled reservation!"); //if else for no
        Console.ReadLine();
    }

    public void GetReservationByNumber(int nr)
    {
        ReservationModel? res = _rf.GetItemFromId(nr);
        if (res == null) System.Console.WriteLine("reservation not found.");
        else System.Console.WriteLine(res.ToString());
    }
    public DateOnly? GetWeekDay()
    {
        //get current day
        DateTime today = DateTime.Today;
        DayOfWeek currentDay = today.DayOfWeek;
        //get amount of weekdays left
        int weekdayInt = (int)currentDay;
        if (weekdayInt < 1) weekdayInt = 1;
        DateOnly? toReturn = null;
        //create inputmenu
        InputMenu selectDay = new InputMenu("| Select a day |", null);
        //add days of week left.
        for (int i = weekdayInt; i < 7; i++)
        {
            DayOfWeek temp = (DayOfWeek)i; //temp DayOfWeekValue. (if you use i it will always be 7)
            selectDay.Add($"{temp}", (x) =>
            {
                var date = today.AddDays((int)temp - weekdayInt);
                toReturn = DateOnly.FromDateTime(date);
                //Console.ReadLine();
            });
        }
        selectDay.UseMenu();
        return toReturn;
    }
    public void GetReservationById()
    {
        bool validInput = false;
        int confirmationNumber = 0;
        while (!validInput)
        {
            Console.Write("Please enter your confirmation number: ");
            string userInput = Universal.takeUserInput("Type...");
            if (int.TryParse(userInput, out confirmationNumber))
            {
                validInput = true;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid input. Please enter a valid confirmation number.\n");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
        GetReservationById(confirmationNumber, true);
    }
    public void GetReservationById(int id, bool checkMail = false)
    {
        ReservationModel? reservation = _rf.GetItemFromId(id, 4);
        if (reservation == null)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nNo reservation found with this confirmation number");
            Console.ForegroundColor = ConsoleColor.White;
            Console.ReadKey();
            return;
        }
        if (checkMail)
        {
            bool validEmail = false;
            while (!validEmail)
            {
                Console.Write("Please enter the E-mail associated with this reservation: ");
                string emaily = Universal.takeUserInput("Type...");
                if (emaily.ToLower() != reservation.Customer.Email.ToLower())
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Sorry this is not the correct E-mail. Please try again\n");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    validEmail = true;
                }
            }
        }
        Console.Clear();
        Console.WriteLine($"These are the details of your reservation:\n");
        Console.WriteLine($"Confirmation number: {reservation.ID}");
        Console.WriteLine($"Name: {reservation.Customer.Name}");
        Console.WriteLine($"E-mail: {reservation.Customer.Email}");
        Console.WriteLine($"Phone number: {reservation.Customer.PhoneNumber}");
        Console.WriteLine($"Movie: {reservation.TimeTable.Movie.Name}");
        Console.WriteLine($"Starts at: {reservation.TimeTable.StartDate.Split(" ")[1]} - {reservation.TimeTable.EndDate.Split(" ")[1]}");
        Console.WriteLine($"Room: {reservation.TimeTable.Room.Name}");
        if (reservation.ReservedSeats != null && reservation.ReservedSeats.Count > 0)
        {
            string seats = string.Join(", ", reservation.ReservedSeats.Select(seat => seat.Name));
            Console.WriteLine($"Seats: {seats}");
        }
        else
        {
            Console.WriteLine("No seats reserved.");
        }
        Console.ReadKey();
    }

    public TimeTableModel? SelectTimeTableInDay(DateOnly weekday)
    {
        TimeTableModel? mov = null;
        InputMenu movieSelecter = new InputMenu("| Selecteer een film |", null);
        var resp = _tf.GetTimeTablesFromDate(weekday);
        TimeTableModel[] timetables = _tf.GetItems(100); //now only first 100
        foreach (TimeTableModel timeTable in timetables)
        {
            Console.Clear();
            //get movies if missing
            if (timeTable.Movie == null)
            {
                _tf.getRelatedItemsFromDb(timeTable, 40);
            }
            if (timeTable.Movie == null) continue;
            movieSelecter.Add($"Film: {timeTable.Movie.Name}", (x) =>
            {
                mov = timeTable;
            });

        }
        movieSelecter.UseMenu();
        return mov;
    }
    public void showReservedSeatsPerTimetable(RoomFactory _rf, SeatFactory sf, CustomerFactory cf, ReservationFactory reservationFactory, ReservationService rs)
    {
        //select timetable day
        DateOnly? day = GetWeekDay();
        if (day == null) return;


        //select timetable
        TimeTableModel? tt = null;

        tt = SelectTimeTableInDay(day ?? DateOnly.MaxValue);
        if (tt == null)
        {
            System.Console.WriteLine("failed to get timetable");
            Console.ReadLine();
            return;
        }
        else if (tt != null)
        {
            if (tt.Room == null || tt.Room.Seats == null || tt.Room.Seats.Count < 1)
            {
                _tf.getRelatedItemsFromDb(tt, 89);
            }
        }
        else
        {
            // Handle the case when tt is null, if needed
            throw new ArgumentNullException(nameof(tt), "The tt object is null.");
        }
        if (tt.Room == null) { return; }
        
        
        // Select a reserved seat to show the info of
        SeatModel seat = RoomLayoutService.selectReservedSeatModel(tt.Room) ?? new();

        ReservationModel[] reservationList = reservationFactory.GetItems(100, 1, 99);
        List<(int, SeatModel)> reservesSeatList = new();
        foreach (ReservationModel reservation in reservationList)
        {
            foreach (SeatModel seatItem in reservation.ReservedSeats)
                if (seatItem.ID == seat.ID && reservation.TimeTableID == tt.ID) { reservesSeatList.Add((reservation.ID ?? 0, seatItem)); }
        }

        if (reservationList.Length == 0) return;
        try
        {
            (int, SeatModel) seatInRes = reservesSeatList[0];
            rs.GetReservationById(seatInRes.Item1);
            Console.ReadLine();
        }
        catch (Exception ex) { }
    }
}