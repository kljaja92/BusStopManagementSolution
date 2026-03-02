using Microsoft.AspNetCore.Mvc;

namespace BusStopManagement.UI.Controllers
{
    public class BusStopController : Controller
    {
        [Route("/")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
