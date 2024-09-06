using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class FisrtMigrationDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tarjeta",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titular = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Numero = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false),
                    FechaVencimiento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Limite = table.Column<decimal>(type: "decimal(16,4)", nullable: false, defaultValue: 0m),
                    SaldoActual = table.Column<decimal>(type: "decimal(16,4)", nullable: false, defaultValue: 0m),
                    SaldoDisponible = table.Column<decimal>(type: "decimal(16,4)", nullable: false, defaultValue: 0m),
                    InteresBonificable = table.Column<decimal>(type: "decimal(16,4)", nullable: false, defaultValue: 0m),
                    PorcentajePagoMinimo = table.Column<double>(type: "float", nullable: false, defaultValue: 0.0),
                    PorcentajeInteres = table.Column<double>(type: "float", nullable: false, defaultValue: 0.0),
                    Estado = table.Column<bool>(type: "bit", nullable: false),
                    FechaCreado = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaModificado = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tarjeta", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Transaccion",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tipo = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Descripcion = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Monto = table.Column<decimal>(type: "decimal(16,4)", nullable: false, defaultValue: 0m),
                    IdTarjeta = table.Column<long>(type: "bigint", nullable: false, defaultValue: 0L),
                    Estado = table.Column<bool>(type: "bit", nullable: false),
                    FechaCreado = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaModificado = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transaccion", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tarjeta");

            migrationBuilder.DropTable(
                name: "Transaccion");
        }
    }
}
