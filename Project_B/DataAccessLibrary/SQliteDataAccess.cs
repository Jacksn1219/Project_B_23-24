using System.Data;
using System.Data.SQLite;
using System.Reflection;
using System.Xml.Schema;
using DataAccessLibrary.models.interfaces;

namespace DataAccessLibrary
{
    public class SQliteDataAccess : IDisposable, ICRUD
    {
        private SQLiteConnection _dbAccess { get; set; }

        public SQliteDataAccess(string connectionString)
        {
            _dbAccess = new SQLiteConnection(connectionString);
        }

        public void Dispose()
        {
            _dbAccess.Dispose();
        }
        public bool SafeData(string sqlStatement, Dictionary<string, string> parameters)
        {
            List<SQLiteParameter> sqliteParameters = new();
            foreach (string key in parameters.Keys)
            {
                sqliteParameters.Add(
                    new SQLiteParameter(key, parameters[key])
                );
            }
            return SafeData(sqlStatement, sqliteParameters.ToArray());
        }
        public bool SafeData(string sqlStatement, SQLiteParameter[] parameters)
        {
            _dbAccess.Open();
            try
            {
                SQLiteCommand command = _dbAccess.CreateCommand();
                command.CommandText = sqlStatement;
                command.Parameters.AddRange(parameters);
                //check if lines affected are greater than 0
                return command.ExecuteNonQuery() > 0;
            }
            finally { _dbAccess.Close(); }
        }
        public List<T> ReadData<T>(string sqlStatement, Dictionary<string, string> parameters)
        {
            throw new NotImplementedException();
        }

        private static T ConvertToObject<T>(SQLiteDataReader rd) where T : class, new()
        {
            Type type = typeof(T);
            throw new NotImplementedException();
            //var accessor = TypeAccessor.Create(type);
            //var members = accessor.GetMembers();
            //var t = new T();
            //
            //for (int i = 0; i < rd.FieldCount; i++)
            //{
            //    if (!rd.IsDBNull(i))
            //    {
            //        string fieldName = rd.GetName(i);
            //
            //        if (members.Any(m => string.Equals(m, fieldName, StringComparison.OrdinalIgnoreCase)))
            //        {
            //            accessor[t, fieldName] = rd.GetValue(i);
            //        }
            //    }
            //}
            //
            //return t;
        }
    }
}