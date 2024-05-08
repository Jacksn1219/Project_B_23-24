using Models;
using Project_B;
using DataAccessLibrary.models;
using Project_B.services;
using DataAccessLibrary;
public class DaySelectorMenu
{
    public readonly InputMenu selectDay;
    public DaySelectorMenu()
    {
        InputMenu selectDay = new InputMenu("| Selecteer een dag |");
        selectDay.Add($"Maandag", (x) =>
        {
            // Console.Clear();
            // InputMenu MovieModelSelecter = new InputMenu("useLambda");
            // foreach (MovieModel movie in maandagFilms)
            // {
            //     MovieModelSelecter.Add(movie.Name ?? "", (x) =>
            //     {
            //         Console.WriteLine($"Film: {movie.Name}\nZaal: Room 1\nGenre: {movie.Genre}\nBeschrijving: {movie.Description}\nDirector: -\nActors: -\nPEGI: {movie.PegiAge}\nTijdsduur: {movie.DurationInMin}\nBegintijd: -\nEindtijd: -");
            //         Console.WriteLine();
            //         Console.ReadLine();
            //     });
            //});
            //Console.ReadLine();
            // foreach (TimeTableModel timeTable in maandagtimeTableList)
            // {
            //     //IEnumerable<MovieModel> query = maandagFilms.Where(movie => movie.ID == timeTable.MovieID);
            //     foreach (MovieModel movie in maandagFilms)
            //     {
            //         MovieModelSelecter.Add($"Film: {movie.Name}", (x) =>
            //         {
            //             IEnumerable<RoomModel> query2 = roomList.Where(room => room.ID == timeTable.RoomID);
            //             Console.WriteLine($"Film: {movie.Name}\nZaal: {roomList.FirstOrDefault(room => room.ID == timeTable.RoomID)?.ID}\nGenre: {movie.Genre}\nBeschrijving: {movie.Description}\nPEGI: {movie.PegiAge}\nTijdsduur: {movie.DurationInMin}\nBegintijd: {timeTable.StartDate}\nEindtijd: {timeTable.EndDate}");
            //             Console.ReadLine();
            //         });
            //     }
            // }
            //MovieModelSelecter.UseMenu(() => Universal.printAsTitle("Selecteer een film"));
        });
        selectDay.Add($"Dinsdag", (x) =>
        {
            List<MovieModel> movies = new List<MovieModel>();
            MovieModel movie = new MovieModel("Cars","Car",12, 10,"");
            movies.Add(movie);
            SelectMovie(movies);
        });
        selectDay.Add($"Woensdag", (x) =>
        {
            // Console.Clear();
            // InputMenu movieSelecter = new InputMenu("| Selecteer een film |");
            // foreach (TimeTableModel timeTable in woensdagTimeTableModelList)
            // {
            //     Console.Clear();
            //     IEnumerable<MovieModel> query = woensdagFilms.Where(movie => movie.ID == timeTable.MovieID);
            //     foreach (MovieModel movie in query)
            //     {
            //         movieSelecter.Add($"Film: {movie.Name}", (x) =>
            //         {
            //             Console.Clear();
            //             IEnumerable<RoomModel> query2 = roomList.Where(room => room.ID == timeTable.RoomID);
            //             Console.WriteLine($"Film: {movie.Name}\nZaal: {roomList.FirstOrDefault(room => room.ID == timeTable.RoomID)?.ID}\nGenre: {movie.Genre}\nBeschrijving: {movie.Description}\nPEGI: {movie.PegiAge}\nTijdsduur: {movie.DurationInMin}\nBegintijd: {timeTable.StartDate}\nEindtijd: {timeTable.EndDate}");
            //             Console.ReadLine();
            //         });
            //     }
            // }
            // movieSelecter.UseMenu();
            // Console.ReadLine();
        });
        selectDay.Add($"Donderdag", (x) =>
        {
            // Console.Clear();
            // InputMenu movieSelecter = new InputMenu("| Selecteer een film |");
            // foreach (TimeTableModel timeTable in donderdagTimeTableModelList)
            // {
            //     Console.Clear();
            //     IEnumerable<MovieModel> query = donderdagFilms.Where(movie => movie.ID == timeTable.MovieID);
            //     foreach (MovieModel movie in query)
            //     {
            //         movieSelecter.Add($"Film: {movie.Name}", (x) =>
            //         {
            //             Console.Clear();
            //             IEnumerable<RoomModel> query2 = roomList.Where(room => room.ID == timeTable.RoomID);
            //             Console.WriteLine($"Film: {movie.Name}\nZaal: {roomList.FirstOrDefault(room => room.ID == timeTable.RoomID)?.ID}\nGenre: {movie.Genre}\nBeschrijving: {movie.Description}\nPEGI: {movie.PegiAge}\nTijdsduur: {movie.DurationInMin}\nBegintijd: {timeTable.StartDate}\nEindtijd: {timeTable.EndDate}");
            //             Console.ReadLine();
            //         });
            //     }
            // }
            // movieSelecter.UseMenu();
            // Console.ReadLine();
        });
        selectDay.Add($"Vrijdag", (x) =>
        {
            // Console.Clear();
            // InputMenu movieSelecter = new InputMenu("| Selecteer een film |");
            // foreach (TimeTableModel timeTable in vrijdagTimeTableModelList)
            // {
            //     Console.Clear();
            //     IEnumerable<MovieModel> query = vrijdagFilms.Where(movie => movie.ID == timeTable.MovieID);
            //     foreach (MovieModel movie in query)
            //     {
            //         movieSelecter.Add($"Film: {movie.Name}", (x) =>
            //         {
            //             Console.Clear();
            //             IEnumerable<RoomModel> query2 = roomList.Where(room => room.ID == timeTable.RoomID);
            //             Console.WriteLine($"Film: {movie.Name}\nZaal: {roomList.FirstOrDefault(room => room.ID == timeTable.RoomID)?.ID}\nGenre: {movie.Genre}\nBeschrijving: {movie.Description}\nPEGI: {movie.PegiAge}\nTijdsduur: {movie.DurationInMin}\nBegintijd: {timeTable.StartDate}\nEindtijd: {timeTable.EndDate}");
            //             Console.ReadLine();
            //         });
            //     }
            // }
            // movieSelecter.UseMenu();
            // Console.ReadLine();
        });
        selectDay.Add($"Zaterdag", (x) =>
        {
            // Console.Clear();
            // InputMenu movieSelecter = new InputMenu("| Selecteer een film |");
            // foreach (TimeTableModel timeTable in zaterdagTimeTableModelList)
            // {
            //     Console.Clear();
            //     IEnumerable<MovieModel> query = zaterdagFilms.Where(movie => movie.ID == timeTable.MovieID);
            //     foreach (MovieModel movie in query)
            //     {
            //         movieSelecter.Add($"Film: {movie.Name}", (x) =>
            //         {
            //             Console.Clear();
            //             IEnumerable<RoomModel> query2 = roomList.Where(room => room.ID == timeTable.RoomID);
            //             Console.WriteLine($"Film: {movie.Name}\nZaal: {roomList.FirstOrDefault(room => room.ID == timeTable.RoomID)?.ID}\nGenre: {movie.Genre}\nBeschrijving: {movie.Description}\nPEGI: {movie.PegiAge}\nTijdsduur: {movie.DurationInMin}\nBegintijd: {timeTable.StartDate}\nEindtijd: {timeTable.EndDate}");
            //             Console.ReadLine();
            //         });
            //     }
            // }
            // movieSelecter.UseMenu();
            // Console.ReadLine();
        });
        selectDay.Add($"Zondag", (x) =>
        {
            // Console.Clear();
            // InputMenu movieSelecter = new InputMenu("| Selecteer een film |");
            // foreach (TimeTableModel timeTable in zondagTimeTableModelList)
            // {
            //     Console.Clear();
            //     IEnumerable<MovieModel> query = zondagFilms.Where(movie => movie.ID == timeTable.MovieID);
            //     foreach (MovieModel movie in query)
            //     {
            //         movieSelecter.Add($"Film: {movie.Name}", (x) =>
            //         {
            //             Console.Clear();
            //             IEnumerable<RoomModel> query2 = roomList.Where(room => room.ID == timeTable.RoomID);
            //             Console.WriteLine($"Film: {movie.Name}\nZaal: {roomList.FirstOrDefault(room => room.ID == timeTable.RoomID)?.ID}\nGenre: {movie.Genre}\nBeschrijving: {movie.Description}\nPEGI: {movie.PegiAge}\nTijdsduur: {movie.DurationInMin}\nBegintijd: {timeTable.StartDate}\nEindtijd: {timeTable.EndDate}");
            //             Console.ReadLine();
            //         });
            //     }
            // }
            // movieSelecter.UseMenu();
            // Console.ReadLine();
        });
        selectDay.UseMenu();
    }
    public MovieModel SelectMovie(List<MovieModel> movies)
    {
        MovieModel toReturn = new();

        InputMenu selectMovie = new InputMenu("", null);
        foreach(MovieModel movie in movies){
            selectMovie.Add(movie.Name, (x) =>{
                toReturn = movie;
            });
        }
        selectMovie.UseMenu();
        return toReturn;
    }
}
