using System.Runtime.CompilerServices;
using Models;

namespace Project_B.menu_s;
public static class MainMenu
{
    public static void UseMenu(Dictionary<string, Action<string>> userActions, Dictionary<string, Action<string>> adminActions)
    {
        InputMenu main = new("main menu:", true);
        main.Add("customer menu",
            (x) =>
            {
                //customer actions here plz (return is already implemented)
                CustomerMenu.UseMenu(
                    userActions
                );
            }
        );
        main.Add("admin menu",
        (x) =>
        {
            AdminMenu.UseMenu(
                //admin actions here plz (login + return is already implemented)
                adminActions
            );
        });

        main.UseMenu();

    }
}