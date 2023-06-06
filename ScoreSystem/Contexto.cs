using Microsoft.EntityFrameworkCore;
using ScoreSystem.Entidades;

namespace ScoreSystem
{
    public class Contexto : DbContext
    {

        public Contexto(DbContextOptions<Contexto> opt) : base(opt)
        {
        }

        public DbSet<Usuarios> USUARIO { get; set; }
        public DbSet<Recompensas> RECOMPENSA{ get; set; }



    }
}
