using CrombieProytecto_V0._2.Context;
using CrombieProytecto_V0._2.Models.Entidades;
using CrombieProytecto_V0._2.Service;
using Microsoft.EntityFrameworkCore;

namespace CrombieProytecto_V0._2.Seeds
{
    public static class UserSeeder
    {
        public static async Task SeedUsersAsync(ProyectContext context, IAuthService authService)
        {
            if (await context.Usuarios.AnyAsync())
                return;

            var adminUser = await authService.RegisterUser(
                nombre: "Admin",
                username: "admin",
                email: "admin@example.com",
                password: "Admin123!"
            );

            adminUser.Roles = UserRole.Admin;
            await context.SaveChangesAsync();

            var regularUser = await authService.RegisterUser(
                nombre: "User",
                username: "user",
                email: "user@example.com",
                password: "User123!"
            );

            await context.SaveChangesAsync();
        }
    }
}

