using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CrombieProytecto_V0._2.Migrations
{
    /// <inheritdoc />
    public partial class CambiosSeeds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Categorias",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 1, 27, 12, 29, 16, 58, DateTimeKind.Utc).AddTicks(6723));

            migrationBuilder.UpdateData(
                table: "Categorias",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 1, 27, 12, 29, 16, 58, DateTimeKind.Utc).AddTicks(6725));

            migrationBuilder.UpdateData(
                table: "Categorias",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 1, 27, 12, 29, 16, 58, DateTimeKind.Utc).AddTicks(6726));

            migrationBuilder.UpdateData(
                table: "Categorias",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 1, 27, 12, 29, 16, 58, DateTimeKind.Utc).AddTicks(6727));

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 1, 27, 12, 29, 16, 58, DateTimeKind.Utc).AddTicks(6648));

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 1, 27, 12, 29, 16, 58, DateTimeKind.Utc).AddTicks(6652));

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 1, 27, 12, 29, 16, 58, DateTimeKind.Utc).AddTicks(6654));

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 1, 27, 12, 29, 16, 58, DateTimeKind.Utc).AddTicks(6655));

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 1, 27, 12, 29, 16, 58, DateTimeKind.Utc).AddTicks(6657));

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash", "Salt" },
                values: new object[] { new DateTime(2025, 1, 27, 12, 29, 16, 58, DateTimeKind.Utc).AddTicks(6451), "Ru5l7/9VNQ1CgSzIZg5na0WWm+sZewJpWPBkBf+1RvA=", "AuDooNbYaGBUTN7lnuViSw==" });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash", "Salt" },
                values: new object[] { new DateTime(2025, 1, 27, 12, 29, 16, 58, DateTimeKind.Utc).AddTicks(6454), "Qmd4q0uhgOtcKfXPg/FIfcLbz855lv98RXbtH7GUbTA=", "R4r0g+D2FKYGM5M8wv9P8w==" });

            migrationBuilder.UpdateData(
                table: "WishList",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 1, 27, 12, 29, 16, 58, DateTimeKind.Utc).AddTicks(6864));

            migrationBuilder.UpdateData(
                table: "WishList",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 1, 27, 12, 29, 16, 58, DateTimeKind.Utc).AddTicks(6867));

            migrationBuilder.UpdateData(
                table: "WishlistProductos",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 1, 27, 12, 29, 16, 58, DateTimeKind.Utc).AddTicks(6927));

            migrationBuilder.UpdateData(
                table: "WishlistProductos",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 1, 27, 12, 29, 16, 58, DateTimeKind.Utc).AddTicks(6930));

            migrationBuilder.UpdateData(
                table: "WishlistProductos",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 1, 27, 12, 29, 16, 58, DateTimeKind.Utc).AddTicks(6931));

            migrationBuilder.UpdateData(
                table: "WishlistProductos",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 1, 27, 12, 29, 16, 58, DateTimeKind.Utc).AddTicks(6932));

            migrationBuilder.UpdateData(
                table: "WishlistProductos",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 1, 27, 12, 29, 16, 58, DateTimeKind.Utc).AddTicks(6933));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Categorias",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 1, 26, 13, 46, 41, 74, DateTimeKind.Utc).AddTicks(4479));

            migrationBuilder.UpdateData(
                table: "Categorias",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 1, 26, 13, 46, 41, 74, DateTimeKind.Utc).AddTicks(4482));

            migrationBuilder.UpdateData(
                table: "Categorias",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 1, 26, 13, 46, 41, 74, DateTimeKind.Utc).AddTicks(4483));

            migrationBuilder.UpdateData(
                table: "Categorias",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 1, 26, 13, 46, 41, 74, DateTimeKind.Utc).AddTicks(4484));

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 1, 26, 13, 46, 41, 74, DateTimeKind.Utc).AddTicks(4386));

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 1, 26, 13, 46, 41, 74, DateTimeKind.Utc).AddTicks(4390));

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 1, 26, 13, 46, 41, 74, DateTimeKind.Utc).AddTicks(4392));

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 1, 26, 13, 46, 41, 74, DateTimeKind.Utc).AddTicks(4393));

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 1, 26, 13, 46, 41, 74, DateTimeKind.Utc).AddTicks(4395));

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash", "Salt" },
                values: new object[] { new DateTime(2025, 1, 26, 13, 46, 41, 74, DateTimeKind.Utc).AddTicks(4220), "Admin123!", "random_salt" });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash", "Salt" },
                values: new object[] { new DateTime(2025, 1, 26, 13, 46, 41, 74, DateTimeKind.Utc).AddTicks(4222), "User123!", "random_salt" });

            migrationBuilder.UpdateData(
                table: "WishList",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 1, 26, 13, 46, 41, 74, DateTimeKind.Utc).AddTicks(4630));

            migrationBuilder.UpdateData(
                table: "WishList",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 1, 26, 13, 46, 41, 74, DateTimeKind.Utc).AddTicks(4632));

            migrationBuilder.UpdateData(
                table: "WishlistProductos",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 1, 26, 13, 46, 41, 74, DateTimeKind.Utc).AddTicks(4697));

            migrationBuilder.UpdateData(
                table: "WishlistProductos",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 1, 26, 13, 46, 41, 74, DateTimeKind.Utc).AddTicks(4699));

            migrationBuilder.UpdateData(
                table: "WishlistProductos",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 1, 26, 13, 46, 41, 74, DateTimeKind.Utc).AddTicks(4700));

            migrationBuilder.UpdateData(
                table: "WishlistProductos",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 1, 26, 13, 46, 41, 74, DateTimeKind.Utc).AddTicks(4702));

            migrationBuilder.UpdateData(
                table: "WishlistProductos",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 1, 26, 13, 46, 41, 74, DateTimeKind.Utc).AddTicks(4703));
        }
    }
}
