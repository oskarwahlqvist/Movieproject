using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Movieproject.Models.ViewModels
{
    public class HomePageVM
    {
        public int MovieId { get; set; }

        public string Title { get; set; }

        public int ReleaseYear { get; set; }

        public int TimesSold { get; set; }
    }
}