using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization; 
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScoreSystem.Entidades;
using ScoreSystem.Models;
using System.Globalization;
using System.Security.Claims;

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
        public async Task<IActionResult> Logar(string CPF, string SENHA,string ReturnUrl)
        {
            CPF = CPF.Replace("-", "").Replace(".", "");
            Usuarios usuario = db.USUARIO.Where(a => a.CPF == CPF && a.SENHA == SENHA).FirstOrDefault();
            if(usuario != null)
            {
                List<Claim> claims = new List<Claim>();

                claims.Add(new Claim(ClaimTypes.Name, usuario.NOME));
                if(usuario.TIPO == "A")
                {
                    claims.Add(new Claim(ClaimTypes.Role, "Administrador"));
                }
                
                claims.Add(new Claim(ClaimTypes.Sid, usuario.CODIGO.ToString()));

                var claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true,
                };

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);

                if (string.IsNullOrEmpty(ReturnUrl))
                {
                    ReturnUrl = "/Home/Index";
                }

                return Redirect(ReturnUrl);
            }
            else
            {
                ModelState.AddModelError("Erro", "Email ou senha invalidos");
                return Redirect("/Usuarios/Login");
            }
        }

        public async Task<IActionResult> LogOff()
        {

            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Login");
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
                string TipoPadrao = "C";
                dados.TIPO = TipoPadrao;
            }

            db.USUARIO.Add(dados);
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
    }
}
