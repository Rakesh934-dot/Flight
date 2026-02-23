using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Flight.Models;

namespace Flight.Controllers
{
    public class HomeController : Controller
    {
        private FlightContext context;

        public HomeController(FlightContext ctx)
        {
            context = ctx;
        }

        public IActionResult Index()
        {
            // prevent caching so the browser always requests fresh data after redirects
            Response.Headers["Cache-Control"] = "no-cache, no-store, must-revalidate";
            Response.Headers["Pragma"] = "no-cache";
            Response.Headers["Expires"] = "0";

            var flights = context.Flights
                .Include(f => f.FromCity)
                .Include(f => f.ToCity)
                .OrderBy(f => f.Date)
                .ThenBy(f => f.FlightNumber)
                .ToList();

            return View(flights);
        }
    }
}
