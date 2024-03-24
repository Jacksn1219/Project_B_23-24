﻿using System.Data.SQLite;

namespace Project_B
{
    class SQLite
    {
        /// <summary>
        /// Standard setup data and database tables needed for projectB.
        /// </summary>
        public static void SetupProjectB()
        {
            SQLiteConnection sqlite_conn;

            try
            {
                sqlite_conn = CreateConnection();

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
                    DirectorID INTEGER  NOT NULL,
                    pegiAge INTEGER  NOT NULL,
                    Discription TEXT  NOT NULL,
                    Genre TEXT  NOT NULL,
                    DurationInSec INTEGER  NOT NULL,
                    FOREIGN KEY (DirectorID) REFERENCES Director (ID)
                )");
                ExcecuteQuerry(sqlite_conn, @"CREATE TABLE Room(
                    ID INTEGER PRIMARY KEY  AUTOINCREMENT  NOT NULL ,
                    Name TEXT  NOT NULL
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
                ExcecuteQuerry(sqlite_conn, @"CREATE TABLE Seat(
                    ID INTEGER PRIMARY KEY  AUTOINCREMENT  NOT NULL ,
                    RoomID INTEGER  NOT NULL,
                    Name TEXT  NOT NULL,
                    Rank TEXT  NOT NULL,
                    Type TEXT  NOT NULL,
                    FOREIGN KEY (RoomID) REFERENCES Room (ID)
                )");
                ExcecuteQuerry(sqlite_conn, @"CREATE TABLE ReservedSeat(
                    ID INTEGER PRIMARY KEY  AUTOINCREMENT  NOT NULL ,
                    SeatID INTEGER  NOT NULL,
                    ReservationID INTEGER  NOT NULL,
                    FOREIGN KEY (SeatID) REFERENCES Seat (ID),
                    FOREIGN KEY (ReservationID) REFERENCES Reservation (ID)
                )");

                //----- Inputting starting data -----//
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
                    DirectorID,
                    pegiAge,
                    Discription,
                    Genre,
                    DurationInSec
                ) VALUES (
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
                ExcecuteQuerry(sqlite_conn, @"INSERT INTO ReservedSeat(
                    SeatID,
                    ReservationID
                ) VALUES (
                    1,
                    1
                ); ");
                ExcecuteQuerry(sqlite_conn, @"INSERT INTO Room(
                    Name
                ) VALUES (
                    'Test'
                ); ");
                ExcecuteQuerry(sqlite_conn, @"INSERT INTO Seat(
                    RoomID,
                    Name,
                    Rank,
                    Type
                ) VALUES (
                    1,
                    'Test',
                    'Test',
                    'Test'
                ); ");
                ExcecuteQuerry(sqlite_conn, @"INSERT INTO TimeTable(
                    MovieID,
                    RoomID,
                    StartDate,
                    EndDate
                ) VALUES (
                    1,
                    1,
                    'Test',
                    'Test'
                ); ");

                //Print data
                //ReadData(sqlite_conn);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }
        /// <summary>
        /// Create folder if it doesnt exsist
        /// </summary>
        /// <param name="folderName"></param>
        public static void setupFolder(string folderName) => System.IO.Directory.CreateDirectory(System.IO.Path.GetFullPath(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\" + folderName)));
        private static string databasePath
        {
            get { setupFolder("DataSource"); return System.IO.Path.GetFullPath(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\DataSource")); }
        }
        /// <summary>
        /// Create database connection
        /// </summary>
        /// <returns>SQLiteConnection object</returns>
        static SQLiteConnection CreateConnection()
        {
            SQLiteConnection sqlite_conn;
            // Create a new database connection:
            sqlite_conn = new SQLiteConnection($"Data Source={databasePath}\\database.db; Version = 3; New = True; Compress = True; ");
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
                    temp.Add(sqlite_datareader.GetValue(i).ToString());
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
