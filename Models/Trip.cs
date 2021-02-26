using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment1B.Models
{
    public class Trip
    {
        public int TripId { get; set; }
        public int SeatId { get; set; }
        public int BookingId { get; set; }
        public DateTime DateCreated { get; set; }
        public String CustomerId { get; set; }

        public Booking Booking { get; set; }
    }
}
