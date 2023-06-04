using Microsoft.AspNetCore.Mvc;

namespace ScoreSystem.Controllers
{
    public class UsuariosController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Cadastro()
        {
            UsuariosViewModel model = new UsuariosViewModel();
            return View(model);
        }

        public IActionResult Lista()
        {
            return View();
        }

        public IActionResult CadastroAdministrador()
        {
            return View();
        }

        public IActionResult SalvarDados(Usuarios dados)
        {

            dados.DT_HR_CADASTRO = DateTime.Now; //Para a hora ser salva no banco corretamente 
            db.USUARIO.Add(dados);
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
    }
}
