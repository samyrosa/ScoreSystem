using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace ScoreSystem.Entidades
{
    public class Cupom
    {
        [Key] public int CODIGO { get; set; }
        public string TITULO { get; set; }
        public string DESCRICAO { get; set; }
        public int PONTO { get; set; }
        public DateTime DT_VENCIMENTO { get; set; }
        public bool ATIVO { get; set; }
        
    }
}
