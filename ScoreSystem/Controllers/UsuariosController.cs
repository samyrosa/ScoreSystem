using Microsoft.AspNetCore.Mvc;

namespace ScoreSystem.Controllers
{
    public class UsuariosController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
    }
}
