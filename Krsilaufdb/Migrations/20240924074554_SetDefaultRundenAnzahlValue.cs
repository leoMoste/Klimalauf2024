using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kreislauf.Migrations
{
    public partial class SetDefaultRundenAnzahlValue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BarcodeNew_Personen_PersonId",
                table: "BarcodeNew");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BarcodeNew",
                table: "BarcodeNew");

            migrationBuilder.RenameTable(
                name: "BarcodeNew",
                newName: "Barcodes");

            migrationBuilder.RenameIndex(
                name: "IX_BarcodeNew_PersonId",
                table: "Barcodes",
                newName: "IX_Barcodes_PersonId");

            migrationBuilder.AlterColumn<int>(
                name: "RundenAnzahl",
                table: "Barcodes",
                type: "int",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "double",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Barcodes",
                table: "Barcodes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Barcodes_Personen_PersonId",
                table: "Barcodes",
                column: "PersonId",
                principalTable: "Personen",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Barcodes_Personen_PersonId",
                table: "Barcodes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Barcodes",
                table: "Barcodes");

            migrationBuilder.RenameTable(
                name: "Barcodes",
                newName: "BarcodeNew");

            migrationBuilder.RenameIndex(
                name: "IX_Barcodes_PersonId",
                table: "BarcodeNew",
                newName: "IX_BarcodeNew_PersonId");

            migrationBuilder.AlterColumn<double>(
                name: "RundenAnzahl",
                table: "BarcodeNew",
                type: "double",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_BarcodeNew",
                table: "BarcodeNew",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BarcodeNew_Personen_PersonId",
                table: "BarcodeNew",
                column: "PersonId",
                principalTable: "Personen",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
