using System.ComponentModel.DataAnnotations;

namespace MovieTickets.Models
{
    public class Auditorium
    {
        public int Id { get; set; }

        [Display(Name = "Name")]
        [Required]
        [StringLength(32)]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Rows")]
        public byte Rows { get; set; }

        [Required]
        [Display(Name = "Seats Per Row")]
        public byte SeatsPerRows { get; set; }

    }
}