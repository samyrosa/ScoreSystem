using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ScoreSystem.Entidades
{
    public class Usuarios
    {

        [Key] public int CODIGO { get; set; }
        public string NOME { get; set; }
        public string CPF { get; set; }
        public string EMAIL { get; set; }
        public string SENHA { get; set; }
        public string TIPO { get; set; }

        public bool ATIVO { get; set; }
        public DateTime DT_HR_CADASTRO { get; set; }

    }

}