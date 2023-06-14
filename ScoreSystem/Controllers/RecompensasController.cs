using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScoreSystem.Models;
using ScoreSystem.Entidades;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace ScoreSystem.Controllers
{
    [Authorize(Roles = "Administrador")]
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
        public IActionResult Editar(int id)
        {
            Recompensas item = db.RECOMPENSA.Find(id);
            if (item != null)
            {
                return View(item);

            }
            else
            {
                return RedirectToAction("Lista");
            }


        }

        public ActionResult Excluir(int id)
        {
            Recompensas item = db.RECOMPENSA.Find(id);
            if (item != null)
            {
                db.RECOMPENSA.Remove(item);
                db.SaveChanges();

                TempData["Sucesso"] = "Recompensa excluido com sucesso!";

            }

            return RedirectToAction("Lista");
        }
        public IActionResult SalvarDados(Recompensas dados)
        {
            if (dados.CODIGO > 0)
            {
                db.RECOMPENSA.Update(dados);
                db.SaveChanges();
                TempData["Sucesso"] = "Recompensa editada com sucesso!";


            }
            else
            {
                if (dados.DT_VENCIMENTO < DateTime.Now)
                {
                    dados.DT_VENCIMENTO = DateTime.Now;
                }
                db.RECOMPENSA.Add(dados);
                db.SaveChanges();
            }

            return RedirectToAction("Lista");
        }
    }
    
}
