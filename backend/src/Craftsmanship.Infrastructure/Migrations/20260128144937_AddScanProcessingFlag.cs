using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Craftsmanship.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddScanProcessingFlag : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsProcessed",
                table: "Scans",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsProcessed",
                table: "Scans");
        }
    }
}
