using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment1B.Models
{
    public class OrderDetail
    {
        public int OrderDetailId { get; set; }
        public int BookingId { get; set; }
        public int OrderId { get; set; }
        public double Quantity { get; set; }
        public double Price { get; set; }

        public Order Order { get; set; }

        public Booking Booking { get; set; }

    }
}
