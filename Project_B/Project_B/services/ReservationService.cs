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
        string? day = GetWeekDay();
        if (day == null) return;

        //select timetable
        TimeTableModel? tt = null;
        while (tt == null)
        {
            tt = SelectTimeTableInDay(day);
            if (tt == null) System.Console.WriteLine("failed to get timetable");
        }
        //get reserved seats,

        //get seats

        //select seats to reserve
        //ook in layout -> select Seatperroom of selectseatmodel

        var seats = RoomLayoutService.selectSeatModel(roomFactory.GetItemFromId(1, 3));
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

        RoomLayoutService.selectSeatModel(roomFactory.GetItemFromId(1, 3));
        var user = UserInfoInput.GetUserInfo();


    }
    public void GetReservationByNumber(int nr)
    {
        ReservationModel? res = _rf.GetItemFromId(nr);
        if (res == null) System.Console.WriteLine("reservation not found.");
        else System.Console.WriteLine(res.ToString());
    }
    public string? GetWeekDay()
    {
        string? toReturn = null;
        InputMenu selectDay = new InputMenu("| Selecteer een dag |", null);
        selectDay.Add($"Maandag", (x) =>
        {
            toReturn = "Maandag";
            Console.ReadLine();
        });
        selectDay.Add($"Dinsdag", (x) =>
        {
            toReturn = "Dinsdag";
            Console.ReadLine();
        });
        selectDay.Add($"Woensdag", (x) =>
        {
            toReturn = "Woensdag";
            Console.ReadLine();
        });
        selectDay.Add($"Donderdag", (x) =>
        {
            toReturn = "Donderdag";
            Console.ReadLine();
        });
        selectDay.Add($"Vrijdag", (x) =>
        {
            toReturn = "Vrijdag";
            Console.ReadLine();
        });
        selectDay.Add($"Zaterdag", (x) =>
        {
            toReturn = "Zaterdag";
            Console.ReadLine();
        });
        selectDay.Add($"Zondag", (x) =>
        {
            toReturn = "Zondag";
            Console.ReadLine();
        });
        selectDay.UseMenu();
        return toReturn;
    }
    public void GetReservationById(int id)
    {
        ReservationModel reservation = _rf.GetItemFromId(id, 3);
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
        Console.WriteLine($"Movie duration: {reservation.TimeTable.Movie.DurationInMin} minutes");
        Console.WriteLine($"Room: {reservation.TimeTable.Room.Name}");
        Console.WriteLine($"Seats: {reservation.ReservedSeats}");
    }

    private TimeTableModel? SelectTimeTableInDay(string weekday)
    {
        TimeTableModel? mov = null;
        InputMenu movieSelecter = new InputMenu("| Selecteer een film |", null);
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