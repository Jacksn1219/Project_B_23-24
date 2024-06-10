using Models;
using DataAccessLibrary;
using Project_B.services;
using Serilog;
using DataAccessLibrary.logic;
using Project_B.menu_s;
using static Project_B.Universal;

namespace Project_B
{
    class Program
    {
        public static void Main()
        {
            Console.Title = "YourEyes";
            //fixes ? euro to € euro
            Console.OutputEncoding = System.Text.Encoding.Unicode;
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
            CreateItems createItems = new CreateItems(af, df, mf, rf, ttf, reservationFactory);
            RoomService roomservice = new(rf);
            ReservationService rs = new(reservationFactory, mf, ttf, rf);
            HistoryService hs = new HistoryService(rf, sf, cf, reservationFactory, rs, ttf);

            //----- Welkom scherm -----//
            StartupMenu.UseMenu(() =>
            {
                //loaddata
                SQLite_setup.SetupProjectB(rf, Universal.datafolderPath);
            });

            //----- main screen -----//
            MainMenu.UseMenu(
                //user options
                new Dictionary<string, Action<string>>(){
                    {"Schedule", (x) => {createItems.DisplayTable();}},
                    {"Browse movies", (x) => {createItems.browseMovies();}},
                    {"\n" + Universal.centerToScreen("Reserve seats"), (x) => {rs.CreateReservation();}},
                    {"\n" + Universal.centerToScreen("Search reservation"), (x) => {rs.GetReservationById();}}
                },
                //admin options
                new Dictionary<string, Action<string>>(){
                    {"Schedule", (x) => { rs.showReservedSeatsPerTimetable(rf, sf, cf, reservationFactory, rs); }},
                    {"Reserved seats", (x) => {Universal.showReservedSeats(sf, cf, reservationFactory, rs, ttf); }},
                    {"\n" + centerToScreen("Create/Edit"), (x) => {
                        InputMenu CreateMenu = new InputMenu("useLambda");
                        CreateMenu.Add(new Dictionary<string, Action<string>>()
                        {
                            {"Add Movie", (x) => {createItems.CreateNewMovie();}},
                            {"Edit Movie", (x) => {createItems.EditMovie();}},
                            {"Remove Movie", (x) => {createItems.DeleteMovie();}},
                            {"\n" + centerToScreen("Plan a movie"), (x) => {createItems.CreateTimeTable();}},
                            {"Edit planned movie", (x) => {createItems.EditTimeTable();}},
                            {"\n" + centerToScreen("Add Director"), (x) => {createItems.CreateDirector();}},
                            {"Edit Director", (x) => {createItems.EditDirector();}},
                            {"\n" + centerToScreen("Add Actor"), (x) => {createItems.CreateActor();}},
                            {"Edit Actor", (x) => {createItems.EditActor();}},
                            {"\n" + centerToScreen("Edit seat prices"), (x) => {SeatPriceCalculator.UpdatePrices();}},
                            {"Change room layout", (x) => {RoomLayoutService.editLayoutPerRoom(rf, sf);}}
                        });
                        CreateMenu.UseMenu(() => Universal.printAsTitle("Create/Edit"));
                    }},
                    {"History", (x) => { hs.UseMenu(); }}
                }
            );
        }
    }
}
