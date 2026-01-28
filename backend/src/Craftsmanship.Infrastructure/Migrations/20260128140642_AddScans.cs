using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Craftsmanship.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddScans : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Scans",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    TeamKey = table.Column<string>(type: "TEXT", nullable: false),
                    Aspect = table.Column<int>(type: "INTEGER", nullable: false),
                    CodebasePath = table.Column<string>(type: "TEXT", nullable: false),
                    CommitId = table.Column<string>(type: "TEXT", nullable: false),
                    ScanTimestamp = table.Column<DateTime>(type: "TEXT", nullable: false),
                    RawPayload = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Scans", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Scans");
        }
    }
}
