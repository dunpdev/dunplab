using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DUNPLab.API.Migrations
{
    /// <inheritdoc />
    public partial class addReport : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PacijentId",
                table: "Uzorci",
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
                name: "IX_Uzorci_PacijentId",
                table: "Uzorci",
                column: "PacijentId");

            migrationBuilder.CreateIndex(
                name: "IX_Recipients_NotificationId",
                table: "Recipients",
                column: "NotificationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Uzorci_Pacijenti_PacijentId",
                table: "Uzorci",
                column: "PacijentId",
                principalTable: "Pacijenti",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Uzorci_Pacijenti_PacijentId",
                table: "Uzorci");

            migrationBuilder.DropTable(
                name: "Recipients");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_Uzorci_PacijentId",
                table: "Uzorci");

            migrationBuilder.DropColumn(
                name: "PacijentId",
                table: "Uzorci");

            migrationBuilder.DropColumn(
                name: "Obradjen",
                table: "Rezultati");
        }
    }
}
