namespace DataAccessLibrary;

public class ReservedSeat
{//denk niet dat deze uberhaubt nodig is
    public int ID { get; }
    public int SeatID { get; }
    public int ReservationID { get; }
    public ReservedSeat(int id, int seatID, int reservationID)
    {
        ID = id;
        SeatID = seatID;
        ReservationID = reservationID;
    }
}
