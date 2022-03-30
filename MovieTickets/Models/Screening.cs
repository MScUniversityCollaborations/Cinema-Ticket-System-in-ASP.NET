using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MovieTickets.Models
{
    public class Screening
    {
        public int Id { get; set; }

        [Required]
        public byte AuditoriumId { get; set; }

        [Required]
        public byte MovieId { get; set; }

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