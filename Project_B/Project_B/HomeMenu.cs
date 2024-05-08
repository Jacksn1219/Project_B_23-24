using System.Globalization;
using Models;
using DataAccessLibrary;
using DataAccessLibrary.models;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using Project_B.services;
using Project_B;
public static class HomeMenu
{
    public static InputMenu menu;
    static HomeMenu()
    {
        menu = new InputMenu("useLambda", true);
        menu.Add("Klant", (x) => { CustomerMenu.UseMenu(() => Universal.printAsTitle("Klant Menu")); });
        menu.Add("Medewerker", (x) =>
        {
            string fileName = "Medewerker.json";
            JObject? jsonData = (JObject?)JsonConvert.DeserializeObject(File.ReadAllText(Universal.databasePath() + "\\" + fileName));
            string passWord = jsonData["Value"].Value<string>() ?? "";

            Console.Write("| Inlog |\nWachtwoord: ");
            string? userInput = Console.ReadLine();
            if (userInput == passWord) EmployeeMenu.UseMenu(() => Universal.printAsTitle("Medewerker Menu"));
            else
            {
                Universal.ChangeColour(ConsoleColor.Red);
                Console.WriteLine("Onjuist wachtwoord !");
                Console.ReadLine();
            }
        });

        menu.Add("Tijdschema Ma 06 Mei tot Zo 13 Mei", (x) =>
        {
            List<RoomModel> roomList = new List<RoomModel>
            {
                new RoomModel("Room 1", 168, 12),
                new RoomModel("Room 2", 342, 18),
                new RoomModel("Room 3", 600, 30)
            };
            List<MovieModel> maandagFilms = new List<MovieModel>
            {
                new MovieModel("Rocky","Rocky Balboa is een hardwerkende bokser die de top probeert te bereiken. Hij werkt in een vleesfabriek in Philadelphia en verdient wat extra geld als inner van schulden.", 12, 120, "Action, Sport, Drama"),
                new MovieModel("Indiana Jones and The Lost Ark", "Wereldreiziger en archeoloog Indiana Jones wordt net voor de Tweede Wereldoorlog door de Amerikaanse overheid ingehuurd om een religieus relikwie op te sporen, voordat dit artefact in de handen van de Nazi's valt.", 16, 150, "Adventure, Action"),
                new MovieModel("Gone In 60 Seconds", "Randall Raines, een ex-autodief, leidt al een aantal jaren een rustig leven. Maar als zijn broer zich in de nesten werkt moet Randall zijn oude activiteiten weer oppakken om zijn broeder te redden.", 18, 90, "Action, Adventure, Thriller, Drama, Horror, Detective"),
                new MovieModel("Interstellar", "Terwijl de aarde niet meer in staat is om de mensheid in haar levensbehoefte te voorzien, gaat een groep ontdekkingsreizigers, ver buiten het melkwegstelsel, op zoek naar een toekomst voor de mens achter de sterren.", 12, 90, "drama, adventure, and speculative fiction"),
                new MovieModel("Cars 2", "Racecar Lightning McQueen en Mater besluiten mee te doen aan de World Grand Prix. Mater raakt onderweg echter betrokken bij spionage.", 7, 120, "action, comedy, and spy thriller"),
                new MovieModel("Rocky", "Rocky Balboa is een hardwerkende bokser die de top probeert te bereiken. Hij werkt in een vleesfabriek in Philadelphia en verdient wat extra geld als inner van schulden.", 12, 120, "Action, Sport, Drama"),
                new MovieModel("Indiana Jones and The Lost Ark", "Wereldreiziger en archeoloog Indiana Jones wordt net voor de Tweede Wereldoorlog door de Amerikaanse overheid ingehuurd om een religieus relikwie op te sporen, voordat dit artefact in de handen van de Nazi's valt.", 16, 150, "Adventure, Action"),
                new MovieModel("Gone In 60 Seconds", "Randall Raines, een ex-autodief, leidt al een aantal jaren een rustig leven. Maar als zijn broer zich in de nesten werkt moet Randall zijn oude activiteiten weer oppakken om zijn broeder te redden.",18, 90, "Action, Adventure, Thriller, Drama, Horror, Detective"),
                new MovieModel("Interstellar", "Terwijl de aarde niet meer in staat is om de mensheid in haar levensbehoefte te voorzien, gaat een groep ontdekkingsreizigers, ver buiten het melkwegstelsel, op zoek naar een toekomst voor de mens achter de sterren.", 12, 90, "drama, adventure, and speculative fiction"),
                new MovieModel("Cars 2", "Racecar Lightning McQueen en Mater besluiten mee te doen aan de World Grand Prix. Mater raakt onderweg echter betrokken bij spionage.", 7, 120, "action, comedy, and spy thriller"),
                new MovieModel("Rocky", "Rocky Balboa is een hardwerkende bokser die de top probeert te bereiken. Hij werkt in een vleesfabriek in Philadelphia en verdient wat extra geld als inner van schulden.", 12, 120, "Action, Sport, Drama"),
                new MovieModel("Indiana Jones and The Lost Ark", "Wereldreiziger en archeoloog Indiana Jones wordt net voor de Tweede Wereldoorlog door de Amerikaanse overheid ingehuurd om een religieus relikwie op te sporen, voordat dit artefact in de handen van de Nazi's valt.", 16, 150, "Adventure, Action"),
                new MovieModel("Gone In 60 Seconds", "Randall Raines, een ex-autodief, leidt al een aantal jaren een rustig leven. Maar als zijn broer zich in de nesten werkt moet Randall zijn oude activiteiten weer oppakken om zijn broeder te redden.", 18, 90, "Action, Adventure, Thriller, Drama, Horror, Detective"),
                new MovieModel("Interstellar", "Terwijl de aarde niet meer in staat is om de mensheid in haar levensbehoefte te voorzien, gaat een groep ontdekkingsreizigers, ver buiten het melkwegstelsel, op zoek naar een toekomst voor de mens achter de sterren.", 12, 90, "drama, adventure, and speculative fiction"),
                new MovieModel("Cars 2", "Racecar Lightning McQueen en Mater besluiten mee te doen aan de World Grand Prix. Mater raakt onderweg echter betrokken bij spionage.", 7, 120, "action, comedy, and spy thriller")
            };

            List<MovieModel> dinsdagFilms = new List<MovieModel>
            {
                new MovieModel("Forest Gump", "Forrest Gump is een simpele jongen met een laag IQ. Dit weerhoudt hem er echter niet van om een grote rol te spelen bij belangrijke gebeurtenissen in de Amerikaanse geschiedenis. Zo vecht hij mee in Vietnam en ontmoet hij grootheden als Elvis en JFK.", 16, 90, "Actie, Thriller en Avontuur."),
                new MovieModel("Bad Boys", "Undercover-agenten Mike Lowrey en Marcus Burnett hebben een grote partij drugs ter waarde van honderd miljoen dollar geconfisqueerd. Maar als de drugs uit het politiebureau in Miami worden gestolen, hebben Marcus en Mike 72 uur de tijd om de daders te vinden voordat de FBI zich ermee gaat bemoeien.", 16, 150, "Action, Comedy, Detective, Crime"),
                new MovieModel("Top Gun", "Waaghals Pete 'Maverick' Mitchell strijdt bij Top Gun, een keiharde opleiding voor toppiloten, om de beste vliegenier van zijn lichting te worden. Ondertussen valt zijn oog op de beeldschone vlieginstructeur Charlie Backwood.", 12, 120, "Action, Adventure, Thriller, Drama, Romance"),
                new MovieModel("Scarface", "Tony Montana, een Cubaanse immigrant, is vastbesloten om de cocaïnehandel in het Miami van de jaren '80 over te nemen. Maar in zijn gewelddadige weg naar de top werkt hij een hoop mensen in de afgrond, inclusief zichzelf.", 16, 120, "Gangster, Thriller, Mafia, Crime, Drama, Detective"),
                new MovieModel("The Matrix", "Computerhacker Thomas A. Anderson komt op een vreemde manier in contact met Morpheus. Hij leidt Thomas binnen in de `echte', maar ongekende wereld. De wereld die we kennen is volgens Morpheus een virtuele wereld, de Matrix genaamd.", 7, 90, "Action, Romance, Fantasy, Superhero, Animation"),
                new MovieModel("Forest Gump", "Forrest Gump is een simpele jongen met een laag IQ. Dit weerhoudt hem er echter niet van om een grote rol te spelen bij belangrijke gebeurtenissen in de Amerikaanse geschiedenis. Zo vecht hij mee in Vietnam en ontmoet hij grootheden als Elvis en JFK.", 16, 90, "Actie, Thriller en Avontuur."),
                new MovieModel("Bad Boys", "Undercover-agenten Mike Lowrey en Marcus Burnett hebben een grote partij drugs ter waarde van honderd miljoen dollar geconfisqueerd. Maar als de drugs uit het politiebureau in Miami worden gestolen, hebben Marcus en Mike 72 uur de tijd om de daders te vinden voordat de FBI zich ermee gaat bemoeien.", 16, 150, "Action, Comedy, Detective, Crime"),
                new MovieModel("Top Gun", "Waaghals Pete 'Maverick' Mitchell strijdt bij Top Gun, een keiharde opleiding voor toppiloten, om de beste vliegenier van zijn lichting te worden. Ondertussen valt zijn oog op de beeldschone vlieginstructeur Charlie Backwood.", 12, 120, "Action, Adventure, Thriller, Drama, Romance"),
                new MovieModel("Scarface", "Tony Montana, een Cubaanse immigrant, is vastbesloten om de cocaïnehandel in het Miami van de jaren '80 over te nemen. Maar in zijn gewelddadige weg naar de top werkt hij een hoop mensen in de afgrond, inclusief zichzelf.", 16, 120, "Gangster, Thriller, Mafia, Crime, Drama, Detective"),
                new MovieModel("The Matrix", "Computerhacker Thomas A. Anderson komt op een vreemde manier in contact met Morpheus. Hij leidt Thomas binnen in de `echte', maar ongekende wereld. De wereld die we kennen is volgens Morpheus een virtuele wereld, de Matrix genaamd.", 7, 90, "Action, Romance, Fantasy, Superhero, Animation"),
                new MovieModel("Forest Gump", "Forrest Gump is een simpele jongen met een laag IQ. Dit weerhoudt hem er echter niet van om een grote rol te spelen bij belangrijke gebeurtenissen in de Amerikaanse geschiedenis. Zo vecht hij mee in Vietnam en ontmoet hij grootheden als Elvis en JFK.", 16, 90, "Actie, Thriller en Avontuur."),
                new MovieModel("Bad Boys", "Undercover-agenten Mike Lowrey en Marcus Burnett hebben een grote partij drugs ter waarde van honderd miljoen dollar geconfisqueerd. Maar als de drugs uit het politiebureau in Miami worden gestolen, hebben Marcus en Mike 72 uur de tijd om de daders te vinden voordat de FBI zich ermee gaat bemoeien.", 16, 150, "Action, Comedy, Detective, Crime"),
                new MovieModel("Top Gun", "Waaghals Pete 'Maverick' Mitchell strijdt bij Top Gun, een keiharde opleiding voor toppiloten, om de beste vliegenier van zijn lichting te worden. Ondertussen valt zijn oog op de beeldschone vlieginstructeur Charlie Backwood.", 12, 120, "Action, Adventure, Thriller, Drama, Romance"),
                new MovieModel("Scarface", "Tony Montana, een Cubaanse immigrant, is vastbesloten om de cocaïnehandel in het Miami van de jaren '80 over te nemen. Maar in zijn gewelddadige weg naar de top werkt hij een hoop mensen in de afgrond, inclusief zichzelf.", 16, 120, "Gangster, Thriller, Mafia, Crime, Drama, Detective"),
                new MovieModel("The Matrix", "Computerhacker Thomas A. Anderson komt op een vreemde manier in contact met Morpheus. Hij leidt Thomas binnen in de `echte', maar ongekende wereld. De wereld die we kennen is volgens Morpheus een virtuele wereld, de Matrix genaamd.", 7, 90, "Action, Romance, Fantasy, Superhero, Animation")
            };

            List<MovieModel> woensdagFilms = new List<MovieModel>
            {
                new MovieModel("Rocky","Rocky Balboa is een hardwerkende bokser die de top probeert te bereiken. Hij werkt in een vleesfabriek in Philadelphia en verdient wat extra geld als inner van schulden.", 12, 120, "Action, Sport, Drama"),
                new MovieModel("Indiana Jones and The Lost Ark", "Wereldreiziger en archeoloog Indiana Jones wordt net voor de Tweede Wereldoorlog door de Amerikaanse overheid ingehuurd om een religieus relikwie op te sporen, voordat dit artefact in de handen van de Nazi's valt.", 16, 150, "Adventure, Action"),
                new MovieModel("Gone In 60 Seconds", "Randall Raines, een ex-autodief, leidt al een aantal jaren een rustig leven. Maar als zijn broer zich in de nesten werkt moet Randall zijn oude activiteiten weer oppakken om zijn broeder te redden.", 18, 90, "Action, Adventure, Thriller, Drama, Horror, Detective"),
                new MovieModel("Interstellar", "Terwijl de aarde niet meer in staat is om de mensheid in haar levensbehoefte te voorzien, gaat een groep ontdekkingsreizigers, ver buiten het melkwegstelsel, op zoek naar een toekomst voor de mens achter de sterren.", 12, 90, "drama, adventure, and speculative fiction"),
                new MovieModel("Cars 2", "Racecar Lightning McQueen en Mater besluiten mee te doen aan de World Grand Prix. Mater raakt onderweg echter betrokken bij spionage.", 7, 120, "action, comedy, and spy thriller"),
                new MovieModel("Rocky", "Rocky Balboa is een hardwerkende bokser die de top probeert te bereiken. Hij werkt in een vleesfabriek in Philadelphia en verdient wat extra geld als inner van schulden.", 12, 120, "Action, Sport, Drama"),
                new MovieModel("Indiana Jones and The Lost Ark", "Wereldreiziger en archeoloog Indiana Jones wordt net voor de Tweede Wereldoorlog door de Amerikaanse overheid ingehuurd om een religieus relikwie op te sporen, voordat dit artefact in de handen van de Nazi's valt.", 16, 150, "Adventure, Action"),
                new MovieModel("Gone In 60 Seconds", "Randall Raines, een ex-autodief, leidt al een aantal jaren een rustig leven. Maar als zijn broer zich in de nesten werkt moet Randall zijn oude activiteiten weer oppakken om zijn broeder te redden.",18, 90, "Action, Adventure, Thriller, Drama, Horror, Detective"),
                new MovieModel("Interstellar", "Terwijl de aarde niet meer in staat is om de mensheid in haar levensbehoefte te voorzien, gaat een groep ontdekkingsreizigers, ver buiten het melkwegstelsel, op zoek naar een toekomst voor de mens achter de sterren.", 12, 90, "drama, adventure, and speculative fiction"),
                new MovieModel("Cars 2", "Racecar Lightning McQueen en Mater besluiten mee te doen aan de World Grand Prix. Mater raakt onderweg echter betrokken bij spionage.", 7, 120, "action, comedy, and spy thriller"),
                new MovieModel("Rocky", "Rocky Balboa is een hardwerkende bokser die de top probeert te bereiken. Hij werkt in een vleesfabriek in Philadelphia en verdient wat extra geld als inner van schulden.", 12, 120, "Action, Sport, Drama"),
                new MovieModel("Indiana Jones and The Lost Ark", "Wereldreiziger en archeoloog Indiana Jones wordt net voor de Tweede Wereldoorlog door de Amerikaanse overheid ingehuurd om een religieus relikwie op te sporen, voordat dit artefact in de handen van de Nazi's valt.", 16, 150, "Adventure, Action"),
                new MovieModel("Gone In 60 Seconds", "Randall Raines, een ex-autodief, leidt al een aantal jaren een rustig leven. Maar als zijn broer zich in de nesten werkt moet Randall zijn oude activiteiten weer oppakken om zijn broeder te redden.", 18, 90, "Action, Adventure, Thriller, Drama, Horror, Detective"),
                new MovieModel("Interstellar", "Terwijl de aarde niet meer in staat is om de mensheid in haar levensbehoefte te voorzien, gaat een groep ontdekkingsreizigers, ver buiten het melkwegstelsel, op zoek naar een toekomst voor de mens achter de sterren.", 12, 90, "drama, adventure, and speculative fiction"),
                new MovieModel("Cars 2", "Racecar Lightning McQueen en Mater besluiten mee te doen aan de World Grand Prix. Mater raakt onderweg echter betrokken bij spionage.", 7, 120, "action, comedy, and spy thriller")
            };

            List<MovieModel> donderdagFilms = new List<MovieModel>
            {
                new MovieModel("Forest Gump", "Forrest Gump is een simpele jongen met een laag IQ. Dit weerhoudt hem er echter niet van om een grote rol te spelen bij belangrijke gebeurtenissen in de Amerikaanse geschiedenis. Zo vecht hij mee in Vietnam en ontmoet hij grootheden als Elvis en JFK.", 16, 90, "Actie, Thriller en Avontuur."),
                new MovieModel("Bad Boys", "Undercover-agenten Mike Lowrey en Marcus Burnett hebben een grote partij drugs ter waarde van honderd miljoen dollar geconfisqueerd. Maar als de drugs uit het politiebureau in Miami worden gestolen, hebben Marcus en Mike 72 uur de tijd om de daders te vinden voordat de FBI zich ermee gaat bemoeien.", 16, 150, "Action, Comedy, Detective, Crime"),
                new MovieModel("Top Gun", "Waaghals Pete 'Maverick' Mitchell strijdt bij Top Gun, een keiharde opleiding voor toppiloten, om de beste vliegenier van zijn lichting te worden. Ondertussen valt zijn oog op de beeldschone vlieginstructeur Charlie Backwood.", 12, 120, "Action, Adventure, Thriller, Drama, Romance"),
                new MovieModel("Scarface", "Tony Montana, een Cubaanse immigrant, is vastbesloten om de cocaïnehandel in het Miami van de jaren '80 over te nemen. Maar in zijn gewelddadige weg naar de top werkt hij een hoop mensen in de afgrond, inclusief zichzelf.", 16, 120, "Gangster, Thriller, Mafia, Crime, Drama, Detective"),
                new MovieModel("The Matrix", "Computerhacker Thomas A. Anderson komt op een vreemde manier in contact met Morpheus. Hij leidt Thomas binnen in de `echte', maar ongekende wereld. De wereld die we kennen is volgens Morpheus een virtuele wereld, de Matrix genaamd.", 7, 90, "Action, Romance, Fantasy, Superhero, Animation"),
                new MovieModel("Forest Gump", "Forrest Gump is een simpele jongen met een laag IQ. Dit weerhoudt hem er echter niet van om een grote rol te spelen bij belangrijke gebeurtenissen in de Amerikaanse geschiedenis. Zo vecht hij mee in Vietnam en ontmoet hij grootheden als Elvis en JFK.", 16, 90, "Actie, Thriller en Avontuur."),
                new MovieModel("Bad Boys", "Undercover-agenten Mike Lowrey en Marcus Burnett hebben een grote partij drugs ter waarde van honderd miljoen dollar geconfisqueerd. Maar als de drugs uit het politiebureau in Miami worden gestolen, hebben Marcus en Mike 72 uur de tijd om de daders te vinden voordat de FBI zich ermee gaat bemoeien.", 16, 150, "Action, Comedy, Detective, Crime"),
                new MovieModel("Top Gun", "Waaghals Pete 'Maverick' Mitchell strijdt bij Top Gun, een keiharde opleiding voor toppiloten, om de beste vliegenier van zijn lichting te worden. Ondertussen valt zijn oog op de beeldschone vlieginstructeur Charlie Backwood.", 12, 120, "Action, Adventure, Thriller, Drama, Romance"),
                new MovieModel("Scarface", "Tony Montana, een Cubaanse immigrant, is vastbesloten om de cocaïnehandel in het Miami van de jaren '80 over te nemen. Maar in zijn gewelddadige weg naar de top werkt hij een hoop mensen in de afgrond, inclusief zichzelf.", 16, 120, "Gangster, Thriller, Mafia, Crime, Drama, Detective"),
                new MovieModel("The Matrix", "Computerhacker Thomas A. Anderson komt op een vreemde manier in contact met Morpheus. Hij leidt Thomas binnen in de `echte', maar ongekende wereld. De wereld die we kennen is volgens Morpheus een virtuele wereld, de Matrix genaamd.", 7, 90, "Action, Romance, Fantasy, Superhero, Animation"),
                new MovieModel("Forest Gump", "Forrest Gump is een simpele jongen met een laag IQ. Dit weerhoudt hem er echter niet van om een grote rol te spelen bij belangrijke gebeurtenissen in de Amerikaanse geschiedenis. Zo vecht hij mee in Vietnam en ontmoet hij grootheden als Elvis en JFK.", 16, 90, "Actie, Thriller en Avontuur."),
                new MovieModel("Bad Boys", "Undercover-agenten Mike Lowrey en Marcus Burnett hebben een grote partij drugs ter waarde van honderd miljoen dollar geconfisqueerd. Maar als de drugs uit het politiebureau in Miami worden gestolen, hebben Marcus en Mike 72 uur de tijd om de daders te vinden voordat de FBI zich ermee gaat bemoeien.", 16, 150, "Action, Comedy, Detective, Crime"),
                new MovieModel("Top Gun", "Waaghals Pete 'Maverick' Mitchell strijdt bij Top Gun, een keiharde opleiding voor toppiloten, om de beste vliegenier van zijn lichting te worden. Ondertussen valt zijn oog op de beeldschone vlieginstructeur Charlie Backwood.", 12, 120, "Action, Adventure, Thriller, Drama, Romance"),
                new MovieModel("Scarface", "Tony Montana, een Cubaanse immigrant, is vastbesloten om de cocaïnehandel in het Miami van de jaren '80 over te nemen. Maar in zijn gewelddadige weg naar de top werkt hij een hoop mensen in de afgrond, inclusief zichzelf.", 16, 120, "Gangster, Thriller, Mafia, Crime, Drama, Detective"),
                new MovieModel("The Matrix", "Computerhacker Thomas A. Anderson komt op een vreemde manier in contact met Morpheus. Hij leidt Thomas binnen in de `echte', maar ongekende wereld. De wereld die we kennen is volgens Morpheus een virtuele wereld, de Matrix genaamd.", 7, 90, "Action, Romance, Fantasy, Superhero, Animation")
            };

            List<MovieModel> vrijdagFilms = new List<MovieModel>
            {
                new MovieModel("Rocky","Rocky Balboa is een hardwerkende bokser die de top probeert te bereiken. Hij werkt in een vleesfabriek in Philadelphia en verdient wat extra geld als inner van schulden.", 12, 120, "Action, Sport, Drama"),
                new MovieModel("Indiana Jones and The Lost Ark", "Wereldreiziger en archeoloog Indiana Jones wordt net voor de Tweede Wereldoorlog door de Amerikaanse overheid ingehuurd om een religieus relikwie op te sporen, voordat dit artefact in de handen van de Nazi's valt.", 16, 150, "Adventure, Action"),
                new MovieModel("Gone In 60 Seconds", "Randall Raines, een ex-autodief, leidt al een aantal jaren een rustig leven. Maar als zijn broer zich in de nesten werkt moet Randall zijn oude activiteiten weer oppakken om zijn broeder te redden.", 18, 90, "Action, Adventure, Thriller, Drama, Horror, Detective"),
                new MovieModel("Interstellar", "Terwijl de aarde niet meer in staat is om de mensheid in haar levensbehoefte te voorzien, gaat een groep ontdekkingsreizigers, ver buiten het melkwegstelsel, op zoek naar een toekomst voor de mens achter de sterren.", 12, 90, "drama, adventure, and speculative fiction"),
                new MovieModel("Cars 2", "Racecar Lightning McQueen en Mater besluiten mee te doen aan de World Grand Prix. Mater raakt onderweg echter betrokken bij spionage.", 7, 120, "action, comedy, and spy thriller"),
                new MovieModel("Rocky", "Rocky Balboa is een hardwerkende bokser die de top probeert te bereiken. Hij werkt in een vleesfabriek in Philadelphia en verdient wat extra geld als inner van schulden.", 12, 120, "Action, Sport, Drama"),
                new MovieModel("Indiana Jones and The Lost Ark", "Wereldreiziger en archeoloog Indiana Jones wordt net voor de Tweede Wereldoorlog door de Amerikaanse overheid ingehuurd om een religieus relikwie op te sporen, voordat dit artefact in de handen van de Nazi's valt.", 16, 150, "Adventure, Action"),
                new MovieModel("Gone In 60 Seconds", "Randall Raines, een ex-autodief, leidt al een aantal jaren een rustig leven. Maar als zijn broer zich in de nesten werkt moet Randall zijn oude activiteiten weer oppakken om zijn broeder te redden.",18, 90, "Action, Adventure, Thriller, Drama, Horror, Detective"),
                new MovieModel("Interstellar", "Terwijl de aarde niet meer in staat is om de mensheid in haar levensbehoefte te voorzien, gaat een groep ontdekkingsreizigers, ver buiten het melkwegstelsel, op zoek naar een toekomst voor de mens achter de sterren.", 12, 90, "drama, adventure, and speculative fiction"),
                new MovieModel("Cars 2", "Racecar Lightning McQueen en Mater besluiten mee te doen aan de World Grand Prix. Mater raakt onderweg echter betrokken bij spionage.", 7, 120, "action, comedy, and spy thriller"),
                new MovieModel("Rocky", "Rocky Balboa is een hardwerkende bokser die de top probeert te bereiken. Hij werkt in een vleesfabriek in Philadelphia en verdient wat extra geld als inner van schulden.", 12, 120, "Action, Sport, Drama"),
                new MovieModel("Indiana Jones and The Lost Ark", "Wereldreiziger en archeoloog Indiana Jones wordt net voor de Tweede Wereldoorlog door de Amerikaanse overheid ingehuurd om een religieus relikwie op te sporen, voordat dit artefact in de handen van de Nazi's valt.", 16, 150, "Adventure, Action"),
                new MovieModel("Gone In 60 Seconds", "Randall Raines, een ex-autodief, leidt al een aantal jaren een rustig leven. Maar als zijn broer zich in de nesten werkt moet Randall zijn oude activiteiten weer oppakken om zijn broeder te redden.", 18, 90, "Action, Adventure, Thriller, Drama, Horror, Detective"),
                new MovieModel("Interstellar", "Terwijl de aarde niet meer in staat is om de mensheid in haar levensbehoefte te voorzien, gaat een groep ontdekkingsreizigers, ver buiten het melkwegstelsel, op zoek naar een toekomst voor de mens achter de sterren.", 12, 90, "drama, adventure, and speculative fiction"),
                new MovieModel("Cars 2", "Racecar Lightning McQueen en Mater besluiten mee te doen aan de World Grand Prix. Mater raakt onderweg echter betrokken bij spionage.", 7, 120, "action, comedy, and spy thriller")
            };

            List<MovieModel> zaterdagFilms = new List<MovieModel>
            {
                new MovieModel("Forest Gump", "Forrest Gump is een simpele jongen met een laag IQ. Dit weerhoudt hem er echter niet van om een grote rol te spelen bij belangrijke gebeurtenissen in de Amerikaanse geschiedenis. Zo vecht hij mee in Vietnam en ontmoet hij grootheden als Elvis en JFK.", 16, 90, "Actie, Thriller en Avontuur."),
                new MovieModel("Bad Boys", "Undercover-agenten Mike Lowrey en Marcus Burnett hebben een grote partij drugs ter waarde van honderd miljoen dollar geconfisqueerd. Maar als de drugs uit het politiebureau in Miami worden gestolen, hebben Marcus en Mike 72 uur de tijd om de daders te vinden voordat de FBI zich ermee gaat bemoeien.", 16, 150, "Action, Comedy, Detective, Crime"),
                new MovieModel("Top Gun", "Waaghals Pete 'Maverick' Mitchell strijdt bij Top Gun, een keiharde opleiding voor toppiloten, om de beste vliegenier van zijn lichting te worden. Ondertussen valt zijn oog op de beeldschone vlieginstructeur Charlie Backwood.", 12, 120, "Action, Adventure, Thriller, Drama, Romance"),
                new MovieModel("Scarface", "Tony Montana, een Cubaanse immigrant, is vastbesloten om de cocaïnehandel in het Miami van de jaren '80 over te nemen. Maar in zijn gewelddadige weg naar de top werkt hij een hoop mensen in de afgrond, inclusief zichzelf.", 16, 120, "Gangster, Thriller, Mafia, Crime, Drama, Detective"),
                new MovieModel("The Matrix", "Computerhacker Thomas A. Anderson komt op een vreemde manier in contact met Morpheus. Hij leidt Thomas binnen in de `echte', maar ongekende wereld. De wereld die we kennen is volgens Morpheus een virtuele wereld, de Matrix genaamd.", 7, 90, "Action, Romance, Fantasy, Superhero, Animation"),
                new MovieModel("Forest Gump", "Forrest Gump is een simpele jongen met een laag IQ. Dit weerhoudt hem er echter niet van om een grote rol te spelen bij belangrijke gebeurtenissen in de Amerikaanse geschiedenis. Zo vecht hij mee in Vietnam en ontmoet hij grootheden als Elvis en JFK.", 16, 90, "Actie, Thriller en Avontuur."),
                new MovieModel("Bad Boys", "Undercover-agenten Mike Lowrey en Marcus Burnett hebben een grote partij drugs ter waarde van honderd miljoen dollar geconfisqueerd. Maar als de drugs uit het politiebureau in Miami worden gestolen, hebben Marcus en Mike 72 uur de tijd om de daders te vinden voordat de FBI zich ermee gaat bemoeien.", 16, 150, "Action, Comedy, Detective, Crime"),
                new MovieModel("Top Gun", "Waaghals Pete 'Maverick' Mitchell strijdt bij Top Gun, een keiharde opleiding voor toppiloten, om de beste vliegenier van zijn lichting te worden. Ondertussen valt zijn oog op de beeldschone vlieginstructeur Charlie Backwood.", 12, 120, "Action, Adventure, Thriller, Drama, Romance"),
                new MovieModel("Scarface", "Tony Montana, een Cubaanse immigrant, is vastbesloten om de cocaïnehandel in het Miami van de jaren '80 over te nemen. Maar in zijn gewelddadige weg naar de top werkt hij een hoop mensen in de afgrond, inclusief zichzelf.", 16, 120, "Gangster, Thriller, Mafia, Crime, Drama, Detective"),
                new MovieModel("The Matrix", "Computerhacker Thomas A. Anderson komt op een vreemde manier in contact met Morpheus. Hij leidt Thomas binnen in de `echte', maar ongekende wereld. De wereld die we kennen is volgens Morpheus een virtuele wereld, de Matrix genaamd.", 7, 90, "Action, Romance, Fantasy, Superhero, Animation"),
                new MovieModel("Forest Gump", "Forrest Gump is een simpele jongen met een laag IQ. Dit weerhoudt hem er echter niet van om een grote rol te spelen bij belangrijke gebeurtenissen in de Amerikaanse geschiedenis. Zo vecht hij mee in Vietnam en ontmoet hij grootheden als Elvis en JFK.", 16, 90, "Actie, Thriller en Avontuur."),
                new MovieModel("Bad Boys", "Undercover-agenten Mike Lowrey en Marcus Burnett hebben een grote partij drugs ter waarde van honderd miljoen dollar geconfisqueerd. Maar als de drugs uit het politiebureau in Miami worden gestolen, hebben Marcus en Mike 72 uur de tijd om de daders te vinden voordat de FBI zich ermee gaat bemoeien.", 16, 150, "Action, Comedy, Detective, Crime"),
                new MovieModel("Top Gun", "Waaghals Pete 'Maverick' Mitchell strijdt bij Top Gun, een keiharde opleiding voor toppiloten, om de beste vliegenier van zijn lichting te worden. Ondertussen valt zijn oog op de beeldschone vlieginstructeur Charlie Backwood.", 12, 120, "Action, Adventure, Thriller, Drama, Romance"),
                new MovieModel("Scarface", "Tony Montana, een Cubaanse immigrant, is vastbesloten om de cocaïnehandel in het Miami van de jaren '80 over te nemen. Maar in zijn gewelddadige weg naar de top werkt hij een hoop mensen in de afgrond, inclusief zichzelf.", 16, 120, "Gangster, Thriller, Mafia, Crime, Drama, Detective"),
                new MovieModel("The Matrix", "Computerhacker Thomas A. Anderson komt op een vreemde manier in contact met Morpheus. Hij leidt Thomas binnen in de `echte', maar ongekende wereld. De wereld die we kennen is volgens Morpheus een virtuele wereld, de Matrix genaamd.", 7, 90, "Action, Romance, Fantasy, Superhero, Animation")
            };

            List<MovieModel> zondagFilms = new List<MovieModel>
            {
                new MovieModel("Rocky","Rocky Balboa is een hardwerkende bokser die de top probeert te bereiken. Hij werkt in een vleesfabriek in Philadelphia en verdient wat extra geld als inner van schulden.", 12, 120, "Action, Sport, Drama"),
                new MovieModel("Indiana Jones and The Lost Ark", "Wereldreiziger en archeoloog Indiana Jones wordt net voor de Tweede Wereldoorlog door de Amerikaanse overheid ingehuurd om een religieus relikwie op te sporen, voordat dit artefact in de handen van de Nazi's valt.", 16, 150, "Adventure, Action"),
                new MovieModel("Gone In 60 Seconds", "Randall Raines, een ex-autodief, leidt al een aantal jaren een rustig leven. Maar als zijn broer zich in de nesten werkt moet Randall zijn oude activiteiten weer oppakken om zijn broeder te redden.", 18, 90, "Action, Adventure, Thriller, Drama, Horror, Detective"),
                new MovieModel("Interstellar", "Terwijl de aarde niet meer in staat is om de mensheid in haar levensbehoefte te voorzien, gaat een groep ontdekkingsreizigers, ver buiten het melkwegstelsel, op zoek naar een toekomst voor de mens achter de sterren.", 12, 90, "drama, adventure, and speculative fiction"),
                new MovieModel("Cars 2", "Racecar Lightning McQueen en Mater besluiten mee te doen aan de World Grand Prix. Mater raakt onderweg echter betrokken bij spionage.", 7, 120, "action, comedy, and spy thriller"),
                new MovieModel("Rocky", "Rocky Balboa is een hardwerkende bokser die de top probeert te bereiken. Hij werkt in een vleesfabriek in Philadelphia en verdient wat extra geld als inner van schulden.", 12, 120, "Action, Sport, Drama"),
                new MovieModel("Indiana Jones and The Lost Ark", "Wereldreiziger en archeoloog Indiana Jones wordt net voor de Tweede Wereldoorlog door de Amerikaanse overheid ingehuurd om een religieus relikwie op te sporen, voordat dit artefact in de handen van de Nazi's valt.", 16, 150, "Adventure, Action"),
                new MovieModel("Gone In 60 Seconds", "Randall Raines, een ex-autodief, leidt al een aantal jaren een rustig leven. Maar als zijn broer zich in de nesten werkt moet Randall zijn oude activiteiten weer oppakken om zijn broeder te redden.",18, 90, "Action, Adventure, Thriller, Drama, Horror, Detective"),
                new MovieModel("Interstellar", "Terwijl de aarde niet meer in staat is om de mensheid in haar levensbehoefte te voorzien, gaat een groep ontdekkingsreizigers, ver buiten het melkwegstelsel, op zoek naar een toekomst voor de mens achter de sterren.", 12, 90, "drama, adventure, and speculative fiction"),
                new MovieModel("Cars 2", "Racecar Lightning McQueen en Mater besluiten mee te doen aan de World Grand Prix. Mater raakt onderweg echter betrokken bij spionage.", 7, 120, "action, comedy, and spy thriller"),
                new MovieModel("Rocky", "Rocky Balboa is een hardwerkende bokser die de top probeert te bereiken. Hij werkt in een vleesfabriek in Philadelphia en verdient wat extra geld als inner van schulden.", 12, 120, "Action, Sport, Drama"),
                new MovieModel("Indiana Jones and The Lost Ark", "Wereldreiziger en archeoloog Indiana Jones wordt net voor de Tweede Wereldoorlog door de Amerikaanse overheid ingehuurd om een religieus relikwie op te sporen, voordat dit artefact in de handen van de Nazi's valt.", 16, 150, "Adventure, Action"),
                new MovieModel("Gone In 60 Seconds", "Randall Raines, een ex-autodief, leidt al een aantal jaren een rustig leven. Maar als zijn broer zich in de nesten werkt moet Randall zijn oude activiteiten weer oppakken om zijn broeder te redden.", 18, 90, "Action, Adventure, Thriller, Drama, Horror, Detective"),
                new MovieModel("Interstellar", "Terwijl de aarde niet meer in staat is om de mensheid in haar levensbehoefte te voorzien, gaat een groep ontdekkingsreizigers, ver buiten het melkwegstelsel, op zoek naar een toekomst voor de mens achter de sterren.", 12, 90, "drama, adventure, and speculative fiction"),
                new MovieModel("Cars 2", "Racecar Lightning McQueen en Mater besluiten mee te doen aan de World Grand Prix. Mater raakt onderweg echter betrokken bij spionage.", 7, 120, "action, comedy, and spy thriller")
            };

            List<TimeTableModel> maandagtimeTableList = new List<TimeTableModel>
            {
                new TimeTableModel(1, 1, DateTime.ParseExact("2024-05-07 10:00:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture), DateTime.ParseExact("2024-05-07 12:00:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)),
                new TimeTableModel(1, 2, DateTime.ParseExact("2024-05-07 12:30:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture), DateTime.ParseExact("2024-05-07 15:00:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)),
                new TimeTableModel(1, 3, DateTime.ParseExact("2024-05-07 15:30:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture), DateTime.ParseExact("2024-05-07 17:00:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)),
                new TimeTableModel(1, 4, DateTime.ParseExact("2024-05-07 17:30:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture), DateTime.ParseExact("2024-05-07 19:00:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)),
                new TimeTableModel(1, 5, DateTime.ParseExact("2024-05-07 19:30:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture), DateTime.ParseExact("2024-05-07 21:30:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)),
                new TimeTableModel(2, 6, DateTime.ParseExact("2024-05-07 10:00:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture), DateTime.ParseExact("2024-05-07 12:00:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)),
                new TimeTableModel(2, 7, DateTime.ParseExact("2024-05-07 12:30:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture), DateTime.ParseExact("2024-05-07 15:00:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)),
                new TimeTableModel(2, 8, DateTime.ParseExact("2024-05-07 15:30:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture), DateTime.ParseExact("2024-05-07 17:00:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)),
                new TimeTableModel(2, 9, DateTime.ParseExact("2024-05-07 17:30:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture), DateTime.ParseExact("2024-05-07 19:00:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)),
                new TimeTableModel(2, 10, DateTime.ParseExact("2024-05-07 19:30:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture), DateTime.ParseExact("2024-05-07 21:30:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)),
                new TimeTableModel(3, 11, DateTime.ParseExact("2024-05-07 10:00:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture), DateTime.ParseExact("2024-05-07 12:00:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)),
                new TimeTableModel(3, 12, DateTime.ParseExact("2024-05-07 12:30:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture), DateTime.ParseExact("2024-05-07 15:00:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)),
                new TimeTableModel(3, 13, DateTime.ParseExact("2024-05-07 15:30:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture), DateTime.ParseExact("2024-05-07 17:00:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)),
                new TimeTableModel(3, 14, DateTime.ParseExact("2024-05-07 17:30:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture), DateTime.ParseExact("2024-05-07 19:00:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)),
                new TimeTableModel(3, 15, DateTime.ParseExact("2024-05-07 19:30:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture), DateTime.ParseExact("2024-05-07 21:30:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture))
            };

            List<TimeTableModel> dinsdagTimeTableModelList = new List<TimeTableModel>
            {
                new TimeTableModel(1, 1, DateTime.ParseExact("2024-05-08 10:00:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture), DateTime.ParseExact("2024-05-08 11:30:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)),
                new TimeTableModel(1, 2, DateTime.ParseExact("2024-05-08 12:00:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture), DateTime.ParseExact("2024-05-08 14:30:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)),
                new TimeTableModel(1, 3, DateTime.ParseExact("2024-05-08 15:00:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture), DateTime.ParseExact("2024-05-08 17:00:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)),
                new TimeTableModel(1, 4, DateTime.ParseExact("2024-05-08 17:30:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture), DateTime.ParseExact("2024-05-08 19:30:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)),
                new TimeTableModel(1, 5, DateTime.ParseExact("2024-05-08 20:00:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture), DateTime.ParseExact("2024-05-08 21:30:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)),
                new TimeTableModel(2, 6, DateTime.ParseExact("2024-05-08 10:00:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture), DateTime.ParseExact("2024-05-08 11:30:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)),
                new TimeTableModel(2, 7, DateTime.ParseExact("2024-05-08 12:00:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture), DateTime.ParseExact("2024-05-08 14:30:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)),
                new TimeTableModel(2, 8, DateTime.ParseExact("2024-05-08 15:00:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture), DateTime.ParseExact("2024-05-08 17:00:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)),
                new TimeTableModel(2, 9, DateTime.ParseExact("2024-05-08 17:30:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture), DateTime.ParseExact("2024-05-08 19:30:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)),
                new TimeTableModel(2, 10, DateTime.ParseExact("2024-05-08 20:00:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture), DateTime.ParseExact("2024-05-08 21:30:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)),
                new TimeTableModel(3, 11, DateTime.ParseExact("2024-05-08 10:00:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture), DateTime.ParseExact("2024-05-08 11:30:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)),
                new TimeTableModel(3, 12, DateTime.ParseExact("2024-05-08 12:00:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture), DateTime.ParseExact("2024-05-08 14:30:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)),
                new TimeTableModel(3, 13, DateTime.ParseExact("2024-05-08 15:00:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture), DateTime.ParseExact("2024-05-08 17:00:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)),
                new TimeTableModel(3, 14, DateTime.ParseExact("2024-05-08 17:30:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture), DateTime.ParseExact("2024-05-08 19:30:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)),
                new TimeTableModel(3, 15, DateTime.ParseExact("2024-05-08 20:00:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture), DateTime.ParseExact("2024-05-08 21:30:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture))
            };

            List<TimeTableModel> woensdagTimeTableModelList = new List<TimeTableModel>
            {
                new TimeTableModel(1, 1, DateTime.ParseExact("2024-05-09 10:00:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture), DateTime.ParseExact("2024-05-09 12:00:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)),
                new TimeTableModel(1, 2, DateTime.ParseExact("2024-05-09 12:30:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture), DateTime.ParseExact("2024-05-09 15:00:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)),
                new TimeTableModel(1, 3, DateTime.ParseExact("2024-05-09 15:30:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture), DateTime.ParseExact("2024-05-09 17:00:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)),
                new TimeTableModel(1, 4, DateTime.ParseExact("2024-05-09 17:30:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture), DateTime.ParseExact("2024-05-09 19:00:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)),
                new TimeTableModel(1, 5, DateTime.ParseExact("2024-05-09 19:30:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture), DateTime.ParseExact("2024-05-09 21:30:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)),
                new TimeTableModel(2, 6, DateTime.ParseExact("2024-05-09 10:00:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture), DateTime.ParseExact("2024-05-09 12:00:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)),
                new TimeTableModel(2, 7, DateTime.ParseExact("2024-05-09 12:30:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture), DateTime.ParseExact("2024-05-09 15:00:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)),
                new TimeTableModel(2, 8, DateTime.ParseExact("2024-05-09 15:30:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture), DateTime.ParseExact("2024-05-09 17:00:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)),
                new TimeTableModel(2, 9, DateTime.ParseExact("2024-05-09 17:30:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture), DateTime.ParseExact("2024-05-09 19:00:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)),
                new TimeTableModel(2, 10, DateTime.ParseExact("2024-05-09 19:30:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture), DateTime.ParseExact("2024-05-09 21:30:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)),
                new TimeTableModel(3, 11, DateTime.ParseExact("2024-05-09 10:00:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture), DateTime.ParseExact("2024-05-09 12:00:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)),
                new TimeTableModel(3, 12, DateTime.ParseExact("2024-05-09 12:30:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture), DateTime.ParseExact("2024-05-09 15:00:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)),
                new TimeTableModel(3, 13, DateTime.ParseExact("2024-05-09 15:30:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture), DateTime.ParseExact("2024-05-09 17:00:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)),
                new TimeTableModel(3, 14, DateTime.ParseExact("2024-05-09 17:30:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture), DateTime.ParseExact("2024-05-09 19:00:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)),
                new TimeTableModel(3, 15, DateTime.ParseExact("2024-05-09 19:30:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture), DateTime.ParseExact("2024-05-09 21:30:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture))
            };

            List<TimeTableModel> donderdagTimeTableModelList = new List<TimeTableModel>
            {
                new TimeTableModel(1, 1, DateTime.ParseExact("2024-05-10 10:00:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture), DateTime.ParseExact("2024-05-10 11:30:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)),
                new TimeTableModel(1, 2, DateTime.ParseExact("2024-05-10 12:00:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture), DateTime.ParseExact("2024-05-10 14:30:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)),
                new TimeTableModel(1, 3, DateTime.ParseExact("2024-05-10 15:00:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture), DateTime.ParseExact("2024-05-10 17:00:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)),
                new TimeTableModel(1, 4, DateTime.ParseExact("2024-05-10 17:30:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture), DateTime.ParseExact("2024-05-10 19:30:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)),
                new TimeTableModel(1, 5, DateTime.ParseExact("2024-05-10 20:00:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture), DateTime.ParseExact("2024-05-10 21:30:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)),
                new TimeTableModel(2, 6, DateTime.ParseExact("2024-05-10 10:00:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture), DateTime.ParseExact("2024-05-10 11:30:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)),
                new TimeTableModel(2, 7, DateTime.ParseExact("2024-05-10 12:00:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture), DateTime.ParseExact("2024-05-10 14:30:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)),
                new TimeTableModel(2, 8, DateTime.ParseExact("2024-05-10 15:00:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture), DateTime.ParseExact("2024-05-10 17:00:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)),
                new TimeTableModel(2, 9, DateTime.ParseExact("2024-05-10 17:30:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture), DateTime.ParseExact("2024-05-10 19:30:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)),
                new TimeTableModel(2, 10, DateTime.ParseExact("2024-05-10 20:00:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture), DateTime.ParseExact("2024-05-10 21:30:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)),
                new TimeTableModel(3, 11, DateTime.ParseExact("2024-05-10 10:00:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture), DateTime.ParseExact("2024-05-10 11:30:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)),
                new TimeTableModel(3, 12, DateTime.ParseExact("2024-05-10 12:00:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture), DateTime.ParseExact("2024-05-10 14:30:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)),
                new TimeTableModel(3, 13, DateTime.ParseExact("2024-05-10 15:00:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture), DateTime.ParseExact("2024-05-10 17:00:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)),
                new TimeTableModel(3, 14, DateTime.ParseExact("2024-05-10 17:30:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture), DateTime.ParseExact("2024-05-10 19:30:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)),
                new TimeTableModel(3, 15, DateTime.ParseExact("2024-05-10 20:00:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture), DateTime.ParseExact("2024-05-10 21:30:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture))
            };

            List<TimeTableModel> vrijdagTimeTableModelList = new List<TimeTableModel>
            {
                new TimeTableModel(1, 1, DateTime.ParseExact("2024-05-11 10:00:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture), DateTime.ParseExact("2024-05-11 12:00:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)),
                new TimeTableModel(1, 2, DateTime.ParseExact("2024-05-11 12:30:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture), DateTime.ParseExact("2024-05-11 15:00:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)),
                new TimeTableModel(1, 3, DateTime.ParseExact("2024-05-11 15:30:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture), DateTime.ParseExact("2024-05-11 17:00:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)),
                new TimeTableModel(1, 4, DateTime.ParseExact("2024-05-11 17:30:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture), DateTime.ParseExact("2024-05-11 19:00:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)),
                new TimeTableModel(1, 5, DateTime.ParseExact("2024-05-11 19:30:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture), DateTime.ParseExact("2024-05-11 21:30:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)),
                new TimeTableModel(2, 6, DateTime.ParseExact("2024-05-11 10:00:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture), DateTime.ParseExact("2024-05-11 12:00:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)),
                new TimeTableModel(2, 7, DateTime.ParseExact("2024-05-11 12:30:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture), DateTime.ParseExact("2024-05-11 15:00:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)),
                new TimeTableModel(2, 8, DateTime.ParseExact("2024-05-11 15:30:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture), DateTime.ParseExact("2024-05-11 17:00:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)),
                new TimeTableModel(2, 9, DateTime.ParseExact("2024-05-11 17:30:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture), DateTime.ParseExact("2024-05-11 19:00:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)),
                new TimeTableModel(2, 10, DateTime.ParseExact("2024-05-11 19:30:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture), DateTime.ParseExact("2024-05-11 21:30:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)),
                new TimeTableModel(3, 11, DateTime.ParseExact("2024-05-11 10:00:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture), DateTime.ParseExact("2024-05-11 12:00:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)),
                new TimeTableModel(3, 12, DateTime.ParseExact("2024-05-11 12:30:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture), DateTime.ParseExact("2024-05-11 15:00:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)),
                new TimeTableModel(3, 13, DateTime.ParseExact("2024-05-11 15:30:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture), DateTime.ParseExact("2024-05-11 17:00:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)),
                new TimeTableModel(3, 14, DateTime.ParseExact("2024-05-11 17:30:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture), DateTime.ParseExact("2024-05-11 19:00:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)),
                new TimeTableModel(3, 15, DateTime.ParseExact("2024-05-11 19:30:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture), DateTime.ParseExact("2024-05-11 21:30:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture))
            };

            List<TimeTableModel> zaterdagTimeTableModelList = new List<TimeTableModel>
            {
                new TimeTableModel(1, 1, DateTime.ParseExact("2024-05-12 10:00:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture), DateTime.ParseExact("2024-05-12 11:30:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)),
                new TimeTableModel(1, 2, DateTime.ParseExact("2024-05-12 12:00:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture), DateTime.ParseExact("2024-05-12 14:30:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)),
                new TimeTableModel(1, 3, DateTime.ParseExact("2024-05-12 15:00:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture), DateTime.ParseExact("2024-05-12 17:00:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)),
                new TimeTableModel(1, 4, DateTime.ParseExact("2024-05-12 17:30:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture), DateTime.ParseExact("2024-05-12 19:30:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)),
                new TimeTableModel(1, 5, DateTime.ParseExact("2024-05-12 20:00:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture), DateTime.ParseExact("2024-05-12 21:30:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)),
                new TimeTableModel(2, 6, DateTime.ParseExact("2024-05-12 10:00:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture), DateTime.ParseExact("2024-05-12 11:30:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)),
                new TimeTableModel(2, 7, DateTime.ParseExact("2024-05-12 12:00:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture), DateTime.ParseExact("2024-05-12 14:30:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)),
                new TimeTableModel(2, 8, DateTime.ParseExact("2024-05-12 15:00:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture), DateTime.ParseExact("2024-05-12 17:00:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)),
                new TimeTableModel(2, 9, DateTime.ParseExact("2024-05-12 17:30:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture), DateTime.ParseExact("2024-05-12 19:30:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)),
                new TimeTableModel(2, 10, DateTime.ParseExact("2024-05-12 20:00:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture), DateTime.ParseExact("2024-05-12 21:30:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)),
                new TimeTableModel(3, 11, DateTime.ParseExact("2024-05-12 10:00:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture), DateTime.ParseExact("2024-05-12 11:30:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)),
                new TimeTableModel(3, 12, DateTime.ParseExact("2024-05-12 12:00:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture), DateTime.ParseExact("2024-05-12 14:30:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)),
                new TimeTableModel(3, 13, DateTime.ParseExact("2024-05-12 15:00:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture), DateTime.ParseExact("2024-05-12 17:00:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)),
                new TimeTableModel(3, 14, DateTime.ParseExact("2024-05-12 17:30:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture), DateTime.ParseExact("2024-05-12 19:30:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)),
                new TimeTableModel(3, 15, DateTime.ParseExact("2024-05-12 20:00:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture), DateTime.ParseExact("2024-05-12 21:30:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture))
            };

            List<TimeTableModel> zondagTimeTableModelList = new List<TimeTableModel>
            {
                new TimeTableModel(1, 1, DateTime.ParseExact("2024-05-13 10:00:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture), DateTime.ParseExact("2024-05-13 12:00:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)),
                new TimeTableModel(1, 2, DateTime.ParseExact("2024-05-13 12:30:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture), DateTime.ParseExact("2024-05-13 15:00:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)),
                new TimeTableModel(1, 3, DateTime.ParseExact("2024-05-13 15:30:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture), DateTime.ParseExact("2024-05-13 17:00:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)),
                new TimeTableModel(1, 4, DateTime.ParseExact("2024-05-13 17:30:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture), DateTime.ParseExact("2024-05-13 19:00:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)),
                new TimeTableModel(1, 5, DateTime.ParseExact("2024-05-13 19:30:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture), DateTime.ParseExact("2024-05-13 21:30:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)),
                new TimeTableModel(2, 6, DateTime.ParseExact("2024-05-13 10:00:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture), DateTime.ParseExact("2024-05-13 12:00:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)),
                new TimeTableModel(2, 7, DateTime.ParseExact("2024-05-13 12:30:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture), DateTime.ParseExact("2024-05-13 15:00:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)),
                new TimeTableModel(2, 8, DateTime.ParseExact("2024-05-13 15:30:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture), DateTime.ParseExact("2024-05-13 17:00:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)),
                new TimeTableModel(2, 9, DateTime.ParseExact("2024-05-13 17:30:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture), DateTime.ParseExact("2024-05-13 19:00:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)),
                new TimeTableModel(2, 10, DateTime.ParseExact("2024-05-13 19:30:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture), DateTime.ParseExact("2024-05-13 21:30:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)),
                new TimeTableModel(3, 11, DateTime.ParseExact("2024-05-13 10:00:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture), DateTime.ParseExact("2024-05-13 12:00:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)),
                new TimeTableModel(3, 12, DateTime.ParseExact("2024-05-13 12:30:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture), DateTime.ParseExact("2024-05-13 15:00:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)),
                new TimeTableModel(3, 13, DateTime.ParseExact("2024-05-13 15:30:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture), DateTime.ParseExact("2024-05-13 17:00:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)),
                new TimeTableModel(3, 14, DateTime.ParseExact("2024-05-13 17:30:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture), DateTime.ParseExact("2024-05-13 19:00:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)),
                new TimeTableModel(3, 15, DateTime.ParseExact("2024-05-13 19:30:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture), DateTime.ParseExact("2024-05-13 21:30:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture))
            };
            DaySelectorMenu daySelectorMenu = new();
        });
    }
    public static void UseMenu(Action printmenu)
    {
        menu.UseMenu(printmenu);
    }
}