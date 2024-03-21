namespace Models
{
    public class InputMenuOption
    {
        //----- parameters -----//
        public string Name;
        public Action<string> Act;
        public bool? isTaken;

        //----- Constructor -----//
        public InputMenuOption(string Name, Action<string> Act, bool? isTaken)
        {
            this.Name = Name;
            this.Act = Act;
            this.isTaken = isTaken;
        }
    }
}
