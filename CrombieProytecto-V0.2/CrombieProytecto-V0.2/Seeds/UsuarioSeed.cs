using CrombieProytecto_V0._2.Context;
using CrombieProytecto_V0._2.Models.Entidades;
using CrombieProytecto_V0._2.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CrombieProytecto_V0._2.Seeds
{
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
                PasswordHash = "JaMzig+xga1jXEFsOQr6x44WhlYoPp9JNDh9RtmZXcA=", //Admin123!
                Salt = "36xB6nGSv2SPw0YwjKevhQ==",
                Roles = UserRole.Admin,
                CreatedAt = DateTime.UtcNow
            };

            var regularUser = new Usuario
            {
                Id = 2,
                Nombre = "User",
                Username = "user",
                Email = "user@example.com",
                PasswordHash = "User123!",
                Salt = "random_salt",
                Roles = UserRole.Regular,
                CreatedAt = DateTime.UtcNow
            };

            builder.HasData(adminUser, regularUser);
        }
    }
}
