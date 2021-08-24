using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Movieproject.Models.ViewModels
{
    public class PageinationVM
    {
        public IEnumerable<Movie> Movies { get; set; }

        public int MoviesPerPage { get; set; }

        public int MoviesPerRow { get; set; }

        public int CurrentPage { get; set; }

        public int PageCount { get; set; }

        public static int TotalPages (int DBCount, int CountPerPage)
        {
            if (DBCount <= CountPerPage) return 1;
            //return Convert.ToInt32((double)(DBCount / CountPerPage));
            return Convert.ToInt32(Math.Ceiling((DBCount / (double)CountPerPage)));
        }

        public PageinationVM()
        {
            Movies = new List<Movie>();
        }

    }
}