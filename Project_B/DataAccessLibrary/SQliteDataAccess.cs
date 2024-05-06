using System.Data;
using System.Data.SQLite;

namespace DataAccessLibrary
{
    public class SQliteDataAccess : DataAccess
    {
        /// <summary>
        /// the SQLiteConnection
        /// </summary>
        protected override IDbConnection _dbAccess { get; set; }

        /// <summary>
        /// the constructor. takes a connectionstring to create a SQLiteConnection
        /// </summary>
        /// <param name="connectionString">the connectionstring of the sqlite db</param>
        public SQliteDataAccess(string connectionString)
        {
            _dbAccess = new SQLiteConnection(connectionString);
        }
        /// <summary>
        /// dispose the SQLiteDataAccess.
        /// </summary>
        public override void Dispose()
        {
            _dbAccess.Dispose();
        }
        /// <summary>
        /// saves the data inside the sql statement. only use internaly, can be used for sql-injection
        /// </summary>
        /// <param name="sqlStatement">the query to execute</param>
        /// <returns>true if data saved, else false</returns>
        /// <exception cref="SQLiteException">fails if bad query is given</exception>
        public override bool SaveData(string sqlStatement)
        {
            SQLiteConnection dbAccess = _dbAccess as SQLiteConnection ?? throw new SQLiteException("_dbAccess is not a type of SQLiteConnection");
            if (!_hasOpenConnection) dbAccess.Open();
            try
            {
                SQLiteCommand command = dbAccess.CreateCommand();
                command.CommandText = sqlStatement;
                //check if lines affected are greater than 0
                return command.ExecuteNonQuery() > 0;
            }
            finally
            {
                if (!_hasOpenConnection) dbAccess.Close();
            }
        }
        /// <summary>
        /// saves data into the db. use this when dealing with userinput. 
        /// </summary>
        /// <param name="sqlStatement">the query to execute (make sure to have $1, $2 statements for parameters)</param>
        /// <param name="parameters">the parameters. example: key = "$1", value = 1 </param>
        /// <returns>true if lines where affected, else false.</returns>
        public override bool SaveData(string sqlStatement, Dictionary<string, dynamic?> parameters)
        {
            List<SQLiteParameter> sqliteParameters = new();
            foreach (string key in parameters.Keys)
            {
                sqliteParameters.Add(
                    new SQLiteParameter(key, parameters[key])
                );
            }
            return SaveData(sqlStatement, sqliteParameters.ToArray());
        }
        /// <summary>
        /// saves data into the db. is not abstract, so try to not use this.
        /// </summary>
        /// <param name="sqlStatement">the query to execute (make sure to have $1, $2 statements for parameters)</param>
        /// <param name="parameters">the parameters as SQLiteParameters (tuple)</param>
        /// <returns>true if rows where affected, esle false</returns>
        /// <exception cref="SQLiteException">error if bad query</exception>
        public bool SaveData(string sqlStatement, SQLiteParameter[] parameters)
        {
            SQLiteConnection dbAccess = _dbAccess as SQLiteConnection ?? throw new SQLiteException("_dbAccess is not a type of SQLiteConnection");
            if (!_hasOpenConnection) dbAccess.Open();
            try
            {
                SQLiteCommand command = dbAccess.CreateCommand();
                command.CommandText = sqlStatement;
                command.Parameters.AddRange(parameters);
                //check if lines affected are greater than 0
                return command.ExecuteNonQuery() > 0;
            }
            finally
            {
                if (!_hasOpenConnection) dbAccess.Close();
            }
        }
        public override T[]? ReadData<T>(string sqlStatement)
        {
            SQLiteConnection dbAccess = _dbAccess as SQLiteConnection ?? throw new SQLiteException("_dbAccess is not a type of SQLiteConnection");
            if (!_hasOpenConnection) dbAccess.Open();
            try
            {
                SQLiteCommand command = dbAccess.CreateCommand();
                command.CommandText = sqlStatement;
                return ConvertToObject<T>(command.ExecuteReader()) ?? new T[0];
            }
            finally
            {
                if (!_hasOpenConnection) dbAccess.Close();
            }
        }
        public override T[] ReadData<T>(string sqlStatement, Dictionary<string, dynamic?> parameters)
        {
            List<SQLiteParameter> sqliteParameters = new();
            foreach (string key in parameters.Keys)
            {
                sqliteParameters.Add(
                    new SQLiteParameter(key, parameters[key])
                );
            }
            return ReadData<T>(sqlStatement, sqliteParameters.ToArray());
        }
        public T[] ReadData<T>(string sqlStatement, SQLiteParameter[] parameters)
        {
            SQLiteConnection dbAccess = _dbAccess as SQLiteConnection ?? throw new SQLiteException("_dbAccess is not a type of SQLiteConnection");
            if (!_hasOpenConnection) dbAccess.Open();
            try
            {
                SQLiteCommand command = dbAccess.CreateCommand();
                command.CommandText = sqlStatement;
                command.Parameters.AddRange(parameters);
                return ConvertToObject<T>(command.ExecuteReader()) ?? new T[0];
            }
            finally
            {
                if (!_hasOpenConnection) dbAccess.Close();
            }
        }

        public override int CreateData(string sqlStatement)
        {
            SQLiteConnection dbAccess = _dbAccess as SQLiteConnection ?? throw new SQLiteException("_dbAccess is not a type of SQLiteConnection");
            if (!_hasOpenConnection) dbAccess.Open();
            try
            {
                SQLiteCommand command = dbAccess.CreateCommand();
                command.CommandText = sqlStatement;
                var result = command.ExecuteScalar(CommandBehavior.KeyInfo);
                if (result == null) throw new FileLoadException("failed to get the result.");
                return (int)result;
            }
            finally
            {
                if (!_hasOpenConnection) dbAccess.Close();
            }
        }

        public override int CreateData(string sqlStatement, Dictionary<string, dynamic?> parameters)
        {
            List<SQLiteParameter> sqliteParameters = new();
            foreach (string key in parameters.Keys)
            {
                sqliteParameters.Add(
                    new SQLiteParameter(key, parameters[key])
                );
            }
            return CreateData(sqlStatement, sqliteParameters.ToArray());
        }
        public int CreateData(string sqlStatement, SQLiteParameter[] parameters)
        {
            SQLiteConnection dbAccess = _dbAccess as SQLiteConnection ?? throw new SQLiteException("_dbAccess is not a type of SQLiteConnection");
            if (!_hasOpenConnection) dbAccess.Open();
            try
            {
                SQLiteCommand command = dbAccess.CreateCommand();
                if (sqlStatement.Last().Equals(';'))
                {
                    sqlStatement.Remove(sqlStatement.Length - 1);
                }
                command.CommandText = sqlStatement + "\nRETURNING ID";
                command.Parameters.AddRange(parameters);
                var result = command.ExecuteScalar();
                return Convert.ToInt32(result);
            }
            finally
            {
                if (!_hasOpenConnection) dbAccess.Close();
            }
        }
    }
}