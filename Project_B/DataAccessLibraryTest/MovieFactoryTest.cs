using System.Text.Json;
using DataAccessLibrary;
using DataAccessLibrary.logic;

namespace DataAccessLibraryTest
{
    [TestClass]
    public class MovieFactoryTest
    {
        public const string TestDbPath = "movieTest.db";
        private readonly DataAccess _db;
        private readonly MovieFactory _mf;
        private readonly ActorFactory _af;
        private readonly DirectorFactory _df;
        public MovieFactoryTest()
        {
            try
            {
                File.Delete(TestDbPath);
            }
            catch (Exception ex)
            {
                System.Console.WriteLine($"cannot delete testdb {TestDbPath}: {ex.Message}");
            }

            _db = new SQliteDataAccess($"Data Source={TestDbPath}; Version = 3; New = True; Compress = True;");
            _af = new ActorFactory(_db);
            _df = new DirectorFactory(_db);
            _mf = new MovieFactory(_db, _df, _af);
        }
        [TestMethod]
        public void TestAddMovieNoOtherFactories()
        {
            MovieModel movie = new MovieModel(
                "Shrek", "the best movie", 7, 123, "art"
            );
            Assert.IsTrue(_mf.ItemToDb(movie));
            Assert.IsNotNull(movie.ID);
        }
        [TestMethod]
        public void TestAddMovieWithOtherFactories()
        {
            List<ActorModel> actors = new List<ActorModel> {
                new ActorModel("shrek", "", 69),
                new ActorModel("lord farquad", "", 45)
            };
            DirectorModel dir = new("someone", "I used to know", 0);
            MovieModel movie = new MovieModel("shrek remastered", "remaster", 18, 321, "horor", dir, actors);
            Assert.IsTrue(_mf.ItemToDb(movie));
            Assert.IsNotNull(movie.ID);
            Assert.IsNotNull(actors[0].ID);
            Assert.IsNotNull(dir.ID);
        }
        public void TestGetMovieNoOtherFactories()
        {
            MovieModel movie = new MovieModel(
                "Toy story", "movie", 3, 211, "child"
            );
            Assert.IsTrue(_mf.CreateItem(movie));
            var newMovie = _mf.GetItemFromId(movie.ID ?? 1);
            Assert.AreEqual(movie.Name, newMovie.Name);
        }
        public void TestGetMovieWithOtherFactories()
        {
            List<ActorModel> actors = new(){
                new ActorModel("buzz", "lightyear", 1),
                new ActorModel("cowboy dude", "howdy", 4)
            };
            DirectorModel dir = new("dude", "a dude", 32);
            MovieModel movie = new MovieModel(
                "toy story 2", "now with actors and director", 3, 343, "kids", dir, actors
            );
            Assert.IsTrue(_mf.ItemToDb(movie));
            var newMovie = _mf.GetItemFromId(movie.ID ?? 1);
            var buzz = _af.GetItemFromId(actors[0].ID ?? 1);
            var newDir = _df.GetItemFromId(dir.ID ?? 1);
            Assert.AreEqual(newMovie.Name, movie.Name);
            Assert.AreEqual(dir.Name, newDir.Name);
            Assert.AreEqual(actors[0].Name, buzz.Name);
        }
        [TestMethod]
        public void TestUpdateMovieNoOtherFactories()
        {
            MovieModel movie = new MovieModel(
                "John Wick", "Baba Yaga", 18, 300, "mindless violence"
            );
            Assert.IsTrue(_mf.ItemToDb(movie));
            movie.Genre = "mindfull violence";
            Assert.IsTrue(_mf.ItemToDb(movie));
            var newMovie = _mf.GetItemFromId(movie.ID ?? 1);
            Assert.AreEqual(newMovie.Genre, movie.Genre);
        }
        [TestMethod]
        public void TestUpdateMovieWithOtherFactories()
        {
            MovieModel movie = new MovieModel(
                "John Wick 2", "Baba Yaga 2 electric bogalo", 18, 306, "violence"
            );
            Assert.IsTrue(_mf.ItemToDb(movie));
            movie.Actors.Add(
                new ActorModel(
                    "John", "Wick", 40
                )
            );
            movie.Actors.Add(
                new ActorModel(
                    "the", "dog2", 1
                )
            );
            movie.Director = new DirectorModel(
                "hanz", "vlammenwerfer", 100
            );
            Assert.IsTrue(_mf.ItemToDb(movie));
            var newMovie = _mf.GetItemFromId(movie.ID ?? 1);
            Assert.AreEqual(newMovie.Genre, movie.Genre);
            var dir = _df.GetItemFromId(newMovie.DirectorID ?? 1);
            Assert.AreEqual(dir.Name, movie.Director.Name);
        }
    }
}