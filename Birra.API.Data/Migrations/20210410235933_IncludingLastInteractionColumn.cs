using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Birra.API.Data.Migrations
{
    public partial class IncludingLastInteractionColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LAST_INTERACTION",
                table: "TB_CURRENT_SESSIONS",
                type: "DATETIME",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LAST_INTERACTION",
                table: "TB_CURRENT_SESSIONS");
        }
    }
}
