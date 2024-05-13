using System.Globalization;
using System.Reflection.Metadata;
using System.Runtime.Serialization;
using DataAccessLibrary;
using DataAccessLibrary.models.interfaces;

namespace DataAccessLibrary.models
{
    public class TimeTableModel : DbItem
    {
        public MovieModel? Movie;
        public int? RoomID { get; set; }
        public RoomModel? Room;
        public int? MovieID { get; set; }
        private string _startDate;
        private string _endDate;
        public DateTime DateTimeStartDate
        {
            get
            {
                return DateTime.Parse(_startDate, CultureInfo.InvariantCulture);
            }
        }
        public DateTime DateTimeEndDate
        {
            get
            {
                return DateTime.Parse(_endDate, CultureInfo.InvariantCulture);
            }
        }
        public string StartDate
        {
            get => _startDate.ToString(CultureInfo.InvariantCulture);
            set
            {
                _startDate = value;
                IsChanged = true;
            }
        }
        public string EndDate
        {
            get => _endDate.ToString(CultureInfo.InvariantCulture);
            set
            {
                _endDate = value;
                IsChanged = true;
            }
        }
        /// <summary>
        /// parameterless ctor to please the jsonserialiser gods
        /// </summary>
        public TimeTableModel(int v)
        {

        }
        public TimeTableModel(RoomModel room, MovieModel movie, DateTime startDate) :
            this(room, movie, startDate, startDate.AddMinutes((double)movie.DurationInMin))
        { }
        public TimeTableModel(RoomModel room, MovieModel movie, DateTime startDate, DateTime endDate)
        {
            Room = room;
            Movie = movie;
            StartDate = startDate.ToString(CultureInfo.InvariantCulture);
            EndDate = endDate.ToString(CultureInfo.InvariantCulture);
        }
        public TimeTableModel(int roomId, int movieId, DateTime startDate, DateTime endDate)
        {
            RoomID = roomId;
            MovieID = movieId;
            StartDate = startDate.ToString(CultureInfo.InvariantCulture);
            EndDate = endDate.ToString(CultureInfo.InvariantCulture);
        }
    }
}