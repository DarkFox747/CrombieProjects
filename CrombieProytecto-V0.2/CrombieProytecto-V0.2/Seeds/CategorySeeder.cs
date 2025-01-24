using CrombieProytecto_V0._2.Context;
using CrombieProytecto_V0._2.Models.Entidades;
using Microsoft.EntityFrameworkCore;

namespace CrombieProytecto_V0._2.Seeds
{
    public static class CategorySeeder
    {
        public static async Task SeedCategoriesAsync(ProyectContext context)
        {
            if (await context.Categorias.AnyAsync())
                return;

            var categorias = new List<Categoria>
            {
                new Categoria { Nombre = "Electrónica", CreatedAt = DateTime.UtcNow },
                new Categoria { Nombre = "Ropa", CreatedAt = DateTime.UtcNow },
                new Categoria { Nombre = "Hogar", CreatedAt = DateTime.UtcNow },
                new Categoria { Nombre = "Juguetes", CreatedAt = DateTime.UtcNow }
            };

            await context.Categorias.AddRangeAsync(categorias);
            await context.SaveChangesAsync();
        }
    }
}

