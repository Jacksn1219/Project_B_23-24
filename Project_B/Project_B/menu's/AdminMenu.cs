using Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Project_B.menu_s
{
    public static class AdminMenu
    {
        public static bool IsLogedIn = false;
        public static void UseMenu(Dictionary<string, Action<string>> menuItems)
        {
            if (LogIn())
            {
                // ------ Medewerker menu met menu opties ------//
                //create menu
                InputMenu medewerkerMenu = new InputMenu("admin menu");
                foreach (string key in menuItems.Keys)
                {
                    medewerkerMenu.Add(key, menuItems[key]);
                }
                medewerkerMenu.UseMenu();
            }
            // medewerkerMenu.Add("Create/Edit", (x) =>
            // {
            //     //Aanmaken nieuwe room, movie, actor, director.
            //     InputMenu createMenu = new InputMenu("useLambda");

            //     createMenu.Add("Create room", (x) =>
            //     {
            //         Layout.MakeNewLayout();
            //     });
            //     createMenu.Add("Edit room", (x) =>
            //     {
            //         Layout.editLayoutPerRoom();
            //     });
            //     createMenu.Add("\n" + Universal.centerToScreen("Create movie"), (x) =>
            //     {
            //         CreateItems.CreateNewMovie();
            //     });
            //     createMenu.Add("Edit movie", (x) =>
            //     {
            //         CreateItems.ChangeMovie();
            //     });
            //     createMenu.UseMenu(() => Universal.printAsTitle("Create/Edit"));
            // });
            // medewerkerMenu.Add("\n" + Universal.centerToScreen("Select a seat"), (x) =>
            // {
            //     Console.WriteLine(Layout.selectSeatPerRoom().ToString() ?? "");
            //     Console.ReadLine();
            // });


            // medewerkerMenu.Add("\n" + Universal.centerToScreen("Test SeeActors"), (x) => // Als klant wil ik de acteurs van een film bekijken
            // {
            //     List<ActorModel> authors = new List<ActorModel>();
            //     authors.Add(new ActorModel("Jack Black", "Plays Po", 43));
            //     authors.Add(new ActorModel("Jackie Chan", "Plays Monkey", 57));
            //     authors.Add(new ActorModel("Ada Wong", "Plays Viper", 27));
            //     authors.Add(new ActorModel("Jada Pinket Smith", "Plays Tigress", 41));
            //     MovieModel movietje = new MovieModel("KUNG FU PANDA 4", "everybody was kung fu fighting", 12, 120, "Horror");
            //     movietje.Director = new DirectorModel("Jaycey", "Director from netherlands", 20);
            //     movietje.Actors.AddRange(authors);
            //     Console.WriteLine(movietje.SeeActors());
            //     Console.ReadLine();
            // });
            // medewerkerMenu.Add("Test SeeDirector", (x) => // Als klant wil ik de regisseur van een film zien
            // {
            //     List<DirectorModel> directors = new List<DirectorModel>();
            //     directors.Add(new DirectorModel("Christopher Nolan", "Famous movie director known for several blockbuster movies such as Oppenheimer, Interstellar, Inception and many more", 53));
            //     MovieModel interStellar = new MovieModel("Interstellar", "While the earth no longer has the resources to supply the human race, a group of astronauts go to beyond the milky way to find a possible future planet for mankind", 12, 190, "Sci-Fi");
            //     Console.WriteLine(interStellar.SeeDirector(directors));
            //     Console.ReadLine();
            // });
            // medewerkerMenu.Add("Test SeeDescription", (x) => // Als klant wil ik de omschrijving (leeftijd + genre) van een film zien
            // {
            //     MovieModel interStellar = new MovieModel("Interstellar", "While the earth no longer has the resources to supply the human race, a group of astronauts go to beyond the milky way to find a possible future planet for mankind", 12, 190, "Sci-Fi");
            //     Console.WriteLine(interStellar.SeeDescription());
            //     Console.ReadLine();
            // });
            // medewerkerMenu.Add("\n" + Universal.centerToScreen("Set prices"), (x) =>
            // {
            //     var prices = SeatPriceCalculator.GetCurrentPrices();
            //     SeatPriceCalculator.WritePrices();
            //     System.Console.WriteLine("\nChange Prices? (Y/N)");
            //     char input = Console.ReadKey().KeyChar;
            //     if (input.Equals('Y') || input.Equals('y'))
            //     {
            //         bool changing = true;
            //         while (changing)
            //         {
            //             System.Console.WriteLine("type price to change: (Q to quit)");
            //             string response = Console.ReadLine() ?? "";
            //             switch (response.ToLower())
            //             {
            //                 case "price tier i" or "tier i" or "i" or "1":
            //                     Console.WriteLine("type new price:");
            //                     response = Console.ReadLine() ?? "";
            //                     prices.PriceTierI = decimal.Parse(response);
            //                     break;
            //                 case "price tier ii" or "tier ii" or "ii" or "2":
            //                     Console.WriteLine("type new price:");
            //                     response = Console.ReadLine() ?? "";
            //                     prices.PriceTierII = decimal.Parse(response);
            //                     break;
            //                 case "price tier iii" or "tier iii" or "iii" or "3":
            //                     Console.WriteLine("type new price:");
            //                     response = Console.ReadLine() ?? "";
            //                     prices.PriceTierIII = decimal.Parse(response);
            //                     break;
            //                 case "extra space" or "extra" or "space":
            //                     Console.WriteLine("type new price:");
            //                     response = Console.ReadLine() ?? "";
            //                     prices.ExtraSpace = decimal.Parse(response);
            //                     break;
            //                 case "loveseat" or "love" or "love seat":
            //                     Console.WriteLine("type new price:");
            //                     response = Console.ReadLine() ?? "";
            //                     prices.LoveSeat = decimal.Parse(response);
            //                     break;
            //                 case "q":
            //                     changing = false;
            //                     break;
            //             }
            //         }

            //     }
            //     SeatPriceCalculator.UpdatePrice(prices);
            //     SeatPriceCalculator.WritePrices();
            //     Console.ReadLine();

            // });
            // medewerkerMenu.Add("Get seat PRICE info", (x) =>
            // {
            //     SeatModel seat = new SeatModel("naam", "II", "loveseat");
            //     Console.WriteLine(SeatPriceCalculator.ShowCalculation(seat));
            //     Console.ReadLine();
            // });

        }
        private static bool LogIn()
        {
            if (IsLogedIn) return IsLogedIn;
            string fileName = "Medewerker.json";
            JObject? jsonData = (JObject?)JsonConvert.DeserializeObject(File.ReadAllText(Universal.datafolderPath + "\\" + fileName));
            string passWord;
            if (jsonData != null)
            {
                var x = jsonData["Value"] ?? "";
                passWord = x.Value<string>() ?? ""; ;
            }
            else passWord = "";
            while (!IsLogedIn)
            {
                Console.Write("| Inlog |\nWachtwoord: ");
                string userInput = Console.ReadLine() ?? "";
                if (userInput.Equals(passWord)) IsLogedIn = true;
                else
                {
                    Universal.ChangeColour(ConsoleColor.Red);
                    Console.WriteLine("Invalid password!\npress Q to quit.\npress any oter button to continue.");
                    Universal.ChangeColour(ConsoleColor.White);
                    char key = Console.ReadKey().KeyChar;
                    System.Console.WriteLine(); // enter
                    if (key.Equals('q') || key.Equals('Q')) return false;
                }
            }
            return true;
        }
    }
}