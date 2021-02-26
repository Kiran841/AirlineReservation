using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment1B.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public String FlightName { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Phone { get; set; }
        public String CustomerAddress { get; set; }
        public Decimal TotalPrice { get; set; }

        public List<OrderDetail> OrderDetails { get; set; }
    }
}
