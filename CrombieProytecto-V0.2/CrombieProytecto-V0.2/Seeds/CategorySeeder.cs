using CrombieProytecto_V0._2.Context;
using CrombieProytecto_V0._2.Models.Entidades;
using Microsoft.EntityFrameworkCore;

namespace CrombieProytecto_V0._2.Seeds
{
    public static class CategorySeeder
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            var categorias = new List<Categoria>
                {
                    new Categoria { Id = 1, Nombre = "Electrónica", CreatedAt = DateTime.UtcNow },
                    new Categoria { Id = 2, Nombre = "Ropa", CreatedAt = DateTime.UtcNow },
                    new Categoria { Id = 3, Nombre = "Hogar", CreatedAt = DateTime.UtcNow },
                    new Categoria { Id = 4, Nombre = "Juguetes", CreatedAt = DateTime.UtcNow }
                };

            modelBuilder.Entity<Categoria>().HasData(categorias);

            // Asignar categorías a los productos
            var productoCategorias = new List<ProductoCategoria>
                {
                    new ProductoCategoria { ProductoId = 1, CategoriaId = 1 }, // Laptop Gaming Pro - Electrónica
                    new ProductoCategoria { ProductoId = 2, CategoriaId = 1 }, // Smartphone Ultra - Electrónica
                    new ProductoCategoria { ProductoId = 3, CategoriaId = 1 }, // Auriculares Wireless - Electrónica
                    new ProductoCategoria { ProductoId = 4, CategoriaId = 1 }, // Monitor 4K - Electrónica
                    new ProductoCategoria { ProductoId = 5, CategoriaId = 1 }, // Teclado Mecánico RGB - Electrónica
                    new ProductoCategoria { ProductoId = 2, CategoriaId = 2 }, // Smartphone Ultra - Ropa (ejemplo)
                    new ProductoCategoria { ProductoId = 3, CategoriaId = 3 }, // Auriculares Wireless - Hogar (ejemplo)
                    new ProductoCategoria { ProductoId = 4, CategoriaId = 4 }  // Monitor 4K - Juguetes (ejemplo)
                };

            modelBuilder.Entity<ProductoCategoria>().HasData(productoCategorias);
        }
    }
}

