using Microsoft.AspNetCore.Mvc;

namespace ScoreSystem.Controllers
{
    public class CuponsController : Controller
    {
        public IActionResult Lista()
        {
            return View();
        }
    }
}
