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
        string? day = GetWeekDay();
        if (day == null) return;
        //select timetable

        TimeTableModel? tt = null;
        while (tt == null)
        {
            tt = SelectTimeTableInDay(day);
        }
        //select seats


        //fill in user data
        var user = UserInfoInput.GetUserInfo()
        //print number

    }
    public void GetReservationByNumber(int id)
    {
        ReservationModel res = _rf.GetItemFromId(id);
        System.Console.WriteLine(res.ToString());
    }
    public string? GetWeekDay()
    {
        string? toReturn = null;
        InputMenu selectDay = new InputMenu("| Selecteer een dag |");
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
        });
        selectDay.Add($"Vrijdag", (x) =>
        {
            toReturn = "Vrijdag";
        });
        selectDay.Add($"Zaterdag", (x) =>
        {
            toReturn = "Zaterdag";
        });
        selectDay.Add($"Zondag", (x) =>
        {
            toReturn = "Zondag";
        });
        selectDay.UseMenu();
        return toReturn;
    }

    private TimeTableModel? SelectTimeTableInDay(string weekday)
    {
        TimeTableModel? mov = null;
        InputMenu movieSelecter = new InputMenu("| Selecteer een film |");
        TimeTableModel[] timetables = new TimeTableModel[0]; //_tf.GetItems(Filter days)
        foreach (TimeTableModel timeTable in timetables)
        {
            Console.Clear();
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