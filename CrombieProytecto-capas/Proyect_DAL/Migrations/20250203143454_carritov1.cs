using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Proyect_DAL.Migrations
{
    /// <inheritdoc />
    public partial class carritov1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Carritos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carritos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Carritos_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Compras",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    FechaCompra = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Compras", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Compras_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CarritoProductos",
                columns: table => new
                {
                    CarritoId = table.Column<int>(type: "int", nullable: false),
                    ProductoId = table.Column<int>(type: "int", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarritoProductos", x => new { x.CarritoId, x.ProductoId });
                    table.ForeignKey(
                        name: "FK_CarritoProductos_Carritos_CarritoId",
                        column: x => x.CarritoId,
                        principalTable: "Carritos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CarritoProductos_Productos_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Productos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CompraProductos",
                columns: table => new
                {
                    CompraId = table.Column<int>(type: "int", nullable: false),
                    ProductoId = table.Column<int>(type: "int", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompraProductos", x => new { x.CompraId, x.ProductoId });
                    table.ForeignKey(
                        name: "FK_CompraProductos_Compras_CompraId",
                        column: x => x.CompraId,
                        principalTable: "Compras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompraProductos_Productos_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Productos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_CarritoProductos_ProductoId",
                table: "CarritoProductos",
                column: "ProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_Carritos_UsuarioId",
                table: "Carritos",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_CompraProductos_ProductoId",
                table: "CompraProductos",
                column: "ProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_Compras_UsuarioId",
                table: "Compras",
                column: "UsuarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CarritoProductos");

            migrationBuilder.DropTable(
                name: "CompraProductos");

            migrationBuilder.DropTable(
                name: "Carritos");

            migrationBuilder.DropTable(
                name: "Compras");

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
                column: "CreatedAt",
                value: new DateTime(2025, 1, 27, 12, 29, 16, 58, DateTimeKind.Utc).AddTicks(6451));

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 1, 27, 12, 29, 16, 58, DateTimeKind.Utc).AddTicks(6454));

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
    }
}
