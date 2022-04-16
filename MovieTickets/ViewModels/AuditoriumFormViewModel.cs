using MovieTickets.Models;
using System.ComponentModel.DataAnnotations;

namespace MovieTickets.ViewModels
{
    public class AuditoriumFormViewModel
    {
        public byte? Id { get; set; }

        [Required]
        [StringLength(32, ErrorMessage = "The {0} can have a max of {1} characters.")]
        public string Name { get; set; }

        [Required]
        public int TotalSeats { get; set; }

        public string PageTitle
        {
            get
            {
                return Id != null ? "Edit Auditorium" : "New Auditorium";
            }
        }

        public string PageDesc
        {
            get
            {
                return Id != null ? "Update an existing Auditorium." : "Create a new Auditorium.";
            }
        }

        public string State
        {
            get
            {
                return Id != null ? "Update" : "Create";
            }
        }

        public AuditoriumFormViewModel()
        {
            Id = null;
        }

        public AuditoriumFormViewModel(Auditorium auditorium)
        {
            Id = auditorium.Id;
            Name = auditorium.Name;
            TotalSeats = auditorium.TotalSeats;
        }
    }
}