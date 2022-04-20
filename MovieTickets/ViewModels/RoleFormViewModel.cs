using Microsoft.AspNet.Identity.EntityFramework;
using MovieTickets.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace MovieTickets.ViewModels
{
    public class RoleFormViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string PageTitle
        {
            get
            {
                return Id != null ? "Edit Role" : "New Role";
            }
        }

        public string PageDesc
        {
            get
            {
                return Id != null ? "Update an existing role." : "Create a new role.";
            }
        }

        public string State
        {
            get
            {
                return Id != null ? "Update" : "Create";
            }
        }

        public RoleFormViewModel()
        {
            Id = null;
        }

    }
}