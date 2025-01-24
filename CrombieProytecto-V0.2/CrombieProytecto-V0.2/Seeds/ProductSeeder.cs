using CrombieProytecto_V0._2.Context;
using CrombieProytecto_V0._2.Models.Entidades;
using Microsoft.EntityFrameworkCore;

namespace CrombieProytecto_V0._2.Seeds
{
    public static class ProductSeeder
    {
        public static async Task SeedProductsAsync(ProyectContext context)
        {
            if (await context.Productos.AnyAsync())
                return;

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
                {
                    Nombre = "Teclado Mecánico RGB",
                    Descripcion = "Teclado gaming con switches Cherry MX",
                    Precio = 129.99m,
                    Stock = 20,
                    CreatedAt = DateTime.UtcNow
                }
            };

            await context.Productos.AddRangeAsync(products);
            await context.SaveChangesAsync();
        }
    }
}

