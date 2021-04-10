using Microsoft.EntityFrameworkCore.Migrations;

namespace Birra.API.Data.Migrations
{
    public partial class InitialDatabaseSetup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TB_ANSWERS",
                columns: table => new
                {
                    ANSWER_ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ANSWER_TEXT = table.Column<string>(type: "VARCHAR(250)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ANSWERS", x => x.ANSWER_ID);
                });

            migrationBuilder.CreateTable(
                name: "TB_MESSAGES",
                columns: table => new
                {
                    MESSAGE_ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MESSAGE_TEXT = table.Column<string>(type: "VARCHAR(250)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MESSAGES", x => x.MESSAGE_ID);
                });

            migrationBuilder.CreateTable(
                name: "TB_QUESTIONS",
                columns: table => new
                {
                    QUESTION_ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    QUESTION_TEXT = table.Column<string>(type: "VARCHAR(250)", nullable: false),
                    QuestionHint = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QUESTIONS", x => x.QUESTION_ID);
                });

            migrationBuilder.CreateTable(
                name: "TB_RELATION_QUESTION_ANSWERS",
                columns: table => new
                {
                    QUESTION_ID = table.Column<int>(type: "INTEGER", nullable: false),
                    ANSWER_ID = table.Column<int>(type: "INTEGER", nullable: false),
                    NEXT_QUESTION_ID = table.Column<int>(type: "INTEGER", nullable: true),
                    MESSAGE_ID = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RELATION_QUESTION_ANSWERS", x => new { x.QUESTION_ID, x.ANSWER_ID });
                    table.ForeignKey(
                        name: "FK1_RELATION_QUESTION_ANSWERS",
                        column: x => x.QUESTION_ID,
                        principalTable: "TB_QUESTIONS",
                        principalColumn: "QUESTION_ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK2_RELATION_QUESTION_ANSWERS",
                        column: x => x.ANSWER_ID,
                        principalTable: "TB_ANSWERS",
                        principalColumn: "ANSWER_ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK3_RELATION_QUESTION_ANSWERS",
                        column: x => x.MESSAGE_ID,
                        principalTable: "TB_MESSAGES",
                        principalColumn: "MESSAGE_ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK4_RELATION_QUESTION_ANSWERS",
                        column: x => x.NEXT_QUESTION_ID,
                        principalTable: "TB_QUESTIONS",
                        principalColumn: "QUESTION_ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TB_RELATION_QUESTION_ANSWERS_ANSWER_ID",
                table: "TB_RELATION_QUESTION_ANSWERS",
                column: "ANSWER_ID");

            migrationBuilder.CreateIndex(
                name: "IX_TB_RELATION_QUESTION_ANSWERS_MESSAGE_ID",
                table: "TB_RELATION_QUESTION_ANSWERS",
                column: "MESSAGE_ID");

            migrationBuilder.CreateIndex(
                name: "IX_TB_RELATION_QUESTION_ANSWERS_NEXT_QUESTION_ID",
                table: "TB_RELATION_QUESTION_ANSWERS",
                column: "NEXT_QUESTION_ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_RELATION_QUESTION_ANSWERS");

            migrationBuilder.DropTable(
                name: "TB_QUESTIONS");

            migrationBuilder.DropTable(
                name: "TB_ANSWERS");

            migrationBuilder.DropTable(
                name: "TB_MESSAGES");
        }
    }
}
