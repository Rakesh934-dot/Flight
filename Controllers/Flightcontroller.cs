using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Flight.Models;
using FlightEntity = Flight.Models.Flight;

namespace Flight.Controllers
{
    public class FlightController : Controller
    {
        private readonly FlightContext context;

        public FlightController(FlightContext ctx)
        {
            context = ctx;
        }

        public IActionResult Index()
        {
            var flights = context.Flights
                .Include(f => f.FromCity)
                .Include(f => f.ToCity)
                .OrderBy(f => f.Date)
                .ThenBy(f => f.FlightNumber)
                .ToList();

            return View(flights);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var flight = context.Flights
                .Include(f => f.FromCity)
                .Include(f => f.ToCity)
                .FirstOrDefault(f => f.FlightId == id);

            if (flight == null)
            {
                return NotFound();
            }

            return View(flight);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Action = "Create";
            ViewBag.Cities = new SelectList(context.Cities.OrderBy(c => c.Name), "CityId", "Name");
            return View("Edit", new FlightEntity());
        }

        [HttpGet]
        public IActionResult Add()
        {
            return RedirectToAction(nameof(Create));
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

            if (flight == null)
            {
                return NotFound();
            }

            ViewBag.Cities = new SelectList(context.Cities.OrderBy(c => c.Name), "CityId", "Name");

            return View(flight);
        }

        // -------------------------
        // ADD/EDIT (POST)
        // -------------------------
        [HttpPost]
        public IActionResult Edit(FlightEntity flight)
        {
            if (ModelState.IsValid)
            {
                if (flight.FlightId == 0)
                    context.Flights.Add(flight);
                else
                    context.Flights.Update(flight);

                context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Action = (flight.FlightId == 0) ? "Create" : "Edit";
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

            if (flight == null)
            {
                return NotFound();
            }

            return View(flight);
        }

        // -------------------------
        // DELETE (POST)
        // -------------------------
        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var flight = context.Flights.Find(id);
            if (flight != null)
            {
                context.Flights.Remove(flight);
                context.SaveChanges();
            }

            TempData["Message"] = "Flight deleted successfully.";
            return RedirectToAction(nameof(Index));
        }
    }
}
