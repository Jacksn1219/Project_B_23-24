using System.Data;
using System.Data.Entity;

namespace DataAccessLibrary.models.interfaces
{
    internal interface ICRUD
    {
        public bool SaveData(string sqlStatement);
        public bool SaveData(string sqlStatement, Dictionary<string, dynamic?> parameters);
        public List<T> ReadData<T>(string sqlStatement);
        public List<T> ReadData<T>(string sqlStatement, Dictionary<string, dynamic?> parameters);
    }
}