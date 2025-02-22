using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Coto.VentasAutomoviles.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class initialdb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ventas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CentroDistribucionId = table.Column<int>(type: "int", nullable: false),
                    ModeloVehiculoId = table.Column<int>(type: "int", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    FechaDeVenta = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PrecioDeVenta = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PrecioBase = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ClienteId = table.Column<int>(type: "int", nullable: false),
                    Vehiculo = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ventas", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ventas");
        }
    }
}
