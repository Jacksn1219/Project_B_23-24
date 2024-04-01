using System.Data;
using System.Data.Common;
using System.Linq.Expressions;
using DataAccessLibrary.models.interfaces;

namespace DataAccessLibrary
{
    public abstract class DataAccess : ICRUD, IDisposable
    {
        protected abstract IDbConnection _dbAccess { get; set; }
        public abstract void Dispose();
        public abstract List<T> ReadData<T>(string sqlStatement);
        public abstract List<T> ReadData<T>(string sqlStatement, Dictionary<string, dynamic> parameters);
        public abstract bool SaveData(string sqlStatement);
        public abstract bool SaveData(string sqlStatement, Dictionary<string, dynamic> parameters);
        /// <summary>
        /// converts the rows of the DbDataReader into a list of T
        /// </summary>
        /// <typeparam name="T">the DbItem</typeparam>
        /// <param name="rd">the DbReader object</param>
        /// <returns>a list of DbItems</returns>
        /// <exception cref="NotImplementedException">currently not implemented</exception>
        public virtual List<T> ConvertToObject<T>(DbDataReader rd)
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