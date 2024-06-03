using DataAccessLibrary;

public class SeatPricesModel
{
    public required decimal PriceTierI { get; set; }
    public required decimal PriceTierII { get; set; }
    public required decimal PriceTierIII { get; set; }
    public required decimal LoveSeat { get; set; }
    public required decimal ExtraSpace { get; set; }
    public decimal CalculatePrice(SeatModel seat)
    {
        decimal price = 0;
        switch (seat.Rank.ToLower())
        {
            case "ii":
                price += PriceTierII;
                break;
            case "iii":
                price += PriceTierIII;
                break;
            default:
                price += PriceTierI;
                break;
        }
        switch (seat.Type.ToLower())
        {
            case "extra beenruimte":
                price += ExtraSpace;
                break;
            case "loveseat":
                price += LoveSeat;
                break;
        }
        return price;
    }
}