using Assignment1B.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment1B.Controllers
{
    public class ViewController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ViewController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            // get a list of flights to display to passangers on the main page
            var flights = _context.Bookings.OrderBy(c => c.Name.ToList());
            return View(flights);
        }
    }
}
