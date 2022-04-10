using Microsoft.AspNet.Identity.EntityFramework;
using MovieTickets.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace MovieTickets.ViewModels
{
    public class UserRolesFormViewModel
    {
        public string RoleId { get; set; }

        public string RoleName { get; set; }

        public bool IsSelected { get; set; }
   
    }
}