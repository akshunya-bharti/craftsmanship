using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Craftsmanship.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddScanTeamForeignKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddUniqueConstraint(
                name: "AK_Teams_TeamKey",
                table: "Teams",
                column: "TeamKey");

            migrationBuilder.CreateIndex(
                name: "IX_Scans_TeamKey",
                table: "Scans",
                column: "TeamKey");

            migrationBuilder.AddForeignKey(
                name: "FK_Scans_Teams_TeamKey",
                table: "Scans",
                column: "TeamKey",
                principalTable: "Teams",
                principalColumn: "TeamKey",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Scans_Teams_TeamKey",
                table: "Scans");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Teams_TeamKey",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Scans_TeamKey",
                table: "Scans");
        }
    }
}
