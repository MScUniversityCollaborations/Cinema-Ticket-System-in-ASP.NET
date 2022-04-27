using System;
using System.ComponentModel.DataAnnotations;

namespace MovieTickets.Models
{
    public class Reservation
    {
        public int Id { get; set; }

        public Screening Screening { get; set; }

        public ApplicationUser User { get; set; }

        [Required]
        [Display(Name = "Screening ID")]
        public int ScreeningId { get; set; }

        [Required]
        [Display(Name = "User ID")]
        public string UserId { get; set; }

        [Display(Name = "Photo")]
        public string Photo { get; set; }

        [Display(Name = "Paid")]
        [Required]
        public bool Paid { get; set; }

        [Display(Name = "Payment Type")]
        [StringLength(32)]
        [Required]
        public string PaymentType { get; set; }

        [Display(Name = "Active")]
        [Required]
        public bool Active { get; set; }

        [Display(Name = "Reserved")]
        [Required]
        public bool Reserved { get; set; }

        [Display(Name = "Row No.")]
        [Required]
        public byte Row { get; set; }

        [Display(Name = "Seat No.")]
        [Required]
        public byte Seat { get; set; }
    }
}