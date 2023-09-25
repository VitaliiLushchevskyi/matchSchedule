using Microsoft.AspNetCore.Mvc;

namespace matchSchedule.Controllers
{
    public class CoachController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
