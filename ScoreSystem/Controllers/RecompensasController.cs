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

        public IActionResult Visualizar(int id)
        {
            var recompensa = db.RECOMPENSA.Find(id);

            if (recompensa != null)
            {
                return View(recompensa);
            }
            else
            {
                return NotFound();
            }

        }
        public ActionResult Excluir(int id)
        {
            Recompensas item = db.RECOMPENSA.Find(id);
            if (item != null)
            {
                db.RECOMPENSA.Remove(item);
                db.SaveChanges();
            }

            return RedirectToAction("Lista");
        }

        
        public IActionResult SalvarDados(Recompensas dados )
        {
            db.RECOMPENSA.Add(dados);
            db.SaveChanges();
            return RedirectToAction("Lista");
        }
    }
}
