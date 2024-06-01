using DataAccessLibrary;
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
                TimeTableModel[] timeTables = _ttf.GetTimeTablesBetweenDates(startDate, endDate);
                InputMenu timeTableMenu = new InputMenu($"Movies played between {startDate.ToString("dd/MM/yyyy")} and {endDate.ToString("dd/MM/yyyy")}");
                foreach (TimeTableModel timeTable in timeTables)
                {
                    if (timeTable.Movie == null) _ttf.getRelatedItemsFromDb(timeTable, 69);
                    timeTableMenu.Add(timeTable.Movie.Name, (x) =>
                    {
                        Console.WriteLine(timeTable.Room.Name + "\n");

                        ReservationModel[] reservedSeats = GetReservationHistory(startDate, endDate).Where(x => x.TimeTableID == timeTable.ID).ToArray();
                        Console.Write($"amount of (reserved)seats:\n{timeTable.Room.Capacity} / {reservedSeats.Count()}");

                        Console.WriteLine(timeTable.Movie.SeeDescription());
                        Console.ReadLine();
                    });
                }
                timeTableMenu.UseMenu();
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
        
        return Convert.ToInt32(total);
    }
}