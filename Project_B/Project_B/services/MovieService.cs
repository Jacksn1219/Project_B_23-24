using DataAccessLibrary;
using Models;

namespace Project_B.services;

public class MovieService
{
    private readonly MovieFactory _mf;
    public MovieService(MovieFactory mf)
    {
        _mf = mf;
    }
    public MovieModel SelectMovie(MovieModel[] movies, int pagesize = 6)
    {
        int index = 0;
        while (true)
        {
            InputMenu movieMenu = new("movies:", true);
            for (int i = index; i < index + pagesize; i++) ;

        }
    }
}