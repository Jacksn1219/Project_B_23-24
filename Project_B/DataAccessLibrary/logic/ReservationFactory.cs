using System.Collections.Generic;
using System.Globalization;
using System.Net.Http.Headers;
using System.Xml;
using DataAccessLibrary;
using Serilog;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DataAccessLibrary.logic
{
    public class ReservationFactory : IDbItemFactory<ReservationModel>
    {
        private readonly DataAccess _db;
        private readonly CustomerFactory _cf;
        private readonly SeatFactory _sf;
        private readonly TimeTableFactory _tf;
        private readonly Serilog.Core.Logger _logger;
        public ReservationFactory(DataAccess db, CustomerFactory cf, SeatFactory sf, TimeTableFactory tf, Serilog.Core.Logger logger)
        {
            _db = db;
            _cf = cf;
            _sf = sf;
            _tf = tf;
            _logger = logger;
            CreateTable();
        }

        public bool CreateItem(ReservationModel item, int deepcopyLv = 99)
        {
            if (deepcopyLv < 0) return true;
            if (item.ID != null) throw new InvalidDataException("the reservation is already in the db.");
            if (!item.IsChanged) return true;
            bool dontClose = _db.IsOpen;
            try
            {
                _db.OpenConnection();
                //add reservation
                bool result = RelatedItemsItemDependsOnToDb(item, deepcopyLv - 1);
                if (!result) return false;
                item.ID = _db.CreateData(
                    @"INSERT INTO Reservation(
                        CustomerID,
                        TimeTableID,
                        Note
                    )
                    VALUES($1,$2,$3)",
                    new Dictionary<string, dynamic?>(){
                        {"$1", item.CustomerID },
                        {"$2", item.TimeTableID},
                        {"$3", item.Note}
                    }
                );
                result = RelatedItemsDependingOnItemToDb(item, deepcopyLv - 1);
                return item.ID > 0 && result;
            }
            catch (Exception ex)
            {
                _logger.Warning(ex, "failed to add reservation");
                return false;
            }
            finally
            {
                if (!dontClose) _db.CloseConnection();
            }
        }

        public void CreateTable()
        {
            bool dontClose = _db.IsOpen;
            try
            {
                _db.OpenConnection();
                _db.SaveData(
                    @"CREATE TABLE IF NOT EXISTS Reservation(
                        ID INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL ,
                        CustomerID INTEGER NOT NULL,
                        TimeTableID INTEGER NOT NULL,
                        Note TEXT,
                        FOREIGN KEY (CustomerID) REFERENCES Customer (ID),
                        FOREIGN KEY (TimeTableID) REFERENCES TimeTable (ID)
                    )"
                );
                _db.SaveData(
                    @"CREATE TABLE IF NOT EXISTS ReservedSeat(
                        ID INTEGER PRIMARY KEY  AUTOINCREMENT  NOT NULL ,
                        SeatID INTEGER  NOT NULL,
                        ReservationID INTEGER  NOT NULL,
                        FOREIGN KEY (SeatID) REFERENCES Seat (ID),
                        FOREIGN KEY (ReservationID) REFERENCES Reservation (ID)
                    )"
                );
            }
            catch (Exception ex)
            {
                _logger.Fatal(ex, "failed to create database tables Reservation and ReservedSeat");
                throw;
            }
            finally
            {
                if (!dontClose) _db.CloseConnection();
            }
        }

        public ReservationModel? GetItemFromId(int id, int deepcopyLv = 0)
        {
            if (deepcopyLv < 0) return null;
            try
            {
                var toReturn = _db.ReadData<ReservationModel>(
                    @"SELECT * FROM Reservation
                    WHERE ID = $1",
                    new Dictionary<string, dynamic?>(){
                        {"$1", id},
                    }
                ).First();
                getRelatedItemsFromDb(toReturn, deepcopyLv - 1);
                return toReturn;
            }
            catch (Exception ex)
            {
                _logger.Warning(ex, $"failed to get Reservation with ID {id} ");
                return null;
            }
        }

        public bool ItemToDb(ReservationModel item, int deepcopyLv = 99)
        {
            if (deepcopyLv < 0) return true;
            bool customerChanged = item.Customer != null && item.Customer.IsChanged;
            bool SeatModelsChanged = false;
            foreach (var SeatModel in item.ReservedSeats)
            {
                if (SeatModel.IsChanged)
                {
                    SeatModelsChanged = true;
                    break;
                }
            }
            if (!item.IsChanged && item.Exists && (customerChanged || SeatModelsChanged))
            {
                bool result = RelatedItemsItemDependsOnToDb(item, deepcopyLv - 1);
                if (!result) return result;
                return RelatedItemsDependingOnItemToDb(item, deepcopyLv - 1);
            }

            if (!item.IsChanged) return true;
            if (item.ID == null) return CreateItem(item, deepcopyLv);
            else return UpdateItem(item, deepcopyLv);
        }

        public bool UpdateItem(ReservationModel item, int deepcopyLv = 99)
        {
            if (deepcopyLv < 0) return true;
            if (item.ID == null) throw new InvalidDataException("the Reservation does not have a value and cannot be updated.");
            if (!item.IsChanged) return true;
            bool dontClose = _db.IsOpen;
            try
            {
                _db.OpenConnection();
                bool result = RelatedItemsItemDependsOnToDb(item, deepcopyLv - 1);
                if (!result) { return result; }
                result = _db.SaveData(
                    @"UPDATE Reservation
                    SET CustomerID = $1,
                        TimeTableID = $2,
                        Note = $3
                    WHERE ID = $4",
                    new Dictionary<string, dynamic?>(){
                        {"$1", item.CustomerID},
                        {"$2", item.TimeTableID},
                        {"$3", item.Note},
                        {"$4", item.ID}
                    }
                );
                if (result)
                {
                    item.IsChanged = false;
                    result = RelatedItemsDependingOnItemToDb(item, deepcopyLv - 1);
                }
                return result;
            }
            catch (Exception ex)
            {
                _logger.Warning(ex, $"failed to update reservation with ID {item.ID}");
                return false;
            }
            finally
            {
                if (!dontClose) _db.CloseConnection();
            }
        }
        private bool RelatedItemsItemDependsOnToDb(ReservationModel item, int deepcopyLv)
        {
            // update/add the customer
            if (item.Customer != null)
            {
                if (item.Customer.IsChanged) _cf.ItemToDb(item.Customer, deepcopyLv);
                item.CustomerID = item.Customer.ID;
            }
            return true;
        }
        private bool RelatedItemsDependingOnItemToDb(ReservationModel item, int deepcopyLv)
        {
            if (deepcopyLv < 0) return true;
            foreach (SeatModel SeatModel in item.ReservedSeats)
            {
                //try add seat
                if (!SeatModel.Exists) _sf.ItemToDb(SeatModel, deepcopyLv);
                //check if seat is already reserved in this reservation.
                try
                {
                    if (_db.ReadData<SeatModel>
                    (
                        @"SELECT ID FROM ReservedSeat
                        WHERE SeatID = $1 AND ReservationID = $2",
                        new Dictionary<string, dynamic?>(){
                            {"$1", SeatModel.ID},
                            {"$2", item.ID}
                        }
                    ).Length > 0) continue;
                    _db.SaveData(
                        @"INSERT INTO ReservedSeat(
                            SeatID,
                            ReservationID
                        )
                        VALUES($1,$2)",
                        new Dictionary<string, dynamic?>(){
                            {"$1", SeatModel.ID},
                            {"$2", item.ID}
                        }
                    );
                }
                catch (Exception ex)
                {
                    _logger.Warning(ex, $"failed to add (some) reserved seats of reservation with ID {item.ID}");
                    return false;
                }
            }
            return true;
        }

        public ReservationModel[] GetItems(int count, int page = 1, int deepcopyLv = 0)
        {
            if (deepcopyLv < 0) return Array.Empty<ReservationModel>();
            bool dontClose = _db.IsOpen;
            try
            {
                _db.OpenConnection();
                ReservationModel[] result = _db.ReadData<ReservationModel>(
                    $"SELECT * FROM Reservation LIMIT {count} OFFSET {count * page - count}"
                );
                if (deepcopyLv < 1) return result;
                foreach (ReservationModel item in result)
                {
                    getRelatedItemsFromDb(item, deepcopyLv - 1);
                }
                return result;
            }
            catch (Exception ex)
            {
                _logger.Warning(ex, "failed to get Reservations");
                return Array.Empty<ReservationModel>();
            }
            finally
            {
                if (!dontClose) _db.CloseConnection();
            }
        }
        public ReservationModel[] GetReservationsBetweenDates(int count, DateTime startDate, DateTime endDate, int page = 1, int deepcopyLv = 0)
        {
            if (deepcopyLv < 0) return Array.Empty<ReservationModel>();
            bool dontClose = _db.IsOpen;
            try
            {
                ReservationModel[] result = _db.ReadData<ReservationModel>(
                    @$"SELECT * FROM Reservation
                    INNER JOIN TimeTable ON TimeTable.ID = Reservation.TimeTableID
                    WHERE TimeTable.StartDate >= $1 AND TimeTable.StartDate <= $2
                    LIMIT $3 OFFSET { count* page - count}",
                    new()
                    {
                        { "$1", startDate.ToString(CultureInfo.InvariantCulture) },
                        { "$2", endDate.ToString(CultureInfo.InvariantCulture) },
                        { "$3", count }
                    }
                );
                if (deepcopyLv < 1) return result;
                foreach (ReservationModel item in result)
                {
                    getRelatedItemsFromDb(item, deepcopyLv - 1);
                }
                return result;
            }
            catch (Exception ex)
            {
                _logger.Warning(ex, $"failed to get Reservations from date {startDate.ToString(CultureInfo.InvariantCulture)} to {endDate.ToString(CultureInfo.InvariantCulture)}");
                return Array.Empty<ReservationModel>();
            }
            finally
            {
                if (!dontClose) _db.CloseConnection();
            }
        }
        public ReservationModel[] GetReservationsAfterDate(int count, DateTime date, int page = 1, int deepcopyLv = 0)
        {
            if (deepcopyLv < 0) return Array.Empty<ReservationModel>();
            bool dontClose = _db.IsOpen;
            try
            {
                _db.OpenConnection();
                ReservationModel[] result = _db.ReadData<ReservationModel>(
                    @$"SELECT * FROM Reservation
                    INNER JOIN TimeTable ON TimeTable.ID = Reservation.TimeTableID
                    WHERE TimeTable.StartDate >= $1
                    LIMIT $2 OFFSET {count * page - count}",
                    new()
                    {
                        { "$1", date.ToString(CultureInfo.InvariantCulture) },
                        { "$2", count }
                    }
                );
                //WHERE Reservation.StartDate >= {date.ToString(CultureInfo.InvariantCulture)}"
                //$"WHERE TimeTable.StartDate >= {DateTime.Now.ToString(CultureInfo.InvariantCulture)}"
                //(als je de timetable nog niet heb, kan je die INNER JOINen)
                /*
                @"SELECT Seat.ID, Seat.RoomID, Seat.Name, Seat.Rank, Seat.Type FROM Seat
                INNER JOIN ReservedSeat ON ReservedSeat.SeatID = Seat.ID
                */
                if (deepcopyLv < 1) return result;
                foreach (ReservationModel item in result)
                {
                    getRelatedItemsFromDb(item, deepcopyLv - 1);
                }
                return result;
            }
            catch (Exception ex)
            {
                _logger.Warning(ex, $"failed to get Reservations from date {date.ToString(CultureInfo.InvariantCulture)}");
                return Array.Empty<ReservationModel>();
            }
            finally
            {
                if (!dontClose) _db.CloseConnection();
            }
        }
        public void getRelatedItemsFromDb(ReservationModel item, int deepcopyLv = 0)
        {
            bool dontClose = _db.IsOpen;
            try
            {
                if (deepcopyLv < 0) return;
                _db.OpenConnection();
                if (item.TimeTableID != null)
                {
                    item.TimeTable = _tf.GetItemFromId(item.TimeTableID ?? 0, deepcopyLv);
                }
                item.ReservedSeats.AddRange(GetReservedSeatsFromDb(item));
                item.Customer = _cf.GetItemFromId(item.CustomerID ?? 0, deepcopyLv);
                return;
            }
            catch (Exception ex)
            {
                _logger.Warning(ex, $"failed to get related items of reservation with ID {item.ID}");
            }
            finally
            {
                if (!dontClose) _db.CloseConnection();
            }
        }

        private SeatModel[] GetReservedSeatsFromDb(ReservationModel item)
        {
            try
            {
                return _db.ReadData<SeatModel>(
                @"SELECT Seat.ID, Seat.RoomID, Seat.Name, Seat.Rank, Seat.Type FROM Seat
                INNER JOIN ReservedSeat ON ReservedSeat.SeatID = Seat.ID
                INNER JOIN Reservation ON Reservation.ID = ReservedSeat.ReservationID
                WHERE Reservation.ID = $1",
                new Dictionary<string, dynamic?>(){
                    {"$1", item.ID}
                }
            );
            }
            catch (Exception ex)
            {
                _logger.Warning(ex, $"failed to get reserved seats from reservation with ID {item.ID}");
                return Array.Empty<SeatModel>();
            }
        }

        public bool ItemsToDb(List<ReservationModel> items, int deepcopyLv = 99)
        {
            if (deepcopyLv < 0) return true;
            foreach (var item in items)
            {
                ItemToDb(item, deepcopyLv);
            }
            return true;
        }
    }

}