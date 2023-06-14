using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScoreSystem.Entidades;
using ScoreSystem.Models;

namespace ScoreSystem.Controllers
{
    [Authorize(Roles ="Administrador")]
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
                return View(item);
            }
            else
            {
                return RedirectToAction("Lista");
            }

        }

        public IActionResult SalvarDados(Cupom dados)
        {
            
           

            if(dados.CODIGO > 0)
            {
                db.CUPOM.Update(dados);
                db.SaveChanges();
                TempData["Sucesso"] = "Cupom editado com sucesso!";

            }
            else
            {
                if (dados.DT_VENCIMENTO < DateTime.Now)
                {
                    dados.DT_VENCIMENTO = DateTime.Now;
                }
                db.CUPOM.Add(dados);
                db.SaveChanges();
            }
           
            return RedirectToAction("Lista");
        }

    }
}
