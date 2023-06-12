using ScoreSystem.Entidades;
using static Azure.Core.HttpHeader;

namespace ScoreSystem.Models
{
    public class IndexViewModel
    {
        public List<Cupom> CUPOM { get; set; }
        public List<Recompensas> RECOMPENSA{ get; set; }
        public Usuarios USUARIO { get; set; }

    }
}