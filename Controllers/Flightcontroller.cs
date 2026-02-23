using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Flight.Models;

namespace Flight.Controllers
{
    public class FlightController : Controller
    {
        private FlightContext context;

        public FlightController(FlightContext ctx)
        {
            context = ctx;
        }

        // -------------------------
        // ADD (GET)
        // -------------------------
        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Action = "Add";
            ViewBag.Cities = new SelectList(context.Cities.OrderBy(c => c.Name), "CityId", "Name");
            return View("Edit", new Flight.Models.Flight());
        }

        // -------------------------
        // EDIT (GET)
        // -------------------------
        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Action = "Edit";
            var flight = context.Flights
                .Include(f => f.FromCity)
                .Include(f => f.ToCity)
                .FirstOrDefault(f => f.FlightId == id);
            ViewBag.Cities = new SelectList(context.Cities.OrderBy(c => c.Name), "CityId", "Name", flight?.FromCityId);

            return View(flight);
        }

        // -------------------------
        // ADD/EDIT (POST)
        // -------------------------
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Flight.Models.Flight flight)
        {
            if (ModelState.IsValid)
            {
                if (flight.FlightId == 0)
                    context.Flights.Add(flight);
                else
                    context.Flights.Update(flight);

                context.SaveChanges();
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Action = (flight.FlightId == 0) ? "Add" : "Edit";
            ViewBag.Cities = new SelectList(context.Cities.OrderBy(c => c.Name), "CityId", "Name", flight?.FromCityId);
            return View(flight);
        }

        // -------------------------
        // DELETE (GET)
        // -------------------------
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var flight = context.Flights
                .Include(f => f.FromCity)
                .Include(f => f.ToCity)
                .FirstOrDefault(f => f.FlightId == id);

            return View(flight);
        }

        // -------------------------
        // DELETE (POST)
        // -------------------------
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var flight = context.Flights.Find(id);
            if (flight != null)
            {
                context.Flights.Remove(flight);
                context.SaveChanges();
            }

            TempData["Message"] = "Flight deleted successfully.";
            return RedirectToAction("Index", "Home");
        }
    }
}
