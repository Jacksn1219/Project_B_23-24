namespace DataAccessLibrary.models.interfaces
{
    public abstract class DbItem
    {
        /// <summary>
        /// the db Id. do not set, only when creating DbItems from a DatabaseReader.
        /// </summary>
        public abstract int? ID { get; internal set; }

        /// <summary>
        /// check if the DbItem exists
        /// </summary>
        public bool Exists { get { return ID != null; } }
        public bool IsChanged { get; internal set; } = false;
    }
}