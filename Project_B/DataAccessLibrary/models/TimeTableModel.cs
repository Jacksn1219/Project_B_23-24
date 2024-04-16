using DataAccessLibrary;
using DataAccessLibrary.models.interfaces;

namespace DataAccessLibrary.models
{
    public class TimeTableModel : DbItem
    {
        public override int? ID { get; internal set; }
        internal int? MovieID { get; set; }
        public readonly MovieModel? Movie;
        internal int? RoomID { get; set; }
        public readonly RoomModel? Room;
        private DateTime _startDate;
        private DateTime _endDate;

        public DateTime StartDate
        {
            get => _startDate;
            set
            {
                _startDate = value; IsChanged = true;
            }
        }
        public DateTime EndDate
        {
            get => _endDate;
            set
            {
                _endDate = value;
                IsChanged = true;
            }
        }
        public TimeTableModel(RoomModel room, MovieModel movie, DateTime startDate) :
            this(room, movie, startDate, startDate.AddMinutes((double)movie.DurationInMin))
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
            RoomID = roomId;
            MovieID = movieId;
            StartDate = startDate;
            EndDate = endDate;
        }
    }
}