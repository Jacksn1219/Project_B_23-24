using System.Text.Json;
using DataAccessLibrary;

namespace Project_B.services
{
    public static class SeatPriceCalculator
    {
        private const string _filePath = "SeatPrices.json";
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
        public static decimal CalculatePrice(SeatModel seat)
        {
            decimal price = 0;
            switch (seat.Rank)
            {
                case "II":
                    price += _prices.PriceTierII;
                    break;
                case "III":
                    price += _prices.PriceTierIII;
                    break;
                default:
                    price += _prices.PriceTierI;
                    break;
            }
            switch (seat.Type)
            {
                case "Extra beenruimte":
                    price += _prices.ExtraSpace;
                    break;
                case "LoveSeat":
                    price += _prices.LoveSeat;
                    break;
            }
            return price;
        }
        public static string ShowCalculation(SeatModel seat)
        {
            string toReturn = "Price:\n";
            switch (seat.Rank)
            {
                case "II":
                    toReturn += $"{seat.Rank} € {_prices.PriceTierII}";
                    break;
                case "III":
                    toReturn += $"{seat.Rank} € {_prices.PriceTierIII}";
                    break;
                default:
                    toReturn += $"{seat.Rank} € {_prices.PriceTierI}";
                    break;
            }
            switch (seat.Type)
            {
                case "Extra beenruimte":
                    toReturn += $"\n+ {seat.Type} € {_prices.ExtraSpace}";
                    break;
                case "LoveSeat":
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