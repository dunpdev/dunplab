using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DUNPLab.API.Migrations
{
    /// <inheritdoc />
    public partial class addReportFiles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Files",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sadrzaj = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    JeLiObrisan = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Files", x => x.Id);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_Reports_IdFile",
                table: "Reports",
                column: "IdFile");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_idPacijent",
                table: "Reports",
                column: "idPacijent");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reports");

            migrationBuilder.DropTable(
                name: "Files");
        }
    }
}
