using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MovieTickets.Models
{
    public class Movie
    {
        public int Id { get; set; }
        [Required]
        [StringLength(256)]
        public string Title { get; set; }
        [StringLength(128)]
        public string Director { get; set; }
        [StringLength(128)]
        public string Cast { get; set; }
        [Required]
        public int DurationMin  { get; set; }
        [Required]
        [StringLength(500)]
        public string Description  { get; set; }
        [Required]
        public string ImagePoster  { get; set; }
    }
}