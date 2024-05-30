using System.Text.Json;
using DataAccessLibrary;

namespace Project_B.services
{
    public static class SeatPriceCalculator
    {
        private static string _filePath { get { return Universal.datafolderPath + "\\SeatPrices.json"; } }
        private static SeatPricesModel _prices;
        static SeatPriceCalculator()
        {
            _prices = new SeatPricesModel()
            {
                PriceTierI = 0,
                PriceTierII = 0,
                PriceTierIII = 0,
                ExtraSpace = 0,
                LoveSeat = 0
            };
            ReadPrices();
        }
        public static void UpdatePrices()
        {
            var prices = SeatPriceCalculator.GetCurrentPrices();
            SeatPriceCalculator.WritePrices();
            System.Console.WriteLine("\nChange Prices? (Y/N)");
            char input = Console.ReadKey().KeyChar;
            if (input.Equals('Y') || input.Equals('y'))
            {
                bool changing = true;
                while (changing)
                {
                    System.Console.WriteLine("type price to change: (Q to quit)");
                    string response = Universal.takeUserInput("Type...") ?? "";
                    switch (response.ToLower())
                    {
                        case "price tier i" or "tier i" or "i" or "1":
                            Console.WriteLine("type new price:");
                            response = Universal.takeUserInput("Type...") ?? "";
                            prices.PriceTierI = decimal.Parse(response);
                            break;
                        case "price tier ii" or "tier ii" or "ii" or "2":
                            Console.WriteLine("type new price:");
                            response = Universal.takeUserInput("Type...") ?? "";
                            prices.PriceTierII = decimal.Parse(response);
                            break;
                        case "price tier iii" or "tier iii" or "iii" or "3":
                            Console.WriteLine("type new price:");
                            response = Universal.takeUserInput("Type...") ?? "";
                            prices.PriceTierIII = decimal.Parse(response);
                            break;
                        case "extra space" or "extra" or "space":
                            Console.WriteLine("type new price:");
                            response = Universal.takeUserInput("Type...") ?? "";
                            prices.ExtraSpace = decimal.Parse(response);
                            break;
                        case "loveseat" or "love" or "love seat":
                            Console.WriteLine("type new price:");
                            response = Universal.takeUserInput("Type...") ?? "";
                            prices.LoveSeat = decimal.Parse(response);
                            break;
                        case "q":
                            changing = false;
                            break;
                    }
                }

            }
            UpdatePrice(prices);
            WritePrices();
            Console.ReadLine();
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
            int[] priceCounter = new int[9];

            priceCounter[0] = seats.Where(x => x.Rank == "1" && x.Type[0] == 'N').Count();
            toReturn += ("\nNormal           | Rank 1 | € " + priceCounter[0] * _prices.PriceTierI);

            priceCounter[1] = seats.Where(x => x.Rank == "1" && x.Type[0] == 'E').Count();
            toReturn += ("\nExtra Beenruimte | Rank 1 | € " + priceCounter[1] * _prices.PriceTierII + priceCounter[1] * _prices.ExtraSpace);

            priceCounter[2] = seats.Where(x => x.Rank == "1" && x.Type[0] == 'L').Count();
            toReturn += ("\nLove seat        | Rank 1 | € " + priceCounter[2] * _prices.PriceTierIII + priceCounter[2] * _prices.LoveSeat);

            priceCounter[3] = seats.Where(x => x.Rank == "2" && x.Type[0] == 'N').Count();
            toReturn += ("\nNormal           | Rank 2 | € " + priceCounter[3] * _prices.PriceTierI);

            priceCounter[4] = seats.Where(x => x.Rank == "2" && x.Type[0] == 'E').Count();
            toReturn += ("\nExtra Beenruimte | Rank 2 | € " + priceCounter[4] * _prices.PriceTierII + priceCounter[4] * _prices.ExtraSpace);

            priceCounter[5] = seats.Where(x => x.Rank == "2" && x.Type[0] == 'L').Count();
            toReturn += ("\nLove seat        | Rank 2 | € " + priceCounter[5] * _prices.PriceTierIII + priceCounter[5] * _prices.LoveSeat);

            priceCounter[6] = seats.Where(x => x.Rank == "3" && x.Type[0] == 'N').Count();
            toReturn += ("\nNormal           | Rank 3 | € " + priceCounter[6] * _prices.PriceTierI);

            priceCounter[7] = seats.Where(x => x.Rank == "3" && x.Type[0] == 'E').Count();
            toReturn += ("\nExtra Beenruimte | Rank 3 | € " + priceCounter[7] * _prices.PriceTierII + priceCounter[7] * _prices.ExtraSpace);

            priceCounter[8] = seats.Where(x => x.Rank == "3" && x.Type[0] == 'L').Count();
            toReturn += ("\nLove seat        | Rank 3 | € " + priceCounter[8] * _prices.PriceTierIII + priceCounter[8] * _prices.LoveSeat);

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
            toReturn += $"\nTotal price: € {CalculatePrices(seats)}";*/
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
            toReturn += $"\nTotal price: € {CalculatePrice(seat)}";
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