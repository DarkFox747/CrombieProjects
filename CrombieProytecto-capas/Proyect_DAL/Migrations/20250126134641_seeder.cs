using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Proyect_DAL.Migrations
{
    /// <inheritdoc />
    public partial class seeder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Eliminar datos existentes en las tablas
            migrationBuilder.Sql("DELETE FROM [WishlistProductos]");
            migrationBuilder.Sql("DELETE FROM [WishList]");
            migrationBuilder.Sql("DELETE FROM [ProductoCategorias]");
            migrationBuilder.Sql("DELETE FROM [Productos]");
            migrationBuilder.Sql("DELETE FROM [Categorias]");
            migrationBuilder.Sql("DELETE FROM [Usuarios]");

            migrationBuilder.InsertData(
                table: "Categorias",
                columns: new[] { "Id", "CreatedAt", "Nombre", "UpdatedAt" },
                values: new object[,]
                {
                        { 1, new DateTime(2025, 1, 26, 13, 46, 41, 74, DateTimeKind.Utc).AddTicks(4479), "Electrónica", null },
                        { 2, new DateTime(2025, 1, 26, 13, 46, 41, 74, DateTimeKind.Utc).AddTicks(4482), "Ropa", null },
                        { 3, new DateTime(2025, 1, 26, 13, 46, 41, 74, DateTimeKind.Utc).AddTicks(4483), "Hogar", null },
                        { 4, new DateTime(2025, 1, 26, 13, 46, 41, 74, DateTimeKind.Utc).AddTicks(4484), "Juguetes", null }
                });

            migrationBuilder.InsertData(
                table: "Productos",
                columns: new[] { "Id", "CreatedAt", "Descripcion", "Nombre", "Precio", "Stock", "URL", "UpdatedAt" },
                values: new object[,]
                {
                        { 1, new DateTime(2025, 1, 26, 13, 46, 41, 74, DateTimeKind.Utc).AddTicks(4386), "Laptop gaming de alta gama con RTX 3080", "Laptop Gaming Pro", 1499.99m, 10, null, null },
                        { 2, new DateTime(2025, 1, 26, 13, 46, 41, 74, DateTimeKind.Utc).AddTicks(4390), "Smartphone de última generación con cámara 108MP", "Smartphone Ultra", 899.99m, 15, null, null },
                        { 3, new DateTime(2025, 1, 26, 13, 46, 41, 74, DateTimeKind.Utc).AddTicks(4392), "Auriculares bluetooth con cancelación de ruido", "Auriculares Wireless", 199.99m, 30, null, null },
                        { 4, new DateTime(2025, 1, 26, 13, 46, 41, 74, DateTimeKind.Utc).AddTicks(4393), "Monitor gaming 4K 144Hz", "Monitor 4K", 499.99m, 8, null, null },
                        { 5, new DateTime(2025, 1, 26, 13, 46, 41, 74, DateTimeKind.Utc).AddTicks(4395), "Teclado gaming con switches Cherry MX", "Teclado Mecánico RGB", 129.99m, 20, null, null }
                });

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "Id", "CreatedAt", "Email", "Nombre", "PasswordHash", "Roles", "Salt", "UpdatedAt", "Username" },
                values: new object[,]
                {
                        { 1, new DateTime(2025, 1, 26, 13, 46, 41, 74, DateTimeKind.Utc).AddTicks(4220), "admin@example.com", "Admin", "JaMzig+xga1jXEFsOQr6x44WhlYoPp9JNDh9RtmZXcA=", 1, "36xB6nGSv2SPw0YwjKevhQ==", null, "admin" },
                        { 2, new DateTime(2025, 1, 26, 13, 46, 41, 74, DateTimeKind.Utc).AddTicks(4222), "user@example.com", "User", "User123!", 0, "random_salt", null, "user" }
                });

            migrationBuilder.InsertData(
                table: "ProductoCategorias",
                columns: new[] { "CategoriaId", "ProductoId" },
                values: new object[,]
                {
                        { 1, 1 },
                        { 1, 2 },
                        { 2, 2 },
                        { 1, 3 },
                        { 3, 3 },
                        { 1, 4 },
                        { 4, 4 },
                        { 1, 5 }
                });

            migrationBuilder.InsertData(
                table: "WishList",
                columns: new[] { "Id", "CreatedAt", "IdUsuario", "Nombre", "UpdatedAt" },
                values: new object[,]
                {
                        { 1, new DateTime(2025, 1, 26, 13, 46, 41, 74, DateTimeKind.Utc).AddTicks(4630), 2, "Mi Setup Gaming", null },
                        { 2, new DateTime(2025, 1, 26, 13, 46, 41, 74, DateTimeKind.Utc).AddTicks(4632), 2, "Tecnología Móvil", null }
                });

            migrationBuilder.InsertData(
                table: "WishlistProductos",
                columns: new[] { "Id", "CreatedAt", "IdProducto", "IdWishList", "UpdatedAt" },
                values: new object[,]
                {
                        { 1, new DateTime(2025, 1, 26, 13, 46, 41, 74, DateTimeKind.Utc).AddTicks(4697), 1, 1, null },
                        { 2, new DateTime(2025, 1, 26, 13, 46, 41, 74, DateTimeKind.Utc).AddTicks(4699), 4, 1, null },
                        { 3, new DateTime(2025, 1, 26, 13, 46, 41, 74, DateTimeKind.Utc).AddTicks(4700), 5, 1, null },
                        { 4, new DateTime(2025, 1, 26, 13, 46, 41, 74, DateTimeKind.Utc).AddTicks(4702), 2, 2, null },
                        { 5, new DateTime(2025, 1, 26, 13, 46, 41, 74, DateTimeKind.Utc).AddTicks(4703), 3, 2, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProductoCategorias",
                keyColumns: new[] { "CategoriaId", "ProductoId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "ProductoCategorias",
                keyColumns: new[] { "CategoriaId", "ProductoId" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "ProductoCategorias",
                keyColumns: new[] { "CategoriaId", "ProductoId" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "ProductoCategorias",
                keyColumns: new[] { "CategoriaId", "ProductoId" },
                keyValues: new object[] { 1, 3 });

            migrationBuilder.DeleteData(
                table: "ProductoCategorias",
                keyColumns: new[] { "CategoriaId", "ProductoId" },
                keyValues: new object[] { 3, 3 });

            migrationBuilder.DeleteData(
                table: "ProductoCategorias",
                keyColumns: new[] { "CategoriaId", "ProductoId" },
                keyValues: new object[] { 1, 4 });

            migrationBuilder.DeleteData(
                table: "ProductoCategorias",
                keyColumns: new[] { "CategoriaId", "ProductoId" },
                keyValues: new object[] { 4, 4 });

            migrationBuilder.DeleteData(
                table: "ProductoCategorias",
                keyColumns: new[] { "CategoriaId", "ProductoId" },
                keyValues: new object[] { 1, 5 });

            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "WishlistProductos",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "WishlistProductos",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "WishlistProductos",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "WishlistProductos",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "WishlistProductos",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Categorias",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categorias",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categorias",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categorias",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "WishList",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "WishList",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
