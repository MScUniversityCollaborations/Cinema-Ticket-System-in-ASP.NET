using System;
using System.ComponentModel.DataAnnotations;

namespace MovieTickets.Models
{
    public class Movie
    {
        public int Id { get; set; }

        [Display(Name = "Title")]
        [Required]
        [StringLength(256)]
        public string Title { get; set; }

        [Display(Name = "Director")]
        [StringLength(128)]
        public string Director { get; set; }

        [Display(Name = "Cast")]
        [StringLength(256)]
        public string Cast { get; set; }

        [Display(Name = "Duration Time (Minutes)")]
        [Required]
        public byte DurationMin { get; set; }

        [Display(Name = "Description")]
        [StringLength(500)]
        public string Description { get; set; }

        [Display(Name = "Poster")]
        [Required]
        public string ImagePoster { get; set; }

        public Genre Genre { get; set; }

        [Display(Name = "Genre")]
        [Required]
        public byte GenreId { get; set; }

        [Display(Name = "Date Added")]
        [Required]
        public DateTime DateAdded { get; set; }

        [Display(Name = "Now Showing?")]
        [Required]
        public bool NowShowing { get; set; }
    }
}