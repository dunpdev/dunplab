using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DUNPLab.API.Migrations
{
    /// <inheritdoc />
    public partial class AddMoreKeys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VrednostiOdMasine_Supstance_SupstancaId",
                table: "VrednostiOdMasine");

            migrationBuilder.DropIndex(
                name: "IX_VrednostiOdMasine_SupstancaId",
                table: "VrednostiOdMasine");

            migrationBuilder.DropColumn(
                name: "SupstancaId",
                table: "VrednostiOdMasine");

            migrationBuilder.DropColumn(
                name: "Ime",
                table: "RezultatiOdMasine");

            migrationBuilder.RenameColumn(
                name: "Prezime",
                table: "RezultatiOdMasine",
                newName: "ImeIPrezime");

            migrationBuilder.AddColumn<string>(
                name: "OznakaSubstance",
                table: "VrednostiOdMasine",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SubstancaOznaka",
                table: "VrednostiOdMasine",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Oznaka",
                table: "Supstance",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Supstance_Oznaka",
                table: "Supstance",
                column: "Oznaka");

            migrationBuilder.CreateIndex(
                name: "IX_VrednostiOdMasine_SubstancaOznaka",
                table: "VrednostiOdMasine",
                column: "SubstancaOznaka");

            migrationBuilder.AddForeignKey(
                name: "FK_VrednostiOdMasine_Supstance_SubstancaOznaka",
                table: "VrednostiOdMasine",
                column: "SubstancaOznaka",
                principalTable: "Supstance",
                principalColumn: "Oznaka");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VrednostiOdMasine_Supstance_SubstancaOznaka",
                table: "VrednostiOdMasine");

            migrationBuilder.DropIndex(
                name: "IX_VrednostiOdMasine_SubstancaOznaka",
                table: "VrednostiOdMasine");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Supstance_Oznaka",
                table: "Supstance");

            migrationBuilder.DropColumn(
                name: "OznakaSubstance",
                table: "VrednostiOdMasine");

            migrationBuilder.DropColumn(
                name: "SubstancaOznaka",
                table: "VrednostiOdMasine");

            migrationBuilder.RenameColumn(
                name: "ImeIPrezime",
                table: "RezultatiOdMasine",
                newName: "Prezime");

            migrationBuilder.AddColumn<int>(
                name: "SupstancaId",
                table: "VrednostiOdMasine",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Oznaka",
                table: "Supstance",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "Ime",
                table: "RezultatiOdMasine",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_VrednostiOdMasine_SupstancaId",
                table: "VrednostiOdMasine",
                column: "SupstancaId");

            migrationBuilder.AddForeignKey(
                name: "FK_VrednostiOdMasine_Supstance_SupstancaId",
                table: "VrednostiOdMasine",
                column: "SupstancaId",
                principalTable: "Supstance",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
