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
                return Id != 0 ? "Edit Auditorium" : "New Auditorium";
            }
        }

        public string PageDesc
        {
            get
            {
                return Id != 0 ? "Update an existing Auditorium." : "Create a new Auditorium.";
            }
        }

        public string State
        {
            get
            {
                return Id != 0 ? "Update" : "Create";
            }
        }

        public AuditoriumFormViewModel()
        {
            Id = 0;
        }

        public AuditoriumFormViewModel(Auditorium auditorium)
        {
            Id = auditorium.Id;
            Name = auditorium.Name;
            TotalSeats = auditorium.TotalSeats;
        }
    }
}