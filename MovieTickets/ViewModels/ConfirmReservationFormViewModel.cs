using MovieTickets.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MovieTickets.ViewModels
{
    public class ConfirmReservationFormViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} can have a max of {1} characters.")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} can have a max of {1} characters.")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

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

        [Required]
        [Display(Name = "Photo")]
        public string Photo { get; set; }

        public Reservation Reservation { get; set; }

        public ConfirmReservationFormViewModel(Reservation reservation, ApplicationUser user)
        {
            Id = reservation.Id;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Email = user.Email;
            PhoneNumber = user.PhoneNumber;
            Reservation = reservation;
        }

        public ConfirmReservationFormViewModel() { 
            Id= 0;
        }
    }
}