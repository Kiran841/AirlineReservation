using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment1B.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public string CustomerId { get; set; }
        public String FlightName { get; set; }
        [Display(Name = "First Name")]
        public String FirstName { get; set; }
        [Display(Name = "Last Name")]
        public String LastName { get; set; }
        public String Phone { get; set; }
        public String CustomerAddress { get; set; }

        [Display(Name = "Postal Code")]
        public String PostalCode { get; set; }
        public double TotalPrice { get; set; }

        public List<OrderDetail> OrderDetails { get; set; }
    }
}
