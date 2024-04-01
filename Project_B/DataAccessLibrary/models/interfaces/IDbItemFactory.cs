using System.CodeDom;

namespace DataAccessLibrary;

public interface IDbItemFactory<T>
{
    /// <summary>
    /// put code for creating the table of the object here.
    /// </summary>
    /// <returns>true if succesfull, else false</returns>
    public void CreateTable();
    /// <summary>
    /// saves/updates the item into the db
    /// </summary>
    /// <returns>true if succesfull, else false</returns>
    public bool ItemToDb(T item);
    /// <summary>
    /// creates a new item in the db
    /// </summary>
    /// <param name="item"> the item to add te the db</param>
    /// <returns>true if succes, else false</returns>
    public bool CreateItem(T item);
    /// <summary>
    /// updates an existing item to the db
    /// </summary>
    /// <param name="item">the item to update</param>
    /// <returns>true if success, else false</returns>
    public bool UpdateItem(T item);
    /// <summary>
    /// put code to try to get the item in the db with the Id param.
    /// </summary>
    /// <typeparam name="T">the type of the DbItem</typeparam>
    /// <param name="id">the Id of the item</param>
    /// <returns>the DbItem as </returns>
    public T GetItemFromId(int id);


}
