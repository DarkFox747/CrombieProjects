using CrombieProytecto_V0._2.Context;
using CrombieProytecto_V0._2.Models.Entidades;
using CrombieProytecto_V0._2.Seeds;
using Microsoft.EntityFrameworkCore;

namespace CrombieProytecto_V0._2.Service
{
    public class DatabaseSeeder
    {
        private readonly ProyectContext _context;
        private readonly IAuthService _authService;

        public DatabaseSeeder(ProyectContext context, IAuthService authService)
        {
            _context = context;
            _authService = authService;
        }

      /*  public async Task SeedDatabase()
        {
            await UserSeeder.SeedUsersAsync(_context, _authService);
            await CategorySeeder.SeedCategoriesAsync(_context);
            await ProductSeeder.SeedProductsAsync(_context);
            await WishListSeeder.SeedWishListsAsync(_context);
        }*/
    }
}
