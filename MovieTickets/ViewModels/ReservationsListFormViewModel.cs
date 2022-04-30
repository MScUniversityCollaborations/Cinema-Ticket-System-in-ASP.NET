using MovieTickets.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MovieTickets.ViewModels
{
    public class ReservationsListFormViewModel
    {
        [Required]
        [EmailAddress]
        [StringLength(100, ErrorMessage = "The {0} can have a max of {1} characters.")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Phone]
        [StringLength(32, ErrorMessage = "The {0} can have a max of {1} characters.")]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        public IEnumerable<Reservation> Reservations { get; set; }

        /*public ReservationsListFormViewModel(Reservation reservation)
        {
        }*/
    }
}