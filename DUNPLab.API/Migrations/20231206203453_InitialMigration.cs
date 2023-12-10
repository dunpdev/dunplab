using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DUNPLab.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pacijenti",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Ime = table.Column<string>(type: "text", nullable: false),
                    Prezime = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    DatumRodjenja = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Adresa = table.Column<string>(type: "text", nullable: false),
                    Grad = table.Column<string>(type: "text", nullable: false),
                    Telefon = table.Column<string>(type: "text", nullable: false),
                    Pol = table.Column<string>(type: "text", nullable: false),
                    JMBG = table.Column<string>(type: "text", nullable: false),
                    BrojDokumenta = table.Column<string>(type: "text", nullable: false),
                    DatumIstekaDokumenta = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pacijenti", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RezultatiOdMasine",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ImeIPrezime = table.Column<string>(type: "text", nullable: false),
                    KodEpruvete = table.Column<string>(type: "text", nullable: false),
                    DatumVreme = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    JesuLiPrebaceni = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RezultatiOdMasine", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Supstance",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Naziv = table.Column<string>(type: "text", nullable: false),
                    Oznaka = table.Column<string>(type: "text", nullable: false),
                    Opis = table.Column<string>(type: "text", nullable: false),
                    Tip = table.Column<string>(type: "text", nullable: false),
                    DonjaGranica = table.Column<double>(type: "double precision", nullable: true),
                    GornjaGranica = table.Column<double>(type: "double precision", nullable: true),
                    MetodTestiranja = table.Column<string>(type: "text", nullable: false),
                    Cena = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Supstance", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Testiranja",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Naziv = table.Column<string>(type: "text", nullable: false),
                    UkupnaCena = table.Column<double>(type: "double precision", nullable: false),
                    NacinPlacanja = table.Column<string>(type: "text", nullable: false),
                    TestOdradio = table.Column<string>(type: "text", nullable: false),
                    JesuLiPotvrdjeniSviUzorci = table.Column<bool>(type: "boolean", nullable: true),
                    DatumVremeTestiranja = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DatumVremeRezultata = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Status = table.Column<string>(type: "text", nullable: false),
                    BrojSobe = table.Column<string>(type: "text", nullable: false),
                    Izmenio = table.Column<string>(type: "text", nullable: false),
                    IzmenioDatumVreme = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Testiranja", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VrednostiOdMasine",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RezultatOdMasineId = table.Column<int>(type: "integer", nullable: false),
                    OznakaSubstance = table.Column<string>(type: "text", nullable: false),
                    Vrednost = table.Column<double>(type: "double precision", nullable: false),
                    JeLiBiloGreske = table.Column<bool>(type: "boolean", nullable: false)
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
                });

            migrationBuilder.CreateTable(
                name: "Uzorci",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Naziv = table.Column<string>(type: "text", nullable: false),
                    KodEpruvete = table.Column<string>(type: "text", nullable: false),
                    MetodTestiranja = table.Column<string>(type: "text", nullable: false),
                    KonacanRezultat = table.Column<string>(type: "text", nullable: false),
                    Komentar = table.Column<string>(type: "text", nullable: false),
                    Cena = table.Column<double>(type: "double precision", nullable: true),
                    Kutija = table.Column<string>(type: "text", nullable: false),
                    IdTestiranja = table.Column<int>(type: "integer", nullable: false),
                    Izmenio = table.Column<string>(type: "text", nullable: false),
                    IzmenioDatumVreme = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
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
                name: "Rezultati",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    JeLiUGranicama = table.Column<bool>(type: "boolean", nullable: true),
                    Vrednost = table.Column<double>(type: "double precision", nullable: true),
                    IdUzorka = table.Column<int>(type: "integer", nullable: false),
                    IdSupstance = table.Column<int>(type: "integer", nullable: false)
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

            migrationBuilder.CreateIndex(
                name: "IX_Rezultati_IdSupstance",
                table: "Rezultati",
                column: "IdSupstance");

            migrationBuilder.CreateIndex(
                name: "IX_Rezultati_IdUzorka",
                table: "Rezultati",
                column: "IdUzorka");

            migrationBuilder.CreateIndex(
                name: "IX_Uzorci_IdTestiranja",
                table: "Uzorci",
                column: "IdTestiranja");

            migrationBuilder.CreateIndex(
                name: "IX_VrednostiOdMasine_RezultatOdMasineId",
                table: "VrednostiOdMasine",
                column: "RezultatOdMasineId");
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
                name: "Supstance");

            migrationBuilder.DropTable(
                name: "Uzorci");

            migrationBuilder.DropTable(
                name: "RezultatiOdMasine");

            migrationBuilder.DropTable(
                name: "Testiranja");
        }
    }
}
