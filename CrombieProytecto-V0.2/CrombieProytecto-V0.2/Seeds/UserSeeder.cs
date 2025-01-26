using CrombieProytecto_V0._2.Context;
using CrombieProytecto_V0._2.Models.Entidades;
using CrombieProytecto_V0._2.Service;
using Microsoft.EntityFrameworkCore;

namespace CrombieProytecto_V0._2.Seeds
{
    public static class UserSeeder
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            var adminUser = new Usuario
            {
                Id = 1,
                Nombre = "Admin",
                Username = "admin",
                Email = "admin@example.com",
                PasswordHash = "Admin123!",
                Salt = "random_salt",
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

            modelBuilder.Entity<Usuario>().HasData(adminUser, regularUser);
        }
    }
}

