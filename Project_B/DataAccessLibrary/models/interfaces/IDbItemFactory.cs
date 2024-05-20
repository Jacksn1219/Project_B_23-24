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
    public bool ItemToDb(T item, int deepcopyLv = 99);
    public bool ItemsToDb(List<T> items, int deepcopyLv = 99);
    /// <summary>
    /// creates a new item in the db
    /// </summary>
    /// <param name="item"> the item to add te the db</param>
    /// <returns>true if succes, else false</returns>

    public bool CreateItem(T item, int deepcopyLv = 99);
    /// <summary>
    /// updates an existing item to the db
    /// </summary>
    /// <param name="item">the item to update</param>
    /// <returns>true if success, else false</returns>
    public bool UpdateItem(T item, int deepcopyLv = 99);
    /// <summary>
    /// put code to try to get the item in the db with the Id param.
    /// </summary>
    /// <typeparam name="T">the type of the DbItem</typeparam>
    /// <param name="id">the Id of the item</param>
    /// <returns>the DbItem as </returns>
    public T? GetItemFromId(int id, int deepcopyLv = 0);
    /// <summary>
    /// gets all items from the db.
    /// </summary>
    /// <param name="count">the max amount to return</param>
    /// <param name="page">the page of the items. for example if count is 10, item number 11 is the first item returned if the page is 2</param>
    /// <param name="deepcopyLv">if you also want connected items set the deepcopy level higher</param>
    /// <returns></returns>
    public T[] GetItems(int count, int page = 1, int deepcopyLv = 0);
}
