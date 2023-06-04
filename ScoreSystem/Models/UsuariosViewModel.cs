namespace ScoreSystem.Models
{
    public class UsuariosViewModel
    {
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string Tipo { get; set; }
        public bool Ativo { get; set; }

        //public DateTime DataHoraCadastro { get; set; }
    }
}
