using DataAccessLibrary;

class LayoutModel
{
    public RoomModel room { get; set; }
    public List<SeatModel> SeatModelList { get; set; }
    public LayoutModel(RoomModel room, List<SeatModel> SeatModelList)
    {
        this.room = room;
        this.SeatModelList = (SeatModelList == null) ? new List<SeatModel>() : SeatModelList;
    }
}