using CrombieProytecto_V0._2.Models.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CrombieProytecto_V0._2.Seeds
{
    public class WishListProductosSeed : IEntityTypeConfiguration<WishListProductos>
    {
        public void Configure(EntityTypeBuilder<WishListProductos> builder)
        {
            var wishListProductos = new List<WishListProductos>
            {
                new WishListProductos
                {
                    Id = 1,
                    IdWishList = 1, // Mi Setup Gaming
                    IdProducto = 1, // Laptop Gaming
                    CreatedAt = DateTime.UtcNow
                },
                new WishListProductos
                {
                    Id = 2,
                    IdWishList = 1, // Mi Setup Gaming
                    IdProducto = 4, // Monitor
                    CreatedAt = DateTime.UtcNow
                },
                new WishListProductos
                {
                    Id = 3,
                    IdWishList = 1, // Mi Setup Gaming
                    IdProducto = 5, // Teclado
                    CreatedAt = DateTime.UtcNow
                },
                new WishListProductos
                {
                    Id = 4,
                    IdWishList = 2, // Tecnología Móvil
                    IdProducto = 2, // Smartphone
                    CreatedAt = DateTime.UtcNow
                },
                new WishListProductos
                {
                    Id = 5,
                    IdWishList = 2, // Tecnología Móvil
                    IdProducto = 3, // Auriculares
                    CreatedAt = DateTime.UtcNow
                }
            };

            builder.HasData(wishListProductos);
        }
    }
}


