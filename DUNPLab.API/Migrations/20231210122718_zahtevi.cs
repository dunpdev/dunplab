using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DUNPLab.API.Migrations
{
    /// <inheritdoc />
    public partial class zahtevi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ZahtevId",
                table: "Supstance",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Obradjen",
                table: "Rezultati",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsSent = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
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
                name: "Recipients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NotificationId = table.Column<int>(type: "int", nullable: false),
                    RecipientName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Recipients_Notifications_NotificationId",
                        column: x => x.NotificationId,
                        principalTable: "Notifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Supstance_ZahtevId",
                table: "Supstance",
                column: "ZahtevId");

            migrationBuilder.CreateIndex(
                name: "IX_Recipients_NotificationId",
                table: "Recipients",
                column: "NotificationId");

            migrationBuilder.CreateIndex(
                name: "IX_Zahtevi_PacijentId",
                table: "Zahtevi",
                column: "PacijentId");

            migrationBuilder.CreateIndex(
                name: "IX_Zahtevi_TestiranjeId",
                table: "Zahtevi",
                column: "TestiranjeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Supstance_Zahtevi_ZahtevId",
                table: "Supstance",
                column: "ZahtevId",
                principalTable: "Zahtevi",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Supstance_Zahtevi_ZahtevId",
                table: "Supstance");

            migrationBuilder.DropTable(
                name: "Recipients");

            migrationBuilder.DropTable(
                name: "Zahtevi");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_Supstance_ZahtevId",
                table: "Supstance");

            migrationBuilder.DropColumn(
                name: "ZahtevId",
                table: "Supstance");

            migrationBuilder.DropColumn(
                name: "Obradjen",
                table: "Rezultati");
        }
    }
}
