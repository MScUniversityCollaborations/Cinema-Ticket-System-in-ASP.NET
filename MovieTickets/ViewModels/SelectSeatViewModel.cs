using MovieTickets.Models;
using System.Collections.Generic;

namespace MovieTickets.ViewModels
{
    public class SelectSeatViewModel
    {
        public IEnumerable<Reservation> Reservations { get; set; }

        public Screening Screening { get; set; }

        public int? ScreeningId { get; set; }

        public SelectSeatViewModel()
        {
            ScreeningId = 0;
        }

        public SelectSeatViewModel(int ScreeningId)
        {
            this.ScreeningId = ScreeningId;
        }

        public SelectSeatViewModel(Screening screening)
        {
            ScreeningId = screening.Id;
        }
    }
}