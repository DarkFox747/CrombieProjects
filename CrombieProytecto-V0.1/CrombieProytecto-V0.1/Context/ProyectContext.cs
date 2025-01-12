using CrombieProytecto_V0._1.Models;
using CrombieProytecto_V0._1.Seeds;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;


namespace CrombieProytecto_V0._1.Context
{
    public class ProyectContext : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<WishList> WishList { get; set; }
        public DbSet<WishListProductos> WishlistProductos { get; set; }


        public ProyectContext(DbContextOptions<ProyectContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // User Configuration
            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasIndex(e => e.Email).IsUnique();
                entity.HasIndex(e => e.Username).IsUnique();
                entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Username).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Nombre).IsRequired().HasMaxLength(100);
                entity.Property(e => e.PasswordHash).IsRequired();
                entity.Property(e => e.Salt).IsRequired();
            });

            // Product Configuration
            modelBuilder.Entity<Producto>(entity =>
            {
                entity.Property(e => e.Nombre).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Precio).HasColumnType("decimal(18,2)");
                entity.Property(e => e.Descripcion).HasMaxLength(1000);
            });

            // WishList Configuration
            modelBuilder.Entity<WishList>(entity =>
            {
                entity.HasOne(w => w.Usuario)
                      .WithMany(u => u.WishLists)
                      .HasForeignKey(w => w.IdUsuario)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // WishListItem Configuration
            modelBuilder.Entity<WishListProductos>(entity =>
            {
                entity.HasOne(wi => wi.WishList)
                      .WithMany(w => w.Productos)
                      .HasForeignKey(wi => wi.IdWishList)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(wi => wi.Producto)
                      .WithMany(p => p.WishListProductos)
                      .HasForeignKey(wi => wi.IdProducto)
                      .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
