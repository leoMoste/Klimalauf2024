using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kreislauf.Migrations
{
    public partial class AddKaSaLTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Klasse_Id",
                table: "Personen",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Lauf_Id",
                table: "Personen",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Lauf",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    RundenAnzahl = table.Column<double>(type: "double", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lauf", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Schule",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Stadt = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schule", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Klasse",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Schule_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Klasse", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Klasse_Schule_Schule_Id",
                        column: x => x.Schule_Id,
                        principalTable: "Schule",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Personen_Klasse_Id",
                table: "Personen",
                column: "Klasse_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Personen_Lauf_Id",
                table: "Personen",
                column: "Lauf_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Klasse_Schule_Id",
                table: "Klasse",
                column: "Schule_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Personen_Klasse_Klasse_Id",
                table: "Personen",
                column: "Klasse_Id",
                principalTable: "Klasse",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Personen_Lauf_Lauf_Id",
                table: "Personen",
                column: "Lauf_Id",
                principalTable: "Lauf",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Personen_Klasse_Klasse_Id",
                table: "Personen");

            migrationBuilder.DropForeignKey(
                name: "FK_Personen_Lauf_Lauf_Id",
                table: "Personen");

            migrationBuilder.DropTable(
                name: "Klasse");

            migrationBuilder.DropTable(
                name: "Lauf");

            migrationBuilder.DropTable(
                name: "Schule");

            migrationBuilder.DropIndex(
                name: "IX_Personen_Klasse_Id",
                table: "Personen");

            migrationBuilder.DropIndex(
                name: "IX_Personen_Lauf_Id",
                table: "Personen");

            migrationBuilder.DropColumn(
                name: "Klasse_Id",
                table: "Personen");

            migrationBuilder.DropColumn(
                name: "Lauf_Id",
                table: "Personen");
        }
    }
}
