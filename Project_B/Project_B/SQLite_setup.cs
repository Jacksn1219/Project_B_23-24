using DataAccessLibrary;
using System;
using System.Data.SQLite;
using System.Xml.Linq;

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

                //Creating Tables
                ExcecuteQuerry(sqlite_conn, "CREATE TABLE SampleTable(Col1 VARCHAR(20), Col2 INT)");
                ExcecuteQuerry(sqlite_conn, "CREATE TABLE SampleTable1(Col1 VARCHAR(20), Col2 INT)");
                ExcecuteQuerry(sqlite_conn, "CREATE TABLE SampleTable2(Col1 VARCHAR(20), Col2 INT)");

                //Create starting data
                ExcecuteQuerry(sqlite_conn, "INSERT INTO SampleTable(Col1, Col2) VALUES('Test Text ', 1); ");
                ExcecuteQuerry(sqlite_conn, "INSERT INTO SampleTable(Col1, Col2) VALUES('Test1 Text1 ', 2); ");
                ExcecuteQuerry(sqlite_conn, "INSERT INTO SampleTable(Col1, Col2) VALUES('Test2 Text2 ', 3); ");

                ExcecuteQuerry(sqlite_conn, "INSERT INTO SampleTable1(Col1, Col2) VALUES('Test3 Text3 ', 3); ");

                ExcecuteQuerry(sqlite_conn, "INSERT INTO SampleTable2(Col1, Col2) VALUES('Test 4       ', 3); ");

                //Print data
                //ReadData(sqlite_conn);
                sqlite_conn.Close();
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
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
