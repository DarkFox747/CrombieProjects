using CrombieProytecto_V0._2.Context;
using CrombieProytecto_V0._2.Models.Entidades;
using CrombieProytecto_V0._2.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CrombieProytecto_V0._2.Seeds
{
    //Define la configuración para la entidad Usuario

    public class UsuarioSeed : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            var adminUser = new Usuario
            {
                Id = 1,
                Nombre = "Admin",
                Username = "admin",
                Email = "admin@example.com",
                PasswordHash = "Ru5l7/9VNQ1CgSzIZg5na0WWm+sZewJpWPBkBf+1RvA=", //Admin123!
                Salt = "AuDooNbYaGBUTN7lnuViSw==",
                Roles = UserRole.Admin,
                CreatedAt = DateTime.UtcNow
            };

            var regularUser = new Usuario
            {
                Id = 2,
                Nombre = "User",
                Username = "user",
                Email = "user@example.com",
                PasswordHash = "Qmd4q0uhgOtcKfXPg/FIfcLbz855lv98RXbtH7GUbTA=", //User123!
                Salt = "R4r0g+D2FKYGM5M8wv9P8w==",
                Roles = UserRole.Regular,
                CreatedAt = DateTime.UtcNow
            };

            builder.HasData(adminUser, regularUser);
        }
    }
}
