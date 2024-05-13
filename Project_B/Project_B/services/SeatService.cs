using DataAccessLibrary.logic;

public class SeatService
{
    private readonly SeatFactory _sf;
    public SeatService(SeatFactory sf)
    {
        _sf = sf;
    }
}