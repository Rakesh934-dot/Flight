using Microsoft.AspNetCore.Mvc;

namespace Flight.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction("Index", "Flight");
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
