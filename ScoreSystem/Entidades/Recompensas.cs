using System.ComponentModel.DataAnnotations;

namespace ScoreSystem.Entidades
{
    public class Recompensas
    {
     [Key]public int CODIGO { get; set; }
        public string TITULO { get; set; }
        public string DESCRICAO { get; set; }
        public int PONTOS { get; set; }
        public DateTime DT_VENCIMENTO { get; set; }
    }
}
