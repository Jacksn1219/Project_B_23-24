using DataAccessLibrary;
using DataAccessLibrary.logic;
using Models;

namespace Project_B.services;
public class RoomService
{
    private readonly RoomFactory _rf;
    private readonly List<RoomModel> _rooms;
    public RoomService(RoomFactory rf)
    {
        _rf = rf;
        _rooms = new();
        GetAllRooms();
    }
    //add methodes to add / show / change rooms
    private void GetAllRooms()
    {
        int page = 1;
        _rooms.Clear();
        while (true)
        {
            var rooms = _rf.GetItems(250, page, 2);
            page++;
            if (rooms.Length < 250) break;
            _rooms.AddRange(rooms);
        }
    }
    public RoomModel? SelectRoom(string title)
    {
        InputMenu selectRoom = new InputMenu("useLambda", null);
        RoomModel? selectedOption = null;
        foreach (RoomModel room in _rooms)
        {
            selectRoom.Add($"{room.Name}", (x) =>
            {
                selectedOption = room;
            });
        }
        selectRoom.Add("return", (x) => selectedOption = null);
        selectRoom.UseMenu(() => Universal.printAsTitle(title));
        return selectedOption;
    }
    public List<SeatModel> SelectSeatsOfRoom(RoomModel room, string title)
    {
        List<SeatModel>? selectedOptions = new();
        while (true)
        {
            var seat = SelectSeatOfRoom(room, title);
            if (seat == null) break;
            selectedOptions.Add(seat);
            Console.Clear();
        }
        return selectedOptions;
    }
    public SeatModel? SelectSeatOfRoom(RoomModel room, string title)
    {
        return RoomLayoutService.selectSeatModel(room.Seats, room);
        // InputMenu seatSelectionMenu = new InputMenu(title, null, room.RowWidth ?? 0);

        // foreach (SeatModel seatModel in room.Seats)
        // {
        //     string seatInfo = $"Type: {seatModel.Type}, Rank: {seatModel.Rank}";
        //     seatSelectionMenu.Add(seatModel.Name, (x) =>
        //     {
        //         selectedOption = seatModel;
        //     });
        // }
        // seatSelectionMenu.Add("return", (x) => selectedOption = null);
        // seatSelectionMenu.UseMenu(() => Universal.printAsTitle(title));
        return selectedOption;
    }
}