using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Proyect_DAL.Migrations
{
    /// <inheritdoc />
    public partial class Fix2Categoria : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Categorias",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 2, 4, 2, 23, 45, 970, DateTimeKind.Utc).AddTicks(7260));

            migrationBuilder.UpdateData(
                table: "Categorias",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 2, 4, 2, 23, 45, 970, DateTimeKind.Utc).AddTicks(7264));

            migrationBuilder.UpdateData(
                table: "Categorias",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 2, 4, 2, 23, 45, 970, DateTimeKind.Utc).AddTicks(7265));

            migrationBuilder.UpdateData(
                table: "Categorias",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 2, 4, 2, 23, 45, 970, DateTimeKind.Utc).AddTicks(7266));

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 2, 4, 2, 23, 45, 970, DateTimeKind.Utc).AddTicks(7163));

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 2, 4, 2, 23, 45, 970, DateTimeKind.Utc).AddTicks(7166));

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 2, 4, 2, 23, 45, 970, DateTimeKind.Utc).AddTicks(7169));

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 2, 4, 2, 23, 45, 970, DateTimeKind.Utc).AddTicks(7171));

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 2, 4, 2, 23, 45, 970, DateTimeKind.Utc).AddTicks(7174));

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 2, 4, 2, 23, 45, 970, DateTimeKind.Utc).AddTicks(6973));

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 2, 4, 2, 23, 45, 970, DateTimeKind.Utc).AddTicks(6975));

            migrationBuilder.UpdateData(
                table: "WishList",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 2, 4, 2, 23, 45, 970, DateTimeKind.Utc).AddTicks(7430));

            migrationBuilder.UpdateData(
                table: "WishList",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 2, 4, 2, 23, 45, 970, DateTimeKind.Utc).AddTicks(7432));

            migrationBuilder.UpdateData(
                table: "WishlistProductos",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 2, 4, 2, 23, 45, 970, DateTimeKind.Utc).AddTicks(7508));

            migrationBuilder.UpdateData(
                table: "WishlistProductos",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 2, 4, 2, 23, 45, 970, DateTimeKind.Utc).AddTicks(7510));

            migrationBuilder.UpdateData(
                table: "WishlistProductos",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 2, 4, 2, 23, 45, 970, DateTimeKind.Utc).AddTicks(7511));

            migrationBuilder.UpdateData(
                table: "WishlistProductos",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 2, 4, 2, 23, 45, 970, DateTimeKind.Utc).AddTicks(7512));

            migrationBuilder.UpdateData(
                table: "WishlistProductos",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 2, 4, 2, 23, 45, 970, DateTimeKind.Utc).AddTicks(7513));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}
