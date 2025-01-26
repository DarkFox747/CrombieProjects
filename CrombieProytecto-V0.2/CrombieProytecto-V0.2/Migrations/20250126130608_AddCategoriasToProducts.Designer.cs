﻿// <auto-generated />
using System;
using CrombieProytecto_V0._2.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CrombieProytecto_V0._2.Migrations
{
    [DbContext(typeof(ProyectContext))]
    [Migration("20250126130608_AddCategoriasToProducts")]
    partial class AddCategoriasToProducts
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CrombieProytecto_V0._2.Models.Entidades.Categoria", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Categorias");
                });

            modelBuilder.Entity("CrombieProytecto_V0._2.Models.Entidades.Producto", b =>
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
                });

            modelBuilder.Entity("CrombieProytecto_V0._2.Models.Entidades.ProductoCategoria", b =>
                {
                    b.Property<int>("ProductoId")
                        .HasColumnType("int");

                    b.Property<int>("CategoriaId")
                        .HasColumnType("int");

                    b.HasKey("ProductoId", "CategoriaId");

                    b.HasIndex("CategoriaId");

                    b.ToTable("ProductoCategorias");
                });

            modelBuilder.Entity("CrombieProytecto_V0._2.Models.Entidades.Usuario", b =>
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
                });

            modelBuilder.Entity("CrombieProytecto_V0._2.Models.Entidades.WishList", b =>
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
                });

            modelBuilder.Entity("CrombieProytecto_V0._2.Models.Entidades.WishListProductos", b =>
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
                });

            modelBuilder.Entity("CrombieProytecto_V0._2.Models.Entidades.ProductoCategoria", b =>
                {
                    b.HasOne("CrombieProytecto_V0._2.Models.Entidades.Categoria", "Categoria")
                        .WithMany("ProductoCategorias")
                        .HasForeignKey("CategoriaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CrombieProytecto_V0._2.Models.Entidades.Producto", "Producto")
                        .WithMany("ProductoCategorias")
                        .HasForeignKey("ProductoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Categoria");

                    b.Navigation("Producto");
                });

            modelBuilder.Entity("CrombieProytecto_V0._2.Models.Entidades.WishList", b =>
                {
                    b.HasOne("CrombieProytecto_V0._2.Models.Entidades.Usuario", "Usuario")
                        .WithMany("WishLists")
                        .HasForeignKey("IdUsuario")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("CrombieProytecto_V0._2.Models.Entidades.WishListProductos", b =>
                {
                    b.HasOne("CrombieProytecto_V0._2.Models.Entidades.Producto", "Producto")
                        .WithMany("WishListProductos")
                        .HasForeignKey("IdProducto")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("CrombieProytecto_V0._2.Models.Entidades.WishList", "WishList")
                        .WithMany("Productos")
                        .HasForeignKey("IdWishList")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Producto");

                    b.Navigation("WishList");
                });

            modelBuilder.Entity("CrombieProytecto_V0._2.Models.Entidades.Categoria", b =>
                {
                    b.Navigation("ProductoCategorias");
                });

            modelBuilder.Entity("CrombieProytecto_V0._2.Models.Entidades.Producto", b =>
                {
                    b.Navigation("ProductoCategorias");

                    b.Navigation("WishListProductos");
                });

            modelBuilder.Entity("CrombieProytecto_V0._2.Models.Entidades.Usuario", b =>
                {
                    b.Navigation("WishLists");
                });

            modelBuilder.Entity("CrombieProytecto_V0._2.Models.Entidades.WishList", b =>
                {
                    b.Navigation("Productos");
                });
#pragma warning restore 612, 618
        }
    }
}
