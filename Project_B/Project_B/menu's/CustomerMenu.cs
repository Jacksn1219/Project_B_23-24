using Models;

namespace Project_B.menu_s
{
    public static class CustomerMenu
    {
        public static void UseMenu(Dictionary<string, Action<string>> menuItems)
        {
            // ------ gebruiker menu met menu opties ------//
            //create menu
            InputMenu gebruikerMenu = new InputMenu("Customer menu");
            gebruikerMenu.Add(menuItems);
            gebruikerMenu.UseMenu((title) => Universal.printAsTitle(title));
        }
    }
}