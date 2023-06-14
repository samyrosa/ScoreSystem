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

        public IActionResult Visualizar(int id)
        {
            var cupom = db.CUPOM.Find(id);

            if (cupom != null)
            {
                return View(cupom);

            }
            else
            {
                return NotFound();
            }

        }

        public ActionResult Excluir(int id)
        {
            Cupom item = db.CUPOM.Find(id);
            if (item != null)
            {
                db.CUPOM.Remove(item);
                db.SaveChanges();

                TempData["Sucesso"] = "Cupom excluido com sucesso";

            }

            return RedirectToAction("Lista");
        }
        public IActionResult Editar(int id)
        {
            Cupom item = db.CUPOM.Find(id);
            if (item != null)
            {
                db.CUPOM.Update(item);
                db.SaveChanges();
                return View();
            }
            else
            {
                return RedirectToAction("Lista");
            }


        }

        public IActionResult SalvarDados(Cupom dados)
        {
            db.CUPOM.Add(dados);
            db.SaveChanges();
            return RedirectToAction("Lista");
        }

    }
}
