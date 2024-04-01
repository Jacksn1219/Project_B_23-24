using System.CodeDom;

namespace DataAccessLibrary;

public interface IDbItemFactory
{
    /// <summary>
    /// put code for creating the table of the object here.
    /// </summary>
    /// <returns>true if succesfull, else false</returns>
    public static abstract bool CreateTable();
    /// <summary>
    /// saves/updates the item into the db
    /// </summary>
    /// <returns>true if succesfull, else false</returns>
    public bool ItemToDb();
    /// <summary>
    /// put code to try to get the item in the db with the Id param.
    /// </summary>
    /// <typeparam name="T">the type of the DbItem</typeparam>
    /// <param name="id">the Id of the item</param>
    /// <returns>the DbItem as </returns>
    public T GetItemFromId<T>(int id);


}
