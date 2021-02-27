using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment1B.Models
{
    public class Booking
    {
        public int BookingId { get; set; }
        
        [Required]
        
        public String Name { get; set; }
        
        [Required]
        
        public String Description { get; set; }
        
        public String Photo { get; set; }

        [DisplayFormat(DataFormatString = "{0:c}")]
        

        public Double Price { get; set; }
        [Range(0.01, 999999)]

        public List<OrderDetail> OrderDetails { get; set; }
        public List<Trip> Trips { get; set; }

        [Display(Name = "Flight")]
        public int FlightId { get; set; }
        public Flight Flight { get; set; }
    }
}
