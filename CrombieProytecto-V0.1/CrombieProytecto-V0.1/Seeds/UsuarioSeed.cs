using CrombieProytecto_V0._1.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CrombieProytecto_V0._1.Seeds
{
    public class UsuarioSeed : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            /*builder.HasData(
                new Usuario
                {
                    Id = 1,
                    Nombre = "Admin",
                    Email = "asd@gmail.com",
                    Password = "123"
                } );*/
        }
    }
}
