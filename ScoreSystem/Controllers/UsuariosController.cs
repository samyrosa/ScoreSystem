using Microsoft.AspNetCore.Authorization; 
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScoreSystem.Entidades;
using ScoreSystem.Models;
using System.Globalization;

namespace ScoreSystem.Controllers
{
    //[Authorize(AuthenticationSchemes = "CookieAuthentication")]
    public class UsuariosController : Controller
    {
        private Contexto db;
        public UsuariosController(Contexto contexto)
        {

            db = contexto;
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]

        public IActionResult Login(string Cpf, string Senha)
        {
            if((Cpf==null) && (Senha == null))
            {
                TempData["Alert"] = "O campo CPF e Senha devem preenchidos!";
                return View();
            }
            if (Cpf == null)
            {
                TempData["Alert"] = "O campo CPF deve ser preenchido!";
                return View();
            }
            if (Senha == null)
            {
                TempData["Alert"] = "O campo Senha deve ser preenchido!";
                return View();
            }
            var usuario = db.USUARIO.FirstOrDefault(u => u.CPF == Cpf && u.SENHA == Senha);

            if(usuario != null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["Erro"] = "O campo CPF ou Senha invalidos. Usuario nao encontrado!";
                return View();

            }

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

        [HttpPost]

        public IActionResult SalvarDados(Usuarios dados)
        {
            //Verificação dos atributos de Usuario
            if (dados.ATIVO == false)
            {
                dados.ATIVO = true;
            }
            if (dados.TIPO == null)
            {
                string TipoPadrão = "C";
                dados.TIPO = TipoPadrão;
            }

            db.USUARIO.Add(dados);
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
    }
}
