using Microsoft.AspNetCore.Mvc;

namespace ScoreSystem.Controllers
{
    public class RecompensasController : Controller
    {
        public IActionResult Lista()
        {
            return View();
        }
        public IActionResult Cadastro()
        {
            return View();
        }
    }
}
