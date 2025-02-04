﻿// <auto-generated />
using System;
//using Proyect_DAL.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Proyect_DAL.Context;

#nullable disable

namespace Proyect_DAL.Migrations
{
    [DbContext(typeof(ProyectContext))]
    partial class ProyectContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Carrito", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("UsuarioId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Carritos");
                });

            modelBuilder.Entity("CarritoProducto", b =>
                {
                    b.Property<int>("CarritoId")
                        .HasColumnType("int");

                    b.Property<int>("ProductoId")
                        .HasColumnType("int");

                    b.Property<int>("Cantidad")
                        .HasColumnType("int");

                    b.HasKey("CarritoId", "ProductoId");

                    b.HasIndex("ProductoId");

                    b.ToTable("CarritoProductos");
                });

            modelBuilder.Entity("Compra", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("FechaCompra")
                        .HasColumnType("datetime2");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Compras");
                });

            modelBuilder.Entity("CompraProducto", b =>
                {
                    b.Property<int>("CompraId")
                        .HasColumnType("int");

                    b.Property<int>("ProductoId")
                        .HasColumnType("int");

                    b.Property<int>("Cantidad")
                        .HasColumnType("int");

                    b.HasKey("CompraId", "ProductoId");

                    b.HasIndex("ProductoId");

                    b.ToTable("CompraProductos");
                });

            modelBuilder.Entity("Proyect_Models.Models.Entidades.Categoria", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Categorias");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedAt = new DateTime(2025, 2, 4, 2, 23, 45, 970, DateTimeKind.Utc).AddTicks(7260),
                            Nombre = "Electrónica"
                        },
                        new
                        {
                            Id = 2,
                            CreatedAt = new DateTime(2025, 2, 4, 2, 23, 45, 970, DateTimeKind.Utc).AddTicks(7264),
                            Nombre = "Ropa"
                        },
                        new
                        {
                            Id = 3,
                            CreatedAt = new DateTime(2025, 2, 4, 2, 23, 45, 970, DateTimeKind.Utc).AddTicks(7265),
                            Nombre = "Hogar"
                        },
                        new
                        {
                            Id = 4,
                            CreatedAt = new DateTime(2025, 2, 4, 2, 23, 45, 970, DateTimeKind.Utc).AddTicks(7266),
                            Nombre = "Juguetes"
                        });
                });

            modelBuilder.Entity("Proyect_Models.Models.Entidades.Producto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Descripcion")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<decimal>("Precio")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Stock")
                        .HasColumnType("int");

                    b.Property<string>("URL")
                        .HasMaxLength(2048)
                        .HasColumnType("nvarchar(2048)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Productos");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedAt = new DateTime(2025, 2, 4, 2, 23, 45, 970, DateTimeKind.Utc).AddTicks(7163),
                            Descripcion = "Laptop gaming de alta gama con RTX 3080",
                            Nombre = "Laptop Gaming Pro",
                            Precio = 1499.99m,
                            Stock = 10
                        },
                        new
                        {
                            Id = 2,
                            CreatedAt = new DateTime(2025, 2, 4, 2, 23, 45, 970, DateTimeKind.Utc).AddTicks(7166),
                            Descripcion = "Smartphone de última generación con cámara 108MP",
                            Nombre = "Smartphone Ultra",
                            Precio = 899.99m,
                            Stock = 15
                        },
                        new
                        {
                            Id = 3,
                            CreatedAt = new DateTime(2025, 2, 4, 2, 23, 45, 970, DateTimeKind.Utc).AddTicks(7169),
                            Descripcion = "Auriculares bluetooth con cancelación de ruido",
                            Nombre = "Auriculares Wireless",
                            Precio = 199.99m,
                            Stock = 30
                        },
                        new
                        {
                            Id = 4,
                            CreatedAt = new DateTime(2025, 2, 4, 2, 23, 45, 970, DateTimeKind.Utc).AddTicks(7171),
                            Descripcion = "Monitor gaming 4K 144Hz",
                            Nombre = "Monitor 4K",
                            Precio = 499.99m,
                            Stock = 8
                        },
                        new
                        {
                            Id = 5,
                            CreatedAt = new DateTime(2025, 2, 4, 2, 23, 45, 970, DateTimeKind.Utc).AddTicks(7174),
                            Descripcion = "Teclado gaming con switches Cherry MX",
                            Nombre = "Teclado Mecánico RGB",
                            Precio = 129.99m,
                            Stock = 20
                        });
                });

            modelBuilder.Entity("Proyect_Models.Models.Entidades.ProductoCategoria", b =>
                {
                    b.Property<int>("ProductoId")
                        .HasColumnType("int");

                    b.Property<int>("CategoriaId")
                        .HasColumnType("int");

                    b.HasKey("ProductoId", "CategoriaId");

                    b.HasIndex("CategoriaId");

                    b.ToTable("ProductoCategorias");

                    b.HasData(
                        new
                        {
                            ProductoId = 1,
                            CategoriaId = 1
                        },
                        new
                        {
                            ProductoId = 2,
                            CategoriaId = 1
                        },
                        new
                        {
                            ProductoId = 3,
                            CategoriaId = 1
                        },
                        new
                        {
                            ProductoId = 4,
                            CategoriaId = 1
                        },
                        new
                        {
                            ProductoId = 5,
                            CategoriaId = 1
                        },
                        new
                        {
                            ProductoId = 2,
                            CategoriaId = 2
                        },
                        new
                        {
                            ProductoId = 3,
                            CategoriaId = 3
                        },
                        new
                        {
                            ProductoId = 4,
                            CategoriaId = 4
                        });
                });

            modelBuilder.Entity("Proyect_Models.Models.Entidades.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Roles")
                        .HasColumnType("int");

                    b.Property<string>("Salt")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("Usuarios");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedAt = new DateTime(2025, 2, 4, 2, 23, 45, 970, DateTimeKind.Utc).AddTicks(6973),
                            Email = "admin@example.com",
                            Nombre = "Admin",
                            PasswordHash = "Ru5l7/9VNQ1CgSzIZg5na0WWm+sZewJpWPBkBf+1RvA=",
                            Roles = 1,
                            Salt = "AuDooNbYaGBUTN7lnuViSw==",
                            Username = "admin"
                        },
                        new
                        {
                            Id = 2,
                            CreatedAt = new DateTime(2025, 2, 4, 2, 23, 45, 970, DateTimeKind.Utc).AddTicks(6975),
                            Email = "user@example.com",
                            Nombre = "User",
                            PasswordHash = "Qmd4q0uhgOtcKfXPg/FIfcLbz855lv98RXbtH7GUbTA=",
                            Roles = 0,
                            Salt = "R4r0g+D2FKYGM5M8wv9P8w==",
                            Username = "user"
                        });
                });

            modelBuilder.Entity("Proyect_Models.Models.Entidades.WishList", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdUsuario")
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("IdUsuario");

                    b.ToTable("WishList");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedAt = new DateTime(2025, 2, 4, 2, 23, 45, 970, DateTimeKind.Utc).AddTicks(7430),
                            IdUsuario = 2,
                            Nombre = "Mi Setup Gaming"
                        },
                        new
                        {
                            Id = 2,
                            CreatedAt = new DateTime(2025, 2, 4, 2, 23, 45, 970, DateTimeKind.Utc).AddTicks(7432),
                            IdUsuario = 2,
                            Nombre = "Tecnología Móvil"
                        });
                });

            modelBuilder.Entity("Proyect_Models.Models.Entidades.WishListProductos", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdProducto")
                        .HasColumnType("int");

                    b.Property<int>("IdWishList")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("IdProducto");

                    b.HasIndex("IdWishList");

                    b.ToTable("WishlistProductos");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedAt = new DateTime(2025, 2, 4, 2, 23, 45, 970, DateTimeKind.Utc).AddTicks(7508),
                            IdProducto = 1,
                            IdWishList = 1
                        },
                        new
                        {
                            Id = 2,
                            CreatedAt = new DateTime(2025, 2, 4, 2, 23, 45, 970, DateTimeKind.Utc).AddTicks(7510),
                            IdProducto = 4,
                            IdWishList = 1
                        },
                        new
                        {
                            Id = 3,
                            CreatedAt = new DateTime(2025, 2, 4, 2, 23, 45, 970, DateTimeKind.Utc).AddTicks(7511),
                            IdProducto = 5,
                            IdWishList = 1
                        },
                        new
                        {
                            Id = 4,
                            CreatedAt = new DateTime(2025, 2, 4, 2, 23, 45, 970, DateTimeKind.Utc).AddTicks(7512),
                            IdProducto = 2,
                            IdWishList = 2
                        },
                        new
                        {
                            Id = 5,
                            CreatedAt = new DateTime(2025, 2, 4, 2, 23, 45, 970, DateTimeKind.Utc).AddTicks(7513),
                            IdProducto = 3,
                            IdWishList = 2
                        });
                });

            modelBuilder.Entity("Carrito", b =>
                {
                    b.HasOne("Proyect_Models.Models.Entidades.Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("CarritoProducto", b =>
                {
                    b.HasOne("Carrito", "Carrito")
                        .WithMany("Productos")
                        .HasForeignKey("CarritoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Proyect_Models.Models.Entidades.Producto", "Producto")
                        .WithMany()
                        .HasForeignKey("ProductoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Carrito");

                    b.Navigation("Producto");
                });

            modelBuilder.Entity("Compra", b =>
                {
                    b.HasOne("Proyect_Models.Models.Entidades.Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("CompraProducto", b =>
                {
                    b.HasOne("Compra", "Compra")
                        .WithMany("Productos")
                        .HasForeignKey("CompraId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Proyect_Models.Models.Entidades.Producto", "Producto")
                        .WithMany()
                        .HasForeignKey("ProductoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Compra");

                    b.Navigation("Producto");
                });

            modelBuilder.Entity("Proyect_Models.Models.Entidades.ProductoCategoria", b =>
                {
                    b.HasOne("Proyect_Models.Models.Entidades.Categoria", "Categoria")
                        .WithMany("ProductoCategorias")
                        .HasForeignKey("CategoriaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Proyect_Models.Models.Entidades.Producto", "Producto")
                        .WithMany("ProductoCategorias")
                        .HasForeignKey("ProductoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Categoria");

                    b.Navigation("Producto");
                });

            modelBuilder.Entity("Proyect_Models.Models.Entidades.WishList", b =>
                {
                    b.HasOne("Proyect_Models.Models.Entidades.Usuario", "Usuario")
                        .WithMany("WishLists")
                        .HasForeignKey("IdUsuario")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("Proyect_Models.Models.Entidades.WishListProductos", b =>
                {
                    b.HasOne("Proyect_Models.Models.Entidades.Producto", "Producto")
                        .WithMany("WishListProductos")
                        .HasForeignKey("IdProducto")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Proyect_Models.Models.Entidades.WishList", "WishList")
                        .WithMany("Productos")
                        .HasForeignKey("IdWishList")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Producto");

                    b.Navigation("WishList");
                });

            modelBuilder.Entity("Carrito", b =>
                {
                    b.Navigation("Productos");
                });

            modelBuilder.Entity("Compra", b =>
                {
                    b.Navigation("Productos");
                });

            modelBuilder.Entity("Proyect_Models.Models.Entidades.Categoria", b =>
                {
                    b.Navigation("ProductoCategorias");
                });

            modelBuilder.Entity("Proyect_Models.Models.Entidades.Producto", b =>
                {
                    b.Navigation("ProductoCategorias");

                    b.Navigation("WishListProductos");
                });

            modelBuilder.Entity("Proyect_Models.Models.Entidades.Usuario", b =>
                {
                    b.Navigation("WishLists");
                });

            modelBuilder.Entity("Proyect_Models.Models.Entidades.WishList", b =>
                {
                    b.Navigation("Productos");
                });
#pragma warning restore 612, 618
        }
    }
}
