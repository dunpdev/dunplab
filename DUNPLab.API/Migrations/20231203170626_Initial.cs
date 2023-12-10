using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DUNPLab.API.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
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
                    Cena = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Supstance", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Testiranja",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    KodEpruvete = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MetodTestiranja = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KonacanRezultat = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Komentar = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cena = table.Column<double>(type: "float", nullable: true),
                    Kutija = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdTestiranja = table.Column<int>(type: "int", nullable: false),
                    Izmenio = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IzmenioDatumVreme = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IdRezultataOdMasine = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Uzorci", x => x.Id);
                    table.UniqueConstraint("AK_Uzorci_KodEpruvete", x => x.KodEpruvete);
                    table.ForeignKey(
                        name: "FK_Uzorci_Testiranja_IdTestiranja",
                        column: x => x.IdTestiranja,
                        principalTable: "Testiranja",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                name: "RezultatiOdMasine",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prezime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KodEpruvete = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UzorakKodEpruvete = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DatumVreme = table.Column<DateTime>(type: "datetime2", nullable: false),
                    JesuLiPrebaceni = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RezultatiOdMasine", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RezultatiOdMasine_Uzorci_UzorakKodEpruvete",
                        column: x => x.UzorakKodEpruvete,
                        principalTable: "Uzorci",
                        principalColumn: "KodEpruvete");
                });

            migrationBuilder.CreateTable(
                name: "VrednostiOdMasine",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RezultatOdMasineId = table.Column<int>(type: "int", nullable: false),
                    SupstancaId = table.Column<int>(type: "int", nullable: false),
                    Vrednost = table.Column<double>(type: "float", nullable: false),
                    JeLiBiloGreske = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VrednostiOdMasine", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VrednostiOdMasine_RezultatiOdMasine_RezultatOdMasineId",
                        column: x => x.RezultatOdMasineId,
                        principalTable: "RezultatiOdMasine",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VrednostiOdMasine_Supstance_SupstancaId",
                        column: x => x.SupstancaId,
                        principalTable: "Supstance",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                name: "IX_RezultatiOdMasine_UzorakKodEpruvete",
                table: "RezultatiOdMasine",
                column: "UzorakKodEpruvete",
                unique: true,
                filter: "[UzorakKodEpruvete] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Uzorci_IdTestiranja",
                table: "Uzorci",
                column: "IdTestiranja");

            migrationBuilder.CreateIndex(
                name: "IX_VrednostiOdMasine_RezultatOdMasineId",
                table: "VrednostiOdMasine",
                column: "RezultatOdMasineId");

            migrationBuilder.CreateIndex(
                name: "IX_VrednostiOdMasine_SupstancaId",
                table: "VrednostiOdMasine",
                column: "SupstancaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pacijenti");

            migrationBuilder.DropTable(
                name: "Rezultati");

            migrationBuilder.DropTable(
                name: "VrednostiOdMasine");

            migrationBuilder.DropTable(
                name: "RezultatiOdMasine");

            migrationBuilder.DropTable(
                name: "Supstance");

            migrationBuilder.DropTable(
                name: "Uzorci");

            migrationBuilder.DropTable(
                name: "Testiranja");
        }
    }
}
