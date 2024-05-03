using DataAccessLibrary;
using Models;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Project_B
{
    class Program
    {
        public static void Main()
        {

            InputMenu menu = new InputMenu("| Main menu |", true);
            menu.Add("Setup Database", (x) =>
            {
                //Opzet Sqlite database
                SQLite.SetupProjectB();
                Console.ReadLine();
            });
            menu.Add("Test Author", (x) =>
            {
                Author testAuthor = new Author(1, "John", "Not succesfull", 25);
                Console.WriteLine($"{testAuthor.Name} - {testAuthor.Age} :\n{testAuthor.Description}");
                Console.ReadLine();
            });
            menu.Add("Layout creator", (x) =>
            {
                DataAccessLibrary.Layout.MakeNewLayout();
                /*
                Als klant wil ik de stoelen in een zaal zien omdat ik wil weten waar ik kan zitten. -Chris
                Als klant wil ik zien wat voor type stoel een bepaalde stoel is, zodat ik mijn favoriete type kan kiezen.(Love-seat, regular, deluxe) -Chris
                */
            });
            menu.Add("Edit layout item", (x) =>
            {
                List<Seat> layout1 = new List<Seat>{
                    new Seat(0, 1, "0", " ", " "),
                    new Seat(1, 1, "1", " ", " "),
                    new Seat(2, 1, "2", "1", "Normaal"),
                    new Seat(3, 1, "3", "1", "Normaal"),
                    new Seat(4, 1, "4", "1", "Normaal"),
                    new Seat(5, 1, "5", "1", "Normaal"),
                    new Seat(6, 1, "6", "1", "Normaal"),
                    new Seat(7, 1, "7", "1", "Normaal"),
                    new Seat(8, 1, "8", "1", "Normaal"),
                    new Seat(9, 1, "9", "1", "Normaal"),
                    new Seat(10, 1, "10", " ", " "),
                    new Seat(11, 1, "11", " ", " "),
                    new Seat(12, 1, "12", " ", " "),
                    new Seat(13, 1, "13", " ", " ")
                };
                List<Room> roomList = new List<Room> { new Room(1, "Room1", layout1.Count, 6) };
                InputMenu selectRoom = new InputMenu("| Select room to edit |");
                foreach (Room room in roomList/*getRoomFromDatabase() - Aymane*/) {
                    selectRoom.Add($"{room.Name}", (x) => DataAccessLibrary.Layout.editLayout(layout1/*getLayoutFromDatabase() - Aymane*/, room));
                }
                selectRoom.UseMenu();
            });
            menu.Add("Tijdschema Ma 07 Mei tot Zo 13 Mei", (x) =>
            {
                List<Room> roomList = new List<Room>
                {
                    new Room(1, "Room 1", 150, 1),
                    new Room(2, "Room 2", 300, 1),
                    new Room(3, "Room 3", 500, 1)

                };
                List<Movie> maandagFilms = new List<Movie>
                {
                    new Movie(1, "Rocky", 1, 12, "Rocky Balboa is een hardwerkende bokser die de top probeert te bereiken. Hij werkt in een vleesfabriek in Philadelphia en verdient wat extra geld als inner van schulden.", "Action, Sport, Drama", 120),
                    new Movie(2, "Indiana Jones and The Lost Ark", 2, 16, "Wereldreiziger en archeoloog Indiana Jones wordt net voor de Tweede Wereldoorlog door de Amerikaanse overheid ingehuurd om een religieus relikwie op te sporen, voordat dit artefact in de handen van de Nazi's valt.", "Adventure, Action", 150),
                    new Movie(3, "Gone In 60 Seconds", 3, 18, "Randall Raines, een ex-autodief, leidt al een aantal jaren een rustig leven. Maar als zijn broer zich in de nesten werkt moet Randall zijn oude activiteiten weer oppakken om zijn broeder te redden.", "Action, Adventure, Thriller, Drama, Horror, Detective", 90),
                    new Movie(4, "Interstellar", 4, 12, "Terwijl de aarde niet meer in staat is om de mensheid in haar levensbehoefte te voorzien, gaat een groep ontdekkingsreizigers, ver buiten het melkwegstelsel, op zoek naar een toekomst voor de mens achter de sterren.", "drama, adventure, and speculative fiction", 90),
                    new Movie(5, "Cars 2", 5, 7, "Racecar Lightning McQueen en Mater besluiten mee te doen aan de World Grand Prix. Mater raakt onderweg echter betrokken bij spionage.", "action, comedy, and spy thriller", 120),
                    new Movie(6, "Rocky", 1, 12, "Rocky Balboa is een hardwerkende bokser die de top probeert te bereiken. Hij werkt in een vleesfabriek in Philadelphia en verdient wat extra geld als inner van schulden.", "Action, Sport, Drama", 120),
                    new Movie(7, "Indiana Jones and The Lost Ark", 2, 16, "Wereldreiziger en archeoloog Indiana Jones wordt net voor de Tweede Wereldoorlog door de Amerikaanse overheid ingehuurd om een religieus relikwie op te sporen, voordat dit artefact in de handen van de Nazi's valt.", "Adventure, Action", 150),
                    new Movie(8, "Gone In 60 Seconds", 3, 18, "Randall Raines, een ex-autodief, leidt al een aantal jaren een rustig leven. Maar als zijn broer zich in de nesten werkt moet Randall zijn oude activiteiten weer oppakken om zijn broeder te redden.", "Action, Adventure, Thriller, Drama, Horror, Detective", 90),
                    new Movie(9, "Interstellar", 4, 12, "Terwijl de aarde niet meer in staat is om de mensheid in haar levensbehoefte te voorzien, gaat een groep ontdekkingsreizigers, ver buiten het melkwegstelsel, op zoek naar een toekomst voor de mens achter de sterren.", "drama, adventure, and speculative fiction", 90),
                    new Movie(10, "Cars 2", 5, 7, "Racecar Lightning McQueen en Mater besluiten mee te doen aan de World Grand Prix. Mater raakt onderweg echter betrokken bij spionage.", "action, comedy, and spy thriller", 120),
                    new Movie(11, "Rocky", 1, 12, "Rocky Balboa is een hardwerkende bokser die de top probeert te bereiken. Hij werkt in een vleesfabriek in Philadelphia en verdient wat extra geld als inner van schulden.", "Action, Sport, Drama", 120),
                    new Movie(12, "Indiana Jones and The Lost Ark", 2, 16, "Wereldreiziger en archeoloog Indiana Jones wordt net voor de Tweede Wereldoorlog door de Amerikaanse overheid ingehuurd om een religieus relikwie op te sporen, voordat dit artefact in de handen van de Nazi's valt.", "Adventure, Action", 150),
                    new Movie(13, "Gone In 60 Seconds", 3, 18, "Randall Raines, een ex-autodief, leidt al een aantal jaren een rustig leven. Maar als zijn broer zich in de nesten werkt moet Randall zijn oude activiteiten weer oppakken om zijn broeder te redden.", "Action, Adventure, Thriller, Drama, Horror, Detective", 90),
                    new Movie(14, "Interstellar", 4, 12, "Terwijl de aarde niet meer in staat is om de mensheid in haar levensbehoefte te voorzien, gaat een groep ontdekkingsreizigers, ver buiten het melkwegstelsel, op zoek naar een toekomst voor de mens achter de sterren.", "drama, adventure, and speculative fiction", 90),
                    new Movie(15, "Cars 2", 5, 7, "Racecar Lightning McQueen en Mater besluiten mee te doen aan de World Grand Prix. Mater raakt onderweg echter betrokken bij spionage.", "action, comedy, and spy thriller", 120)
                };

                List<Movie> dinsdagFilms = new List<Movie>
                {
                    new Movie(1, "Forest Gump", 6, 16, "Forrest Gump is een simpele jongen met een laag IQ. Dit weerhoudt hem er echter niet van om een grote rol te spelen bij belangrijke gebeurtenissen in de Amerikaanse geschiedenis. Zo vecht hij mee in Vietnam en ontmoet hij grootheden als Elvis en JFK.", "Actie, Thriller en Avontuur.", 90),
                    new Movie(2, "Bad Boys", 7, 16, "Undercover-agenten Mike Lowrey en Marcus Burnett hebben een grote partij drugs ter waarde van honderd miljoen dollar geconfisqueerd. Maar als de drugs uit het politiebureau in Miami worden gestolen, hebben Marcus en Mike 72 uur de tijd om de daders te vinden voordat de FBI zich ermee gaat bemoeien.", "Action, Comedy, Detective, Crime", 150),
                    new Movie(3, "Top Gun", 8, 12, "Waaghals Pete 'Maverick' Mitchell strijdt bij Top Gun, een keiharde opleiding voor toppiloten, om de beste vliegenier van zijn lichting te worden. Ondertussen valt zijn oog op de beeldschone vlieginstructeur Charlie Backwood.", "Action, Adventure, Thriller, Drama, Romance", 120),
                    new Movie(4, "Scarface", 9, 16, "Tony Montana, een Cubaanse immigrant, is vastbesloten om de cocaïnehandel in het Miami van de jaren '80 over te nemen. Maar in zijn gewelddadige weg naar de top werkt hij een hoop mensen in de afgrond, inclusief zichzelf.", "Gangster, Thriller, Mafia, Crime, Drama, Detective", 120),
                    new Movie(5, "The Matrix", 10, 7, "Computerhacker Thomas A. Anderson komt op een vreemde manier in contact met Morpheus. Hij leidt Thomas binnen in de `echte', maar ongekende wereld. De wereld die we kennen is volgens Morpheus een virtuele wereld, de Matrix genaamd.", "Action, Romance, Fantasy, Superhero, Animation", 90),
                    new Movie(6, "Forest Gump", 6, 16, "Forrest Gump is een simpele jongen met een laag IQ. Dit weerhoudt hem er echter niet van om een grote rol te spelen bij belangrijke gebeurtenissen in de Amerikaanse geschiedenis. Zo vecht hij mee in Vietnam en ontmoet hij grootheden als Elvis en JFK.", "Actie, Thriller en Avontuur.", 90),
                    new Movie(7, "Bad Boys", 7, 16, "Undercover-agenten Mike Lowrey en Marcus Burnett hebben een grote partij drugs ter waarde van honderd miljoen dollar geconfisqueerd. Maar als de drugs uit het politiebureau in Miami worden gestolen, hebben Marcus en Mike 72 uur de tijd om de daders te vinden voordat de FBI zich ermee gaat bemoeien.", "Action, Comedy, Detective, Crime", 150),
                    new Movie(8, "Top Gun", 8, 12, "Waaghals Pete 'Maverick' Mitchell strijdt bij Top Gun, een keiharde opleiding voor toppiloten, om de beste vliegenier van zijn lichting te worden. Ondertussen valt zijn oog op de beeldschone vlieginstructeur Charlie Backwood.", "Action, Adventure, Thriller, Drama, Romance", 120),
                    new Movie(9, "Scarface", 9, 16, "Tony Montana, een Cubaanse immigrant, is vastbesloten om de cocaïnehandel in het Miami van de jaren '80 over te nemen. Maar in zijn gewelddadige weg naar de top werkt hij een hoop mensen in de afgrond, inclusief zichzelf.", "Gangster, Thriller, Mafia, Crime, Drama, Detective", 120),
                    new Movie(10, "The Matrix", 10, 7, "Computerhacker Thomas A. Anderson komt op een vreemde manier in contact met Morpheus. Hij leidt Thomas binnen in de `echte', maar ongekende wereld. De wereld die we kennen is volgens Morpheus een virtuele wereld, de Matrix genaamd.", "Action, Romance, Fantasy, Superhero, Animation", 90),
                    new Movie(11, "Forest Gump", 6, 16, "Forrest Gump is een simpele jongen met een laag IQ. Dit weerhoudt hem er echter niet van om een grote rol te spelen bij belangrijke gebeurtenissen in de Amerikaanse geschiedenis. Zo vecht hij mee in Vietnam en ontmoet hij grootheden als Elvis en JFK.", "Actie, Thriller en Avontuur.", 90),
                    new Movie(12, "Bad Boys", 7, 16, "Undercover-agenten Mike Lowrey en Marcus Burnett hebben een grote partij drugs ter waarde van honderd miljoen dollar geconfisqueerd. Maar als de drugs uit het politiebureau in Miami worden gestolen, hebben Marcus en Mike 72 uur de tijd om de daders te vinden voordat de FBI zich ermee gaat bemoeien.", "Action, Comedy, Detective, Crime", 150),
                    new Movie(13, "Top Gun", 8, 12, "Waaghals Pete 'Maverick' Mitchell strijdt bij Top Gun, een keiharde opleiding voor toppiloten, om de beste vliegenier van zijn lichting te worden. Ondertussen valt zijn oog op de beeldschone vlieginstructeur Charlie Backwood.", "Action, Adventure, Thriller, Drama, Romance", 120),
                    new Movie(14, "Scarface", 9, 16, "Tony Montana, een Cubaanse immigrant, is vastbesloten om de cocaïnehandel in het Miami van de jaren '80 over te nemen. Maar in zijn gewelddadige weg naar de top werkt hij een hoop mensen in de afgrond, inclusief zichzelf.", "Gangster, Thriller, Mafia, Crime, Drama, Detective", 120),
                    new Movie(15, "The Matrix", 10, 7, "Computerhacker Thomas A. Anderson komt op een vreemde manier in contact met Morpheus. Hij leidt Thomas binnen in de `echte', maar ongekende wereld. De wereld die we kennen is volgens Morpheus een virtuele wereld, de Matrix genaamd.", "Action, Romance, Fantasy, Superhero, Animation", 90)
                };

                List<Movie> woensdagFilms = new List<Movie>
                {
                    new Movie(1, "Rocky", 1, 12, "Rocky Balboa is een hardwerkende bokser die de top probeert te bereiken. Hij werkt in een vleesfabriek in Philadelphia en verdient wat extra geld als inner van schulden.", "Action, Sport, Drama", 120),
                    new Movie(2, "Indiana Jones and The Lost Ark", 2, 16, "Wereldreiziger en archeoloog Indiana Jones wordt net voor de Tweede Wereldoorlog door de Amerikaanse overheid ingehuurd om een religieus relikwie op te sporen, voordat dit artefact in de handen van de Nazi's valt.", "Adventure, Action", 150),
                    new Movie(3, "Gone In 60 Seconds", 3, 18, "Randall Raines, een ex-autodief, leidt al een aantal jaren een rustig leven. Maar als zijn broer zich in de nesten werkt moet Randall zijn oude activiteiten weer oppakken om zijn broeder te redden.", "Action, Adventure, Thriller, Drama, Horror, Detective", 90),
                    new Movie(4, "Interstellar", 4, 12, "Terwijl de aarde niet meer in staat is om de mensheid in haar levensbehoefte te voorzien, gaat een groep ontdekkingsreizigers, ver buiten het melkwegstelsel, op zoek naar een toekomst voor de mens achter de sterren.", "drama, adventure, and speculative fiction", 90),
                    new Movie(5, "Cars 2", 5, 7, "Racecar Lightning McQueen en Mater besluiten mee te doen aan de World Grand Prix. Mater raakt onderweg echter betrokken bij spionage.", "action, comedy, and spy thriller", 120),
                    new Movie(6, "Rocky", 1, 12, "Rocky Balboa is een hardwerkende bokser die de top probeert te bereiken. Hij werkt in een vleesfabriek in Philadelphia en verdient wat extra geld als inner van schulden.", "Action, Sport, Drama", 120),
                    new Movie(7, "Indiana Jones and The Lost Ark", 2, 16, "Wereldreiziger en archeoloog Indiana Jones wordt net voor de Tweede Wereldoorlog door de Amerikaanse overheid ingehuurd om een religieus relikwie op te sporen, voordat dit artefact in de handen van de Nazi's valt.", "Adventure, Action", 150),
                    new Movie(8, "Gone In 60 Seconds", 3, 18, "Randall Raines, een ex-autodief, leidt al een aantal jaren een rustig leven. Maar als zijn broer zich in de nesten werkt moet Randall zijn oude activiteiten weer oppakken om zijn broeder te redden.", "Action, Adventure, Thriller, Drama, Horror, Detective", 90),
                    new Movie(9, "Interstellar", 4, 12, "Terwijl de aarde niet meer in staat is om de mensheid in haar levensbehoefte te voorzien, gaat een groep ontdekkingsreizigers, ver buiten het melkwegstelsel, op zoek naar een toekomst voor de mens achter de sterren.", "drama, adventure, and speculative fiction", 90),
                    new Movie(10, "Cars 2", 5, 7, "Racecar Lightning McQueen en Mater besluiten mee te doen aan de World Grand Prix. Mater raakt onderweg echter betrokken bij spionage.", "action, comedy, and spy thriller", 120),
                    new Movie(11, "Rocky", 1, 12, "Rocky Balboa is een hardwerkende bokser die de top probeert te bereiken. Hij werkt in een vleesfabriek in Philadelphia en verdient wat extra geld als inner van schulden.", "Action, Sport, Drama", 120),
                    new Movie(12, "Indiana Jones and The Lost Ark", 2, 16, "Wereldreiziger en archeoloog Indiana Jones wordt net voor de Tweede Wereldoorlog door de Amerikaanse overheid ingehuurd om een religieus relikwie op te sporen, voordat dit artefact in de handen van de Nazi's valt.", "Adventure, Action", 150),
                    new Movie(13, "Gone In 60 Seconds", 3, 18, "Randall Raines, een ex-autodief, leidt al een aantal jaren een rustig leven. Maar als zijn broer zich in de nesten werkt moet Randall zijn oude activiteiten weer oppakken om zijn broeder te redden.", "Action, Adventure, Thriller, Drama, Horror, Detective", 90),
                    new Movie(14, "Interstellar", 4, 12, "Terwijl de aarde niet meer in staat is om de mensheid in haar levensbehoefte te voorzien, gaat een groep ontdekkingsreizigers, ver buiten het melkwegstelsel, op zoek naar een toekomst voor de mens achter de sterren.", "drama, adventure, and speculative fiction", 90),
                    new Movie(15, "Cars 2", 5, 7, "Racecar Lightning McQueen en Mater besluiten mee te doen aan de World Grand Prix. Mater raakt onderweg echter betrokken bij spionage.", "action, comedy, and spy thriller", 120)
                };

                List<Movie> donderdagFilms = new List<Movie>
                {
                    new Movie(1, "Forest Gump", 6, 16, "Forrest Gump is een simpele jongen met een laag IQ. Dit weerhoudt hem er echter niet van om een grote rol te spelen bij belangrijke gebeurtenissen in de Amerikaanse geschiedenis. Zo vecht hij mee in Vietnam en ontmoet hij grootheden als Elvis en JFK.", "Actie, Thriller en Avontuur.", 90),
                    new Movie(2, "Bad Boys", 7, 16, "Undercover-agenten Mike Lowrey en Marcus Burnett hebben een grote partij drugs ter waarde van honderd miljoen dollar geconfisqueerd. Maar als de drugs uit het politiebureau in Miami worden gestolen, hebben Marcus en Mike 72 uur de tijd om de daders te vinden voordat de FBI zich ermee gaat bemoeien.", "Action, Comedy, Detective, Crime", 150),
                    new Movie(3, "Top Gun", 8, 12, "Waaghals Pete 'Maverick' Mitchell strijdt bij Top Gun, een keiharde opleiding voor toppiloten, om de beste vliegenier van zijn lichting te worden. Ondertussen valt zijn oog op de beeldschone vlieginstructeur Charlie Backwood.", "Action, Adventure, Thriller, Drama, Romance", 120),
                    new Movie(4, "Scarface", 9, 16, "Tony Montana, een Cubaanse immigrant, is vastbesloten om de cocaïnehandel in het Miami van de jaren '80 over te nemen. Maar in zijn gewelddadige weg naar de top werkt hij een hoop mensen in de afgrond, inclusief zichzelf.", "Gangster, Thriller, Mafia, Crime, Drama, Detective", 120),
                    new Movie(5, "The Matrix", 10, 7, "Computerhacker Thomas A. Anderson komt op een vreemde manier in contact met Morpheus. Hij leidt Thomas binnen in de `echte', maar ongekende wereld. De wereld die we kennen is volgens Morpheus een virtuele wereld, de Matrix genaamd.", "Action, Romance, Fantasy, Superhero, Animation", 90),
                    new Movie(6, "Forest Gump", 6, 16, "Forrest Gump is een simpele jongen met een laag IQ. Dit weerhoudt hem er echter niet van om een grote rol te spelen bij belangrijke gebeurtenissen in de Amerikaanse geschiedenis. Zo vecht hij mee in Vietnam en ontmoet hij grootheden als Elvis en JFK.", "Actie, Thriller en Avontuur.", 90),
                    new Movie(7, "Bad Boys", 7, 16, "Undercover-agenten Mike Lowrey en Marcus Burnett hebben een grote partij drugs ter waarde van honderd miljoen dollar geconfisqueerd. Maar als de drugs uit het politiebureau in Miami worden gestolen, hebben Marcus en Mike 72 uur de tijd om de daders te vinden voordat de FBI zich ermee gaat bemoeien.", "Action, Comedy, Detective, Crime", 150),
                    new Movie(8, "Top Gun", 8, 12, "Waaghals Pete 'Maverick' Mitchell strijdt bij Top Gun, een keiharde opleiding voor toppiloten, om de beste vliegenier van zijn lichting te worden. Ondertussen valt zijn oog op de beeldschone vlieginstructeur Charlie Backwood.", "Action, Adventure, Thriller, Drama, Romance", 120),
                    new Movie(9, "Scarface", 9, 16, "Tony Montana, een Cubaanse immigrant, is vastbesloten om de cocaïnehandel in het Miami van de jaren '80 over te nemen. Maar in zijn gewelddadige weg naar de top werkt hij een hoop mensen in de afgrond, inclusief zichzelf.", "Gangster, Thriller, Mafia, Crime, Drama, Detective", 120),
                    new Movie(10, "The Matrix", 10, 7, "Computerhacker Thomas A. Anderson komt op een vreemde manier in contact met Morpheus. Hij leidt Thomas binnen in de `echte', maar ongekende wereld. De wereld die we kennen is volgens Morpheus een virtuele wereld, de Matrix genaamd.", "Action, Romance, Fantasy, Superhero, Animation", 90),
                    new Movie(11, "Forest Gump", 6, 16, "Forrest Gump is een simpele jongen met een laag IQ. Dit weerhoudt hem er echter niet van om een grote rol te spelen bij belangrijke gebeurtenissen in de Amerikaanse geschiedenis. Zo vecht hij mee in Vietnam en ontmoet hij grootheden als Elvis en JFK.", "Actie, Thriller en Avontuur.", 90),
                    new Movie(12, "Bad Boys", 7, 16, "Undercover-agenten Mike Lowrey en Marcus Burnett hebben een grote partij drugs ter waarde van honderd miljoen dollar geconfisqueerd. Maar als de drugs uit het politiebureau in Miami worden gestolen, hebben Marcus en Mike 72 uur de tijd om de daders te vinden voordat de FBI zich ermee gaat bemoeien.", "Action, Comedy, Detective, Crime", 150),
                    new Movie(13, "Top Gun", 8, 12, "Waaghals Pete 'Maverick' Mitchell strijdt bij Top Gun, een keiharde opleiding voor toppiloten, om de beste vliegenier van zijn lichting te worden. Ondertussen valt zijn oog op de beeldschone vlieginstructeur Charlie Backwood.", "Action, Adventure, Thriller, Drama, Romance", 120),
                    new Movie(14, "Scarface", 9, 16, "Tony Montana, een Cubaanse immigrant, is vastbesloten om de cocaïnehandel in het Miami van de jaren '80 over te nemen. Maar in zijn gewelddadige weg naar de top werkt hij een hoop mensen in de afgrond, inclusief zichzelf.", "Gangster, Thriller, Mafia, Crime, Drama, Detective", 120),
                    new Movie(15, "The Matrix", 10, 7, "Computerhacker Thomas A. Anderson komt op een vreemde manier in contact met Morpheus. Hij leidt Thomas binnen in de `echte', maar ongekende wereld. De wereld die we kennen is volgens Morpheus een virtuele wereld, de Matrix genaamd.", "Action, Romance, Fantasy, Superhero, Animation", 90)
                };

                List<Movie> vrijdagFilms = new List<Movie>
                {
                    new Movie(1, "Rocky", 1, 12, "Rocky Balboa is een hardwerkende bokser die de top probeert te bereiken. Hij werkt in een vleesfabriek in Philadelphia en verdient wat extra geld als inner van schulden.", "Action, Sport, Drama", 120),
                    new Movie(2, "Indiana Jones and The Lost Ark", 2, 16, "Wereldreiziger en archeoloog Indiana Jones wordt net voor de Tweede Wereldoorlog door de Amerikaanse overheid ingehuurd om een religieus relikwie op te sporen, voordat dit artefact in de handen van de Nazi's valt.", "Adventure, Action", 150),
                    new Movie(3, "Gone In 60 Seconds", 3, 18, "Randall Raines, een ex-autodief, leidt al een aantal jaren een rustig leven. Maar als zijn broer zich in de nesten werkt moet Randall zijn oude activiteiten weer oppakken om zijn broeder te redden.", "Action, Adventure, Thriller, Drama, Horror, Detective", 90),
                    new Movie(4, "Interstellar", 4, 12, "Terwijl de aarde niet meer in staat is om de mensheid in haar levensbehoefte te voorzien, gaat een groep ontdekkingsreizigers, ver buiten het melkwegstelsel, op zoek naar een toekomst voor de mens achter de sterren.", "drama, adventure, and speculative fiction", 90),
                    new Movie(5, "Cars 2", 5, 7, "Racecar Lightning McQueen en Mater besluiten mee te doen aan de World Grand Prix. Mater raakt onderweg echter betrokken bij spionage.", "action, comedy, and spy thriller", 120),
                    new Movie(6, "Rocky", 1, 12, "Rocky Balboa is een hardwerkende bokser die de top probeert te bereiken. Hij werkt in een vleesfabriek in Philadelphia en verdient wat extra geld als inner van schulden.", "Action, Sport, Drama", 120),
                    new Movie(7, "Indiana Jones and The Lost Ark", 2, 16, "Wereldreiziger en archeoloog Indiana Jones wordt net voor de Tweede Wereldoorlog door de Amerikaanse overheid ingehuurd om een religieus relikwie op te sporen, voordat dit artefact in de handen van de Nazi's valt.", "Adventure, Action", 150),
                    new Movie(8, "Gone In 60 Seconds", 3, 18, "Randall Raines, een ex-autodief, leidt al een aantal jaren een rustig leven. Maar als zijn broer zich in de nesten werkt moet Randall zijn oude activiteiten weer oppakken om zijn broeder te redden.", "Action, Adventure, Thriller, Drama, Horror, Detective", 90),
                    new Movie(9, "Interstellar", 4, 12, "Terwijl de aarde niet meer in staat is om de mensheid in haar levensbehoefte te voorzien, gaat een groep ontdekkingsreizigers, ver buiten het melkwegstelsel, op zoek naar een toekomst voor de mens achter de sterren.", "drama, adventure, and speculative fiction", 90),
                    new Movie(10, "Cars 2", 5, 7, "Racecar Lightning McQueen en Mater besluiten mee te doen aan de World Grand Prix. Mater raakt onderweg echter betrokken bij spionage.", "action, comedy, and spy thriller", 120),
                    new Movie(11, "Rocky", 1, 12, "Rocky Balboa is een hardwerkende bokser die de top probeert te bereiken. Hij werkt in een vleesfabriek in Philadelphia en verdient wat extra geld als inner van schulden.", "Action, Sport, Drama", 120),
                    new Movie(12, "Indiana Jones and The Lost Ark", 2, 16, "Wereldreiziger en archeoloog Indiana Jones wordt net voor de Tweede Wereldoorlog door de Amerikaanse overheid ingehuurd om een religieus relikwie op te sporen, voordat dit artefact in de handen van de Nazi's valt.", "Adventure, Action", 150),
                    new Movie(13, "Gone In 60 Seconds", 3, 18, "Randall Raines, een ex-autodief, leidt al een aantal jaren een rustig leven. Maar als zijn broer zich in de nesten werkt moet Randall zijn oude activiteiten weer oppakken om zijn broeder te redden.", "Action, Adventure, Thriller, Drama, Horror, Detective", 90),
                    new Movie(14, "Interstellar", 4, 12, "Terwijl de aarde niet meer in staat is om de mensheid in haar levensbehoefte te voorzien, gaat een groep ontdekkingsreizigers, ver buiten het melkwegstelsel, op zoek naar een toekomst voor de mens achter de sterren.", "drama, adventure, and speculative fiction", 90),
                    new Movie(15, "Cars 2", 5, 7, "Racecar Lightning McQueen en Mater besluiten mee te doen aan de World Grand Prix. Mater raakt onderweg echter betrokken bij spionage.", "action, comedy, and spy thriller", 120)
                };

                List<Movie> zaterdagFilms = new List<Movie>
                {
                    new Movie(1, "Forest Gump", 6, 16, "Forrest Gump is een simpele jongen met een laag IQ. Dit weerhoudt hem er echter niet van om een grote rol te spelen bij belangrijke gebeurtenissen in de Amerikaanse geschiedenis. Zo vecht hij mee in Vietnam en ontmoet hij grootheden als Elvis en JFK.", "Actie, Thriller en Avontuur.", 90),
                    new Movie(2, "Bad Boys", 7, 16, "Undercover-agenten Mike Lowrey en Marcus Burnett hebben een grote partij drugs ter waarde van honderd miljoen dollar geconfisqueerd. Maar als de drugs uit het politiebureau in Miami worden gestolen, hebben Marcus en Mike 72 uur de tijd om de daders te vinden voordat de FBI zich ermee gaat bemoeien.", "Action, Comedy, Detective, Crime", 150),
                    new Movie(3, "Top Gun", 8, 12, "Waaghals Pete 'Maverick' Mitchell strijdt bij Top Gun, een keiharde opleiding voor toppiloten, om de beste vliegenier van zijn lichting te worden. Ondertussen valt zijn oog op de beeldschone vlieginstructeur Charlie Backwood.", "Action, Adventure, Thriller, Drama, Romance", 120),
                    new Movie(4, "Scarface", 9, 16, "Tony Montana, een Cubaanse immigrant, is vastbesloten om de cocaïnehandel in het Miami van de jaren '80 over te nemen. Maar in zijn gewelddadige weg naar de top werkt hij een hoop mensen in de afgrond, inclusief zichzelf.", "Gangster, Thriller, Mafia, Crime, Drama, Detective", 120),
                    new Movie(5, "The Matrix", 10, 7, "Computerhacker Thomas A. Anderson komt op een vreemde manier in contact met Morpheus. Hij leidt Thomas binnen in de `echte', maar ongekende wereld. De wereld die we kennen is volgens Morpheus een virtuele wereld, de Matrix genaamd.", "Action, Romance, Fantasy, Superhero, Animation", 90),
                    new Movie(6, "Forest Gump", 6, 16, "Forrest Gump is een simpele jongen met een laag IQ. Dit weerhoudt hem er echter niet van om een grote rol te spelen bij belangrijke gebeurtenissen in de Amerikaanse geschiedenis. Zo vecht hij mee in Vietnam en ontmoet hij grootheden als Elvis en JFK.", "Actie, Thriller en Avontuur.", 90),
                    new Movie(7, "Bad Boys", 7, 16, "Undercover-agenten Mike Lowrey en Marcus Burnett hebben een grote partij drugs ter waarde van honderd miljoen dollar geconfisqueerd. Maar als de drugs uit het politiebureau in Miami worden gestolen, hebben Marcus en Mike 72 uur de tijd om de daders te vinden voordat de FBI zich ermee gaat bemoeien.", "Action, Comedy, Detective, Crime", 150),
                    new Movie(8, "Top Gun", 8, 12, "Waaghals Pete 'Maverick' Mitchell strijdt bij Top Gun, een keiharde opleiding voor toppiloten, om de beste vliegenier van zijn lichting te worden. Ondertussen valt zijn oog op de beeldschone vlieginstructeur Charlie Backwood.", "Action, Adventure, Thriller, Drama, Romance", 120),
                    new Movie(9, "Scarface", 9, 16, "Tony Montana, een Cubaanse immigrant, is vastbesloten om de cocaïnehandel in het Miami van de jaren '80 over te nemen. Maar in zijn gewelddadige weg naar de top werkt hij een hoop mensen in de afgrond, inclusief zichzelf.", "Gangster, Thriller, Mafia, Crime, Drama, Detective", 120),
                    new Movie(10, "The Matrix", 10, 7, "Computerhacker Thomas A. Anderson komt op een vreemde manier in contact met Morpheus. Hij leidt Thomas binnen in de `echte', maar ongekende wereld. De wereld die we kennen is volgens Morpheus een virtuele wereld, de Matrix genaamd.", "Action, Romance, Fantasy, Superhero, Animation", 90),
                    new Movie(11, "Forest Gump", 6, 16, "Forrest Gump is een simpele jongen met een laag IQ. Dit weerhoudt hem er echter niet van om een grote rol te spelen bij belangrijke gebeurtenissen in de Amerikaanse geschiedenis. Zo vecht hij mee in Vietnam en ontmoet hij grootheden als Elvis en JFK.", "Actie, Thriller en Avontuur.", 90),
                    new Movie(12, "Bad Boys", 7, 16, "Undercover-agenten Mike Lowrey en Marcus Burnett hebben een grote partij drugs ter waarde van honderd miljoen dollar geconfisqueerd. Maar als de drugs uit het politiebureau in Miami worden gestolen, hebben Marcus en Mike 72 uur de tijd om de daders te vinden voordat de FBI zich ermee gaat bemoeien.", "Action, Comedy, Detective, Crime", 150),
                    new Movie(13, "Top Gun", 8, 12, "Waaghals Pete 'Maverick' Mitchell strijdt bij Top Gun, een keiharde opleiding voor toppiloten, om de beste vliegenier van zijn lichting te worden. Ondertussen valt zijn oog op de beeldschone vlieginstructeur Charlie Backwood.", "Action, Adventure, Thriller, Drama, Romance", 120),
                    new Movie(14, "Scarface", 9, 16, "Tony Montana, een Cubaanse immigrant, is vastbesloten om de cocaïnehandel in het Miami van de jaren '80 over te nemen. Maar in zijn gewelddadige weg naar de top werkt hij een hoop mensen in de afgrond, inclusief zichzelf.", "Gangster, Thriller, Mafia, Crime, Drama, Detective", 120),
                    new Movie(15, "The Matrix", 10, 7, "Computerhacker Thomas A. Anderson komt op een vreemde manier in contact met Morpheus. Hij leidt Thomas binnen in de `echte', maar ongekende wereld. De wereld die we kennen is volgens Morpheus een virtuele wereld, de Matrix genaamd.", "Action, Romance, Fantasy, Superhero, Animation", 90)
                };

                List<Movie> zondagFilms = new List<Movie>
                {
                    new Movie(1, "Rocky", 1, 12, "Rocky Balboa is een hardwerkende bokser die de top probeert te bereiken. Hij werkt in een vleesfabriek in Philadelphia en verdient wat extra geld als inner van schulden.", "Action, Sport, Drama", 120),
                    new Movie(2, "Indiana Jones and The Lost Ark", 2, 16, "Wereldreiziger en archeoloog Indiana Jones wordt net voor de Tweede Wereldoorlog door de Amerikaanse overheid ingehuurd om een religieus relikwie op te sporen, voordat dit artefact in de handen van de Nazi's valt.", "Adventure, Action", 150),
                    new Movie(3, "Gone In 60 Seconds", 3, 18, "Randall Raines, een ex-autodief, leidt al een aantal jaren een rustig leven. Maar als zijn broer zich in de nesten werkt moet Randall zijn oude activiteiten weer oppakken om zijn broeder te redden.", "Action, Adventure, Thriller, Drama, Horror, Detective", 90),
                    new Movie(4, "Interstellar", 4, 12, "Terwijl de aarde niet meer in staat is om de mensheid in haar levensbehoefte te voorzien, gaat een groep ontdekkingsreizigers, ver buiten het melkwegstelsel, op zoek naar een toekomst voor de mens achter de sterren.", "drama, adventure, and speculative fiction", 90),
                    new Movie(5, "Cars 2", 5, 7, "Racecar Lightning McQueen en Mater besluiten mee te doen aan de World Grand Prix. Mater raakt onderweg echter betrokken bij spionage.", "action, comedy, and spy thriller", 120),
                    new Movie(6, "Rocky", 1, 12, "Rocky Balboa is een hardwerkende bokser die de top probeert te bereiken. Hij werkt in een vleesfabriek in Philadelphia en verdient wat extra geld als inner van schulden.", "Action, Sport, Drama", 120),
                    new Movie(7, "Indiana Jones and The Lost Ark", 2, 16, "Wereldreiziger en archeoloog Indiana Jones wordt net voor de Tweede Wereldoorlog door de Amerikaanse overheid ingehuurd om een religieus relikwie op te sporen, voordat dit artefact in de handen van de Nazi's valt.", "Adventure, Action", 150),
                    new Movie(8, "Gone In 60 Seconds", 3, 18, "Randall Raines, een ex-autodief, leidt al een aantal jaren een rustig leven. Maar als zijn broer zich in de nesten werkt moet Randall zijn oude activiteiten weer oppakken om zijn broeder te redden.", "Action, Adventure, Thriller, Drama, Horror, Detective", 90),
                    new Movie(9, "Interstellar", 4, 12, "Terwijl de aarde niet meer in staat is om de mensheid in haar levensbehoefte te voorzien, gaat een groep ontdekkingsreizigers, ver buiten het melkwegstelsel, op zoek naar een toekomst voor de mens achter de sterren.", "drama, adventure, and speculative fiction", 90),
                    new Movie(10, "Cars 2", 5, 7, "Racecar Lightning McQueen en Mater besluiten mee te doen aan de World Grand Prix. Mater raakt onderweg echter betrokken bij spionage.", "action, comedy, and spy thriller", 120),
                    new Movie(11, "Rocky", 1, 12, "Rocky Balboa is een hardwerkende bokser die de top probeert te bereiken. Hij werkt in een vleesfabriek in Philadelphia en verdient wat extra geld als inner van schulden.", "Action, Sport, Drama", 120),
                    new Movie(12, "Indiana Jones and The Lost Ark", 2, 16, "Wereldreiziger en archeoloog Indiana Jones wordt net voor de Tweede Wereldoorlog door de Amerikaanse overheid ingehuurd om een religieus relikwie op te sporen, voordat dit artefact in de handen van de Nazi's valt.", "Adventure, Action", 150),
                    new Movie(13, "Gone In 60 Seconds", 3, 18, "Randall Raines, een ex-autodief, leidt al een aantal jaren een rustig leven. Maar als zijn broer zich in de nesten werkt moet Randall zijn oude activiteiten weer oppakken om zijn broeder te redden.", "Action, Adventure, Thriller, Drama, Horror, Detective", 90),
                    new Movie(14, "Interstellar", 4, 12, "Terwijl de aarde niet meer in staat is om de mensheid in haar levensbehoefte te voorzien, gaat een groep ontdekkingsreizigers, ver buiten het melkwegstelsel, op zoek naar een toekomst voor de mens achter de sterren.", "drama, adventure, and speculative fiction", 90),
                    new Movie(15, "Cars 2", 5, 7, "Racecar Lightning McQueen en Mater besluiten mee te doen aan de World Grand Prix. Mater raakt onderweg echter betrokken bij spionage.", "action, comedy, and spy thriller", 120)
                };

                List<TimeTable> maandagtimeTableList = new List<TimeTable>
                {
                    new TimeTable(1, 1, 1, "2024-05-07 10:00:00", "2024-05-07 12:00:00"),
                    new TimeTable(1, 2, 1, "2024-05-07 12:30:00", "2024-05-07 15:00:00"),
                    new TimeTable(1, 3, 1, "2024-05-07 15:30:00", "2024-05-07 17:00:00"),
                    new TimeTable(1, 4, 1, "2024-05-07 17:30:00", "2024-05-07 19:00:00"),
                    new TimeTable(1, 5, 1, "2024-05-07 19:30:00", "2024-05-07 21:30:00"),
                    new TimeTable(1, 6, 2, "2024-05-07 10:00:00", "2024-05-07 12:00:00"),
                    new TimeTable(1, 7, 2, "2024-05-07 12:30:00", "2024-05-07 15:00:00"),
                    new TimeTable(1, 8, 2, "2024-05-07 15:30:00", "2024-05-07 17:00:00"),
                    new TimeTable(1, 9, 2, "2024-05-07 17:30:00", "2024-05-07 19:00:00"),
                    new TimeTable(1, 10, 2, "2024-05-07 19:30:00", "2024-05-07 21:30:00"),
                    new TimeTable(1, 11, 3, "2024-05-07 10:00:00", "2024-05-07 12:00:00"),
                    new TimeTable(1, 12, 3, "2024-05-07 12:30:00", "2024-05-07 15:00:00"),
                    new TimeTable(1, 13, 3, "2024-05-07 15:30:00", "2024-05-07 17:00:00"),
                    new TimeTable(1, 14, 3, "2024-05-07 17:30:00", "2024-05-07 19:00:00"),
                    new TimeTable(1, 15, 3, "2024-05-07 19:30:00", "2024-05-07 21:30:00")
                };

                List<TimeTable> dinsdagtimeTableList = new List<TimeTable>
                {
                    new TimeTable(1, 1, 1, "2024-05-08 10:00:00", "2024-05-08 11:30:00"),
                    new TimeTable(1, 2, 1, "2024-05-08 12:00:00", "2024-05-08 14:30:00"),
                    new TimeTable(1, 3, 1, "2024-05-08 15:00:00", "2024-05-08 17:00:00"),
                    new TimeTable(1, 4, 1, "2024-05-08 17:30:00", "2024-05-08 19:30:00"),
                    new TimeTable(1, 5, 1, "2024-05-08 20:00:00", "2024-05-08 21:30:00"),
                    new TimeTable(1, 6, 2, "2024-05-08 10:00:00", "2024-05-08 11:30:00"),
                    new TimeTable(1, 7, 2, "2024-05-08 12:00:00", "2024-05-08 14:30:00"),
                    new TimeTable(1, 8, 2, "2024-05-08 15:00:00", "2024-05-08 17:00:00"),
                    new TimeTable(1, 9, 2, "2024-05-08 17:30:00", "2024-05-08 19:30:00"),
                    new TimeTable(1, 10, 2, "2024-05-08 20:00:00", "2024-05-08 21:30:00"),
                    new TimeTable(1, 11, 3, "2024-05-08 10:00:00", "2024-05-08 11:30:00"),
                    new TimeTable(1, 12, 3, "2024-05-08 12:00:00", "2024-05-08 14:30:00"),
                    new TimeTable(1, 13, 3, "2024-05-08 15:00:00", "2024-05-08 17:00:00"),
                    new TimeTable(1, 14, 3, "2024-05-08 17:30:00", "2024-05-08 19:30:00"),
                    new TimeTable(1, 15, 3, "2024-05-08 20:00:00", "2024-05-08 21:30:00")
                };

                List<TimeTable> woensdagtimeTableList = new List<TimeTable>
                {
                    new TimeTable(1, 1, 1, "2024-05-09 10:00:00", "2024-05-09 12:00:00"),
                    new TimeTable(1, 2, 1, "2024-05-09 12:30:00", "2024-05-09 15:00:00"),
                    new TimeTable(1, 3, 1, "2024-05-09 15:30:00", "2024-05-09 17:00:00"),
                    new TimeTable(1, 4, 1, "2024-05-09 17:30:00", "2024-05-09 19:00:00"),
                    new TimeTable(1, 5, 1, "2024-05-09 19:30:00", "2024-05-09 21:30:00"),
                    new TimeTable(1, 6, 2, "2024-05-09 10:00:00", "2024-05-09 12:00:00"),
                    new TimeTable(1, 7, 2, "2024-05-09 12:30:00", "2024-05-09 15:00:00"),
                    new TimeTable(1, 8, 2, "2024-05-09 15:30:00", "2024-05-09 17:00:00"),
                    new TimeTable(1, 9, 2, "2024-05-09 17:30:00", "2024-05-09 19:00:00"),
                    new TimeTable(1, 10, 2, "2024-05-09 19:30:00", "2024-05-09 21:30:00"),
                    new TimeTable(1, 11, 3, "2024-05-09 10:00:00", "2024-05-09 12:00:00"),
                    new TimeTable(1, 12, 3, "2024-05-09 12:30:00", "2024-05-09 15:00:00"),
                    new TimeTable(1, 13, 3, "2024-05-09 15:30:00", "2024-05-09 17:00:00"),
                    new TimeTable(1, 14, 3, "2024-05-09 17:30:00", "2024-05-09 19:00:00"),
                    new TimeTable(1, 15, 3, "2024-05-09 19:30:00", "2024-05-09 21:30:00")
                };

                List<TimeTable> donderdagtimeTableList = new List<TimeTable>
                {
                    new TimeTable(1, 1, 1, "2024-05-10 10:00:00", "2024-05-10 11:30:00"),
                    new TimeTable(1, 2, 1, "2024-05-10 12:00:00", "2024-05-10 14:30:00"),
                    new TimeTable(1, 3, 1, "2024-05-10 15:00:00", "2024-05-10 17:00:00"),
                    new TimeTable(1, 4, 1, "2024-05-10 17:30:00", "2024-05-10 19:30:00"),
                    new TimeTable(1, 5, 1, "2024-05-10 20:00:00", "2024-05-10 21:30:00"),
                    new TimeTable(1, 6, 2, "2024-05-10 10:00:00", "2024-05-10 11:30:00"),
                    new TimeTable(1, 7, 2, "2024-05-10 12:00:00", "2024-05-10 14:30:00"),
                    new TimeTable(1, 8, 2, "2024-05-10 15:00:00", "2024-05-10 17:00:00"),
                    new TimeTable(1, 9, 2, "2024-05-10 17:30:00", "2024-05-10 19:30:00"),
                    new TimeTable(1, 10, 2, "2024-05-10 20:00:00", "2024-05-10 21:30:00"),
                    new TimeTable(1, 11, 3, "2024-05-10 10:00:00", "2024-05-10 11:30:00"),
                    new TimeTable(1, 12, 3, "2024-05-10 12:00:00", "2024-05-10 14:30:00"),
                    new TimeTable(1, 13, 3, "2024-05-10 15:00:00", "2024-05-10 17:00:00"),
                    new TimeTable(1, 14, 3, "2024-05-10 17:30:00", "2024-05-10 19:30:00"),
                    new TimeTable(1, 15, 3, "2024-05-10 20:00:00", "2024-05-10 21:30:00")
                };

                List<TimeTable> vrijdagtimeTableList = new List<TimeTable>
                {
                    new TimeTable(1, 1, 1, "2024-05-11 10:00:00", "2024-05-11 12:00:00"),
                    new TimeTable(1, 2, 1, "2024-05-11 12:30:00", "2024-05-11 15:00:00"),
                    new TimeTable(1, 3, 1, "2024-05-11 15:30:00", "2024-05-11 17:00:00"),
                    new TimeTable(1, 4, 1, "2024-05-11 17:30:00", "2024-05-11 19:00:00"),
                    new TimeTable(1, 5, 1, "2024-05-11 19:30:00", "2024-05-11 21:30:00"),
                    new TimeTable(1, 6, 2, "2024-05-11 10:00:00", "2024-05-11 12:00:00"),
                    new TimeTable(1, 7, 2, "2024-05-11 12:30:00", "2024-05-11 15:00:00"),
                    new TimeTable(1, 8, 2, "2024-05-11 15:30:00", "2024-05-11 17:00:00"),
                    new TimeTable(1, 9, 2, "2024-05-11 17:30:00", "2024-05-11 19:00:00"),
                    new TimeTable(1, 10, 2, "2024-05-11 19:30:00", "2024-05-11 21:30:00"),
                    new TimeTable(1, 11, 3, "2024-05-11 10:00:00", "2024-05-11 12:00:00"),
                    new TimeTable(1, 12, 3, "2024-05-11 12:30:00", "2024-05-11 15:00:00"),
                    new TimeTable(1, 13, 3, "2024-05-11 15:30:00", "2024-05-11 17:00:00"),
                    new TimeTable(1, 14, 3, "2024-05-11 17:30:00", "2024-05-11 19:00:00"),
                    new TimeTable(1, 15, 3, "2024-05-11 19:30:00", "2024-05-11 21:30:00")
                };

                List<TimeTable> zaterdagtimeTableList = new List<TimeTable>
                {
                    new TimeTable(1, 1, 1, "2024-05-12 10:00:00", "2024-05-12 11:30:00"),
                    new TimeTable(1, 2, 1, "2024-05-12 12:00:00", "2024-05-12 14:30:00"),
                    new TimeTable(1, 3, 1, "2024-05-12 15:00:00", "2024-05-12 17:00:00"),
                    new TimeTable(1, 4, 1, "2024-05-12 17:30:00", "2024-05-12 19:30:00"),
                    new TimeTable(1, 5, 1, "2024-05-12 20:00:00", "2024-05-12 21:30:00"),
                    new TimeTable(1, 6, 2, "2024-05-12 10:00:00", "2024-05-12 11:30:00"),
                    new TimeTable(1, 7, 2, "2024-05-12 12:00:00", "2024-05-12 14:30:00"),
                    new TimeTable(1, 8, 2, "2024-05-12 15:00:00", "2024-05-12 17:00:00"),
                    new TimeTable(1, 9, 2, "2024-05-12 17:30:00", "2024-05-12 19:30:00"),
                    new TimeTable(1, 10, 2, "2024-05-12 20:00:00", "2024-05-12 21:30:00"),
                    new TimeTable(1, 11, 3, "2024-05-12 10:00:00", "2024-05-12 11:30:00"),
                    new TimeTable(1, 12, 3, "2024-05-12 12:00:00", "2024-05-12 14:30:00"),
                    new TimeTable(1, 13, 3, "2024-05-12 15:00:00", "2024-05-12 17:00:00"),
                    new TimeTable(1, 14, 3, "2024-05-12 17:30:00", "2024-05-12 19:30:00"),
                    new TimeTable(1, 15, 3, "2024-05-12 20:00:00", "2024-05-12 21:30:00")
                };

                List<TimeTable> zondagtimeTableList = new List<TimeTable>
                {
                    new TimeTable(1, 1, 1, "2024-05-13 10:00:00", "2024-05-13 12:00:00"),
                    new TimeTable(1, 2, 1, "2024-05-13 12:30:00", "2024-05-13 15:00:00"),
                    new TimeTable(1, 3, 1, "2024-05-13 15:30:00", "2024-05-13 17:00:00"),
                    new TimeTable(1, 4, 1, "2024-05-13 17:30:00", "2024-05-13 19:00:00"),
                    new TimeTable(1, 5, 1, "2024-05-13 19:30:00", "2024-05-13 21:30:00"),
                    new TimeTable(1, 6, 2, "2024-05-13 10:00:00", "2024-05-13 12:00:00"),
                    new TimeTable(1, 7, 2, "2024-05-13 12:30:00", "2024-05-13 15:00:00"),
                    new TimeTable(1, 8, 2, "2024-05-13 15:30:00", "2024-05-13 17:00:00"),
                    new TimeTable(1, 9, 2, "2024-05-13 17:30:00", "2024-05-13 19:00:00"),
                    new TimeTable(1, 10, 2, "2024-05-13 19:30:00", "2024-05-13 21:30:00"),
                    new TimeTable(1, 11, 3, "2024-05-13 10:00:00", "2024-05-13 12:00:00"),
                    new TimeTable(1, 12, 3, "2024-05-13 12:30:00", "2024-05-13 15:00:00"),
                    new TimeTable(1, 13, 3, "2024-05-13 15:30:00", "2024-05-13 17:00:00"),
                    new TimeTable(1, 14, 3, "2024-05-13 17:30:00", "2024-05-13 19:00:00"),
                    new TimeTable(1, 15, 3, "2024-05-13 19:30:00", "2024-05-13 21:30:00")
                };

                InputMenu selectDay = new InputMenu("| Selecteer een dag |");
                selectDay.Add($"Maandag", (x) =>
                {
                    Console.Clear();
                    InputMenu movieSelecter = new InputMenu("| Selecteer een film |");
                    foreach (TimeTable timeTable in maandagtimeTableList)
                    {
                        Console.Clear();
                        IEnumerable<Movie> query = maandagFilms.Where(movie => movie.ID == timeTable.MovieID);
                        foreach (Movie movie in query)
                        {
                            movieSelecter.Add($"Film: {movie.Title}", (x) =>
                            {
                                Console.Clear();
                                IEnumerable<Room> query2 = roomList.Where(room => room.ID == timeTable.RoomID);
                                Console.WriteLine($"Film: {movie.Title}\nZaal: {roomList.FirstOrDefault(room => room.ID == timeTable.RoomID)?.ID}\nGenre: {movie.Genre}\nBeschrijving: {movie.Discription}\nPEGI: {movie.pegiAge}\nTijdsduur: {movie.DurationInMin}\nBegintijd: {timeTable.StartDate}\nEindtijd: {timeTable.EndDate}");
                                Console.ReadLine();
                            });
                        }
                    }
                    movieSelecter.UseMenu();
                    Console.ReadLine();
                });
                selectDay.Add($"Dinsdag", (x) =>
                {
                    Console.Clear();
                    InputMenu movieSelecter = new InputMenu("| Selecteer een film |");
                    foreach (TimeTable timeTable in dinsdagtimeTableList)
                    {
                        Console.Clear();
                        IEnumerable<Movie> query = dinsdagFilms.Where(movie => movie.ID == timeTable.MovieID);
                        foreach (Movie movie in query)
                        {
                            movieSelecter.Add($"Film: {movie.Title}", (x) =>
                            {
                                Console.Clear();
                                IEnumerable<Room> query2 = roomList.Where(room => room.ID == timeTable.RoomID);
                                Console.WriteLine($"Film: {movie.Title}\nZaal: {roomList.FirstOrDefault(room => room.ID == timeTable.RoomID)?.ID}\nGenre: {movie.Genre}\nBeschrijving: {movie.Discription}\nPEGI: {movie.pegiAge}\nTijdsduur: {movie.DurationInMin}\nBegintijd: {timeTable.StartDate}\nEindtijd: {timeTable.EndDate}");
                                Console.ReadLine();
                            });
                        }
                    }
                    movieSelecter.UseMenu();
                    Console.ReadLine();
                });
                selectDay.Add($"Woensdag", (x) =>
                {
                    Console.Clear();
                    InputMenu movieSelecter = new InputMenu("| Selecteer een film |");
                    foreach (TimeTable timeTable in woensdagtimeTableList)
                    {
                        Console.Clear();
                        IEnumerable<Movie> query = woensdagFilms.Where(movie => movie.ID == timeTable.MovieID);
                        foreach (Movie movie in query)
                        {
                            movieSelecter.Add($"Film: {movie.Title}", (x) =>
                            {
                                Console.Clear();
                                IEnumerable<Room> query2 = roomList.Where(room => room.ID == timeTable.RoomID);
                                Console.WriteLine($"Film: {movie.Title}\nZaal: {roomList.FirstOrDefault(room => room.ID == timeTable.RoomID)?.ID}\nGenre: {movie.Genre}\nBeschrijving: {movie.Discription}\nPEGI: {movie.pegiAge}\nTijdsduur: {movie.DurationInMin}\nBegintijd: {timeTable.StartDate}\nEindtijd: {timeTable.EndDate}");
                                Console.ReadLine();
                            });
                        }
                    }
                    movieSelecter.UseMenu();
                    Console.ReadLine();
                });
                selectDay.Add($"Donderdag", (x) =>
                {
                    Console.Clear();
                    InputMenu movieSelecter = new InputMenu("| Selecteer een film |");
                    foreach (TimeTable timeTable in donderdagtimeTableList)
                    {
                        Console.Clear();
                        IEnumerable<Movie> query = donderdagFilms.Where(movie => movie.ID == timeTable.MovieID);
                        foreach (Movie movie in query)
                        {
                            movieSelecter.Add($"Film: {movie.Title}", (x) =>
                            {
                                Console.Clear();
                                IEnumerable<Room> query2 = roomList.Where(room => room.ID == timeTable.RoomID);
                                Console.WriteLine($"Film: {movie.Title}\nZaal: {roomList.FirstOrDefault(room => room.ID == timeTable.RoomID)?.ID}\nGenre: {movie.Genre}\nBeschrijving: {movie.Discription}\nPEGI: {movie.pegiAge}\nTijdsduur: {movie.DurationInMin}\nBegintijd: {timeTable.StartDate}\nEindtijd: {timeTable.EndDate}");
                                Console.ReadLine();
                            });
                        }
                    }
                    movieSelecter.UseMenu();
                    Console.ReadLine();
                });
                selectDay.Add($"Vrijdag", (x) =>
                {
                    Console.Clear();
                    InputMenu movieSelecter = new InputMenu("| Selecteer een film |");
                    foreach (TimeTable timeTable in vrijdagtimeTableList)
                    {
                        Console.Clear();
                        IEnumerable<Movie> query = vrijdagFilms.Where(movie => movie.ID == timeTable.MovieID);
                        foreach (Movie movie in query)
                        {
                            movieSelecter.Add($"Film: {movie.Title}", (x) =>
                            {
                                Console.Clear();
                                IEnumerable<Room> query2 = roomList.Where(room => room.ID == timeTable.RoomID);
                                Console.WriteLine($"Film: {movie.Title}\nZaal: {roomList.FirstOrDefault(room => room.ID == timeTable.RoomID)?.ID}\nGenre: {movie.Genre}\nBeschrijving: {movie.Discription}\nPEGI: {movie.pegiAge}\nTijdsduur: {movie.DurationInMin}\nBegintijd: {timeTable.StartDate}\nEindtijd: {timeTable.EndDate}");
                                Console.ReadLine();
                            });
                        }
                    }
                    movieSelecter.UseMenu();
                    Console.ReadLine();
                });
                selectDay.Add($"Zaterdag", (x) =>
                {
                    Console.Clear();
                    InputMenu movieSelecter = new InputMenu("| Selecteer een film |");
                    foreach (TimeTable timeTable in zaterdagtimeTableList)
                    {
                        Console.Clear();
                        IEnumerable<Movie> query = zaterdagFilms.Where(movie => movie.ID == timeTable.MovieID);
                        foreach (Movie movie in query)
                        {
                            movieSelecter.Add($"Film: {movie.Title}", (x) =>
                            {
                                Console.Clear();
                                IEnumerable<Room> query2 = roomList.Where(room => room.ID == timeTable.RoomID);
                                Console.WriteLine($"Film: {movie.Title}\nZaal: {roomList.FirstOrDefault(room => room.ID == timeTable.RoomID)?.ID}\nGenre: {movie.Genre}\nBeschrijving: {movie.Discription}\nPEGI: {movie.pegiAge}\nTijdsduur: {movie.DurationInMin}\nBegintijd: {timeTable.StartDate}\nEindtijd: {timeTable.EndDate}");
                                Console.ReadLine();
                            });
                        }
                    }
                    movieSelecter.UseMenu();
                    Console.ReadLine();
                });
                selectDay.Add($"Zondag", (x) =>
                {
                    Console.Clear();
                    InputMenu movieSelecter = new InputMenu("| Selecteer een film |");
                    foreach (TimeTable timeTable in zondagtimeTableList)
                    {
                        Console.Clear();
                        IEnumerable<Movie> query = zondagFilms.Where(movie => movie.ID == timeTable.MovieID);
                        foreach (Movie movie in query)
                        {
                            movieSelecter.Add($"Film: {movie.Title}", (x) =>
                            {
                                Console.Clear();
                                IEnumerable<Room> query2 = roomList.Where(room => room.ID == timeTable.RoomID);
                                Console.WriteLine($"Film: {movie.Title}\nZaal: {roomList.FirstOrDefault(room => room.ID == timeTable.RoomID)?.ID}\nGenre: {movie.Genre}\nBeschrijving: {movie.Discription}\nPEGI: {movie.pegiAge}\nTijdsduur: {movie.DurationInMin}\nBegintijd: {timeTable.StartDate}\nEindtijd: {timeTable.EndDate}");
                                Console.ReadLine();
                            });
                        }
                    }
                    movieSelecter.UseMenu();
                    Console.ReadLine();
                });
                selectDay.UseMenu();
                Console.ReadLine();
            });
            menu.UseMenu();
        }
    }
}
