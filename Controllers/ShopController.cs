using Assignment1B.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assignment1B.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

//read the stripe api keys from appssettings.json
using Stripe;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using Stripe.Checkout;

namespace Assignment1B.Controllers
{
    public class ShopController : Controller
    {
        private readonly ApplicationDbContext _context;
        IConfiguration _iconfiguation;

        public ShopController(ApplicationDbContext context, IConfiguration iconfiguation)
        {
            _context = context;
            _iconfiguation = iconfiguation;
        }

        public IActionResult Index()
        {
            var flights = _context.Flights.OrderBy(f => f.Name).ToList();
            return View(flights);
        }

        public IActionResult Browse(int id)
        {
            //Query Bookings for the selected flight
            var flights = _context.Bookings.Where(b => b.FlightId == id).OrderBy(b => b.Name).ToList();
            //get the name of the selected flight

            ViewBag.category = _context.Flights.Find(id).Name.ToString();
            return View(flights);
        }

        public IActionResult AddToCart (int BookingId, int Quantity)
        {
            //query the db for flight price
            var price = _context.Bookings.Find(BookingId).Price;

            //get current date and time
            var currentDateTime = DateTime.Now;

            var CustomerId = GetCustomerId();

            //create and save a new flight trip
            var trip = new Trip
            {
                BookingId = BookingId,
                Quantity = Quantity,
                Price = price,
                DateCreated = currentDateTime,
                CustomerId = CustomerId
            };
            _context.Trips.Add(trip);
            _context.SaveChanges();

            return RedirectToAction("Trip");
        }

        private string GetCustomerId()
        {
            //check the session for an existing CustomerId
            if (HttpContext.Session.GetString("CustomerId") == null)
            {
                //if customerid does not already exist
                var CustomerId = "";

                //if customer is logged in
                if (User.Identity.IsAuthenticated)
                {
                    CustomerId = User.Identity.Name;
                }
                //if customer is anonymous
                else
                {
                    CustomerId = Guid.NewGuid().ToString();
                }
                //now to store the customer id in session variable
                HttpContext.Session.SetString("CustomerId", CustomerId);
            }
            return HttpContext.Session.GetString("CustomerId");
        }

        public IActionResult Trip()
        {
            var CustomerId = "";

            if (HttpContext.Session.GetString("CustomerId") == null)
            {
                CustomerId = GetCustomerId();
            }
            else
            {
                CustomerId = HttpContext.Session.GetString("CustomerId");
            }

            var trips = _context.Trips.Include(c => c.Booking).Where(c => c.CustomerId == CustomerId).ToList();

            return View(trips);
        }


        //Shop/Checkout
        [Authorize]
        public IActionResult CheckOut()
        {

            return View();
        }


        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Checkout([Bind("OrderName, CustomerName, Phone, CustomerAddress, TotalPrice")] Models.Order order)
        {
            order.OrderDate = DateTime.Now;
            order.CustomerId = User.Identity.Name;

            order.TotalPrice = (from c in _context.Trips
                                where c.CustomerId == HttpContext.Session.GetString("CustomerId")
                                select c.Quantity * c.Price).Sum();

            HttpContext.Session.SetObject("Order", order);

            return RedirectToAction("Payment");
        }

        public IActionResult Payment()
        {
            var order = HttpContext.Session.GetObject<Models.Order>("Order");
            ViewBag.Total = order.TotalPrice;

            ViewBag.PublishableKey = _iconfiguation.GetSection("Stripe")["PublishableKey"];


            return View();
        }

        public IActionResult RemoveFromCart(int id)
        {
            var flight = _context.Trips.Find(id);
            if (flight != null)
            {
                _context.Trips.Remove(flight);
                _context.SaveChanges();
            }
            return RedirectToAction("Trip");
        }

        [Authorize]
        [HttpPost]


        public IActionResult PaymentProcess()
        {
            var order = HttpContext.Session.GetObject<Models.Order>("Order");
            
            StripeConfiguration.ApiKey = _iconfiguation.GetSection("Stripe")["SecretKey"];

            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string>
                {
                  "card",
                },
                LineItems = new List<SessionLineItemOptions>
                {
                  new SessionLineItemOptions
                  {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                      UnitAmount = (long?)(order.TotalPrice * 100),
                      Currency = "card",
                      ProductData = new SessionLineItemPriceDataProductDataOptions
                      {
                        Name = "Airline Booking Payment",
                      },
                    },
                    Quantity = 1,
                  },
                },
                Mode = "payment",
                SuccessUrl = "https://" + Request.Host + "/Shop/SaveOrder",
                CancelUrl = "https://" + Request.Host + "/Shop/Trip",
            };
            var service = new SessionService();
            Session session = service.Create(options);
            return Json(new { id = session.Id });
        }

        public IActionResult SaveOrder()
        {
            var order = HttpContext.Session.GetObject<Models.Order>("Order");
            _context.Orders.Add(order);
            _context.SaveChanges();

            var flights = _context.Trips.Where(c => c.CustomerId == HttpContext.Session.GetString("CustomerId"));

            foreach (var item in flights)
            {
                var orderDetail = new OrderDetail
                {
                    BookingId = item.BookingId,
                    Quantity = item.Quantity,
                    Price = item.Price,
                    OrderId = order.OrderId
                };
            }

            foreach (var item in flights)
            {
                _context.Trips.Remove(item);
            }
            HttpContext.Session.SetInt32("ItemCount", 0);

            return RedirectToAction("Details", "Orders", new { @id = order.OrderId });
        }

    }
}
