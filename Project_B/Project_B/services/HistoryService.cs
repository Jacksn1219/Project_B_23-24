﻿using DataAccessLibrary;
using DataAccessLibrary.logic;
using DataAccessLibrary.models;
using Models;

namespace Project_B.services;
public class HistoryService
{
    private RoomFactory _rf;
    private SeatFactory _sf;
    private CustomerFactory _cf;
    private ReservationFactory _reservationFactory;
    private ReservationService _rs;
    private TimeTableFactory _ttf;

    //public TimeTableModel[] timeTables;
    public HistoryService(RoomFactory rf, SeatFactory sf, CustomerFactory cf, ReservationFactory reservationFactory, ReservationService rs, TimeTableFactory ttf)
    {
        this._rf = rf;
        this._sf = sf;
        this._cf = cf;
        this._reservationFactory = reservationFactory;
        this._rs = rs;
        this._ttf = ttf;
    }

    public void UseMenu()
    {
        InputMenu HistoryMenu = new InputMenu("useLambda");

        DateTime startDate;
        Console.WriteLine("Enter the start date (yyyy-MM-dd):");
        DateTime.TryParse(Universal.takeUserInput("Type..."), out startDate);

        DateTime endDate;
        Console.WriteLine("\nEnter the end date (yyyy-MM-dd):");
        DateTime.TryParse(Universal.takeUserInput("Type..."), out endDate);

        HistoryMenu.Add(new Dictionary<string, Action<string>>{
            {"Profits", (x) => {
                Console.Clear();
                GetProfitsBetweenDates(startDate, endDate);
            }},
            {"Schedule", (x) => {
                GetScheduleBetweenDates(startDate, endDate);
            }}
        });

        HistoryMenu.UseMenu(() => Universal.printAsTitle("History"));
    }
    public void showReservedSeatsPerTimetable() {
        _rs.showReservedSeatsPerTimetable(_rf, _sf, _cf, _reservationFactory, _rs);
    }
    private ReservationModel[] GetReservationHistory(DateTime startDate, DateTime endDate) {
        return _reservationFactory.GetReservationsBetweenDates(100, startDate, endDate, 1, 69);
        //return _tf.GetTimeTablesInRoomBetweenDates(startDate, endDate);
    }
    public int GetProfitsBetweenDates(DateTime startDate, DateTime endDate)
    {
        ReservationModel[] reservationModels = GetReservationHistory(startDate, endDate);
        List<SeatModel> seats = new List<SeatModel>();
        foreach(ReservationModel item in reservationModels)
        {
            seats.AddRange(item.ReservedSeats);
        }

        decimal total = SeatPriceCalculator.CalculatePrices(seats);
        Console.Write($"The total price over ");
        Universal.WriteColor(startDate.ToString("dd/MM/yyyy"), ConsoleColor.Blue);
        Console.Write(" till ");
        Universal.WriteColor(endDate.ToString("dd/MM/yyyy"), ConsoleColor.Blue);
        Console.Write(" is:");
        Console.WriteLine(SeatPriceCalculator.ShowCalculation(seats));

        Universal.PressAnyKeyWaiter();
        
        return Convert.ToInt32(total);
    }
    public void GetScheduleBetweenDates(DateTime startDate, DateTime endDate)
    {
        TimeTableModel[] timeTables = _ttf.GetTimeTablesBetweenDates(startDate, endDate);
        InputMenu timeTableMenu = new InputMenu($"Movies played between {startDate.ToString("dd/MM/yyyy")} and {endDate.ToString("dd/MM/yyyy")}");
        foreach (TimeTableModel timeTable in timeTables)
        {
            if (timeTable.Movie == null) _ttf.getRelatedItemsFromDb(timeTable, 69);
            timeTableMenu.Add(timeTable.Movie.Name, (x) =>
            {
                Universal.WriteColor($"{timeTable.Movie.Name}\n", ConsoleColor.Blue);
                Console.WriteLine($"\nDate: {timeTable.DateTimeStartDate.ToString("dd/MM/yyyy")}");
                Console.WriteLine($"Time: {timeTable.DateTimeStartDate.ToString("HH:mm")}\n");
                //Console.WriteLine(timeTable.Movie.SeeDescription() + "\n");
                Console.WriteLine($"Room: {timeTable.Room.Name}");

                //ReservationModel[] reservations = _reservationFactory.GetReservationsBetweenDates(100, timeTable.DateTimeStartDate, timeTable.DateTimeEndDate).Where(x => x.TimeTableID == timeTable.ID).ToArray();
                ReservationModel[] reservations = GetReservationHistory(startDate, endDate).Where(x => x.TimeTableID == timeTable.ID).ToArray();
                List<SeatModel> reservedSeats = new();
                foreach (ReservationModel reservation in reservations)
                {
                    reservedSeats.AddRange(reservation.ReservedSeats);
                }
                List<SeatModel> tempseatsList = (timeTable.Room.Seats).Where(x => x.Type != "_").Where(x => x.Type != " ").ToList();
                Console.WriteLine($"Reserved seats: {reservedSeats.Count()} / {tempseatsList.Count()}\n");
                Console.WriteLine($"Profit: €{SeatPriceCalculator.CalculatePrices(reservedSeats)}\n");

                Universal.PressAnyKeyWaiter();
            });
        }
        if (timeTableMenu.GetMenuOptionsCount() > 0) timeTableMenu.UseMenu();
        else
        {
            Console.Write("There were no planned movies between ");
            Universal.WriteColor(startDate.ToString("dd/MM/yyyy"), ConsoleColor.Blue);
            Console.Write(" and ");
            Universal.WriteColor(endDate.ToString("dd/MM/yyyy"), ConsoleColor.Blue);
            Console.Write(" time period.\n");

            Universal.PressAnyKeyWaiter();
        }
    }
}