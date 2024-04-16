using DataAccessLibrary;
using Models;

namespace TimeTablemodels
{
    public class InputMenuTT
    {
        string introduction;
        bool exit;
        List<InputMenuOptionTT> menuoptions = new List<InputMenuOptionTT>();
        int row;

        public InputMenuTT(string introduction = "", bool exit = false, int row = 1)
        {
            this.introduction = introduction;
            this.exit = exit;
            this.row = row;
        }

        /// <summary>
        /// Add item to menu option list
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Act"></param>
        /// <param name="isTaken"></param>
        public void Add(string Name, Action<string> Act, bool? isTaken = null, int? ID = null) => this.menuoptions.Add(ID == null ? new InputMenuOptionTT(Name, Act, isTaken) : new InputMenuOptionTT(Name, Act, isTaken, ID));

        /// <summary>
        /// Remove item from menu option list
        /// </summary>
        public void Remove() => this.menuoptions.Remove(this.menuoptions[this.menuoptions.Count - 1]);

        public void Edit(int ID, string newName) => this.menuoptions[this.menuoptions.FindIndex(x => x.ID == ID)].Name = newName;

        public void Draw(int cursor)
        {
            Console.Clear();
            Console.WriteLine(this.introduction);
        }
    }
}