using DataAccessLibrary;
using DataAccessLibrary.models;
using System;
using System.Data.SQLite;
using System.IO;
using System.Text.Json;
using System.Xml.Linq;

namespace Project_B
{
    public class SQLite
    {
        /// <summary>
        /// TEMPORARY FUNCTION : Made to upload a new Layout to the database.
        /// </summary>
        /// <param name="layout"></param>
        /// <param name="room"></param>
        public static void upload_to_database(SeatModel[] SeatModels, RoomModel room)
        {
            SQLiteConnection sqlite_conn;
            sqlite_conn = CreateConnection();

            try
            {
                ExcecuteQuerry(sqlite_conn, $@"INSERT INTO Room(
                    Name,
                    Capacity,
                    RowWidth
                ) VALUES (
                    '{room.Name}',
                    {room.Capacity},
                    {room.RowWidth}
                ); ");
                foreach (SeatModel SeatModel in SeatModels)
                {
                    ExcecuteQuerry(sqlite_conn, $@"INSERT INTO SeatModel(
                    RoomID,
                    Name,
                    Rank,
                    Type
                ) VALUES (
                    {SeatModel.RoomID},
                    '{SeatModel.Name}',
                    '{SeatModel.Rank}',
                    '{SeatModel.Type}'
                ); ");
                }
            }
            catch { sqlite_conn.Close(); }
            sqlite_conn.Close();
        }
        /// <summary>
        /// Standard setup data and database tables needed for projectB.
        /// </summary>
        public static void SetupProjectB()
        {
            SQLiteConnection sqlite_conn = CreateConnection();

            try
            {
                //sqlite_conn = CreateConnection();

                //----- Creating Tables -----//
                ExcecuteQuerry(sqlite_conn, @"CREATE TABLE Author(
                    ID INTEGER PRIMARY KEY  AUTOINCREMENT  NOT NULL ,
                    Name TEXT  NOT NULL,
                    Description TEXT  NOT NULL,
                    Age INTEGER  NOT NULL
                )");
                ExcecuteQuerry(sqlite_conn, @"CREATE TABLE Director(
                    ID INTEGER PRIMARY KEY  AUTOINCREMENT  NOT NULL ,
                    Name TEXT  NOT NULL,
                    Discription TEXT  NOT NULL,
                    Age INTEGER  NOT NULL
                )");
                ExcecuteQuerry(sqlite_conn, @"CREATE TABLE Movie(
                    ID INTEGER PRIMARY KEY  AUTOINCREMENT  NOT NULL ,
                    Title TEXT  NOT NULL,
                    DirectorID INTEGER  NOT NULL,
                    pegiAge INTEGER  NOT NULL,
                    Discription TEXT  NOT NULL,
                    Genre TEXT  NOT NULL,
                    DurationInMin INTEGER  NOT NULL,
                    FOREIGN KEY (DirectorID) REFERENCES Director (ID)
                )");
                ExcecuteQuerry(sqlite_conn, @"CREATE TABLE Room(
                    ID INTEGER PRIMARY KEY  AUTOINCREMENT  NOT NULL ,
                    Name TEXT  NOT NULL,
                    Capacity INTEGER  NOT NULL,
                    RowWidth INTEGER  NOT NULL
                )");
                ExcecuteQuerry(sqlite_conn, @"CREATE TABLE TimeTable(
                    ID INTEGER PRIMARY KEY  AUTOINCREMENT  NOT NULL ,
                    MovieID INTEGER  NOT NULL,
                    RoomID INTEGER  NOT NULL,
                    StartDate TEXT  NOT NULL,
                    EndDate TEXT  NOT NULL,
                    FOREIGN KEY (MovieID) REFERENCES Movie (ID),
                    FOREIGN KEY (RoomID) REFERENCES Room (ID)
                )");
                ExcecuteQuerry(sqlite_conn, @"CREATE TABLE AuthorInMovie(
                    ID INTEGER PRIMARY KEY  AUTOINCREMENT  NOT NULL ,
                    AuthorID INTEGER  NOT NULL,
                    MovieID INTEGER  NOT NULL,
                    FOREIGN KEY (AuthorID) REFERENCES Author (ID),
                    FOREIGN KEY (MovieID) REFERENCES Movie (ID)
                )");
                ExcecuteQuerry(sqlite_conn, @"CREATE TABLE Costumer(
                    ID INTEGER PRIMARY KEY  AUTOINCREMENT  NOT NULL ,
                    Name TEXT  NOT NULL,
                    Age INTEGER  NOT NULL,
                    Email TEXT  NOT NULL,
                    PhoneNumber TEXT  NOT NULL,
                    IsSubsxribed INTEGER  NOT NULL
                )");
                ExcecuteQuerry(sqlite_conn, @"CREATE TABLE Reservation(
                    ID INTEGER PRIMARY KEY  AUTOINCREMENT  NOT NULL ,
                    CostumerID INTEGER  NOT NULL,
                    TimeTableID INTEGER  NOT NULL,
                    Note TEXT  NOT NULL,
                    FOREIGN KEY (CostumerID) REFERENCES Costumer (ID),
                    FOREIGN KEY (TimeTableID) REFERENCES TimeTable (ID)
                )");
                ExcecuteQuerry(sqlite_conn, @"CREATE TABLE SeatModel(
                    ID INTEGER PRIMARY KEY  AUTOINCREMENT  NOT NULL ,
                    RoomID INTEGER  NOT NULL,
                    Name TEXT  NOT NULL,
                    Rank TEXT  NOT NULL,
                    Type TEXT  NOT NULL,
                    FOREIGN KEY (RoomID) REFERENCES Room (ID)
                )");
                ExcecuteQuerry(sqlite_conn, @"CREATE TABLE ReservedSeatModel(
                    ID INTEGER PRIMARY KEY  AUTOINCREMENT  NOT NULL ,
                    SeatModelID INTEGER  NOT NULL,
                    ReservationID INTEGER  NOT NULL,
                    FOREIGN KEY (SeatModelID) REFERENCES SeatModel (ID),
                    FOREIGN KEY (ReservationID) REFERENCES Reservation (ID)
                )");

                //----- Inputting test data -----//
                ExcecuteQuerry(sqlite_conn, @"INSERT INTO Author(
                    Name,
                    Description,
                    Age
                ) VALUES (
                    'Test',
                    'Test',
                    1
                ); ");
                ExcecuteQuerry(sqlite_conn, @"INSERT INTO AuthorInMovie(
                    AuthorID,
                    MovieID
                ) VALUES (
                    1,
                    1
                ); ");
                ExcecuteQuerry(sqlite_conn, @"INSERT INTO Costumer(
                    Name,
                    Age,
                    Email,
                    PhoneNumber,
                    IsSubsxribed
                ) VALUES (
                    'Test',
                    1,
                    'Test',
                    'Test',
                    1
                ); ");
                ExcecuteQuerry(sqlite_conn, @"INSERT INTO Director(
                    Name,
                    Discription,
                    Age
                ) VALUES (
                    'Test',
                    'Test',
                    1
                ); ");
                ExcecuteQuerry(sqlite_conn, @"INSERT INTO Movie(
                    Title,
                    DirectorID,
                    pegiAge,
                    Discription,
                    Genre,
                    DurationInMin
                ) VALUES (
                    'Title',
                    1,
                    1,
                    'Test',
                    'Test',
                    1
                ); ");
                ExcecuteQuerry(sqlite_conn, @"INSERT INTO Reservation(
                    CostumerID,
                    TimeTableID,
                    Note
                ) VALUES (
                    1,
                    1,
                    'Test'
                ); ");
                ExcecuteQuerry(sqlite_conn, @"INSERT INTO ReservedSeatModel(
                    SeatModelID,
                    ReservationID
                ) VALUES (
                    1,
                    1
                ); ");
                /*ExcecuteQuerry(sqlite_conn, @"INSERT INTO Room(
                    Name,
                    Capacity,
                    RowWidth
                ) VALUES (
                    'Test',
                    1,
                    1
                ); ");
                ExcecuteQuerry(sqlite_conn, @"INSERT INTO SeatModel(
                    RoomID,
                    Name,
                    Rank,
                    Type
                ) VALUES (
                    1,
                    'Test',
                    'Test',
                    'Test'
                ); ");*/
                
                DateTime StartDateData = new DateTime(2024, 3, 24, 12, 0, 0);
                /*Console.WriteLine($@"
                    {StartDateData.ToString("yyyy-MM-dd HH:mm:ss")}\n
                    {StartDateData.AddMinutes(120).ToString("yyyy-MM-dd HH:mm:ss")}\n
                ");*/
                ExcecuteQuerry(sqlite_conn, $@"INSERT INTO TimeTable(
                    MovieID,
                    RoomID,
                    StartDate,
                    EndDate
                ) VALUES (
                    1,
                    1,
                    '{StartDateData.ToString("yyyy-MM-dd HH:mm:ss")}',
                    '{StartDateData.AddMinutes(120).ToString("yyyy-MM-dd HH:mm:ss")}'
                ); ");


                //Print data
                //ReadData(sqlite_conn);
                sqlite_conn.Close();
                //Console.WriteLine("\n--- 50% ---\nDatabase setup!");
            }
            catch (Exception) { 
                //Console.WriteLine("X Database setup : Something went wrong!");
            }
            finally { sqlite_conn.Close(); }
            if (true /* getRoomsFromDatabase().Count() >= 3 - Aymane */)
            {
                //Row widths Layouts
                List<int> RowWidthsLayouts = new List<int> { 12, 18, 30 };

                //Capacities Layouts
                List<int> capacitiesLayouts = new List<int> { 168, 18, 30 };

                //New room creation
                for (int i = 0; i < 3; i++) {
                    RoomModel currentRoom = new RoomModel($"Room{layout[0].RoomID}", layout.Length, layout[0].RoomID, );
                }

                //Here add seatLayout[] to rooms  using room.AddSeatModels(seatLayout[]);

                //Put the arrays here......

                /*try
                {
                    List<SeatModel[]> _layoutList = new List<SeatModel[]> { layout1, layout2, layout3 };

                    //Saving to DB
                    foreach (SeatModel[] layout in _layoutList) {
                        upload_to_database(layout, new Room(layout[0].RoomID, $"Room{layout[0].RoomID}", layout.Length, RowWidthsLayouts[layout[0].RoomID - 1]));
                    }
                    //Console.WriteLine("\n--- 100% ---\nLayout database setup!");
                }
                catch (Exception) {
                    //Console.WriteLine("X Layout database setup : Something went wrong!");
                }
                finally { sqlite_conn.Close(); }
                //Console.ReadLine();*/
            }
        }

        public static void addSeatToDatabase(int roomID, string name, string rank, string type)
        {
            SQLiteConnection sqlite_conn;
            sqlite_conn = CreateConnection();

            try
            {
                ExcecuteQuerry(sqlite_conn, @$"INSERT INTO SeatModel( RoomID, Name, Rank, Type ) VALUES (
                    {roomID}, '{name}', '{rank}', '{type}'
                ); ");
                sqlite_conn.Close();
            }
            catch
            {
                sqlite_conn.Close();
            }
        }
        public static void addSeatModelToDatabase(List<SeatModel> SeatModels)
        {
            foreach (SeatModel SeatModel in SeatModels) { addSeatModelToDatabase(SeatModel.RoomID, SeatModel.Name, SeatModel.Rank, SeatModel.Type); }
        }

        /// <summary>
        /// Create database connection
        /// </summary>
        /// <returns>SQLiteConnection object</returns>
        public static SQLiteConnection CreateConnection()
        {
            SQLiteConnection sqlite_conn;
            // Create a new database connection:
            sqlite_conn = new SQLiteConnection($"Data Source={Universal.databasePath()}\\database.db; Version = 3; New = True; Compress = True; ");
            // Open the connection:
            try { sqlite_conn.Open(); }
            catch (Exception) { }
            return sqlite_conn;
        }
        /// <summary>
        /// Create a table by excecuting the querry
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="CreateQuerry"></param>
        /// <exception cref="System.Data.SQLite.SQLiteException"></exception>
        static void ExcecuteQuerry(SQLiteConnection conn, string CreateQuerry)
        {
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = conn.CreateCommand();
            try
            {
                sqlite_cmd.CommandText = CreateQuerry;
                sqlite_cmd.ExecuteNonQuery();
            }
            catch (System.Data.SQLite.SQLiteException)
            {
                throw new System.Data.SQLite.SQLiteException();
            }
        }

        static List<List<string>> ReadData(SQLiteConnection conn)
        {
            SQLiteDataReader sqlite_datareader;
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = conn.CreateCommand();
            sqlite_cmd.CommandText = "SELECT * FROM SampleTable";

            sqlite_datareader = sqlite_cmd.ExecuteReader();
            List<List<string>> toReturn = new List<List<string>> { };
            List<string> temp = new List<string> { };
            while (sqlite_datareader.Read())
            {
                temp.Clear();
                for (int i = 0; i < sqlite_datareader.FieldCount; i++)
                {
                    temp.Add(sqlite_datareader.GetValue(i).ToString() ?? "Not null");
                }
                toReturn.Add(temp);
                //Console.WriteLine();
                //string temp = sqlite_datareader.GetTextReader(0).ReadToEnd();
                //int temp2 = sqlite_datareader.GetInt32(1);
                //Console.WriteLine(temp);
                //Console.WriteLine(temp2);
                //foreach (var item in temp) Console.WriteLine(item);
                /*
                var myreader = sqlite_datareader.GetValues(); //GetString(0);
                foreach (var key in myreader)
                {
                    Console.WriteLine(key);
                    foreach (var item in key.ToString())
                    {
                        Console.WriteLine(item);
                    }
                }*/
            }
            conn.Close();
            return toReturn;
        }
    }
}
