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
            if ((Cpf == null) && (Senha == null))
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

            if (usuario != null)
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
        [Authorize(Roles = "Administrador")]
        public IActionResult Lista(Usuarios dados)
        {

            return View(db.USUARIO.ToList());
        }

        [Authorize(Roles = "Administrador")]
        public IActionResult CadastroAdministrador()
        {
            UsuariosViewModel model = new UsuariosViewModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logar(string CPF, string SENHA, string ReturnUrl)
        {
            if ((CPF == null) && (SENHA == null))
            {
                TempData["Alert"] = "O campo CPF e Senha devem preenchidos!";
                return Redirect("/Usuarios/Login");
            }
            if (CPF == null)
            {
                TempData["Alert"] = "O campo CPF deve ser preenchido!";
                return Redirect("/Usuarios/Login");
            }
            if (SENHA == null)
            {
                TempData["Alert"] = "O campo Senha deve ser preenchido!";
                return Redirect("/Usuarios/Login");
            }
            CPF = CPF.Replace("-", "").Replace(".", "");
            Usuarios usuario = db.USUARIO.Where(a => a.CPF == CPF && a.SENHA == SENHA).FirstOrDefault();
            if (usuario != null)
            {
                List<Claim> claims = new List<Claim>();

                claims.Add(new Claim(ClaimTypes.Name, usuario.NOME));
                if (usuario.TIPO == "A")
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
                TempData["Erro"] = "O campo CPF ou Senha invalidos. Usuario nao encontrado!";
                return Redirect("/Usuarios/Login");
            }
        }

        public async Task<IActionResult> LogOff()
        {

            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Login");
        }

        [Authorize(Roles = "Administrador")]
        public IActionResult Visualizar(int id)
        {
            var usuario = db.USUARIO.Find(id);

            if (usuario != null)
            {
                return View(usuario);

            }
            else
            {
                return NotFound();
            }

        }
        [Authorize(Roles = "Administrador")]
        public IActionResult Editar(int id)
        {
            Usuarios item = db.USUARIO.Find(id);
            if (item != null)
            {
                return View(item);

            }
            else
            {
                return RedirectToAction("Lista");
            }


        }
        public ActionResult ExcluirPerfil(int id)
        {
            Usuarios item = db.USUARIO.Find(id);

            if (item != null)
            {
                db.USUARIO.Remove(item);
                db.SaveChanges();

            }

            return RedirectToAction("Cadastro", "Usuarios");
        }

        [Authorize(Roles = "Administrador")]
        public ActionResult Excluir(int id)
        {
            Usuarios item = db.USUARIO.Find(id);

            if (item != null)
            {
                db.USUARIO.Remove(item);
                TempData["Sucesso"] = "Usuario excluido com sucesso!";
                db.SaveChanges();

            }

            return RedirectToAction("Lista", "Usuarios");
        }


        [HttpPost]

        public IActionResult SalvarDados(Usuarios dados)
        {
           
            dados.CPF = dados.CPF.Replace("-", "").Replace(".", "");
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
            return RedirectToAction("Login", "Usuarios");
        }

        [Authorize(Roles = "Administrador")]
        public IActionResult SalvarDadosManual(Usuarios dados)
        {
            dados.CPF = dados.CPF.Replace("-", "").Replace(".", "");

            if (dados.CODIGO > 0)
            {
                var usuarioExistente = db.USUARIO.Find(dados.CODIGO);
                if (usuarioExistente != null)
                {
                    dados.SENHA = usuarioExistente.SENHA;

                    // Atualiza apenas os outros atributos
                    db.Entry(usuarioExistente).CurrentValues.SetValues(dados);
                    if (dados.ATIVO == false)
                    {
                        dados.ATIVO = true;
                    }
                    TempData["Sucesso"] = "Usuario editado com sucesso!";
                    db.SaveChanges();

                }
                else
                {
                    TempData["Erro"] = "Usuário não encontrado!";
                }
            }
            else
            {
                // Verificação dos atributos de usuário
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

                TempData["Sucesso"] = "Novo usuario cadastrado com sucesso!";
            }

            return RedirectToAction("Lista");
        }

    }
}
