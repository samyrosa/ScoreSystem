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
            return View(db.CUPOM.ToList());
        }

        public IActionResult Cadastro()
        {
            CuponsViewModel model = new CuponsViewModel();
            return View(model);
        }

        public IActionResult Visualizar(int codigo)
        {
            var cupom = db.CUPOM.Find(codigo);

            if (cupom != null)
            {
                return View(cupom);

            }
            else
            {
                return NotFound();
            }

        }

        public ActionResult Excluir(int codigo)
        {
            Cupom item = db.CUPOM.Find(codigo);
            if (item != null)
            {
                db.CUPOM.Remove(item);
                db.SaveChanges();
            }

            return RedirectToAction("Lista");
        }

        public IActionResult SalvarDados(Cupom dados)
        {
            db.CUPOM.Add(dados);
            db.SaveChanges();
            return RedirectToAction("Lista");
        }

    }
}
