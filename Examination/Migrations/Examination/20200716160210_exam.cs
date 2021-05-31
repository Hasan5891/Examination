using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Examination.Migrations.Examination
{
    public partial class exam : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PartAs",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nomi = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartAs", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Predmets",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nomi = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Predmets", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Abiturinets",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nomi = table.Column<string>(nullable: true),
                    PartAID = table.Column<int>(nullable: false),
                    isactive = table.Column<bool>(nullable: false),
                    UserID = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Price = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Abiturinets", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Abiturinets_PartAs_PartAID",
                        column: x => x.PartAID,
                        principalTable: "PartAs",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Parts",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nomi = table.Column<string>(nullable: true),
                    PredmetID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parts", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Parts_Predmets_PredmetID",
                        column: x => x.PredmetID,
                        principalTable: "Predmets",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tests",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nomi = table.Column<string>(nullable: true),
                    PartID = table.Column<int>(nullable: false),
                    isactive = table.Column<bool>(nullable: false),
                    UserID = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tests", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Tests_Parts_PartID",
                        column: x => x.PartID,
                        principalTable: "Parts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nomi = table.Column<string>(nullable: true),
                    TestID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Questions_Tests_TestID",
                        column: x => x.TestID,
                        principalTable: "Tests",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TestAs",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TestID = table.Column<int>(nullable: false),
                    TotalItems = table.Column<int>(nullable: false),
                    AbiturientID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestAs", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TestAs_Abiturinets_AbiturientID",
                        column: x => x.AbiturientID,
                        principalTable: "Abiturinets",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TestAs_Tests_TestID",
                        column: x => x.TestID,
                        principalTable: "Tests",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserExams",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserID = table.Column<int>(nullable: false),
                    UserName = table.Column<string>(nullable: true),
                    TestID = table.Column<int>(nullable: true),
                    AbiturientID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserExams", x => x.ID);
                    table.ForeignKey(
                        name: "FK_UserExams_Abiturinets_AbiturientID",
                        column: x => x.AbiturientID,
                        principalTable: "Abiturinets",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserExams_Tests_TestID",
                        column: x => x.TestID,
                        principalTable: "Tests",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Answers",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nomi = table.Column<string>(nullable: true),
                    isTrue = table.Column<bool>(nullable: false),
                    QuestionID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answers", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Answers_Questions_QuestionID",
                        column: x => x.QuestionID,
                        principalTable: "Questions",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AnsweredQuesttions",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AnswerID = table.Column<int>(nullable: true),
                    QuestionID = table.Column<int>(nullable: false),
                    UserExamsID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnsweredQuesttions", x => x.ID);
                    table.ForeignKey(
                        name: "FK_AnsweredQuesttions_Answers_AnswerID",
                        column: x => x.AnswerID,
                        principalTable: "Answers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AnsweredQuesttions_Questions_QuestionID",
                        column: x => x.QuestionID,
                        principalTable: "Questions",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnsweredQuesttions_UserExams_UserExamsID",
                        column: x => x.UserExamsID,
                        principalTable: "UserExams",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Abiturinets_PartAID",
                table: "Abiturinets",
                column: "PartAID");

            migrationBuilder.CreateIndex(
                name: "IX_AnsweredQuesttions_AnswerID",
                table: "AnsweredQuesttions",
                column: "AnswerID");

            migrationBuilder.CreateIndex(
                name: "IX_AnsweredQuesttions_QuestionID",
                table: "AnsweredQuesttions",
                column: "QuestionID");

            migrationBuilder.CreateIndex(
                name: "IX_AnsweredQuesttions_UserExamsID",
                table: "AnsweredQuesttions",
                column: "UserExamsID");

            migrationBuilder.CreateIndex(
                name: "IX_Answers_QuestionID",
                table: "Answers",
                column: "QuestionID");

            migrationBuilder.CreateIndex(
                name: "IX_Parts_PredmetID",
                table: "Parts",
                column: "PredmetID");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_TestID",
                table: "Questions",
                column: "TestID");

            migrationBuilder.CreateIndex(
                name: "IX_TestAs_AbiturientID",
                table: "TestAs",
                column: "AbiturientID");

            migrationBuilder.CreateIndex(
                name: "IX_TestAs_TestID",
                table: "TestAs",
                column: "TestID");

            migrationBuilder.CreateIndex(
                name: "IX_Tests_PartID",
                table: "Tests",
                column: "PartID");

            migrationBuilder.CreateIndex(
                name: "IX_UserExams_AbiturientID",
                table: "UserExams",
                column: "AbiturientID");

            migrationBuilder.CreateIndex(
                name: "IX_UserExams_TestID",
                table: "UserExams",
                column: "TestID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnsweredQuesttions");

            migrationBuilder.DropTable(
                name: "TestAs");

            migrationBuilder.DropTable(
                name: "Answers");

            migrationBuilder.DropTable(
                name: "UserExams");

            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "Abiturinets");

            migrationBuilder.DropTable(
                name: "Tests");

            migrationBuilder.DropTable(
                name: "PartAs");

            migrationBuilder.DropTable(
                name: "Parts");

            migrationBuilder.DropTable(
                name: "Predmets");
        }
    }
}
