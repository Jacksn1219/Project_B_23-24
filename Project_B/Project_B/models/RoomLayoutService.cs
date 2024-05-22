using DataAccessLibrary;
using DataAccessLibrary.logic;
using DataAccessLibrary.models;
using Models;
using Project_B.services;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.SQLite;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Project_B;

class RoomLayoutService : LayoutModel
{
    public RoomLayoutService(RoomModel room, List<SeatModel> SeatModelList) : base(room, SeatModelList)
    {
    }
    //public Layout(RoomModel room, List<SeatModel> SeatModelList) : base(room, SeatModelList) { }

    public void drawLayout(RoomModel room)
    {
        Console.Clear();
        Console.ResetColor();

        List<SeatModel> layout = room.Seats;

        string alfabet = "abcdefghijklmnopqrstuvwxyz";
        for (int i = 1; i < room.RowWidth + 1; i++) { Console.Write("  " + alfabet[i - 1]); }

        int Row = 1;
        Console.Write("\n" + Row);
        for (int i = 0; i < layout.Count; i++)
        {
            Console.ForegroundColor = layout[i].Type switch
            {
                "Normaal" => ConsoleColor.Blue,
                "Extra Beenruimte" => ConsoleColor.DarkYellow,
                "Love SeatModel" => ConsoleColor.Magenta,
                _ => ConsoleColor.Gray
            };
            if (i % room.RowWidth == 0) { Row++; Console.Write("\n" + Row); }
            else if (layout[i].Rank == " ") { Console.Write("   "); }
            else { Console.Write($" []"); }
        }
        Console.Write($"[{room.RowWidth - 8 / 2}Screen{room.RowWidth - 8 / 2}]");
        Console.ResetColor();
    }
    public static SeatModel? selectSeatModel(RoomModel room)
    {
        List<SeatModel> layout = room.Seats;
        //drawLayout(layout, room);

        SeatModel? selectedOption = null;

        InputMenu SeatModelSelectionMenu = new InputMenu($"Soort Stoel:                  Betaal niveau (1 = laag)\n(N) = Normaal                 Blauw = Niveau 1\n(E) = Extra beenruimte        Geel = Niveau 2\n(L) = Love seat               Rood = Niveau 3\n\n [   Screen   ]", null, room.RowWidth ?? 0);
        foreach (SeatModel SeatModel in layout)
        {
            string SeatModelName = SeatModel.Type == " " ? "   " : $" []";
            SeatModelSelectionMenu.Add($"{SeatModel.Type[0]}", (x) =>
            {
                SeatModel selectedSeatModel = SeatModel;
                selectedOption = selectedSeatModel;
                Console.Clear();
            }, SeatModel.IsReserved);
        }
        SeatModelSelectionMenu.UseMenu();
        return selectedOption;
    }
    public void editLayout(SeatFactory _sf, RoomFactory _rf)
    {
        List<RoomModel> roomList = new List<RoomModel>();
        try
        {
            int i = 1;
            RoomModel? room = null;
            do
            {
                room = _rf.GetItemFromId(i);
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
                seat = _sf.GetItemFromId(i, 1);
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

        //SeatModel? selectedOption = null;

        InputMenu selectRoom = new InputMenu("useLambda", null);
        foreach (RoomModel room in roomList)
        {
            selectRoom.Add($"{room.Name}", (x) =>
            {
                room.AddSeatModels(layouts[(room.ID ?? 2) - 1].ToArray());
                editLayout(room);
                //selectedOption = RoomLayoutService.selectSeatModel(room.Seats, room);
            });
        }
        selectRoom.UseMenu(() => Universal.printAsTitle("Select room to edit"));
        //return selectedOption;
    }
    public void editLayout()
    {
        editLayout(room);
    }
    public static void editLayout(RoomModel room)
    {
        //List<SeatModel> layout = getSeatModelsFromDatabase(); - Aymane
        //Room room = getRoomFromDatabase(); - Aymane

        InputMenu SeatModelSelectionMenu = new InputMenu($"  [   Screen   ]", false, room.RowWidth ?? 0);
        //string SeatModelName;

        string getType;
        string getRank;
        foreach (SeatModel SeatModel in room.Seats)
        {
            //SeatModelName = SeatModel.Type == "" ? "   " : $" []";
            SeatModelSelectionMenu.Add($"{SeatModel.Type[0]}", (x) =>
            {
                getType = SeatModel.Type;
                getRank = SeatModel.Rank;
                SeatModel selectedSeatModel = SeatModel;
                Console.Clear();

                ConsoleKey userInput = ConsoleKey.Delete;
                while (userInput != ConsoleKey.Q)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.White;
                    switch (getType)
                    {
                        case "Normaal":
                            Console.Write("\n\nType: ");
                            Universal.WriteColor("N", ConsoleColor.Blue);
                            Console.Write(" E L");
                            break;
                        case "Extra Beenruimte":
                            Console.Write("\n\nType: N ");
                            Universal.WriteColor("E", ConsoleColor.DarkYellow);
                            Console.Write(" L");
                            break;
                        case "Love SeatModel":
                            Console.Write("\n\nType: N E ");
                            Universal.WriteColor("L", ConsoleColor.Magenta);
                            break;
                        default:
                            Console.Write("\n\nType: N E L");
                            break;
                    };
                    switch (getRank)
                    {
                        case "1":
                            Console.Write($"\nRank: ");
                            Universal.WriteColor(getRank, ConsoleColor.DarkCyan);
                            Console.Write($" 2 3");
                            break;
                        case "2":
                            Console.Write($"\nRank: 1 ");
                            Universal.WriteColor(getRank, ConsoleColor.DarkCyan);
                            Console.Write($" 3");
                            break;
                        case "3":
                            Console.Write($"\nRank: 1 2 ");
                            Universal.WriteColor(getRank, ConsoleColor.DarkCyan);
                            break;
                        default:
                            Console.Write("\nRank: 1 2 3");
                            break;
                    };
                    Console.Write("\n\nDruk op een van de volgende toetsen om het nieuwe type te selecteren (");
                    Universal.WriteColor("N", ConsoleColor.Blue);
                    Console.Write(", ");
                    Universal.WriteColor("E", ConsoleColor.DarkYellow);
                    Console.Write(", ");
                    Universal.WriteColor("L", ConsoleColor.Magenta);
                    Console.Write(", 1, 2, 3, Enter, Spatiebalk)\n\n");

                    Console.Write("Uitleg:\n  (");
                    Universal.WriteColor("N", ConsoleColor.Blue);
                    Console.Write(") = Normaal                (1) = Betaal niveau 1        (Spatiebalk) = Lege plek instellen\n  (");
                    Universal.WriteColor("E", ConsoleColor.DarkYellow);
                    Console.Write(") = Extra beenruimte       (2) = Betaal niveau 2        (Enter) = goedkeuren aanpassing\n  (");
                    Universal.WriteColor("L", ConsoleColor.Magenta);
                    Console.Write(") = Love SeatModel              (3) = Betaal niveau 3");

                    //Getting User choice
                    userInput = Console.ReadKey().Key;

                    if (new List<ConsoleKey> { ConsoleKey.N, ConsoleKey.E, ConsoleKey.L, ConsoleKey.Spacebar, ConsoleKey.NumPad1, ConsoleKey.NumPad2, ConsoleKey.NumPad3, ConsoleKey.D1, ConsoleKey.D2, ConsoleKey.D3 }.Contains(userInput))
                    {
                        try
                        {
                            getType = userInput switch
                            {
                                ConsoleKey.Spacebar => " ",
                                ConsoleKey.N => "Normaal",
                                ConsoleKey.E => "Extra Beenruimte",
                                ConsoleKey.L => "Love SeatModel",
                                _ => throw new NotImplementedException()
                            };
                        }
                        catch { }
                        try
                        {
                            getRank = userInput switch
                            {
                                ConsoleKey.Spacebar => " ",
                                ConsoleKey.D1 or ConsoleKey.NumPad1 => "1",
                                ConsoleKey.D2 or ConsoleKey.NumPad2 => "2",
                                ConsoleKey.D3 or ConsoleKey.NumPad3 => "3",
                                _ => throw new NotImplementedException()
                            };
                        }
                        catch { }
                    }
                    else if (userInput == ConsoleKey.Enter)
                    {
                        if (getType == "0" || getRank == "0") Console.WriteLine("Not all required fields are filled in...");
                        else
                        {
                            SeatModelSelectionMenu.Edit(Int32.Parse(SeatModel.Name), $"{getType[0]}");
                            room.Seats[Int32.Parse(selectedSeatModel.Name)].Type = getType;
                            room.Seats[Int32.Parse(selectedSeatModel.Name)].Rank = getRank;
                            userInput = ConsoleKey.Q;
                        }
                    }
                };
            }, false, Convert.ToInt16(SeatModel.Name));
        }
        SeatModelSelectionMenu.UseMenu();
    }
    public void editRoom()
    {
        editLayout();
    }

    public static void editLayoutPerRoom(RoomFactory rf, SeatFactory sf)
    {
        List<RoomModel> roomList = new List<RoomModel>();
        try
        {
            int i = 1;
            RoomModel? room = null;
            do
            {
                room = rf.GetItemFromId(i);
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
                seat = sf.GetItemFromId(i, 1);
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

        InputMenu selectRoom = new InputMenu("useLambda");
        foreach (RoomModel room in roomList)
        {
            selectRoom.Add($"{room.Name}", (x) =>
            {
                room.AddSeatModels(layouts[(room.ID ?? 2) - 1].ToArray());
                RoomLayoutService rls = new RoomLayoutService(room, room.Seats);
                rls.editLayout();
                rf.ItemToDb(room);
            });
        }
        selectRoom.UseMenu(() => Universal.printAsTitle("Select room to edit"));
    }

    public static void MakeNewLayout()
    {
        //Getting the correct room ID
        /*int Room_ID = getRoomsFromDatabase().Count;*/

        List<SeatModel> SeatModels = new List<SeatModel>();
        /*Room currentRoom = new Room(Room_ID, $"Room{Room_ID}", SeatModels.Count)*/
        RoomModel currentRoom = new RoomModel("Room1", SeatModels.Count, 1);

        Console.Clear();
        Console.WriteLine("  [   screen   ]");
        //Console.WriteLine("Press one of these:\nN = Normaal\nE = Extra beenruimte\nL = Love SeatModel\nRank: (1, 2, 3)\nBackspace\nEnter = 1x dan automatisch\nQ = goedkeuren volgorde.\n");

        string getType = "0";
        string getRank = "0";

        ConsoleKey userInput = ConsoleKey.Delete;
        while (userInput != ConsoleKey.Q)
        {
            Console.ForegroundColor = ConsoleColor.White;
            switch (getType)
            {
                case "Normaal":
                    Console.Write("\n\nType: ");
                    Universal.WriteColor("N", ConsoleColor.Blue);
                    Console.Write(" E L");
                    break;
                case "Extra Beenruimte":
                    Console.Write("\n\nType: N ");
                    Universal.WriteColor("E", ConsoleColor.DarkYellow);
                    Console.Write(" L");
                    break;
                case "Love SeatModel":
                    Console.Write("\n\nType: N E ");
                    Universal.WriteColor("L", ConsoleColor.Magenta);
                    break;
                default:
                    Console.Write("\n\nType: N E L");
                    break;
            };
            switch (getRank)
            {
                case "1":
                    Console.Write($"\nRank: ");
                    Universal.WriteColor(getRank, ConsoleColor.DarkCyan);
                    Console.Write($" 2 3");
                    break;
                case "2":
                    Console.Write($"\nRank: 1 ");
                    Universal.WriteColor(getRank, ConsoleColor.DarkCyan);
                    Console.Write($" 3");
                    break;
                case "3":
                    Console.Write($"\nRank: 1 2 ");
                    Universal.WriteColor(getRank, ConsoleColor.DarkCyan);
                    break;
                default:
                    Console.Write("\nRank: 1 2 3");
                    break;
            };
            Console.Write("\n\nDruk op een van de volgende toetsen (");
            Universal.WriteColor("N", ConsoleColor.Blue);
            Console.Write(", ");
            Universal.WriteColor("E", ConsoleColor.DarkYellow);
            Console.Write(", ");
            Universal.WriteColor("L", ConsoleColor.Magenta);
            Console.Write(", 1, 2, 3, Enter, Backspace, ");
            Universal.WriteColor("Q", ConsoleColor.Red);
            Console.Write(", Spatiebalk, A)\n\n");

            Console.Write("Uitleg:\n  (");
            Universal.WriteColor("N", ConsoleColor.Blue);
            Console.Write(") = Normaal                (1) = Betaal niveau 1        (Enter) = 1x dan automatisch        (Spatiebalk) = Lege plek instellen\n  (");
            Universal.WriteColor("E", ConsoleColor.DarkYellow);
            Console.Write(") = Extra beenruimte       (2) = Betaal niveau 2        (Backspace)                         (A) = Ingestelde stoel toevoegen\n  (");
            Universal.WriteColor("L", ConsoleColor.Magenta);
            Console.Write(") = Love SeatModel              (3) = Betaal niveau 3        (");
            Universal.WriteColor("Q", ConsoleColor.Red);
            Console.Write(") = goedkeuren volgorde \n");

            //Getting User choice
            userInput = Console.ReadKey().Key;

            Console.Clear();
            if (userInput == ConsoleKey.Backspace && SeatModels.Count > 0) SeatModels.RemoveAt(SeatModels.Count - 1);
            else if (userInput == ConsoleKey.Enter && currentRoom.RowWidth == 1 && SeatModels.Count > 0)
            {
                currentRoom.RowWidth = SeatModels.Count;
            }
            else if (userInput == ConsoleKey.A)
            {
                if (getType == "0" || getRank == "0") Console.WriteLine("Not all required fields are filled in...");
                else SeatModels.Add(new SeatModel($"{SeatModels.Where(s => s.RoomID == 1).Count()}", getRank, getType, currentRoom)); //SeatModels.Count,
            }
            else
            {
                try
                {
                    getType = userInput switch
                    {
                        ConsoleKey.Spacebar => " ",
                        ConsoleKey.N => "Normaal",
                        ConsoleKey.E => "Extra Beenruimte",
                        ConsoleKey.L => "Love SeatModel",
                        _ => throw new NotImplementedException()
                    };
                }
                catch { }
                try
                {
                    getRank = userInput switch
                    {
                        ConsoleKey.Spacebar => " ",
                        ConsoleKey.D1 or ConsoleKey.NumPad1 => "1",
                        ConsoleKey.D2 or ConsoleKey.NumPad2 => "2",
                        ConsoleKey.D3 or ConsoleKey.NumPad3 => "3",
                        _ => throw new NotImplementedException()
                    };
                }
                catch { }
            }
            //Console.WriteLine("Press one of these:\nN = Normaal\nE = Extra beenruimte\nL = Love SeatModel\nRank: (1, 2, 3)\nBackspace\nEnter = 1x dan automatisch\nQ = goedkeuren volgorde.\n");
            Console.WriteLine("  [   Screen   ]");

            for (int i = 0; i < SeatModels.Count; i++)
            {
                Console.ForegroundColor = SeatModels[i].Type switch
                {
                    "Normaal" => ConsoleColor.Blue,
                    "Extra Beenruimte" => ConsoleColor.DarkYellow,
                    "Love SeatModel" => ConsoleColor.Magenta,
                    _ => ConsoleColor.Gray
                };
                if (i == 0 || currentRoom.RowWidth == 1) Console.Write(SeatModels[i].Type == " " ? " []" : " " + SeatModels[i].Type[0] + SeatModels[i].Rank);
                else Console.Write((i % currentRoom.RowWidth == 0) ? (SeatModels[i].Type == " ") ? "\n []" : "\n " + SeatModels[i].Type[0] + SeatModels[i].Rank : (SeatModels[i].Type == " ") ? " []" : " " + SeatModels[i].Type[0] + SeatModels[i].Rank);
            }
        }
        //Adding the SeatModels to the database
        currentRoom.AddSeatModels(SeatModels.ToArray());

        Console.Write("The room is created ");
        Universal.WriteColor("succesfully", ConsoleColor.Green);
        Console.ReadLine();

        selectSeatModel(currentRoom);
    }

    public static SeatModel? selectSeatModelForUser(List<SeatModel> layout, RoomModel room)
    {
        SeatModel? selectedOption = null;

        InputMenu seatSelectionMenu = new InputMenu($"Select a seat:", null, room.RowWidth ?? 0);
        foreach (SeatModel seatModel in layout)
        {
            string seatInfo = $"Type: {seatModel.Type}, Rank: {seatModel.Rank}";
            seatSelectionMenu.Add(seatModel.Name, (x) =>
            {
                selectedOption = seatModel;
                Console.Clear();
                Console.WriteLine($"You've selected seat {seatModel.Name}. Please provide your information.");
                // Here you can prompt the user for their information and handle it accordingly
                string fullName;
                while (true)
                {
                    Console.Write("Enter your full name: ");
                    fullName = Universal.takeUserInput("Type...") ?? "";
                    if (IsValidFullName(fullName))
                    {
                        break;  // Exit the loop if a valid full name is entered
                    }
                    else
                    {
                        Console.WriteLine("Please enter a valid full name.");
                    }
                }
                Console.Write("Enter your email: ");
                string email = Universal.takeUserInput("Type...");

                string phoneNumber;
                while (true)
                {
                    Console.Write("Enter your phone number (starting with 0 and max 10 digits): ");
                    phoneNumber = Universal.takeUserInput("Type...") ?? "";
                    if (IsValidPhoneNumber(phoneNumber))
                    {
                        break;  // Exit the loop if a valid phone number is entered
                    }
                    else
                    {
                        Console.WriteLine("Please enter a valid phone number starting with 0 and max 10 digits.");
                    }
                }
                // Now you can use this information to reserve the seat or perform other actions


                static bool IsValidFullName(string fullName)
                {
                    return !string.IsNullOrWhiteSpace(fullName) && fullName.Replace(" ", "").All(char.IsLetter);
                }



                static bool IsValidPhoneNumber(string phoneNumber)
                {
                    // Phone number must start with '0' and have a maximum length of 10 characters
                    return phoneNumber.StartsWith("0") && phoneNumber.Length == 10 && phoneNumber.All(char.IsDigit);
                }
            }, seatModel.IsReserved);
        }
        seatSelectionMenu.UseMenu();
        return selectedOption;
    }
}
