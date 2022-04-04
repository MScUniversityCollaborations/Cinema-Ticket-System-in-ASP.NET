using System;
using System.ComponentModel.DataAnnotations;

namespace MovieTickets.Models
{
    public class Roles
    {
        [Required]
        [StringLength(128)]
        public string Id { get; set; }

        [Display(Name = "Name")]
        [Required]
        [StringLength(256)]
        public string Title { get; set; }
    }
}