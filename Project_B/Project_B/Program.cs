﻿using Models;
using DataAccessLibrary;
using Project_B.services;
using Serilog;
using DataAccessLibrary.logic;
using Project_B.menu_s;
using System.Diagnostics;
using Microsoft.Extensions.Logging;
using static Project_B.Universal;

namespace Project_B
{
    class Program
    {
        public static void Main()
        {
            Console.Title = "YourEyes";
            Console.ResetColor();
            // setup logger and db
            using Serilog.Core.Logger logger = new LoggerConfiguration()
                .WriteTo.File("logs/dbErrors.txt", rollingInterval: RollingInterval.Day, retainedFileCountLimit: 7)
                .CreateLogger();
            using var db = new SQliteDataAccess($"Data Source={Universal.datafolderPath}\\database.db; Version = 3; New = True; Compress = True;", logger);
            //set up factories
            ActorFactory af = new(db, logger);
            DirectorFactory df = new(db, logger);
            MovieFactory mf = new(db, df, af, logger);
            SeatFactory sf = new SeatFactory(db, logger);
            RoomFactory rf = new(db, sf, logger);
            CustomerFactory cf = new CustomerFactory(db, logger);
            TimeTableFactory ttf = new TimeTableFactory(db, mf, rf, logger);
            ReservationFactory reservationFactory = new ReservationFactory(db, cf, sf, ttf, logger);

            //set up services
            CreateItems createItems = new CreateItems(af, df, mf, rf, ttf);
            RoomService roomservice = new(rf);
            ReservationService rs = new(reservationFactory, mf, ttf);

            //----- Welkom scherm -----//
            StartupMenu.UseMenu(() =>
            {
                //loaddata
                SQLite_setup.SetupProjectB(rf, mf, Universal.datafolderPath);
            });
            
            //----- main screen -----//
            MainMenu.UseMenu(
                //user options
                new Dictionary<string, Action<string>>(){
                    {"# Show schedule #", (x) => { takeUserInput("Movie title");/*not yet*/ }},
                    {"# Browse movies #", (x) => { /*not yet*/ }},
                    {"\n" + Universal.centerToScreen("Reserve seats"), (x) => {rs.CreateReservation(rf);}},
                    {"Select seat", (x) => {rs.SelectSeat(rf);}},
                    {"\n" + Universal.centerToScreen("Search reservation"), (x) => { rs.GetReservation(); Console.ReadLine(); }}
                },
                //admin options
                new Dictionary<string, Action<string>>(){
                    {"Schedule", (x) => { rs.showReservedSeatsPerTimetable(rf, sf, cf, reservationFactory, rs); }},
                    {"Reserved seats", (x) => {Universal.showReservedSeats(sf, cf, reservationFactory, rs, ttf); }},
                    {"\n" + Universal.centerToScreen("Create/Edit"), (x) => {
                        InputMenu CreateMenu = new InputMenu("useLambda");
                        CreateMenu.Add(new Dictionary<string, Action<string>>()
                        {
                            {"Add movie", (x) => {createItems.CreateNewMovie();}},
                            {"Edit movie", (x) => {createItems.ChangeMovie();}},
                            {"\n" + centerToScreen("Add timetable"), (x) => {createItems.CreateTimeTable();}},
                            {"# Edit timetable #", (x) => {/*not yet*/}},
                            {"# Add movie to timetable #", (x) => {/*not yet*/}},
                            {"\n" + centerToScreen("Edit seat prices"), (x) => {SeatPriceCalculator.UpdatePrices();}},
                            {"Change room layout", (x) => {RoomLayoutService.editLayoutPerRoom(rf, sf);}}
                        });
                        CreateMenu.UseMenu(() => Universal.printAsTitle("Create/Edit"));
                    }},
                    {"# History #", (x) => {/*not yet*/}}
                }
            );
        }
    }
}

/*
 * 18 - Als administratie wil ik graag zien hoe vol een zaal is, zodat ik kan zien of de desbetreffende film een grotere zaal nodig heeft of niet zo populair is.
 * 14 - Als administratie wil ik de gereserveerde stoelen terugzien, zodat ik de klanten naar hun stoel kan begeleiden.
 * 10 - Als klant wil ik zien welke stoelen al bezet zijn zodat ik niet per ongeluk een al gereserveerde stoel pak.
 */
