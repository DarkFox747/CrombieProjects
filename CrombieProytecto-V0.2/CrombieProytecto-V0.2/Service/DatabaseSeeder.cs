using CrombieProytecto_V0._2.Context;
using CrombieProytecto_V0._2.Models.Entidades;
using CrombieProytecto_V0._2.Seeds;
using Microsoft.EntityFrameworkCore;

namespace CrombieProytecto_V0._2.Service
{
    public  class DatabaseSeeder
    {
        public static async Task SeedDatabase(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ProyectContext>();
            var authService = scope.ServiceProvider.GetRequiredService<IAuthService>();

            await UserSeeder.SeedUsersAsync(context, authService);
            await CategorySeeder.SeedCategoriesAsync(context);
            await ProductSeeder.SeedProductsAsync(context);
            await WishListSeeder.SeedWishListsAsync(context);
        }
    }
}
