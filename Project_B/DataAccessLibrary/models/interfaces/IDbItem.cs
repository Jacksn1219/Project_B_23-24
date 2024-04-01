public interface IDbItem
{
    /// <summary>
    /// the db Id. do not set, only when creating DbItems from a DatabaseReader.
    /// </summary>
    public abstract int Id { get; protected set; }
}