using Models;
using DataAccessLibrary;
using Project_B.services;
using Serilog;
using DataAccessLibrary.logic;
using Project_B.menu_s;
using System.Diagnostics;

namespace Project_B
{
    class Program
    {
        public static void Main()
        {

            // setup logger and db
            using var logger = new LoggerConfiguration()
                .WriteTo.File("logs/dbErrors.txt", rollingInterval: RollingInterval.Day, retainedFileCountLimit: 7)
                .CreateLogger();
            using var db = new SQliteDataAccess($"Data Source={Universal.datafolderPath}\\database.db; Version = 3; New = True; Compress = True;", logger);
            //set up factories
            ActorFactory af = new(db);
            DirectorFactory df = new(db);
            MovieFactory mf = new(db, df, af);
            SeatFactory sf = new SeatFactory(db);
            RoomFactory rf = new(db, sf);
            CustomerFactory cf = new CustomerFactory(db);
            TimeTableFactory ttf = new TimeTableFactory(db, mf, rf);
            ReservationFactory reservationFactory = new ReservationFactory(db, cf, sf, ttf);

            //set up services
            CreateItems createItems = new CreateItems(af, df, mf);
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
                    {"reserve seats", (x) => {rs.CreateReservation(); Console.ReadLine();}},
                    {"browse movies", (x) => {Console.ReadLine();}},
                    {"get reservation", (x) => {rs.GetReservationByNumber(); Console.ReadLine();}}

                },
                //admin options
                new Dictionary<string, Action<string>>(){
                    {"edit seat prices", (x) => {SeatPriceCalculator.UpdatePrices();}},
                    {"add movie", (x) => {createItems.CreateNewMovie();}},
                    {"edit movie", (x) => {createItems.ChangeMovie();}},
                    {"add timetable", (x) => {/*not yet*/}},
                    {"edit timetable", (x) => {/*not yet*/}},
                    {"change room layout", (x) => {RoomLayoutService.editLayoutPerRoom(rf, sf);}}
                }
            );

        }
    }
}

// ------ Klant menu met menu opties ------//
//             InputMenu klantMenu = new InputMenu("useLambda");


//             klantMenu.Add("\n" + Universal.centerToScreen("Reserve Seat"), (x) =>
//             {
//                 ReserveSeat()
//                 RoomModel? room = roomservice.SelectRoom("select room:");
//                 if (room == null) return;
//                 SeatModel? selectedSeat = roomservice.SelectSeatOfRoom(room, "select seat:");
//                 if (selectedSeat != null)
//                 {
//                     Console.WriteLine(selectedSeat.ToString());
//                     Console.WriteLine("Please provide your information.");

//                     // Get user information using the method from the UserInfoInput class
//                     var user = UserInfoInput.GetUserInfo();

//                     // Now you can use this information to reserve the seat or perform other actions
//                 }
//                 else
//                 {
//                     Console.WriteLine("No seat selected.");
//                 }
//                 Console.ReadLine();
//             });


//             // ------ Medewerker menu met menu opties ------//
//             InputMenu medewerkerMenu = new InputMenu("useLambda");
//             /*medewerkerMenu.Add("Timetable", (x) =>
//             {
//                 //Planning movies and edit what has been planned and See the notes made by costumers
//             });
//             medewerkerMenu.Add("Reservations", (x) =>
//             {
//                 //See created reservations for timetable movies
//             });
//             medewerkerMenu.Add("History", (x) =>
//             {
//                 //See sales per movie, week and month and be able to filter on amount of sales
//             });*/
//             medewerkerMenu.Add("Create/Edit", (x) =>
//             {
//                 //Aanmaken nieuwe room, movie, actor, director.
//                 InputMenu createMenu = new InputMenu("useLambda");

//                 createMenu.Add("Create room", (x) =>
//                 {
//                     RoomLayoutService.MakeNewLayout();
//                 });
//                 createMenu.Add("Edit room", (x) =>
//                 {

//                 });
//                 createMenu.Add("\n" + Universal.centerToScreen("Create movie"), (x) =>
//                 {
//                     createItems.CreateNewMovie();
//                 });
//                 createMenu.Add("Edit movie", (x) =>
//                 {
//                     createItems.ChangeMovie();
//                 });
//                 createMenu.UseMenu(() => Universal.printAsTitle("Create/Edit"));
//             });
//             medewerkerMenu.Add("\n" + Universal.centerToScreen("Select a seat"), (x) =>
//             {
//                 var room = roomservice.SelectRoom("select room.");
//                 if (room == null) return;
//                 var seat = roomservice.SelectSeatOfRoom(room, "select seat");
//                 System.Console.WriteLine(seat.ToString());
//                 Console.ReadLine();
//             });
//             medewerkerMenu.Add("\n" + Universal.centerToScreen("Set prices"), (x) =>
//             {
//                 var prices = SeatPriceCalculator.GetCurrentPrices();
//                 SeatPriceCalculator.WritePrices();
//                 System.Console.WriteLine("\nChange Prices? (Y/N)");
//                 char input = Console.ReadKey().KeyChar;
//                 if (input.Equals('Y') || input.Equals('y'))
//                 {
//                     bool changing = true;
//                     while (changing)
//                     {
//                         System.Console.WriteLine("type price to change: (Q to quit)");
//                         string response = Console.ReadLine() ?? "";
//                         switch (response.ToLower())
//                         {
//                             case "price tier i" or "tier i" or "i" or "1":
//                                 Console.WriteLine("type new price:");
//                                 response = Console.ReadLine() ?? "";
//                                 prices.PriceTierI = decimal.Parse(response);
//                                 break;
//                             case "price tier ii" or "tier ii" or "ii" or "2":
//                                 Console.WriteLine("type new price:");
//                                 response = Console.ReadLine() ?? "";
//                                 prices.PriceTierII = decimal.Parse(response);
//                                 break;
//                             case "price tier iii" or "tier iii" or "iii" or "3":
//                                 Console.WriteLine("type new price:");
//                                 response = Console.ReadLine() ?? "";
//                                 prices.PriceTierIII = decimal.Parse(response);
//                                 break;
//                             case "extra space" or "extra" or "space":
//                                 Console.WriteLine("type new price:");
//                                 response = Console.ReadLine() ?? "";
//                                 prices.ExtraSpace = decimal.Parse(response);
//                                 break;
//                             case "loveseat" or "love" or "love seat":
//                                 Console.WriteLine("type new price:");
//                                 response = Console.ReadLine() ?? "";
//                                 prices.LoveSeat = decimal.Parse(response);
//                                 break;
//                             case "q":
//                                 changing = false;
//                                 break;
//                         }
//                     }

//                 }
//                 SeatPriceCalculator.UpdatePrice(prices);
//                 SeatPriceCalculator.WritePrices();
//                 Console.ReadLine();

//             });
//             medewerkerMenu.Add("Get seat PRICE info", (x) =>
//             {
//                 SeatModel seat = new SeatModel("naam", "II", "loveseat");
//                 Console.WriteLine(SeatPriceCalculator.ShowCalculation(seat));
//                 Console.ReadLine();
//             });




//         }
//     }
// }
