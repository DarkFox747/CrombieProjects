using CrombieProytecto_V0._2.Context;
using CrombieProytecto_V0._2.Models.Entidades;
using CrombieProytecto_V0._2.Service;
using Microsoft.EntityFrameworkCore;

namespace CrombieProytecto_V0._2.Seeds
{
    public static class UsuarioSeed
    {
        public static async Task Seed(ProyectContext context, IAuthService authService)
        {
            if (await context.Usuarios.AnyAsync()) return;

            var adminUser = await authService.RegisterUser("Admin", "admin", "admin@example.com", "Admin123!");
            adminUser.Roles = UserRole.Admin;

            var regularUser = await authService.RegisterUser("User", "user", "user@example.com", "User123!");

            await context.SaveChangesAsync();
        }
    }
}
