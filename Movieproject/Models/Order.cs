using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Movieproject.Models
{
    public class Order
    {
        public int Id { get; set; }
        
        [Required, DataType(DataType.DateTime)]
        public DateTime OrderDate { get; set; }
        
        public int CustomerId { get; set; } //Foregin key
        
        public Customer Customer { get; set; }

        [Required, DataType(DataType.Currency)]
        public decimal TotalPrice { get; set; }
    }
}