using CrombieProytecto_V0._2.Context;
using CrombieProytecto_V0._2.Models.Entidades;
using CrombieProytecto_V0._2.Service;
using Microsoft.EntityFrameworkCore;

namespace CrombieProytecto_V0._2.Seeds
{
    public static class DatabaseSeeder
    {
        public static async Task SeedDatabase(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ProyectContext>();
            var authService = scope.ServiceProvider.GetRequiredService<IAuthService>();

            // Verificar si ya hay datos
            if (await context.Usuarios.AnyAsync())
                return;

            // Crear usuarios de prueba
            var adminUser = await authService.RegisterUser(
                nombre: "Admin",
                username: "admin",
                email: "admin@example.com",
                password: "Admin123!"
            );

            // Asignar rol de administrador
            adminUser.Roles = UserRole.Admin;
            await context.SaveChangesAsync();

            var regularUser = await authService.RegisterUser(
                nombre: "User",
                username: "user",
                email: "user@example.com",
                password: "User123!"
            );

            // Crear productos de ejemplo
            var products = new List<Producto>
        {
            new Producto
            {
                Nombre = "Laptop Gaming Pro",
                Descripcion = "Laptop gaming de alta gama con RTX 3080",
                Precio = 1499.99m,
                Stock = 10,
                CreatedAt = DateTime.UtcNow
            },

            new Producto
            {
                Nombre = "Smartphone Ultra",
                Descripcion = "Smartphone de última generación con cámara 108MP",
                Precio = 899.99m,
                Stock = 15,
                CreatedAt = DateTime.UtcNow
            },

            new Producto
            {
                Nombre = "Auriculares Wireless",
                Descripcion = "Auriculares bluetooth con cancelación de ruido",
                Precio = 199.99m,
                Stock = 30,
                CreatedAt = DateTime.UtcNow
            },

            new Producto
            {
                Nombre = "Monitor 4K",
                Descripcion = "Monitor gaming 4K 144Hz",
                Precio = 499.99m,
                Stock = 8,
                CreatedAt = DateTime.UtcNow
            },

            new Producto
            {   Nombre = "Teclado Mecánico RGB",
                Descripcion = "Teclado gaming con switches Cherry MX",
                Precio = 129.99m,
                Stock = 20,
                CreatedAt = DateTime.UtcNow }
 
        };

            await context.Productos.AddRangeAsync(products);
            await context.SaveChangesAsync();

            // Crear WishLists de ejemplo
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
