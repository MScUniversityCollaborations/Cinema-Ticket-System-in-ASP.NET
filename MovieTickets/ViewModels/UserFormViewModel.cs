using MovieTickets.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MovieTickets.ViewModels
{
    public class UserFormViewModel
    {
        public List<string> Claims { get; set; }

        public IList<string> Roles { get; set; }

        public string Id { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} can have a max of {1} characters.")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} can have a max of {1} characters.")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100, ErrorMessage = "The {0} can have a max of {1} characters.")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Phone]
        [StringLength(32, ErrorMessage = "The {0} can have a max of {1} characters.")]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Required]
        [StringLength(32, ErrorMessage = "The {0} can have a max of {1} characters.")]
        [Display(Name = "Username")]
        public string UserName { get; set; }

        public string PageTitle
        {
            get
            {
                return Id != null ? "Edit User" : "New User";
            }
        }

        public string PageDesc
        {
            get
            {
                return Id != null ? "Update an existing user." : "Create a new user.";
            }
        }

        public string State
        {
            get
            {
                return Id != null ? "Update" : "Create";
            }
        }

        public UserFormViewModel()
        {
            Id = null;
            Claims = new List<string>();
            Roles = new List<string>();
        }

        public UserFormViewModel(ApplicationUser user)
        {
            Id = user.Id;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Email = user.Email;
            PhoneNumber = user.PhoneNumber;
            UserName = user.UserName;
        }
    }
}