using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieTickets.Models
{
    public class Movies
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Director { get; set; }
        public string Cast { get; set; }
        public int DurationMin  { get; set; }
        public string Description  { get; set; }
        public string ImagePoster  { get; set; }
    }
}