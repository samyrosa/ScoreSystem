using System.ComponentModel.DataAnnotations;

namespace ScoreSystem.Entidades
{
    public class Usuario
    {
        [Key] public int CODIGO { get; set; }
        public string NOME { get; set; }
        public string CPF { get; set; }
        public string EMAIL { get; set; }
        public string SENHA { get; set; }
        public string TIPO { get; set; }
        public bool ATIVO { get; set; }
    }
}
