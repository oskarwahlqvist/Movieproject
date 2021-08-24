using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Movieproject.Models
{
    public class OrderRow
    {
        public int Id { get; set; }

        // 2 Foreign Keys
        public int OrderId { get; set; }
        public Order Order { get; set; }

        public int MovieId { get; set; }
        public Movie Movie { get; set; }

        [Required, DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [Required]
        public int NoofCopies { get; set; }
    }
}