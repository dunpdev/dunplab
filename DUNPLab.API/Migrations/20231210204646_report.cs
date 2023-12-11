using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DUNPLab.API.Migrations
{
    /// <inheritdoc />
    public partial class report : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Emails_File_IdFile",
                table: "Emails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_File",
                table: "File");

            migrationBuilder.RenameTable(
                name: "File",
                newName: "Files");

            migrationBuilder.AddColumn<int>(
                name: "PacijentId",
                table: "Uzorci",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "DaLiJeArhiviran",
                table: "Pacijenti",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Files",
                table: "Files",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Reports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idPacijent = table.Column<int>(type: "int", nullable: false),
                    IdFile = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reports_Files_IdFile",
                        column: x => x.IdFile,
                        principalTable: "Files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reports_Pacijenti_idPacijent",
                        column: x => x.idPacijent,
                        principalTable: "Pacijenti",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Pacijenti",
                keyColumn: "Id",
                keyValue: 1,
                column: "DaLiJeArhiviran",
                value: false);

            migrationBuilder.UpdateData(
                table: "Pacijenti",
                keyColumn: "Id",
                keyValue: 2,
                column: "DaLiJeArhiviran",
                value: false);

            migrationBuilder.UpdateData(
                table: "Uzorci",
                keyColumn: "Id",
                keyValue: 1,
                column: "PacijentId",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Uzorci",
                keyColumn: "Id",
                keyValue: 2,
                column: "PacijentId",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Zahtevi",
                keyColumn: "Id",
                keyValue: 1,
                column: "TestiranjeId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Zahtevi",
                keyColumn: "Id",
                keyValue: 2,
                column: "TestiranjeId",
                value: 2);

            migrationBuilder.CreateIndex(
                name: "IX_Uzorci_PacijentId",
                table: "Uzorci",
                column: "PacijentId");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_IdFile",
                table: "Reports",
                column: "IdFile");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_idPacijent",
                table: "Reports",
                column: "idPacijent");

            migrationBuilder.AddForeignKey(
                name: "FK_Emails_Files_IdFile",
                table: "Emails",
                column: "IdFile",
                principalTable: "Files",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Uzorci_Pacijenti_PacijentId",
                table: "Uzorci",
                column: "PacijentId",
                principalTable: "Pacijenti",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Emails_Files_IdFile",
                table: "Emails");

            migrationBuilder.DropForeignKey(
                name: "FK_Uzorci_Pacijenti_PacijentId",
                table: "Uzorci");

            migrationBuilder.DropTable(
                name: "Reports");

            migrationBuilder.DropIndex(
                name: "IX_Uzorci_PacijentId",
                table: "Uzorci");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Files",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "PacijentId",
                table: "Uzorci");

            migrationBuilder.DropColumn(
                name: "DaLiJeArhiviran",
                table: "Pacijenti");

            migrationBuilder.RenameTable(
                name: "Files",
                newName: "File");

            migrationBuilder.AddPrimaryKey(
                name: "PK_File",
                table: "File",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Zahtevi",
                keyColumn: "Id",
                keyValue: 1,
                column: "TestiranjeId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Zahtevi",
                keyColumn: "Id",
                keyValue: 2,
                column: "TestiranjeId",
                value: null);

            migrationBuilder.AddForeignKey(
                name: "FK_Emails_File_IdFile",
                table: "Emails",
                column: "IdFile",
                principalTable: "File",
                principalColumn: "Id");
        }
    }
}
