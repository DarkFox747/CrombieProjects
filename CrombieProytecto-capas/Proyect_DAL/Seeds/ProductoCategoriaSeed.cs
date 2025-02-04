using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Proyect_Models.Models.Entidades;

namespace Proyect_DAL.Seeds
{
    //Define la configuración para la entidad ProductoCategoría

    public class ProductoCategoriaSeed : IEntityTypeConfiguration<ProductoCategoria>
    {
        public void Configure(EntityTypeBuilder<ProductoCategoria> builder)
        {
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

            builder.HasData(productoCategorias);
        }
    }
}


