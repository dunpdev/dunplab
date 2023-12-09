using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DUNPLab.API.Migrations
{
    /// <inheritdoc />
    public partial class RemoveKeys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RezultatiOdMasine_Uzorci_UzorakKodEpruvete",
                table: "RezultatiOdMasine");

            migrationBuilder.DropForeignKey(
                name: "FK_VrednostiOdMasine_Supstance_SubstancaOznaka",
                table: "VrednostiOdMasine");

            migrationBuilder.DropIndex(
                name: "IX_VrednostiOdMasine_SubstancaOznaka",
                table: "VrednostiOdMasine");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Uzorci_KodEpruvete",
                table: "Uzorci");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Supstance_Oznaka",
                table: "Supstance");

            migrationBuilder.DropIndex(
                name: "IX_RezultatiOdMasine_UzorakKodEpruvete",
                table: "RezultatiOdMasine");

            migrationBuilder.DropColumn(
                name: "SubstancaOznaka",
                table: "VrednostiOdMasine");

            migrationBuilder.DropColumn(
                name: "IdRezultataOdMasine",
                table: "Uzorci");

            migrationBuilder.DropColumn(
                name: "UzorakKodEpruvete",
                table: "RezultatiOdMasine");

            migrationBuilder.AddColumn<int>(
                name: "SubstancaId",
                table: "VrednostiOdMasine",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "KodEpruvete",
                table: "Uzorci",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Oznaka",
                table: "Supstance",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "SubstancaOznaka",
                table: "VrednostiOdMasine",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "KodEpruvete",
                table: "Uzorci",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "IdRezultataOdMasine",
                table: "Uzorci",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Oznaka",
                table: "Supstance",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "UzorakKodEpruvete",
                table: "RezultatiOdMasine",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Uzorci_KodEpruvete",
                table: "Uzorci",
                column: "KodEpruvete");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Supstance_Oznaka",
                table: "Supstance",
                column: "Oznaka");

            migrationBuilder.CreateIndex(
                name: "IX_VrednostiOdMasine_SubstancaOznaka",
                table: "VrednostiOdMasine",
                column: "SubstancaOznaka");

            migrationBuilder.CreateIndex(
                name: "IX_RezultatiOdMasine_UzorakKodEpruvete",
                table: "RezultatiOdMasine",
                column: "UzorakKodEpruvete",
                unique: true,
                filter: "[UzorakKodEpruvete] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_RezultatiOdMasine_Uzorci_UzorakKodEpruvete",
                table: "RezultatiOdMasine",
                column: "UzorakKodEpruvete",
                principalTable: "Uzorci",
                principalColumn: "KodEpruvete");

            migrationBuilder.AddForeignKey(
                name: "FK_VrednostiOdMasine_Supstance_SubstancaOznaka",
                table: "VrednostiOdMasine",
                column: "SubstancaOznaka",
                principalTable: "Supstance",
                principalColumn: "Oznaka");
        }
    }
}
