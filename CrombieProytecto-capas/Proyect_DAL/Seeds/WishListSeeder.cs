using Proyect_DAL.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Proyect_Models.Models.Entidades;

namespace Proyect_DAL.Seeds
{
    public static class WishListSeeder
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            var wishLists = new List<WishList>
                {
                    new WishList
                    {
                        Id = 1,
                        Nombre = "Mi Setup Gaming",
                        IdUsuario = 2, // Assuming the regular user has Id = 2
                        CreatedAt = DateTime.UtcNow
                    },
                    new WishList
                    {
                        Id = 2,
                        Nombre = "Tecnología Móvil",
                        IdUsuario = 2, // Assuming the regular user has Id = 2
                        CreatedAt = DateTime.UtcNow
                    }
                };

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

            modelBuilder.Entity<WishList>().HasData(wishLists);
            modelBuilder.Entity<WishListProductos>().HasData(wishListProductos);
        }
    }
    public class WishListSeed : IEntityTypeConfiguration<WishList>
    {
        public void Configure(EntityTypeBuilder<WishList> builder)
        {
            var wishLists = new List<WishList>
            {
                new WishList
                {
                    Id = 1,
                    Nombre = "Mi Setup Gaming",
                    IdUsuario = 2, // Assuming the regular user has Id = 2
                    CreatedAt = DateTime.UtcNow
                },
                new WishList
                {
                    Id = 2,
                    Nombre = "Tecnología Móvil",
                    IdUsuario = 2, // Assuming the regular user has Id = 2
                    CreatedAt = DateTime.UtcNow
                }
            };

            builder.HasData(wishLists);
        }
    }
}

