using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DUNPLab.API.Migrations
{
    /// <inheritdoc />
    public partial class updateSeedDataTestiranje : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Testiranja",
                keyColumn: "Id",
                keyValue: 1,
                column: "ZahtevId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Testiranja",
                keyColumn: "Id",
                keyValue: 2,
                column: "ZahtevId",
                value: 2);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
        }
    }
}
