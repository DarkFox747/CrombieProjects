using CrombieProytecto_V0._1.Models;
using CrombieProytecto_V0._1.Seeds;
using Microsoft.EntityFrameworkCore;


namespace CrombieProytecto_V0._1.Context
{
    public class ProyectContext : DbContext
    {
        public DbSet<Usuario> Usuario { get; set; }

        public ProyectContext(DbContextOptions<ProyectContext> options) : base(options) { }



        //seeds
        protected override void OnModelCreating(ModelBuilder builder)
        {
           builder.ApplyConfiguration(new UsuarioSeed());
        }
    }
}
