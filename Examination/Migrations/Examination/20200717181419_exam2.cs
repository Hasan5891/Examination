using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Examination.Migrations.Examination
{
    public partial class exam2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserExams_Abiturinets_AbiturientID",
                table: "UserExams");

            migrationBuilder.DropTable(
                name: "TestAs");

            migrationBuilder.DropTable(
                name: "Abiturinets");

            migrationBuilder.DropTable(
                name: "PartAs");

            migrationBuilder.DropIndex(
                name: "IX_UserExams_AbiturientID",
                table: "UserExams");

            migrationBuilder.DropColumn(
                name: "AbiturientID",
                table: "UserExams");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "UserExams");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AbiturientID",
                table: "UserExams",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserID",
                table: "UserExams",
                nullable: false,
                defaultValue: 0);

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
                name: "Abiturinets",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true),
                    Nomi = table.Column<string>(nullable: true),
                    PartAID = table.Column<int>(nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    UserID = table.Column<string>(nullable: true),
                    isactive = table.Column<bool>(nullable: false)
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
                name: "TestAs",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AbiturientID = table.Column<int>(nullable: false),
                    TestID = table.Column<int>(nullable: false),
                    TotalItems = table.Column<int>(nullable: false)
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

            migrationBuilder.CreateIndex(
                name: "IX_UserExams_AbiturientID",
                table: "UserExams",
                column: "AbiturientID");

            migrationBuilder.CreateIndex(
                name: "IX_Abiturinets_PartAID",
                table: "Abiturinets",
                column: "PartAID");

            migrationBuilder.CreateIndex(
                name: "IX_TestAs_AbiturientID",
                table: "TestAs",
                column: "AbiturientID");

            migrationBuilder.CreateIndex(
                name: "IX_TestAs_TestID",
                table: "TestAs",
                column: "TestID");

            migrationBuilder.AddForeignKey(
                name: "FK_UserExams_Abiturinets_AbiturientID",
                table: "UserExams",
                column: "AbiturientID",
                principalTable: "Abiturinets",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
