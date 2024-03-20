using System.Data;
using System.Data.Entity;

namespace DataAccessLibrary.models.interfaces
{
    internal interface ICRUD
    {
        public bool SaveData(string sqlStatement, Dictionary<string, string> parameters);
        public List<T> ReadData<T>(string sqlStatement, Dictionary<string, string> parameters);
    }
}