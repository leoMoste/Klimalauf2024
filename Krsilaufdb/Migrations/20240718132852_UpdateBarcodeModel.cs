using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kreislauf.Migrations
{
    public partial class UpdateBarcodeModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
               name: "Barcodes",
               columns: table => new
               {
                   Id = table.Column<int>(nullable: false)
                       .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                   PersonId = table.Column<int>(nullable: false),
                   Value = table.Column<string>(maxLength: 255, nullable: false),
                   Type = table.Column<string>(maxLength: 100, nullable: true),
                   LaufId = table.Column<int>(nullable: false)
               },
               constraints: table =>
               {
                   table.PrimaryKey("PK_Barcodes", x => x.Id);
                   table.ForeignKey(
                       name: "FK_Barcodes_Personen_PersonId",
                       column: x => x.PersonId,
                       principalTable: "Personen",
                       principalColumn: "Id",
                       onDelete: ReferentialAction.Cascade);
                   table.ForeignKey(
                       name: "FK_Barcodes_Laeufe_LaufId",
                       column: x => x.LaufId,
                       principalTable: "Laeufe",
                       principalColumn: "Id",
                       onDelete: ReferentialAction.Cascade);
               });

            migrationBuilder.CreateIndex(
                name: "IX_Barcodes_PersonId",
                table: "Barcodes",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Barcodes_LaufId",
                table: "Barcodes",
                column: "LaufId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Barcodes");
        }
    }
    }

