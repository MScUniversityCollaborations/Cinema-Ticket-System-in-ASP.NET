using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MovieTickets.Models
{
    public class Auditorium
    {
        public byte Id { get; set; }

        [Display(Name = "Name")]
        [Required]
        [StringLength(32)]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Total Seats")]
        public int TotalSeats { get; set; }

    }
}