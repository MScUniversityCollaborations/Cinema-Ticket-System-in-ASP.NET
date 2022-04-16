using MovieTickets.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MovieTickets.ViewModels
{
    public class ScreeningFormViewModel
    {
        public IEnumerable<Auditorium> Auditoriums { get; set; }

        public IEnumerable<Movie> Movies { get; set; }

        public int? Id { get; set; }

        [Display(Name = "Auditorium")]
        [Required]
        public byte? AuditoriumId { get; set; }

        [Display(Name = "Movie")]
        [Required]
        public int? MovieId { get; set; }

        [Display(Name = "Screening Start")]
        [Required]
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MMM/yyyy HH:mm}")]
        public DateTime ScreeningStart { get; set; }

        [Display(Name = "Screening Ending")]
        [Required]
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MMM/yyyy}")]
        public DateTime ScreeningEnd { get; set; }

        public string PageTitle
        {
            get
            {
                return Id != 0 ? "Edit Screening" : "New Screening";
            }
        }

        public string PageDesc
        {
            get
            {
                return Id != 0 ? "Update an existing screening." : "Create a new screening.";
            }
        }

        public string State
        {
            get
            {
                return Id != 0 ? "Update" : "Create";
            }
        }

        public ScreeningFormViewModel()
        {
            Id = 0;
        }

        public ScreeningFormViewModel(Screening screening)
        {
            Id = screening.Id;
            AuditoriumId = screening.AuditoriumId;
            MovieId = screening.MovieId;
            ScreeningStart = screening.ScreeningStart;
            ScreeningEnd = screening.ScreeningEnd;
        }
    }
}