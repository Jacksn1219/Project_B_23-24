using System.Reflection.Metadata.Ecma335;
using System.Text.Json;
using DataAccessLibrary;
using Models;
using static Project_B.Universal;

namespace Project_B.services
{
    public static class SeatPriceCalculator
    {
        private static string _filePath { get { return datafolderPath + "\\SeatPrices.json"; } }
        private static SeatPricesModel _prices;
        static SeatPriceCalculator()
        {
            _prices = new SeatPricesModel()
            {
                PriceTierI = 10,
                PriceTierII = 20,
                PriceTierIII = 30,
                ExtraSpace = 5,
                LoveSeat = 10
            };
            ReadPrices();
        }
        public static void UpdatePrices()
        {

            WritePrices();
            System.Console.WriteLine();
            PressAnyKeyWaiter();
            InputMenu PricesMenu = new("Change prices:");
            SeatPricesModel prices = GetCurrentPrices();
            PricesMenu.Add(new Dictionary<string, Action<string>>()
            {
                {
                    "Price tier I", (x) =>
                    {
                        while(true){
                            string? resp = takeUserInput("Fill in New price:");
                            if (resp == null) return;
                            if(decimal.TryParse(resp, out var price))
                            {
                                prices.PriceTierI = price;
                                UpdatePrice(prices);
                                WritePrices();
                                Console.ReadKey();
                                break;
                            }
                            else
                            {
                                Console.WriteLine(ChangeColour(ConsoleColor.DarkRed) + "Invalid input. \n Please fill in a (floating point) number." + ChangeColour(ConsoleColor.White));
                            }
                        }

                    }
                },
                {
                    "Price tier II", (x) =>
                    {

                        while(true){
                            string? resp = takeUserInput("Fill in New price:");
                            if (resp == null) return;
                            if(decimal.TryParse(resp, out var price))
                            {
                                prices.PriceTierII = price;
                                UpdatePrice(prices);
                                WritePrices();
                                Console.ReadKey();
                                break;
                            }
                            else
                            {
                                Console.WriteLine(ChangeColour(ConsoleColor.DarkRed) + "Invalid input. \n Please fill in a (floating point) number." + ChangeColour(ConsoleColor.White));
                            }
                        }
                    }
                },
                {
                    "Price tier III", (x) =>
                    {

                        while(true){
                            string resp = takeUserInput("Fill in New price:");
                            if (resp == null) return;
                            if(decimal.TryParse(resp, out var price))
                            {
                                prices.PriceTierIII = price;
                                UpdatePrice(prices);
                                WritePrices();
                                Console.ReadKey();
                                break;
                            }
                            else
                            {
                                Console.WriteLine(ChangeColour(ConsoleColor.DarkRed) + "Invalid input. \n Please fill in a (floating point) number." + ChangeColour(ConsoleColor.White));
                            }
                        }
                    }
                },
                {
                    "Extra space", (x) =>
                    {
                        while(true){
                            string resp = takeUserInput("Fill in New price:");
                            if (resp == null) return;
                            if(decimal.TryParse(resp, out var price))
                            {
                                prices.ExtraSpace = price;
                                UpdatePrice(prices);
                                WritePrices();
                                Console.ReadKey();
                                break;
                            }
                            else
                            {
                                Console.WriteLine(ChangeColour(ConsoleColor.DarkRed) + "Invalid input. \n Please fill in a (floating point) number." + ChangeColour(ConsoleColor.White));
                            }
                        }
                    }
                },
                {
                    "LoveSeat", (x) =>
                    {
                        while(true){
                            string? resp = takeUserInput("Fill in New price:");
                            if (resp == null) return;
                            if(decimal.TryParse(resp, out var price))
                            {
                                prices.LoveSeat = price;
                                UpdatePrice(prices);
                                WritePrices();
                                Console.ReadKey();
                                break;
                            }
                            else
                            {
                                Console.WriteLine(ChangeColour(ConsoleColor.DarkRed) + "Invalid input. \n Please fill in a (floating point) number." + ChangeColour(ConsoleColor.White));
                            }
                        }
                    }
                }
            });
            PricesMenu.UseMenu();
            UpdatePrice(prices);

        }
        public static void ReadPrices()
        {
            try
            {
                _prices = JsonSerializer.Deserialize<SeatPricesModel>(
                File.ReadAllText(_filePath) ?? ""
                ) //return new seatmodel if the serialiser returns null
                ?? new SeatPricesModel()
                {
                    PriceTierI = 0,
                    PriceTierII = 0,
                    PriceTierIII = 0,
                    ExtraSpace = 0,
                    LoveSeat = 0
                };
            }
            catch { }

        }
        public static void WritePrices()
        {
            System.Console.WriteLine($"\ncurrent prices:\n\nPrice tier I: € {_prices.PriceTierI}\nPrice tier II: € {_prices.PriceTierII}\nPrice tier III: € {_prices.PriceTierIII}\nExtra space costs: € {_prices.ExtraSpace}\nLoveseat costs: € {_prices.LoveSeat}");
        }
        public static SeatPricesModel GetCurrentPrices()
        {//return a copy of the current prices model.
            return new SeatPricesModel()
            {
                PriceTierI = _prices.PriceTierI,
                PriceTierII = _prices.PriceTierII,
                PriceTierIII = _prices.PriceTierIII,
                LoveSeat = _prices.LoveSeat,
                ExtraSpace = _prices.ExtraSpace,
            };
        }
        public static void UpdatePrice(SeatPricesModel prices)
        {
            _prices = prices;
            File.WriteAllText
            (
                _filePath,
                JsonSerializer.Serialize(_prices)
            );
        }
        public static decimal CalculatePrices(List<SeatModel> seats)
        {
            return seats.Sum(CalculatePrice);
        }

        public static decimal CalculatePrice(SeatModel seat)
        {
            decimal price = 0;
            switch (seat.Rank.ToLower())
            {
                case "ii":
                    price += _prices.PriceTierII;
                    break;
                case "iii":
                    price += _prices.PriceTierIII;
                    break;
                default:
                    price += _prices.PriceTierI;
                    break;
            }
            switch (seat.Type.ToLower())
            {
                case "extra beenruimte":
                    price += _prices.ExtraSpace;
                    break;
                case "loveseat":
                    price += _prices.LoveSeat;
                    break;
            }
            return price;
        }
        public static string ShowCalculation(List<SeatModel> seats)
        {
            string toReturn = "\n";
            decimal[] priceCounter = new decimal[9];

            int temp = seats.Where(x => x.Rank == "1" && x.Type[0] == 'N').Count();
            priceCounter[0] = temp * _prices.PriceTierI;
            if (priceCounter[0] > 0) toReturn += ($"\n{temp} x Normal           | Rank 1 | € " + priceCounter[0]);

            temp = seats.Where(x => x.Rank == "1" && x.Type[0] == 'E').Count();
            priceCounter[1] = temp * _prices.PriceTierII + temp * _prices.ExtraSpace;
            if (priceCounter[1] > 0) toReturn += ($"\n{temp} x Extra Beenruimte | Rank 1 | € " + priceCounter[1]);

            temp = seats.Where(x => x.Rank == "1" && x.Type[0] == 'L').Count();
            priceCounter[2] = temp *_prices.PriceTierIII + temp * _prices.LoveSeat;
            if (priceCounter[2] > 0) toReturn += ($"\n{temp} x Love seat        | Rank 1 | € " + priceCounter[2]);

            temp = seats.Where(x => x.Rank == "2" && x.Type[0] == 'N').Count();
            priceCounter[3] = temp * _prices.PriceTierI;
            if (priceCounter[3] > 0) toReturn += ($"\n\n{temp} x Normal           | Rank 2 | € " + priceCounter[3]);

            temp = seats.Where(x => x.Rank == "2" && x.Type[0] == 'E').Count();
            priceCounter[4] = temp * _prices.PriceTierII + temp * _prices.ExtraSpace;
            if (priceCounter[4] > 0) toReturn += ($"\n{temp} x Extra Beenruimte | Rank 2 | € " + priceCounter[4]);

            temp = seats.Where(x => x.Rank == "2" && x.Type[0] == 'L').Count();
            priceCounter[5] = temp * _prices.PriceTierIII + temp * _prices.LoveSeat;
            if (priceCounter[5] > 0) toReturn += ($"\n{temp} x Love seat        | Rank 2 | € " + priceCounter[5]);

            priceCounter[6] = seats.Where(x => x.Rank == "3" && x.Type[0] == 'N').Count() * _prices.PriceTierI;
            if (priceCounter[6] > 0) toReturn += ($"\n\n{temp} x Normal           | Rank 3 | € " + priceCounter[6]);

            temp = seats.Where(x => x.Rank == "3" && x.Type[0] == 'E').Count();
            priceCounter[7] = temp * _prices.PriceTierII + temp * _prices.ExtraSpace;
            if (priceCounter[7] > 0) toReturn += ($"\n{temp} x Extra Beenruimte | Rank 3 | € " + priceCounter[7]);

            temp = seats.Where(x => x.Rank == "3" && x.Type[0] == 'L').Count();
            priceCounter[8] = temp * _prices.PriceTierIII + temp * _prices.LoveSeat;
            if (priceCounter[8] > 0) toReturn += ($"\n{temp} x Love seat        | Rank 3 | € " + priceCounter[8]);

            toReturn += $"\n\nTotal                     | € {priceCounter.Sum()}";

            /*foreach (var seat in seats)
            {
                switch (seat.Rank.ToLower())
                {
                    case "ii":
                        toReturn += $"{seat.Rank} € {_prices.PriceTierII}";
                        break;
                    case "iii":
                        toReturn += $"{seat.Rank} € {_prices.PriceTierIII}";
                        break;
                    default:
                        toReturn += $"{seat.Rank} € {_prices.PriceTierI}";
                        break;
                }
                switch (seat.Type.ToLower())
                {
                    case "extra beenruimte":
                        toReturn += $"\n+ {seat.Type} € {_prices.ExtraSpace}";
                        break;
                    case "loveseat":
                        toReturn += $"\n+ {seat.Type} € {_prices.ExtraSpace}";
                        break;
                }
            }
            toReturn += $"\nTotal price: € {CalculatePrices(seats)}";
            return toReturn;
        }
        public static string ShowCalculation(SeatModel seat)
        {
            string toReturn = "Price:\n";
            switch (seat.Rank.ToLower())
            {
                case "2":
                    toReturn += $"Seat II: € {_prices.PriceTierII}";
                    break;
                case "3":
                    toReturn += $"Seat III: € {_prices.PriceTierIII}";
                    break;
                default:
                    toReturn += $"Seat I: € {_prices.PriceTierI}";
                    break;
            }
            switch (seat.Type.ToLower())
            {
                case "extra beenruimte":
                    toReturn += $"\n+ {seat.Type} € {_prices.ExtraSpace}";
                    break;
                case "loveseat":
                    toReturn += $"\n+ {seat.Type} € {_prices.ExtraSpace}";
                    break;
            }
            toReturn += $"\nTotal price: € {CalculatePrice(seat)}";*/
            return toReturn;
        }
    }
    public class SeatPricesModel
    {
        public required decimal PriceTierI { get; set; }
        public required decimal PriceTierII { get; set; }
        public required decimal PriceTierIII { get; set; }
        public required decimal LoveSeat { get; set; }
        public required decimal ExtraSpace { get; set; }
    }
}