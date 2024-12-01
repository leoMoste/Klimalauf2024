using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kreislauf.Migrations
{
    public partial class addTheRundenAnzahlToBarCodeT : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
              name: "RundenAnzahl",
              table: "Barcode",
              type: "float",
              nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
              name: "RundenAnzahl",
              table: "Barcode");
        }
    }
}
