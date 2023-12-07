using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DUNPLab.API.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pacijenti",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prezime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DatumRodjenja = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Adresa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Grad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Pol = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JMBG = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BrojDokumenta = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DatumIstekaDokumenta = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pacijenti", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Testiranja",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ZahtevId = table.Column<int>(type: "int", nullable: false),
                    UkupnaCena = table.Column<double>(type: "float", nullable: false),
                    NacinPlacanja = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TestOdradio = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JesuLiPotvrdjeniSviUzorci = table.Column<bool>(type: "bit", nullable: true),
                    DatumVremeTestiranja = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DatumVremeRezultata = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BrojSobe = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Izmenio = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IzmenioDatumVreme = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Testiranja", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Uzorci",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KodEpruvete = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MetodTestiranja = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KonacanRezultat = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Komentar = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cena = table.Column<double>(type: "float", nullable: true),
                    Kutija = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdTestiranja = table.Column<int>(type: "int", nullable: false),
                    Izmenio = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IzmenioDatumVreme = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Uzorci", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Uzorci_Testiranja_IdTestiranja",
                        column: x => x.IdTestiranja,
                        principalTable: "Testiranja",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Zahtevi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DatumTestiranja = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Metode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TestiranjeId = table.Column<int>(type: "int", nullable: true),
                    PacijentId = table.Column<int>(type: "int", nullable: true),
                    JeLiObradjen = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zahtevi", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Zahtevi_Pacijenti_PacijentId",
                        column: x => x.PacijentId,
                        principalTable: "Pacijenti",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Zahtevi_Testiranja_TestiranjeId",
                        column: x => x.TestiranjeId,
                        principalTable: "Testiranja",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Supstance",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Oznaka = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Opis = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tip = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DonjaGranica = table.Column<double>(type: "float", nullable: true),
                    GornjaGranica = table.Column<double>(type: "float", nullable: true),
                    MetodTestiranja = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cena = table.Column<double>(type: "float", nullable: false),
                    ZahtevId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Supstance", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Supstance_Zahtevi_ZahtevId",
                        column: x => x.ZahtevId,
                        principalTable: "Zahtevi",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Rezultati",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JeLiUGranicama = table.Column<bool>(type: "bit", nullable: true),
                    Vrednost = table.Column<double>(type: "float", nullable: true),
                    IdUzorka = table.Column<int>(type: "int", nullable: false),
                    IdSupstance = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rezultati", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rezultati_Supstance_IdSupstance",
                        column: x => x.IdSupstance,
                        principalTable: "Supstance",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Rezultati_Uzorci_IdUzorka",
                        column: x => x.IdUzorka,
                        principalTable: "Uzorci",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                columns: new[] { "Id", "BrojSobe", "DatumVremeRezultata", "DatumVremeTestiranja", "Izmenio", "IzmenioDatumVreme", "JesuLiPotvrdjeniSviUzorci", "NacinPlacanja", "Naziv", "Status", "TestOdradio", "UkupnaCena", "ZahtevId" },
                values: new object[,]
                {
                    { 1, "101", null, new DateTime(2023, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), "Modifier 1", new DateTime(2023, 1, 1, 11, 0, 0, 0, DateTimeKind.Unspecified), false, "Card", "Test 1", "In Progress", "Tester 1", 300.0, 1 },
                    { 2, "102", null, new DateTime(2023, 2, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), "Modifier 2", new DateTime(2023, 2, 1, 11, 0, 0, 0, DateTimeKind.Unspecified), false, "Cash", "Test 2", "In Progress", "Tester 2", 400.0, 2 }
                });

            migrationBuilder.InsertData(
                table: "Uzorci",
                columns: new[] { "Id", "Cena", "IdTestiranja", "Izmenio", "IzmenioDatumVreme", "KodEpruvete", "Komentar", "KonacanRezultat", "Kutija", "MetodTestiranja", "Naziv" },
                values: new object[,]
                {
                    { 1, 100.0, 1, "Modifier 1", new DateTime(2023, 1, 1, 12, 0, 0, 0, DateTimeKind.Unspecified), "E1", "Comment 1", "Result 1", "BX20230101000001", "Method 1", "Sample 1" },
                    { 2, 200.0, 2, "Modifier 2", new DateTime(2023, 2, 1, 12, 0, 0, 0, DateTimeKind.Unspecified), "E2", "Comment 2", "Result 2", "BX20230201000002", "Method 2", "Sample 2" }
                });

            migrationBuilder.InsertData(
                table: "Zahtevi",
                columns: new[] { "Id", "DatumTestiranja", "JeLiObradjen", "Metode", "PacijentId", "TestiranjeId" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "[]", 1, null },
                    { 2, new DateTime(2023, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "[]", 2, null }
                });

            migrationBuilder.InsertData(
                table: "ZahtevSubstance",
                columns: new[] { "Id", "SubstancaId", "ZahtevId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 2, 1 },
                    { 3, 1, 2 },
                    { 4, 2, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rezultati_IdSupstance",
                table: "Rezultati",
                column: "IdSupstance");

            migrationBuilder.CreateIndex(
                name: "IX_Rezultati_IdUzorka",
                table: "Rezultati",
                column: "IdUzorka");

            migrationBuilder.CreateIndex(
                name: "IX_Supstance_ZahtevId",
                table: "Supstance",
                column: "ZahtevId");

            migrationBuilder.CreateIndex(
                name: "IX_Uzorci_IdTestiranja",
                table: "Uzorci",
                column: "IdTestiranja");

            migrationBuilder.CreateIndex(
                name: "IX_Zahtevi_PacijentId",
                table: "Zahtevi",
                column: "PacijentId");

            migrationBuilder.CreateIndex(
                name: "IX_Zahtevi_TestiranjeId",
                table: "Zahtevi",
                column: "TestiranjeId",
                unique: true,
                filter: "[TestiranjeId] IS NOT NULL");

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
                name: "Rezultati");

            migrationBuilder.DropTable(
                name: "ZahtevSubstance");

            migrationBuilder.DropTable(
                name: "Uzorci");

            migrationBuilder.DropTable(
                name: "Supstance");

            migrationBuilder.DropTable(
                name: "Zahtevi");

            migrationBuilder.DropTable(
                name: "Pacijenti");

            migrationBuilder.DropTable(
                name: "Testiranja");
        }
    }
}
