using Microsoft.EntityFrameworkCore.Migrations;

namespace OTM.Migrations
{
    public partial class init7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Forsem",
                table: "Tests");

            

            migrationBuilder.DropColumn(
                name: "Sem",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "SemesterId",
                table: "Tests",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Tests_SemesterId",
                table: "Tests",
                column: "SemesterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tests_Semester_SemesterId",
                table: "Tests",
                column: "SemesterId",
                principalTable: "Semester",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tests_Semester_SemesterId",
                table: "Tests");

            migrationBuilder.DropIndex(
                name: "IX_Tests_SemesterId",
                table: "Tests");

            migrationBuilder.DropColumn(
                name: "SemesterId",
                table: "Tests");

            migrationBuilder.AddColumn<int>(
                name: "Forsem",
                table: "Tests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Sem",
                table: "Students",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Sem",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
