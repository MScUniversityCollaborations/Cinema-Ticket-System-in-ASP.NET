using MovieTickets.Models;
using System.Collections.Generic;

namespace MovieTickets.ViewModels
{
    public class SelectDateTimeViewModel
    {
        public Movie Movie { get; set; }

        public IEnumerable<Screening> Screenings { get; set; }

        public int? MovieId { get; set; }

        public SelectDateTimeViewModel()
        {
            MovieId = 0;
        }

        public SelectDateTimeViewModel(int MovieId)
        {
            this.MovieId = MovieId;
        }

        public SelectDateTimeViewModel(Movie movie)
        {
            MovieId = movie.Id;
        }
    }
}