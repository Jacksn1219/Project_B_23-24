using System.Data;
using System.Data.SQLite;

namespace DataAccessLibrary
{
    public class SQliteDataAccess : DataAccess
    {
        protected override IDbConnection _dbAccess { get; set; }

        public SQliteDataAccess(string connectionString)
        {
            _dbAccess = new SQLiteConnection(connectionString);
        }

        public override void Dispose()
        {
            _dbAccess.Dispose();
        }
        public override bool SaveData(string sqlStatement, Dictionary<string, string> parameters)
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
        public bool SaveData(string sqlStatement, SQLiteParameter[] parameters)
        {
            SQLiteConnection dbAccess = _dbAccess as SQLiteConnection ?? throw new SQLiteException("_dbAccess is not a type of SQLiteConnection");
            dbAccess.Open();
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
                dbAccess.Close();
                dbAccess.Dispose();
            }
        }
        public override List<T> ReadData<T>(string sqlStatement, Dictionary<string, string> parameters)
        {
            throw new NotImplementedException();

        }
    }
}