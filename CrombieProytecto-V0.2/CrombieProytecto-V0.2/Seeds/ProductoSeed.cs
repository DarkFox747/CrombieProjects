using CrombieProytecto_V0._2.Models.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CrombieProytecto_V0._2.Seeds
{
    //Define la configuración para la entidad Producto

    public class ProductoSeed : IEntityTypeConfiguration<Producto>
    {
        public void Configure(EntityTypeBuilder<Producto> builder)
        {
            var products = new List<Producto>
            {
                new Producto
                {
                    Id = 1,
                    Nombre = "Laptop Gaming Pro",
                    Descripcion = "Laptop gaming de alta gama con RTX 3080",
                    Precio = 1499.99m,
                    Stock = 10,
                    CreatedAt = DateTime.UtcNow
                },
                new Producto
                {
                    Id = 2,
                    Nombre = "Smartphone Ultra",
                    Descripcion = "Smartphone de última generación con cámara 108MP",
                    Precio = 899.99m,
                    Stock = 15,
                    CreatedAt = DateTime.UtcNow
                },
                new Producto
                {
                    Id = 3,
                    Nombre = "Auriculares Wireless",
                    Descripcion = "Auriculares bluetooth con cancelación de ruido",
                    Precio = 199.99m,
                    Stock = 30,
                    CreatedAt = DateTime.UtcNow
                },
                new Producto
                {
                    Id = 4,
                    Nombre = "Monitor 4K",
                    Descripcion = "Monitor gaming 4K 144Hz",
                    Precio = 499.99m,
                    Stock = 8,
                    CreatedAt = DateTime.UtcNow
                },
                new Producto
                {
                    Id = 5,
                    Nombre = "Teclado Mecánico RGB",
                    Descripcion = "Teclado gaming con switches Cherry MX",
                    Precio = 129.99m,
                    Stock = 20,
                    CreatedAt = DateTime.UtcNow
                }
            };

            builder.HasData(products);
        }
    }
}
