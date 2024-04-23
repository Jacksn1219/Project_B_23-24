using DataAccessLibrary;
using System;
using System.Data.SQLite;
using System.IO;
using System.Text.Json;
using System.Xml.Linq;

namespace Project_B
{
    class SQLite
    {
        /// <summary>
        /// TEMPORARY FUNCTION : Made to upload a new Layout to the database.
        /// </summary>
        /// <param name="layout"></param>
        /// <param name="room"></param>
        public static void upload_to_database(List<Seat> seats, Room room, TimeTable timeTable, List<Movie> movies)
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
                foreach (Seat seat in seats)
                {
                    ExcecuteQuerry(sqlite_conn, $@"INSERT INTO Seat(
                    RoomID,
                    Name,
                    Rank,
                    Type
                ) VALUES (
                    {seat.RoomID},
                    '{seat.Name}',
                    '{seat.Rank}',
                    '{seat.Type}'
                ); ");
                }
                ExcecuteQuerry(sqlite_conn, $@"INSERT INTO TimeTable(
                    ID,
                    Name,
                    MovieID,
                    RoomID,
                    StartDate,
                    EndDate
                ) VALUES (
                    {timeTable.ID},
                    {timeTable.Name},
                    {timeTable.MovieID},
                    {timeTable.RoomID},
                    {timeTable.StartDate},
                    {timeTable.EndDate}
                ); ");
                foreach (Movie movie in movies)
                {
                    ExcecuteQuerry(sqlite_conn, $@"INSERT INTO Movie(
                    Title,
                    DirectorID,
                    pegiAge,
                    Discription,
                    Genre,
                    DurationInMin
                ) VALUES (
                    {movie.Title}
                    {movie.DirectorID}
                    {movie.pegiAge}
                    {movie.Discription}
                    {movie.Genre}
                    {movie.DurationInMin}
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
                ExcecuteQuerry(sqlite_conn, @"INSERT INTO ReservedSeat(
                    SeatID,
                    ReservationID
                ) VALUES (
                    1,
                    1
                ); ");
                DateTime StartDateData = new DateTime(2024, 3, 24, 12, 0, 0);
                Console.WriteLine($@"
                    {StartDateData.ToString("yyyy-MM-dd HH:mm:ss")}\n
                    {StartDateData.AddMinutes(120).ToString("yyyy-MM-dd HH:mm:ss")}\n
                ");


                //Print data
                //ReadData(sqlite_conn);
                sqlite_conn.Close();
                Console.WriteLine("\n--- 50% ---\nDatabase setup!");
            }
            catch (Exception) { Console.WriteLine("X Database setup : Something went wrong!"); }
            finally { sqlite_conn.Close(); }
            if (true /* getRoomsFromDatabase().Count() >= 3 - Aymane */)
            {
                List<Seat> layout1 = new List<Seat> {
                    new Seat(0, 1, "0", " ", " "),
                    new Seat(1, 1, "1", " ", " "),
                    new Seat(2, 1, "2", "1", "Normaal"),
                    new Seat(3, 1, "3", "1", "Normaal"),
                    new Seat(4, 1, "4", "1", "Normaal"),
                    new Seat(5, 1, "5", "1", "Normaal"),
                    new Seat(6, 1, "6", "1", "Normaal"),
                    new Seat(7, 1, "7", "1", "Normaal"),
                    new Seat(8, 1, "8", "1", "Normaal"),
                    new Seat(9, 1, "9", "1", "Normaal"),
                    new Seat(10, 1, "10", " ", " "),
                };

                List<Movie> movieLayout1 = new List<Movie> {
                    new Movie(0, "Rocky", 0, 14, "Much action and good plot", "Action", 120)
                };

                try
                {
                    List<List<Seat>> _layoutList = new List<List<Seat>> { layout1 };
                    List<List<Movie>> _movieLayoutList = new List<List<Movie>> { movieLayout1 };

                    //Saving to DB
                    List<int> RowWidthsLayouts = new List<int> { 12, 18, 30 };
                    foreach (List<Seat> layout in _layoutList) {
                        upload_to_database(layout, new Room(layout[0].RoomID, $"Room{layout[0].RoomID}", layout.Count, RowWidthsLayouts[layout[0].RoomID - 1]), new TimeTable(0, "", 0, 0, "2024-3-24 12:00:00", "2024-3-24 14:00:00"), movieLayout1);
                    }
                    Console.WriteLine("\n--- 100% ---\nLayout database setup!");
                }
                catch (Exception) { Console.WriteLine("X Layout database setup : Something went wrong!"); }
                finally { sqlite_conn.Close(); }
                Console.ReadLine();
            }
        }

        public static void addSeatToDatabase(int roomID, string name, string rank, string type)
        {
            SQLiteConnection sqlite_conn;
            sqlite_conn = CreateConnection();

            try
            {
                ExcecuteQuerry(sqlite_conn, @$"INSERT INTO Seat( RoomID, Name, Rank, Type ) VALUES (
                    {roomID}, '{name}', '{rank}', '{type}'
                ); ");
                sqlite_conn.Close();
            }
            catch
            {
                sqlite_conn.Close();
            }
        }
        public static void addSeatToDatabase(List<Seat> seats)
        {
            foreach (Seat seat in seats) { addSeatToDatabase(seat.RoomID, seat.Name, seat.Rank, seat.Type); }
        }

        public static void addMovieToDatabase(string title, int directorID, int pegiAge, string discription, string genre, int durationInMin)
        {
            SQLiteConnection sqlite_conn;
            sqlite_conn = CreateConnection();

            try
            {
                ExcecuteQuerry(sqlite_conn, @$"INSERT INTO Movie( Title, DirectorID, pegiAge, Discription, Genre, DurationInMin ) VALUES (
                    '{title}', '{directorID}', '{pegiAge}', '{discription}', '{genre}', '{durationInMin}'
                ); ");
                sqlite_conn.Close();
            }
            catch
            {
                sqlite_conn.Close();
            }
        }

        public static void addMovieToDatabase(List<Movie> movies)
        {
            foreach (Movie movie in movies) { addMovieToDatabase(movie.Title, movie.DirectorID, movie.pegiAge, movie.Discription, movie.Genre, movie.DurationInMin); }
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
                    temp.Add(sqlite_datareader.GetValue(i).ToString() ?? "Not null");
                }
                toReturn.Add(temp);
            }
            conn.Close();
            return toReturn;
        }
    }
}
