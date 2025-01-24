using CrombieProytecto_V0._2.Context;
using CrombieProytecto_V0._2.Models.Entidades;
using Microsoft.EntityFrameworkCore;

namespace CrombieProytecto_V0._2.Seeds
{
    public static class WishListSeeder
    {
        public static async Task SeedWishListsAsync(ProyectContext context)
        {
            if (await context.WishList.AnyAsync())
                return;

            var regularUser = await context.Usuarios.FirstOrDefaultAsync(u => u.Username == "user");
            if (regularUser == null)
                return;

            var products = await context.Productos.ToListAsync();

            var wishLists = new List<WishList>
            {
                new WishList
                {
                    Nombre = "Mi Setup Gaming",
                    IdUsuario = regularUser.Id,
                    CreatedAt = DateTime.UtcNow,
                    Productos = new List<WishListProductos>
                    {
                        new WishListProductos
                        {
                            IdProducto = products[0].Id, // Laptop Gaming
                            CreatedAt = DateTime.UtcNow
                        },
                        new WishListProductos
                        {
                            IdProducto = products[3].Id, // Monitor
                            CreatedAt = DateTime.UtcNow
                        },
                        new WishListProductos
                        {
                            IdProducto = products[4].Id, // Teclado
                            CreatedAt = DateTime.UtcNow
                        }
                    }
                },
                new WishList
                {
                    Nombre = "Tecnología Móvil",
                    IdUsuario = regularUser.Id,
                    CreatedAt = DateTime.UtcNow,
                    Productos = new List<WishListProductos>
                    {
                        new WishListProductos
                        {
                            IdProducto = products[1].Id, // Smartphone
                            CreatedAt = DateTime.UtcNow
                        },
                        new WishListProductos
                        {
                            IdProducto = products[2].Id, // Auriculares
                            CreatedAt = DateTime.UtcNow
                        }
                    }
                }
            };

            await context.WishList.AddRangeAsync(wishLists);
            await context.SaveChangesAsync();
        }
    }
}

