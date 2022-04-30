using System.ComponentModel.DataAnnotations;

namespace MovieTickets.ViewModels
{
    public class FindReservationFormViewModel
    {
        [EmailAddress]
        [StringLength(100, ErrorMessage = "The {0} can have a max of {1} characters.")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Phone]
        [StringLength(32, ErrorMessage = "The {0} can have a max of {1} characters.")]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
    }
}