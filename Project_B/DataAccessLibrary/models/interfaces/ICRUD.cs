using System.Data;
using System.Data.Entity;

namespace DataAccessLibrary.models.interfaces
{
    internal interface ICRUD
    {
        public int CreateData(string sqlStatement);
        public int CreateData(string sqlStatement, Dictionary<string, dynamic?> parameters);
        public bool SaveData(string sqlStatement);
        public bool SaveData(string sqlStatement, Dictionary<string, dynamic?> parameters);
        public T[] ReadData<T>(string sqlStatement);
        public T[] ReadData<T>(string sqlStatement, Dictionary<string, dynamic?> parameters);
    }
}