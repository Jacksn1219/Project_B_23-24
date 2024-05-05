﻿using System.Globalization;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using DataAccessLibrary;
using DataAccessLibrary.logic;
using DataAccessLibrary.models;
using Models;
using Project_B.services;

namespace Project_B
{
    class Program
    {
        private const string DbPath = "database.db";
        public static void Main()
        {
            //start of app
            //create database connection
            DataAccess db = new SQliteDataAccess($"Data Source={DbPath}; Version = 3; New = True; Compress = True;");
            //create factories to add DbItems to the db
            DirectorFactory df = new(db);
            ActorFactory af = new(db);
            MovieFactory mf = new(db, df, af);
            SeatFactory sf = new(db);
            RoomFactory roomf = new(db, sf);
            TimeTableFactory tf = new(db, mf, roomf);
            CustomerFactory cf = new(db);
            ReservationFactory resf = new(db, cf, sf);


            InputMenu menu = new InputMenu("| Main menu |", true);
            /*menu.Add("Setup Database", (x) =>
            {
                //Opzet Sqlite database
                //SQLite.SetupProjectB();
                Console.ReadLine();
            });
            menu.Add("Test Author", (x) =>
            {
                ActorModel testAuthor = new ActorModel("John", "Not succesfull", 25);
                Console.WriteLine($"{testAuthor.Name} - {testAuthor.Age} :\n{testAuthor.Description}");
                Console.ReadLine();
            });*/
            menu.Add("Layout creator", (x) =>
            {
                Layout l = new Layout(roomf, sf);
                l.MakeNewLayout();
                /*
                Als klant wil ik de stoelen in een zaal zien omdat ik wil weten waar ik kan zitten. -Chris
                Als klant wil ik zien wat voor type stoel een bepaalde stoel is, zodat ik mijn favoriete type kan kiezen.(Love-seat, regular, deluxe) -Chris
                */
            });
            menu.Add("Edit layout item", (x) =>
            {
                List<SeatModel> layout1 = new List<SeatModel>{
                    new SeatModel("0", " ", " "),
                    new SeatModel("1", " ", " "),
                    new SeatModel("2", "1", "Normaal"),
                    new SeatModel("3", "1", "Normaal"),
                    new SeatModel("4", "1", "Normaal"),
                    new SeatModel("5", "1", "Normaal"),
                    new SeatModel("6", "1", "Normaal"),
                    new SeatModel( "7", "1", "Normaal"),
                    new SeatModel("8", "1", "Normaal"),
                    new SeatModel( "9", "1", "Normaal"),
                    new SeatModel("10", " ", " "),
                    new SeatModel("11", " ", " "),
                    new SeatModel("12", " ", " "),
                    new SeatModel("13", " ", " ")
                };
                List<RoomModel> roomList = new List<RoomModel> { new RoomModel("Room1", layout1.Count, 6) };
                InputMenu selectRoom = new InputMenu("| Select room to edit |");
                foreach (RoomModel room in roomList/*getRoomFromDatabase() - Aymane*/)
                {
                    selectRoom.Add($"{room.Name}", (x) => DataAccessLibrary.Layout.editLayout(layout1/*getLayoutFromDatabase() - Aymane*/, room));
                }
                selectRoom.UseMenu();
            });
            menu.Add("Tijdschema Ma 06 Mei tot Zo 13 Mei", (x) =>
            {
                List<RoomModel> roomList = new List<RoomModel>
                {
                    new RoomModel("Room 1", 150, 1),
                    new RoomModel("Room 2", 300, 1),
                    new RoomModel("Room 3", 500, 1)

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

                InputMenu selectDay = new InputMenu("| Selecteer een dag |");
                selectDay.Add($"Maandag", (x) =>
                {
                    Console.Clear();
                    InputMenu MovieModelSelecter = new InputMenu("| Selecteer een film |");
                    foreach (TimeTableModel timeTable in maandagtimeTableList)
                    {
                        Console.Clear();
                        IEnumerable<MovieModel> query = maandagFilms.Where(movie => movie.ID == timeTable.MovieID);
                        foreach (MovieModel movie in query)
                        {
                            MovieModelSelecter.Add($"Film: {movie.Name}", (x) =>
                            {
                                Console.Clear();
                                IEnumerable<RoomModel> query2 = roomList.Where(room => room.ID == timeTable.RoomID);
                                Console.WriteLine($"Film: {movie.Name}\nZaal: {roomList.FirstOrDefault(room => room.ID == timeTable.RoomID)?.ID}\nGenre: {movie.Genre}\nBeschrijving: {movie.Description}\nPEGI: {movie.PegiAge}\nTijdsduur: {movie.DurationInMin}\nBegintijd: {timeTable.StartDate}\nEindtijd: {timeTable.EndDate}");
                                Console.ReadLine();
                            });
                        }
                    }
                    MovieModelSelecter.UseMenu();
                    Console.ReadLine();
                });
                selectDay.Add($"Dinsdag", (x) =>
                {
                    Console.Clear();
                    InputMenu movieSelecter = new InputMenu("| Selecteer een film |");
                    foreach (TimeTableModel timeTable in dinsdagTimeTableModelList)
                    {
                        Console.Clear();
                        IEnumerable<MovieModel> query = dinsdagFilms.Where(movie => movie.ID == timeTable.MovieID);
                        foreach (MovieModel movie in query)
                        {
                            movieSelecter.Add($"Film: {movie.Name}", (x) =>
                            {
                                Console.Clear();
                                IEnumerable<RoomModel> query2 = roomList.Where(room => room.ID == timeTable.RoomID);
                                Console.WriteLine($"Film: {movie.Name}\nZaal: {roomList.FirstOrDefault(room => room.ID == timeTable.RoomID)?.ID}\nGenre: {movie.Genre}\nBeschrijving: {movie.Description}\nPEGI: {movie.PegiAge}\nTijdsduur: {movie.DurationInMin}\nBegintijd: {timeTable.StartDate}\nEindtijd: {timeTable.EndDate}");
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
                    foreach (TimeTableModel timeTable in woensdagTimeTableModelList)
                    {
                        Console.Clear();
                        IEnumerable<MovieModel> query = woensdagFilms.Where(movie => movie.ID == timeTable.MovieID);
                        foreach (MovieModel movie in query)
                        {
                            movieSelecter.Add($"Film: {movie.Name}", (x) =>
                            {
                                Console.Clear();
                                IEnumerable<RoomModel> query2 = roomList.Where(room => room.ID == timeTable.RoomID);
                                Console.WriteLine($"Film: {movie.Name}\nZaal: {roomList.FirstOrDefault(room => room.ID == timeTable.RoomID)?.ID}\nGenre: {movie.Genre}\nBeschrijving: {movie.Description}\nPEGI: {movie.PegiAge}\nTijdsduur: {movie.DurationInMin}\nBegintijd: {timeTable.StartDate}\nEindtijd: {timeTable.EndDate}");
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
                    foreach (TimeTableModel timeTable in donderdagTimeTableModelList)
                    {
                        Console.Clear();
                        IEnumerable<MovieModel> query = donderdagFilms.Where(movie => movie.ID == timeTable.MovieID);
                        foreach (MovieModel movie in query)
                        {
                            movieSelecter.Add($"Film: {movie.Name}", (x) =>
                            {
                                Console.Clear();
                                IEnumerable<RoomModel> query2 = roomList.Where(room => room.ID == timeTable.RoomID);
                                Console.WriteLine($"Film: {movie.Name}\nZaal: {roomList.FirstOrDefault(room => room.ID == timeTable.RoomID)?.ID}\nGenre: {movie.Genre}\nBeschrijving: {movie.Description}\nPEGI: {movie.PegiAge}\nTijdsduur: {movie.DurationInMin}\nBegintijd: {timeTable.StartDate}\nEindtijd: {timeTable.EndDate}");
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
                    foreach (TimeTableModel timeTable in vrijdagTimeTableModelList)
                    {
                        Console.Clear();
                        IEnumerable<MovieModel> query = vrijdagFilms.Where(movie => movie.ID == timeTable.MovieID);
                        foreach (MovieModel movie in query)
                        {
                            movieSelecter.Add($"Film: {movie.Name}", (x) =>
                            {
                                Console.Clear();
                                IEnumerable<RoomModel> query2 = roomList.Where(room => room.ID == timeTable.RoomID);
                                Console.WriteLine($"Film: {movie.Name}\nZaal: {roomList.FirstOrDefault(room => room.ID == timeTable.RoomID)?.ID}\nGenre: {movie.Genre}\nBeschrijving: {movie.Description}\nPEGI: {movie.PegiAge}\nTijdsduur: {movie.DurationInMin}\nBegintijd: {timeTable.StartDate}\nEindtijd: {timeTable.EndDate}");
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
                    foreach (TimeTableModel timeTable in zaterdagTimeTableModelList)
                    {
                        Console.Clear();
                        IEnumerable<MovieModel> query = zaterdagFilms.Where(movie => movie.ID == timeTable.MovieID);
                        foreach (MovieModel movie in query)
                        {
                            movieSelecter.Add($"Film: {movie.Name}", (x) =>
                            {
                                Console.Clear();
                                IEnumerable<RoomModel> query2 = roomList.Where(room => room.ID == timeTable.RoomID);
                                Console.WriteLine($"Film: {movie.Name}\nZaal: {roomList.FirstOrDefault(room => room.ID == timeTable.RoomID)?.ID}\nGenre: {movie.Genre}\nBeschrijving: {movie.Description}\nPEGI: {movie.PegiAge}\nTijdsduur: {movie.DurationInMin}\nBegintijd: {timeTable.StartDate}\nEindtijd: {timeTable.EndDate}");
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
                    foreach (TimeTableModel timeTable in zondagTimeTableModelList)
                    {
                        Console.Clear();
                        IEnumerable<MovieModel> query = zondagFilms.Where(movie => movie.ID == timeTable.MovieID);
                        foreach (MovieModel movie in query)
                        {
                            movieSelecter.Add($"Film: {movie.Name}", (x) =>
                            {
                                Console.Clear();
                                IEnumerable<RoomModel> query2 = roomList.Where(room => room.ID == timeTable.RoomID);
                                Console.WriteLine($"Film: {movie.Name}\nZaal: {roomList.FirstOrDefault(room => room.ID == timeTable.RoomID)?.ID}\nGenre: {movie.Genre}\nBeschrijving: {movie.Description}\nPEGI: {movie.PegiAge}\nTijdsduur: {movie.DurationInMin}\nBegintijd: {timeTable.StartDate}\nEindtijd: {timeTable.EndDate}");
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

            menu.Add("Reserve Seats", (x) =>
            {
                int selectedMovieID;

                while (true)
                {
                    // Display available movies
                    ReservationServices.DisplayAvailableMovies();

                    // Ask user to choose a movie
                    Console.Write("Choose a movie (1 or 2): ");
                    if (int.TryParse(Console.ReadLine(), out selectedMovieID) && (selectedMovieID == 1 || selectedMovieID == 2))
                    {
                        break;  // Exit the loop if a valid movie is selected
                    }
                    else
                    {
                        Console.WriteLine("Invalid movie selection. Please choose either 1 or 2.");
                    }
                }

                // Ask for user's information
                string fullName;
                while (true)
                {
                    Console.Write("Enter your full name: ");
                    fullName = Console.ReadLine();
                    if (IsValidFullName(fullName))
                    {
                        break;  // Exit the loop if a valid full name is entered
                    }
                    else
                    {
                        Console.WriteLine("Please enter a valid full name.");
                    }
                }

                // Ask for user's age
                int userAge;
                while (true)
                {
                    Console.Write("Enter your age: ");
                    if (int.TryParse(Console.ReadLine(), out userAge) && userAge > 0 && userAge <= 100)
                    {
                        break;  // Exit the loop if a valid age is entered
                    }
                    else
                    {
                        Console.WriteLine("Please enter a valid age between 1 and 100.");
                    }
                }

                // Ask for user's email
                string email;
                while (true)
                {
                    Console.Write("Enter your email: ");
                    email = Console.ReadLine();
                    if (IsValidEmail(email))
                    {
                        break;  // Exit the loop if a valid email is entered
                    }
                    else
                    {
                        Console.WriteLine("Please enter a valid email.");
                    }
                }

                // Ask for user's phone number
                string phoneNumber;
                while (true)
                {
                    Console.Write("Enter your phone number (starting with 0 and max 10 digits): ");
                    phoneNumber = Console.ReadLine();
                    if (IsValidPhoneNumber(phoneNumber))
                    {
                        break;  // Exit the loop if a valid phone number is entered
                    }
                    else
                    {
                        Console.WriteLine("Please enter a valid phone number starting with 0 and max 10 digits.");
                    }
                }

                // Ask user to choose a room
                int selectedRoomID;
                while (true)
                {
                    Console.Write("Choose a room (1, 2, or 3): ");
                    if (int.TryParse(Console.ReadLine(), out selectedRoomID) && selectedRoomID >= 1 && selectedRoomID <= 3)
                    {
                        break;  // Exit the loop if a valid room is selected
                    }
                    else
                    {
                        Console.WriteLine("Invalid room selection.");
                    }
                }

                // Display the layout of the selected room
                ReservationServices.DisplayRoomLayout(selectedRoomID);

                // Display available seats
                var availableSeats = ReservationServices.GetAvailableSeats(selectedRoomID);
                Console.WriteLine("Available Seats:");
                foreach (var seat in availableSeats)
                {
                    Console.WriteLine($"Seat ID: {seat.ID}, Room ID: {seat.ID}, Name: {seat.Name}, Rank: {seat.Rank}, Type: {seat.Type}");
                }

                // Ask user to choose seats
                Console.WriteLine("Enter seat numbers to reserve (comma-separated):");
                var input = Console.ReadLine();
                var seatNumbers = new List<int>();

                foreach (var seatNumber in input.Split(','))
                {
                    if (int.TryParse(seatNumber.Trim(), out int num))
                    {
                        seatNumbers.Add(num);
                    }
                }

                // Reserve seats
                ReservationServices.ReserveSeats(selectedRoomID, seatNumbers, userAge, fullName, email, phoneNumber);



                Console.ReadLine();
            });

            static bool IsValidFullName(string fullName)
            {
                return !string.IsNullOrWhiteSpace(fullName) && fullName.Replace(" ", "").All(char.IsLetter);
            }
            menu.Add("Test SeeActors", (x) => // Als klant wil ik de acteurs van een film bekijken
            {
                List<ActorModel> authors = new List<ActorModel>();
                authors.Add(new ActorModel("Jack Black", "Plays Po", 43));
                authors.Add(new ActorModel("Jackie Chan", "Plays Monkey", 57));
                authors.Add(new ActorModel("Ada Wong", "Plays Viper", 27));
                authors.Add(new ActorModel("Jada Pinket Smith", "Plays Tigress", 41));
                MovieModel movietje = new MovieModel("KUNG FU PANDA 4", "everybody was kung fu fighting", 12, 120, "Horror");
                movietje.Director = new DirectorModel("Jaycey", "Director from netherlands", 20);
                movietje.Actors.AddRange(authors);
                Console.WriteLine(movietje.SeeActors());
                Console.ReadLine();
            });
            menu.Add("Test SeeDirector", (x) => // Als klant wil ik de regisseur van een film zien
            {
                List<DirectorModel> directors = new List<DirectorModel>();
                directors.Add(new DirectorModel("Christopher Nolan", "Famous movie director known for several blockbuster movies such as Oppenheimer, Interstellar, Inception and many more", 53));
                MovieModel interStellar = new MovieModel("Interstellar", "While the earth no longer has the resources to supply the human race, a group of astronauts go to beyond the milky way to find a possible future planet for mankind", 12, 190, "Sci-Fi");
                Console.WriteLine(interStellar.SeeDirector(directors));
                Console.ReadLine();
            });
            menu.Add("Test SeeDescription", (x) => // Als klant wil ik de omschrijving (leeftijd + genre) van een film zien
            {
                MovieModel interStellar = new MovieModel("Interstellar", "While the earth no longer has the resources to supply the human race, a group of astronauts go to beyond the milky way to find a possible future planet for mankind", 12, 190, "Sci-Fi");
                Console.WriteLine(interStellar.SeeDescription());
                Console.ReadLine();
            });
            menu.Add("set prices", (x) =>
            {
                var prices = SeatPriceCalculator.GetCurrentPrices();
                SeatPriceCalculator.WritePrices();
                System.Console.WriteLine("\nChange Prices? (Y/N)");
                char input = Console.ReadKey().KeyChar;
                if (input.Equals('Y') || input.Equals('y'))
                {
                    bool changing = true;
                    while (changing)
                    {
                        System.Console.WriteLine("type price to change: (Q to quit)");
                        string response = Console.ReadLine() ?? "";
                        switch (response.ToLower())
                        {
                            case "price tier i" or "tier i" or "i" or "1":
                                Console.WriteLine("type new price:");
                                response = Console.ReadLine() ?? "";
                                prices.PriceTierI = decimal.Parse(response);
                                break;
                            case "price tier ii" or "tier ii" or "ii" or "2":
                                Console.WriteLine("type new price:");
                                response = Console.ReadLine() ?? "";
                                prices.PriceTierII = decimal.Parse(response);
                                break;
                            case "price tier iii" or "tier iii" or "iii" or "3":
                                Console.WriteLine("type new price:");
                                response = Console.ReadLine() ?? "";
                                prices.PriceTierIII = decimal.Parse(response);
                                break;
                            case "extra space" or "extra" or "space":
                                Console.WriteLine("type new price:");
                                response = Console.ReadLine() ?? "";
                                prices.ExtraSpace = decimal.Parse(response);
                                break;
                            case "loveseat" or "love" or "love seat":
                                Console.WriteLine("type new price:");
                                response = Console.ReadLine() ?? "";
                                prices.LoveSeat = decimal.Parse(response);
                                break;
                            case "q":
                                changing = false;
                                break;
                        }
                    }

                }
                SeatPriceCalculator.UpdatePrice(prices);
                SeatPriceCalculator.WritePrices();
                Console.ReadLine();

            });
            menu.Add("get seat PRICE info", (x) =>
            {
                SeatModel seat = new SeatModel("naam", "II", "loveseat");
                Console.WriteLine(SeatPriceCalculator.ShowCalculation(seat));
                Console.ReadLine();
            });
            menu.UseMenu();


            static bool IsValidEmail(string email)
            {
                return Regex.IsMatch(email, @"^[a-zA-Z0-9._%+-]+@(gmail\.com|hotmail\.com|outlook\.com|hotmail\.nl)$");
            }

            static bool IsValidPhoneNumber(string phoneNumber)
            {
                // Phone number must start with '0' and have a maximum length of 10 characters
                return phoneNumber.StartsWith("0") && phoneNumber.Length <= 10 && phoneNumber.All(char.IsDigit);
            }



            menu.UseMenu();
        }
    }
}
