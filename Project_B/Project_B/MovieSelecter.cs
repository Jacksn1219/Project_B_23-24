using Models;
using Project_B;
using DataAccessLibrary.models;
using Project_B.services;
using DataAccessLibrary;
public static class MovieSelecter
{
    public static InputMenu movieSelecter;
    static MovieSelecter()
    {
        // InputMenu movieSelecter = new InputMenu("| Selecteer een film |");
        // foreach (TimeTableModel timeTable in dinsdagTimeTableModelList)
        // {
        //     Console.Clear();
        //     IEnumerable<MovieModel> query = dinsdagFilms.Where(movie => movie.ID == timeTable.MovieID);
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
    }
}