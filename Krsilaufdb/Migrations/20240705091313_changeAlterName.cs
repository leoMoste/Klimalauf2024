using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kreislauf.Migrations
{
    public partial class changeAlterName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Geburtsdatum",
                table: "Personen");

            migrationBuilder.AddColumn<int>(
                name: "Lebensalter",
                table: "Personen",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Lebensalter",
                table: "Personen");

            migrationBuilder.AddColumn<DateOnly>(
                name: "Geburtsdatum",
                table: "Personen",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));
        }
    }
}
