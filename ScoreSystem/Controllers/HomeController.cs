using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScoreSystem.Entidades;
using ScoreSystem.Models;
using System.Diagnostics;
using System.Security.Claims;

namespace ScoreSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private Contexto db;

        public HomeController(ILogger<HomeController> logger, Contexto _db)
        {
            _logger = logger;
            db = _db;
        }
        [Authorize]
        public IActionResult Index()
        {
           

            HomeIndexViewModel model = new HomeIndexViewModel();
            model.usuarioLogado = db.USUARIO.Find( int.Parse(User.FindFirstValue(ClaimTypes.Sid) ) );
            model.todosCupons = db.CUPOM.ToList();
            model.todasRecompensas = db.RECOMPENSA.ToList();


            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult AcessoNegado()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}