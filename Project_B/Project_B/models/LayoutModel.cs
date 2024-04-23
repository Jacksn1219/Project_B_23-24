using DataAccessLibrary;

class LayoutModel
{
    RoomModel room { get; set; }
    List<SeatModel> SeatModelList { get; set; }
    public LayoutModel(RoomModel room, List<SeatModel> SeatModelList)
    {
        this.room = room;
        this.SeatModelList = (SeatModelList == null) ? new List<SeatModel>() : SeatModelList;
    }
}