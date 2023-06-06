namespace ScoreSystem.Models
{
    public class RecompensasViewModel
    {
        public int Codigo { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }

        public int Pontos { get; set; }
        public DateTime DataVencimento { get; set; }

    }
}
