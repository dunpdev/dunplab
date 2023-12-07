using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DUNPLab.API.Migrations
{
    /// <inheritdoc />
    public partial class relationTestiranjeZahtev : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Zahtevi_TestiranjeId",
                table: "Zahtevi");

            migrationBuilder.AddColumn<int>(
                name: "ZahtevId",
                table: "Testiranja",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Testiranja",
                keyColumn: "Id",
                keyValue: 1,
                column: "ZahtevId",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Testiranja",
                keyColumn: "Id",
                keyValue: 2,
                column: "ZahtevId",
                value: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Zahtevi_TestiranjeId",
                table: "Zahtevi",
                column: "TestiranjeId",
                unique: true,
                filter: "[TestiranjeId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Zahtevi_TestiranjeId",
                table: "Zahtevi");

            migrationBuilder.DropColumn(
                name: "ZahtevId",
                table: "Testiranja");

            migrationBuilder.CreateIndex(
                name: "IX_Zahtevi_TestiranjeId",
                table: "Zahtevi",
                column: "TestiranjeId");
        }
    }
}
