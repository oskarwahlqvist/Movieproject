using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Movieproject.Models.ViewModels
{
    public class OrderHistoryListVM
    {
        public string MovieTitle { get; set;}

        public int NoofCopies { get; set; }

        public decimal Price { get; set; }

    }
}