using System;
using System.ComponentModel.DataAnnotations;

namespace MovieTickets.Models
{
    public class Screening
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Auditorium Id")]
        public byte AuditoriumId { get; set; }

        [Required]
        public int MovieId { get; set; }

        public Auditorium Auditorium { get; set; }

        public Movie Movie { get; set; }

        [Display(Name = "Screening Start")]
        [Required]
        public DateTime ScreeningStart { get; set; }

        [Display(Name = "Screening End")]
        [Required]
        public DateTime ScreeningEnd { get; set; }
    }
}