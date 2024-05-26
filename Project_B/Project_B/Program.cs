using Models;
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
                    {"reserve seats", (x) => {rs.CreateReservation(rf); Console.ReadLine();}},
                    {"browse movies", (x) => {Console.ReadLine();}},
                    {"select seat", (x) => {rs.SelectSeat(rf);} },

                    // {"get reservation", (x) => 
                    {"Check reservation", (x) => 
                    { 
                        while(true)
                        {
                            Console.Write("Please enter your confirmation number: ");
                            string result = Console.ReadLine();
                            if (result != null && int.TryParse(result, out int reservationId))
                            {
                                rs.GetReservationById(reservationId); 
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Invalid input. Please try again");
                                break;
                            }
                            System.Console.WriteLine(ChangeColour(ConsoleColor.Red) + "invalid input, please fill in a number higher than 0" + ChangeColour(ConsoleColor.Black));
                        }
                        Console.ReadLine();}}

                },
                //admin options
                new Dictionary<string, Action<string>>(){
                    {"edit seat prices", (x) => {SeatPriceCalculator.UpdatePrices();}},
                    {"add movie", (x) => {createItems.CreateNewMovie();}},
                    {"edit movie", (x) => {createItems.ChangeMovie();}},
                    {"add timetable", (x) => {createItems.CreateTimeTable();}},
                    {"edit timetable", (x) => {createItems.EditTimeTable();}},
                    {"change room layout", (x) => {RoomLayoutService.editLayoutPerRoom(rf, sf);}},
                    {"show reserved seats", (x) => {Universal.showReservedSeats(sf, cf, reservationFactory);}}
                }
            );
        }
    }
}
