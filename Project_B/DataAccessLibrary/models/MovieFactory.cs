using DataAccessLibrary;

public class Moviefactory : IDbItemFactory
{
    public static bool CreateTable()
    {
        throw new NotImplementedException();
    }

    public T GetItemFromId<T>(int id)
    {
        throw new NotImplementedException();
    }

    public bool ItemToDb()
    {
        throw new NotImplementedException();
    }
}