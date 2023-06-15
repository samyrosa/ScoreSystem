using ScoreSystem.Entidades;
using System.Security.Claims;

namespace ScoreSystem.Models
{
    public class HomeIndexViewModel
    {
        public HomeIndexViewModel()
        {
            todosCupons = new List<Cupom>();
            todasRecompensas = new List<Recompensas>();
        }
        public Usuarios usuarioLogado { get; set; }
        public List<Cupom> todosCupons { get; set; }
       
        public List<Recompensas> todasRecompensas { get; set; }
    }
}


