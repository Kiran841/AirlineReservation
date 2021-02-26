using Assignment1B.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment1B.Controllers
{
    public class AirlineController : Controller
    {
        public IActionResult Index(string booking)
        {
            // used fake Flight class/model to display 10 flights
            // created an object to hold a list of categories
            var bookings = new List<Flight>();
            for (var i = 1; i < 10; i++)
            {
                bookings.Add(new Flight { FlightId = i, Name = "Flight" + i.ToString() });
            }
            // modified the return value so that now it can accept a list of categories to pass to the view
            return View(bookings);
        }

        public IActionResult Reservation()
        {
            return View();
        }

        // Airline/AddFlight
        public IActionResult AddFlight()
        {

            // load a form to capture an object from the user
            return View();
        }
    }
}
