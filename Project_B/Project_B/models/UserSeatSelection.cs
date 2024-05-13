using System;
using System.Collections.Generic;
using DataAccessLibrary;
using DataAccessLibrary.logic;
using Project_B.services;

namespace Project_B.services
{
    public static class UserSeatSelection
    {
        public static SeatModel? SelectSeatForUser()
        {
            List<RoomModel> roomList = new List<RoomModel>();
            SeatModelFactory seatModelFactory = new SeatModelFactory(Universal.Db);
            RoomFactory roomFactory = new RoomFactory(Universal.Db, seatModelFactory);
            try
            {
                int i = 1;
                RoomModel? room = null;
                do
                {
                    room = roomFactory.GetItemFromId(i);
                    if (room != null) roomList.Add(room);
                    i++;
                } while (room != null);
            }
            catch { }

            List<SeatModel> seatList = new List<SeatModel>();
            try
            {
                int i = 1;
                SeatModel? seat = new SeatModel();
                while (seat != null)
                {
                    seat = seatModelFactory.GetItemFromId(i);
                    if (seat != null) seatList.Add(seat);
                    i++;
                }
            }
            catch { }

            List<List<SeatModel>> layouts = new List<List<SeatModel>>();
            foreach (SeatModel seat in seatList)
            {
                if (seat == null) continue;
                else if (seat.RoomID > layouts.Count) layouts.Add(new List<SeatModel> { seat });
                else layouts[(seat.RoomID ?? 0) - 1].Add(seat);
            }

            SeatModel? selectedOption = null;

            Models.InputMenu selectRoom = new Models.InputMenu("useLambda", null);
            foreach (RoomModel room in roomList)
            {
                selectRoom.Add($"{room.Name}", (x) => {
                    room.AddSeatModels(layouts[(room.ID ?? 2) - 1].ToArray());
                    selectedOption = Layout.selectSeatModelForUser(room.SeatModels, room);
                });
            }
            selectRoom.UseMenu(() => Universal.printAsTitle("Select your seat"));
            return selectedOption;
        }
    }
}
