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
        var seats = RoomLayoutService.selectSeatModel(tt.Room);
        if (seats == null /*||seats.Length < 1*/) return;
        //fill in user data
        var user = UserInfoInput.GetUserInfo();
        CustomerModel cust = new CustomerModel(user.fullName, user.age, user.email, user.phoneNumber, true);

        System.Console.WriteLine(SeatPriceCalculator.ShowCalculation(seats));
        System.Console.WriteLine($"\nCreate reservation? \n {Universal.WriteColor("Once created, the reservation canNOT be cancelled!", ConsoleColor.Red)}(Y/N)");
        ConsoleKeyInfo key = System.Console.ReadKey();
        if (key.KeyChar == 'y' || key.KeyChar == 'Y')
        {
            // create reservation
            ReservationModel res = new ReservationModel(cust, tt, new List<SeatModel>() { seats }, user.userinput);
            _rf.ItemToDb(res);
            //print number
            System.Console.WriteLine("Reservation is created!\nYour reservation number is: " + res.ID + "\n have a great day.");
        }
        else System.Console.WriteLine("Canceled reservation!");
        Console.ReadLine();
    }

    public void SelectSeat(RoomFactory roomFactory)
    {

        var seat = RoomLayoutService.selectSeatModel(roomFactory.GetItemFromId(1, 3));
        if (seat != null)
        {
            var user = UserInfoInput.GetUserInfo();
        }


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
        ReservationModel? reservation = _rf.GetItemFromId(confirmationNumber, 4);
        if (reservation == null)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nNo reservation found with this confirmation number");
            Console.ForegroundColor = ConsoleColor.White;
            Console.ReadKey();
            return;
        }
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

    private TimeTableModel? SelectTimeTableInDay(DateOnly weekday)
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
}