using DataAccessLibrary;
using DataAccessLibrary.logic;
using DataAccessLibrary.models;
using Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        HistoryMenu.Add(new Dictionary<string, Action<string>>{
            {"Show profits between dates", (x) => {
                DateTime startDate;
                Console.WriteLine("Enter the start date (yyyy-MM-dd):");
                DateTime.TryParse(Universal.takeUserInput("Type..."), out startDate);

                DateTime endDate;
                Console.WriteLine("\nEnter the start date (yyyy-MM-dd):");
                DateTime.TryParse(Universal.takeUserInput("Type..."), out endDate);

                Console.Clear();
                GetProfitsBetweenDates(startDate, endDate);
            }},
            {"# Show schedule between dates #", (x) => {
                DateTime startDate;
                Console.WriteLine("Enter the start date (yyyy-MM-dd):");
                DateTime.TryParse(Universal.takeUserInput("Type..."), out startDate);

                DateTime endDate;
                Console.WriteLine("\nEnter the start date (yyyy-MM-dd):");
                DateTime.TryParse(Universal.takeUserInput("Type..."), out endDate);

                _ttf.GetTimeTablesBetweenDates(startDate, endDate);
                Console.ReadLine();
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
        
        do
        {
            Console.Write("Press <Any> key to go back...");
            Thread.Sleep(700);
            Console.SetCursorPosition(Console.CursorLeft - 30, Console.CursorTop);
            if (Console.KeyAvailable) break;
            Console.Write("                              ");
            Thread.Sleep(700);
            Console.SetCursorPosition(Console.CursorLeft - 30, Console.CursorTop);
        } while (!Console.KeyAvailable);

        return Convert.ToInt32(total);
    }
}