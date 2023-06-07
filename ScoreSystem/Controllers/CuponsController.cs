using Microsoft.AspNetCore.Mvc;
using ScoreSystem.Entidades;
using ScoreSystem.Models;

namespace ScoreSystem.Controllers
{
    public class CuponsController : Controller
    {
        private Contexto db;
        public CuponsController(Contexto contexto)
        {

            db = contexto;
        }
        public IActionResult Lista()
        {
            return View();
        }

        public IActionResult Cadastro()
        {
            CuponsViewModel model = new CuponsViewModel();
            return View(model);
        }

        public IActionResult Visualizar()
        {
            return View();
        }

        public IActionResult SalvarDados(Cupom dados)
        {
            db.CUPOM.Add(dados);
            db.SaveChanges();
            return RedirectToAction("Lista");
        }

    }
}
