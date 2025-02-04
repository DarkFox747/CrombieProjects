using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CrombieProytecto_V0._2.Migrations
{
    /// <inheritdoc />
    public partial class FixCategoriaId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "Categorias",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "Categorias",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 2, 4, 2, 16, 51, 995, DateTimeKind.Utc).AddTicks(8264));

            migrationBuilder.UpdateData(
                table: "Categorias",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 2, 4, 2, 16, 51, 995, DateTimeKind.Utc).AddTicks(8266));

            migrationBuilder.UpdateData(
                table: "Categorias",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 2, 4, 2, 16, 51, 995, DateTimeKind.Utc).AddTicks(8267));

            migrationBuilder.UpdateData(
                table: "Categorias",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 2, 4, 2, 16, 51, 995, DateTimeKind.Utc).AddTicks(8268));

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 2, 4, 2, 16, 51, 995, DateTimeKind.Utc).AddTicks(8178));

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 2, 4, 2, 16, 51, 995, DateTimeKind.Utc).AddTicks(8181));

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 2, 4, 2, 16, 51, 995, DateTimeKind.Utc).AddTicks(8183));

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 2, 4, 2, 16, 51, 995, DateTimeKind.Utc).AddTicks(8184));

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 2, 4, 2, 16, 51, 995, DateTimeKind.Utc).AddTicks(8186));

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 2, 4, 2, 16, 51, 995, DateTimeKind.Utc).AddTicks(8003));

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 2, 4, 2, 16, 51, 995, DateTimeKind.Utc).AddTicks(8006));

            migrationBuilder.UpdateData(
                table: "WishList",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 2, 4, 2, 16, 51, 995, DateTimeKind.Utc).AddTicks(8418));

            migrationBuilder.UpdateData(
                table: "WishList",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 2, 4, 2, 16, 51, 995, DateTimeKind.Utc).AddTicks(8420));

            migrationBuilder.UpdateData(
                table: "WishlistProductos",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 2, 4, 2, 16, 51, 995, DateTimeKind.Utc).AddTicks(8494));

            migrationBuilder.UpdateData(
                table: "WishlistProductos",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 2, 4, 2, 16, 51, 995, DateTimeKind.Utc).AddTicks(8496));

            migrationBuilder.UpdateData(
                table: "WishlistProductos",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 2, 4, 2, 16, 51, 995, DateTimeKind.Utc).AddTicks(8497));

            migrationBuilder.UpdateData(
                table: "WishlistProductos",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 2, 4, 2, 16, 51, 995, DateTimeKind.Utc).AddTicks(8498));

            migrationBuilder.UpdateData(
                table: "WishlistProductos",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 2, 4, 2, 16, 51, 995, DateTimeKind.Utc).AddTicks(8499));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "Categorias",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.UpdateData(
                table: "Categorias",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 2, 3, 14, 34, 53, 849, DateTimeKind.Utc).AddTicks(7247));

            migrationBuilder.UpdateData(
                table: "Categorias",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 2, 3, 14, 34, 53, 849, DateTimeKind.Utc).AddTicks(7249));

            migrationBuilder.UpdateData(
                table: "Categorias",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 2, 3, 14, 34, 53, 849, DateTimeKind.Utc).AddTicks(7250));

            migrationBuilder.UpdateData(
                table: "Categorias",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 2, 3, 14, 34, 53, 849, DateTimeKind.Utc).AddTicks(7251));

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 2, 3, 14, 34, 53, 849, DateTimeKind.Utc).AddTicks(7140));

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 2, 3, 14, 34, 53, 849, DateTimeKind.Utc).AddTicks(7143));

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 2, 3, 14, 34, 53, 849, DateTimeKind.Utc).AddTicks(7145));

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 2, 3, 14, 34, 53, 849, DateTimeKind.Utc).AddTicks(7147));

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 2, 3, 14, 34, 53, 849, DateTimeKind.Utc).AddTicks(7148));

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 2, 3, 14, 34, 53, 849, DateTimeKind.Utc).AddTicks(6978));

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 2, 3, 14, 34, 53, 849, DateTimeKind.Utc).AddTicks(6981));

            migrationBuilder.UpdateData(
                table: "WishList",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 2, 3, 14, 34, 53, 849, DateTimeKind.Utc).AddTicks(7437));

            migrationBuilder.UpdateData(
                table: "WishList",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 2, 3, 14, 34, 53, 849, DateTimeKind.Utc).AddTicks(7439));

            migrationBuilder.UpdateData(
                table: "WishlistProductos",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 2, 3, 14, 34, 53, 849, DateTimeKind.Utc).AddTicks(7513));

            migrationBuilder.UpdateData(
                table: "WishlistProductos",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 2, 3, 14, 34, 53, 849, DateTimeKind.Utc).AddTicks(7515));

            migrationBuilder.UpdateData(
                table: "WishlistProductos",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 2, 3, 14, 34, 53, 849, DateTimeKind.Utc).AddTicks(7516));

            migrationBuilder.UpdateData(
                table: "WishlistProductos",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 2, 3, 14, 34, 53, 849, DateTimeKind.Utc).AddTicks(7517));

            migrationBuilder.UpdateData(
                table: "WishlistProductos",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 2, 3, 14, 34, 53, 849, DateTimeKind.Utc).AddTicks(7518));
        }
    }
}
