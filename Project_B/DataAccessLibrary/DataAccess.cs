using System.Data;
using System.Data.Common;
using DataAccessLibrary.models.interfaces;

namespace DataAccessLibrary
{
    public abstract class DataAccess : ICRUD, IDisposable
    {
        protected abstract IDbConnection _dbAccess { get; set; }
        public abstract void Dispose();
        public abstract List<T> ReadData<T>(string sqlStatement, Dictionary<string, string> parameters);
        public abstract bool SaveData(string sqlStatement, Dictionary<string, string> parameters);
        public virtual T ConvertToObject<T>(DbDataReader rd) where T : class, new()
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