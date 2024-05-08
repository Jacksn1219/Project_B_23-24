using System.Data;
using System.Data.Common;
using System.Reflection;
using DataAccessLibrary.models.interfaces;
using Newtonsoft.Json;

namespace DataAccessLibrary
{
    public abstract class DataAccess : ICRUD, IDisposable
    {
        protected abstract IDbConnection _dbAccess { get; set; }
        public abstract void Dispose();
        public abstract List<T> ReadData<T>(string sqlStatement);
        public abstract List<T> ReadData<T>(string sqlStatement, Dictionary<string, dynamic?> parameters);
        public abstract bool SaveData(string sqlStatement);
        public abstract bool SaveData(string sqlStatement, Dictionary<string, dynamic?> parameters);
        /// <summary>
        /// converts the rows of the DbDataReader into a list of T
        /// </summary>
        /// <typeparam name="T">the DbItem</typeparam>
        /// <param name="rd">the DbReader object</param>
        /// <returns>a list of DbItems</returns>
        /// <exception cref="NotImplementedException">currently not implemented</exception>
        public static List<T>? ConvertToObject<T>(DbDataReader rd)
        {
            // all the type T objects as dictionaries
            List<Dictionary<string, dynamic?>> typeOfT = new();

            // get the properties of the type T
            Type type = typeof(T);
            PropertyInfo[] fields = type.GetProperties();

            // get the rows of the reader
            DataTable dt = new DataTable();
            dt.Load(rd);
            var dv = dt.AsDataView();
            foreach (DataRow row in dt.Rows)
            {
                //new type T dict 
                Dictionary<string, dynamic?> rowDict = new();
                foreach (DataColumn column in dt.Columns)
                {
                    // try to find fields with the sam name
                    foreach (PropertyInfo field in fields)
                    {
                        // checks all properties in the model
                        if (field.CanWrite && field.Name.Equals(column.ColumnName, StringComparison.OrdinalIgnoreCase))
                        {
                            var value = row[column];
                            var x = value.GetType();
                            // DBNull is the null in databases
                            if (value.GetType() == typeof(DBNull)) rowDict.Add(field.Name, null);
                            else if (field.PropertyType == typeof(bool))
                            {
                                rowDict.Add(field.Name, Convert.ToBoolean(value));
                            }
                            else rowDict.Add(field.Name, value);
                            break;
                        }
                    }
                }
                // add dict to dictlist
                typeOfT.Add(rowDict);
            }
            // make a class from the list of dicts
            string Tstring = JsonConvert.SerializeObject(typeOfT);
            List<T>? items = JsonConvert.DeserializeObject<List<T>>(Tstring);
            return items;
        }

        public abstract int CreateData(string sqlStatement);

        public abstract int CreateData(string sqlStatement, Dictionary<string, dynamic?> parameters);
    }
}