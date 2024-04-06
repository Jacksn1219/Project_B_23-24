using DataAccessLibrary;

namespace DataAccessLibrary.models
{
    public class TimeTableModel : IDbItem
    {
        public int? ID { get; }
        public int? MovieId { get; }
        public MovieModel? Movie;
        public int? RoomId { get; }
        public RoomModel? Room;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public TimeTableModel(RoomModel room, MovieModel movie, DateTime startDate) :
            this(room, movie, startDate, startDate.AddMinutes(movie.DurationInMin))
        { }
        public TimeTableModel(RoomModel room, MovieModel movie, DateTime startDate, DateTime endDate)
        {
            Room = room;
            Movie = movie;
            StartDate = startDate;
            EndDate = endDate;
        }
        internal TimeTableModel(int roomId, int movieId, DateTime startDate, DateTime endDate)
        {
            RoomId = roomId;
            MovieId = movieId;
            StartDate = startDate;
            EndDate = endDate;
        }
    }
}