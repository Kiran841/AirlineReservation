using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment1B.Models
{
    public class Flight
    {
        public int FlightId { get; set; }

        [Required]

        public String Name { get; set; }

        public List<Booking> Bookings { get; set; }
    }
}
