using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DUNPLab.API.Migrations
{
    /// <inheritdoc />
    public partial class zahtevSubstanca : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ZahtevSubstance",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ZahtevId = table.Column<int>(type: "int", nullable: true),
                    SubstancaId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZahtevSubstance", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ZahtevSubstance_Supstance_SubstancaId",
                        column: x => x.SubstancaId,
                        principalTable: "Supstance",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ZahtevSubstance_Zahtevi_ZahtevId",
                        column: x => x.ZahtevId,
                        principalTable: "Zahtevi",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ZahtevSubstance_SubstancaId",
                table: "ZahtevSubstance",
                column: "SubstancaId");

            migrationBuilder.CreateIndex(
                name: "IX_ZahtevSubstance_ZahtevId",
                table: "ZahtevSubstance",
                column: "ZahtevId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ZahtevSubstance");
        }
    }
}
