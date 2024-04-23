namespace DataAccessLibrary;

public class ReservedSeatModel
{//denk niet dat deze uberhaubt nodig is
    public int ID { get; }
    public int SeatModelID { get; }
    public int ReservationID { get; }
    public ReservedSeatModel(int id, int SeatModelID, int reservationID)
    {
        ID = id;
        SeatModelID = SeatModelID;
        ReservationID = reservationID;
    }
}
