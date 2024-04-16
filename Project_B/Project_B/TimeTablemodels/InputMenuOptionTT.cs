namespace Models
{
    public class InputMenuOptionTT
    {
        //----- parameters -----//
        public int? ID { get; set; }
        public string Name;
        public Action<string> Act;
        public bool? isTaken;

        //----- Constructor -----//
        public InputMenuOptionTT(string Name, Action<string> Act, bool? isTaken)
        {
            this.Name = Name;
            this.Act = Act;
            this.isTaken = isTaken;
        }
        public InputMenuOptionTT(string Name, Action<string> Act, bool? isTaken, int? id) : this(Name, Act, isTaken)
        {
            this.ID = id;
        }
    }
}
