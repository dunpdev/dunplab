using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DUNPLab.API.Migrations
{
    /// <inheritdoc />
    public partial class removeKeys2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VrednostiOdMasine_Supstance_SubstancaId",
                table: "VrednostiOdMasine");

            migrationBuilder.DropIndex(
                name: "IX_VrednostiOdMasine_SubstancaId",
                table: "VrednostiOdMasine");

            migrationBuilder.DropColumn(
                name: "SubstancaId",
                table: "VrednostiOdMasine");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SubstancaId",
                table: "VrednostiOdMasine",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_VrednostiOdMasine_SubstancaId",
                table: "VrednostiOdMasine",
                column: "SubstancaId");

            migrationBuilder.AddForeignKey(
                name: "FK_VrednostiOdMasine_Supstance_SubstancaId",
                table: "VrednostiOdMasine",
                column: "SubstancaId",
                principalTable: "Supstance",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
