using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScoreSystem.Models;
using ScoreSystem.Entidades;

namespace ScoreSystem.Controllers
{
    public class RecompensasController : Controller
    {
        private Contexto db;
        public RecompensasController(Contexto contexto)
        {

            db = contexto;
        }

        public IActionResult Lista()
        {
            return View(db.RECOMPENSA.ToList());
        }
        public IActionResult Cadastro()
        {
            RecompensasViewModel model = new RecompensasViewModel();
            return View(model);
        }

        public IActionResult SalvarDados(Recompensas dados )
        {
            db.RECOMPENSA.Add(dados);
            db.SaveChanges();
            return RedirectToAction("Lista");
        }
    }
}
