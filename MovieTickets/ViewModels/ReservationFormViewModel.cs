using MovieTickets.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MovieTickets.ViewModels
{
    public class ReservationFormViewModel
    {
        public IEnumerable<ApplicationUser> User { get; set; }

        public IEnumerable<Screening> Screening { get; set; }

        public int? Id { get; set; }

        [Display(Name = "User")]
        [StringLength(128, ErrorMessage = "The {0} can have a max of {1} characters.")]
        [Required]
        public string UserId { get; set; }

        [Display(Name = "Screening")]
        [Required]
        public int? ScreeningId { get; set; }

        [Display(Name = "Reserved?")]
        [Required]
        public bool Reserved { get; set; }

        [Display(Name = "User Photo")]
        public string Photo { get; set; }

        [Display(Name = "Paid?")]
        [Required]
        public bool Paid { get; set; }

        [StringLength(32, ErrorMessage = "The {0} can have a max of {1} characters.")]
        [Required]
        public string PaymentType { get; set; }

        [Display(Name = "Active?")]
        [Required]
        public bool Active { get; set; }

        [Display(Name = "Seat No.")]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]
        [Required]
        public byte Seat { get; set; }

        public string PageTitle
        {
            get
            {
                return Id != 0 ? "Edit Reservation" : "New Reservation";
            }
        }

        public string PageDesc
        {
            get
            {
                return Id != 0 ? "Update an existing reservation." : "Create a new reservation.";
            }
        }

        public string State
        {
            get
            {
                return Id != 0 ? "Update" : "Create";
            }
        }

        public ReservationFormViewModel()
        {
            Id = 0;
        }

        public ReservationFormViewModel(Reservation reservation)
        {
            Id = reservation.Id;
            ScreeningId = reservation.ScreeningId;
            UserId = reservation.UserId;
            Photo = reservation.Photo;
            Paid = reservation.Paid;
            PaymentType = reservation.PaymentType;
            Active = reservation.Active;
            Reserved = reservation.Reserved;
        }
    }
}