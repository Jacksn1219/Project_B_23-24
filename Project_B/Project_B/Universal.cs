using DataAccessLibrary;
using DataAccessLibrary.logic;
using Models;

namespace Project_B;
static class Universal
{
    /// <summary>
    /// Database connection
    /// </summary>
    //public static SQliteDataAccess? Db { get; set; }

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
    public static void showReservedSeats()
    {
        SeatModelFactory seatModelFactory = new SeatModelFactory(Universal.Db);
        CustomerFactory customerFactory = new CustomerFactory(Db);
        ReservationFactory reservationFactory = new ReservationFactory(Db, customerFactory, seatModelFactory);

        List<ReservationModel> reservationList = new List<ReservationModel>();
        try
        {
            int i = 1;
            ReservationModel? reservation = new ReservationModel();
            while (reservation != null)
            {
                reservation = reservationFactory.GetItemFromId(i);
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
                seat = seatModelFactory.GetItemFromId(reservationList[i].ID ?? 1, 1);
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