using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DUNPLab.API.Migrations
{
    /// <inheritdoc />
    public partial class dataSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Pacijenti",
                columns: new[] { "Id", "Adresa", "BrojDokumenta", "DatumIstekaDokumenta", "DatumRodjenja", "Email", "Grad", "Ime", "JMBG", "Pol", "Prezime", "Telefon" },
                values: new object[,]
                {
                    { 1, "123 Main St", "AB123456", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1980, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "john.doe@example.com", "Belgrade", "John", "1234567890123", "M", "Doe", "1234567890" },
                    { 2, "456 Elm St", "CD987654", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1985, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "jane.doe@example.com", "Novi Sad", "Jane", "9876543210987", "F", "Doe", "0987654321" }
                });

            migrationBuilder.InsertData(
                table: "Supstance",
                columns: new[] { "Id", "Cena", "DonjaGranica", "GornjaGranica", "MetodTestiranja", "Naziv", "Opis", "Oznaka", "Tip", "ZahtevId" },
                values: new object[,]
                {
                    { 1, 100.0, 0.10000000000000001, 1.0, "Method 1", "Substance 1", "Description for Substance 1", "S1", "Type 1", null },
                    { 2, 200.0, 0.20000000000000001, 2.0, "Method 2", "Substance 2", "Description for Substance 2", "S2", "Type 2", null }
                });

            migrationBuilder.InsertData(
                table: "Testiranja",
                columns: new[] { "Id", "BrojSobe", "DatumVremeRezultata", "DatumVremeTestiranja", "Izmenio", "IzmenioDatumVreme", "JesuLiPotvrdjeniSviUzorci", "NacinPlacanja", "Naziv", "Status", "TestOdradio", "UkupnaCena" },
                values: new object[,]
                {
                    { 1, "101", null, new DateTime(2023, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), "Modifier 1", new DateTime(2023, 1, 1, 11, 0, 0, 0, DateTimeKind.Unspecified), false, "Card", "Test 1", "In Progress", "Tester 1", 300.0 },
                    { 2, "102", null, new DateTime(2023, 2, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), "Modifier 2", new DateTime(2023, 2, 1, 11, 0, 0, 0, DateTimeKind.Unspecified), false, "Cash", "Test 2", "In Progress", "Tester 2", 400.0 }
                });

            migrationBuilder.InsertData(
                table: "Zahtevi",
                columns: new[] { "Id", "DatumTestiranja", "JeLiObradjen", "Metode", "PacijentId", "TestiranjeId" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "[]", 1, null },
                    { 2, new DateTime(2023, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "[]", 2, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Supstance",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Supstance",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Testiranja",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Testiranja",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Zahtevi",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Zahtevi",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Pacijenti",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Pacijenti",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
