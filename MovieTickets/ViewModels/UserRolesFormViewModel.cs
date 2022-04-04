using Microsoft.AspNet.Identity.EntityFramework;
using MovieTickets.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace MovieTickets.ViewModels
{
    public class UserRolesFormViewModel
    {
        public UserRolesFormViewModel(ApplicationUser user)
        {
            User = user;
        }

        public string Id { get; set; }

        public IdentityUser User { get; set; }

        public SelectList Roles { get; set; }
    }
}