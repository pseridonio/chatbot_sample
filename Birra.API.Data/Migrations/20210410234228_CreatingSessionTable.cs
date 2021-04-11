using Microsoft.EntityFrameworkCore.Migrations;

namespace Birra.API.Data.Migrations
{
    public partial class CreatingSessionTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TB_CURRENT_SESSIONS",
                columns: table => new
                {
                    CURRENT_SESSION_ID = table.Column<string>(type: "VARCHAR(50)", nullable: false),
                    CURRENT_QUESTION_ID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CURRENT_SESSIONS", x => x.CURRENT_SESSION_ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_CURRENT_SESSIONS");
        }
    }
}
