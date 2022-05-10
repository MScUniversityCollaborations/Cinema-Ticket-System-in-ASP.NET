using MovieTickets.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MovieTickets.ViewModels
{
    public class UserReservationsListViewModel
    {
        [Required]
        public string UserId { get; set; }

        public string UserName { get; set; }


        public IEnumerable<Reservation> Reservations { get; set; }
    }
}