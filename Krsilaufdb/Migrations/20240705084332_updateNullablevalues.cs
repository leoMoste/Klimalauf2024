using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kreislauf.Migrations
{
    public partial class updateNullablevalues : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Personen_Klassen_Klasse_Id",
                table: "Personen");

            migrationBuilder.DropForeignKey(
                name: "FK_Personen_Laeufe_Lauf_Id",
                table: "Personen");

            migrationBuilder.AlterColumn<int>(
                name: "Lauf_Id",
                table: "Personen",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Klasse_Id",
                table: "Personen",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Personen_Klassen_Klasse_Id",
                table: "Personen",
                column: "Klasse_Id",
                principalTable: "Klassen",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Personen_Laeufe_Lauf_Id",
                table: "Personen",
                column: "Lauf_Id",
                principalTable: "Laeufe",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Personen_Klassen_Klasse_Id",
                table: "Personen");

            migrationBuilder.DropForeignKey(
                name: "FK_Personen_Laeufe_Lauf_Id",
                table: "Personen");

            migrationBuilder.AlterColumn<int>(
                name: "Lauf_Id",
                table: "Personen",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Klasse_Id",
                table: "Personen",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Personen_Klassen_Klasse_Id",
                table: "Personen",
                column: "Klasse_Id",
                principalTable: "Klassen",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Personen_Laeufe_Lauf_Id",
                table: "Personen",
                column: "Lauf_Id",
                principalTable: "Laeufe",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
