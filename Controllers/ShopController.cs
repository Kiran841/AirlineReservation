using Assignment1B.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assignment1B.Models;
using Microsoft.AspNetCore.Http;

namespace Assignment1B.Controllers
{
    public class ShopController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ShopController(ApplicationDbContext context)
        {
            _context = context;
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
    }
}
