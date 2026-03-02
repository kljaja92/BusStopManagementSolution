using Microsoft.AspNetCore.Mvc;

namespace BusStopManagement.UI.Controllers
{
    public class DepartureController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
