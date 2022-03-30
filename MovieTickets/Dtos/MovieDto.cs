using System;
using System.ComponentModel.DataAnnotations;

namespace MovieTickets.Dtos
{
    public class MovieDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(256)]
        public string Title { get; set; }

        [StringLength(128)]
        public string Director { get; set; }

        [StringLength(256)]
        public string Cast { get; set; }

        [Required]
        public byte DurationMin { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [Required]
        public string ImagePoster { get; set; }

        public GenreDto Genre { get; set; }

        [Required]
        public byte GenreId { get; set; }

        [Required]
        public DateTime DateAdded { get; set; }

        [Required]
        public bool NowShowing { get; set; }

    }
}