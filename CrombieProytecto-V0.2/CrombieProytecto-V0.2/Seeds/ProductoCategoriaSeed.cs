using CrombieProytecto_V0._2.Models.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CrombieProytecto_V0._2.Seeds
{
    //Define la configuraci�n para la entidad ProductoCategor�a

    public class ProductoCategoriaSeed : IEntityTypeConfiguration<ProductoCategoria>
    {
        public void Configure(EntityTypeBuilder<ProductoCategoria> builder)
        {
            var productoCategorias = new List<ProductoCategoria>
            {
                new ProductoCategoria { ProductoId = 1, CategoriaId = 1 }, // Laptop Gaming Pro - Electr�nica
                new ProductoCategoria { ProductoId = 2, CategoriaId = 1 }, // Smartphone Ultra - Electr�nica
                new ProductoCategoria { ProductoId = 3, CategoriaId = 1 }, // Auriculares Wireless - Electr�nica
                new ProductoCategoria { ProductoId = 4, CategoriaId = 1 }, // Monitor 4K - Electr�nica
                new ProductoCategoria { ProductoId = 5, CategoriaId = 1 }, // Teclado Mec�nico RGB - Electr�nica
                new ProductoCategoria { ProductoId = 2, CategoriaId = 2 }, // Smartphone Ultra - Ropa (ejemplo)
                new ProductoCategoria { ProductoId = 3, CategoriaId = 3 }, // Auriculares Wireless - Hogar (ejemplo)
                new ProductoCategoria { ProductoId = 4, CategoriaId = 4 }  // Monitor 4K - Juguetes (ejemplo)
            };

            builder.HasData(productoCategorias);
        }
    }
}


