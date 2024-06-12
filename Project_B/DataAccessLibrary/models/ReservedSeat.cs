namespace DataAccessLibrary;

public class ReservedSeatModel
{//denk niet dat deze uberhaubt nodig is
    public int ID { get; }
    public int SeatModelID { get; }
    public int ReservationID { get; }
    public int Price { get; }
    public ReservedSeatModel(int id, int seatModelID, int reservationID, int price)
    {
        ID = id;
        SeatModelID = seatModelID;
        ReservationID = reservationID;
        Price = price;
    }
}
