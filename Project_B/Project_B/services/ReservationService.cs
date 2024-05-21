using System.ComponentModel.Design;
using System.Formats.Tar;
using DataAccessLibrary;
using DataAccessLibrary.logic;
using DataAccessLibrary.models;
using Microsoft.Extensions.Logging;
using Models;
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
    public void CreateReservation()
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

        //fill in user data
        var user = UserInfoInput.GetUserInfo();
        //print number

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