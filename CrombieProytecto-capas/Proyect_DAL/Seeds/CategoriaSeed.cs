using Proyect_Models.Models.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Proyect_DAL.Seeds
{
    //Define la configuración para la entidad Categoría
    public class CategoriaSeed : IEntityTypeConfiguration<Categoria>
    {
        public void Configure(EntityTypeBuilder<Categoria> builder)
        {
            var categorias = new List<Categoria>
            {
                new Categoria { Id = 1, Nombre = "Electrónica", CreatedAt = DateTime.UtcNow },
                new Categoria { Id = 2, Nombre = "Ropa", CreatedAt = DateTime.UtcNow },
                new Categoria { Id = 3, Nombre = "Hogar", CreatedAt = DateTime.UtcNow },
                new Categoria { Id = 4, Nombre = "Juguetes", CreatedAt = DateTime.UtcNow }
            };

            builder.HasData(categorias);
        }
    }
}


