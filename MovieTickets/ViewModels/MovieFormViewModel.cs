using MovieTickets.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MovieTickets.ViewModels
{
    public class MovieFormViewModel
    {
        public IEnumerable<Genre> Genres { get; set; }

        public int? Id { get; set; }

        [Required]
        [StringLength(256, ErrorMessage = "The {0} can have a max of {1} characters.")]
        public string Title { get; set; }

        [StringLength(128, ErrorMessage = "The {0} can have a max of {1} characters.")]
        public string Director { get; set; }

        [StringLength(256, ErrorMessage = "The {0} can have a max of {1} characters.")]
        public string Cast { get; set; }

        [StringLength(500, ErrorMessage = "The {0} can have a max of {1} characters.")]
        public string Description { get; set; }

        [Display(Name = "Poster")]
        [Required]
        public string ImagePoster { get; set; }

        [Display(Name = "Genre")]
        [Required]
        public byte? GenreId { get; set; }

        [Display(Name = "Duration Time (Minutes)")]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]
        [Required]
        public byte? DurationMin { get; set; }

        [Display(Name = "Now Showing?")]
        [Required]
        public bool NowShowing { get; set; }

        public string PageTitle
        {
            get
            {
                return Id != 0 ? "Edit Movie" : "New Movie";
            }
        }

        public string PageDesc
        {
            get
            {
                return Id != 0 ? "Update an existing movie." : "Create a new movie.";
            }
        }

        public string State
        {
            get
            {
                return Id != 0 ? "Update" : "Create";
            }
        }

        public MovieFormViewModel()
        {
            Id = 0;
        }

        public MovieFormViewModel(Movie movie)
        {
            Id = movie.Id;
            Title = movie.Title;
            Director = movie.Director;
            Cast = movie.Cast;
            Description = movie.Description;
            ImagePoster = movie.ImagePoster;
            GenreId = movie.GenreId;
            DurationMin = movie.DurationMin;
        }
    }
}