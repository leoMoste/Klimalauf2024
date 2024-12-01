using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kreislauf.Migrations
{
    public partial class removeLauftable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Drop foreign keys related to the Lauf table
            migrationBuilder.DropForeignKey(
                name: "FK_Personen_Laeufe_Lauf_Id",
                table: "Personen");

            migrationBuilder.DropForeignKey(
                name: "FK_Barcode_Laeufe_LaufId",
                table: "Barcode");

            // Drop indexes related to the Lauf table
            migrationBuilder.DropIndex(
                name: "IX_Personen_Lauf_Id",
                table: "Personen");

            migrationBuilder.DropIndex(
                name: "IX_Barcode_LaufId",
                table: "Barcode");

            // Drop the Lauf table
            migrationBuilder.DropTable(
                name: "Laeufe");

            // Drop columns referencing the Lauf table
            migrationBuilder.DropColumn(
                name: "Lauf_Id",
                table: "Personen");

            migrationBuilder.DropColumn(
                name: "LaufId",
                table: "Barcode");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Recreate the Lauf table
            migrationBuilder.CreateTable(
                name: "Laeufe",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    RundenAnzahl = table.Column<double>(type: "double", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Laeufe", x => x.Id);
                });

            // Add columns referencing the Lauf table back to Personen and Barcode
            migrationBuilder.AddColumn<int>(
                name: "Lauf_Id",
                table: "Personen",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LaufId",
                table: "Barcode",
                type: "int",
                nullable: false,
                defaultValue: 0);

            // Recreate indexes
            migrationBuilder.CreateIndex(
                name: "IX_Personen_Lauf_Id",
                table: "Personen",
                column: "Lauf_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Barcode_LaufId",
                table: "Barcode",
                column: "LaufId");

            // Recreate foreign keys
            migrationBuilder.AddForeignKey(
                name: "FK_Personen_Laeufe_Lauf_Id",
                table: "Personen",
                column: "Lauf_Id",
                principalTable: "Laeufe",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Barcode_Laeufe_LaufId",
                table: "Barcode",
                column: "LaufId",
                principalTable: "Laeufe",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
