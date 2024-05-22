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
        while (tt == null)
        {
            tt = SelectTimeTableInDay(day ?? DateOnly.MaxValue);
            if (tt == null)
            {
                return;
            }
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
        //fill in user data
        var user = UserInfoInput.GetUserInfo();
        CustomerModel cust = new CustomerModel(user.fullName, user.age, user.email, user.phoneNumber, true);
        // create reservation
        ReservationModel res = new ReservationModel(cust, tt, new List<SeatModel>() { seats }, user.userinput);
        _rf.ItemToDb(res);
        //print number
        System.Console.WriteLine("Reservation is created!\nYour reservation number is: " + res.ID + "\n have a great day.");

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
            selectDay.Add($"{(DayOfWeek)i}", (x) =>
            {
                var date = today.AddDays(i - weekdayInt);
                toReturn = DateOnly.FromDateTime(date);
                //Console.ReadLine();
            });
        }
        selectDay.UseMenu();
        return toReturn;
    }
    public void GetReservationById(int id)
    {
        ReservationModel? reservation = _rf.GetItemFromId(id, 4);
        if (reservation == null)
        {
            Console.Clear();
            Console.WriteLine("No reservation found with this confirmation number");
            return;
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
                _tf.getRelatedItemsFromDb(timeTable);
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