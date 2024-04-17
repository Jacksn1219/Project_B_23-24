namespace DataAccessLibrary.models.interfaces
{
    public class DbItem
    {
        /// <summary>
        /// the db Id. do not set, only when creating DbItems from a DatabaseReader.
        /// </summary>
        private int? _id;
        public int? ID
        {
            get
            {
                return _id;
            }
            set
            {
                if (_id == null)
                {
                    _id = value;
                    IsChanged = true;
                }
                else throw new InvalidOperationException("you cannot set an id with a value.");
            }
        }

        /// <summary>
        /// check if the DbItem exists
        /// </summary>
        public bool Exists { get { return ID != null; } }
        public bool IsChanged { get; internal set; } = false;
    }
}