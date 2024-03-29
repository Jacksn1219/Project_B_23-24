using System.CodeDom;

namespace DataAccessLibrary;

public interface IDbItemFactory
{
    public static abstract bool CreateTable();
    public bool ItemToDb();
    public T GetItemFromId<T>(int id);


}
