using Microsoft.EntityFrameworkCore.Migrations;

namespace OTM.Migrations
{
    public partial class init9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Semester_SemesterId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Semester_SemesterId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Tests_Semester_SemesterId",
                table: "Tests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Semester",
                table: "Semester");

            migrationBuilder.RenameTable(
                name: "Semester",
                newName: "semesters");

            migrationBuilder.AddPrimaryKey(
                name: "PK_semesters",
                table: "semesters",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_semesters_SemesterId",
                table: "AspNetUsers",
                column: "SemesterId",
                principalTable: "semesters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_semesters_SemesterId",
                table: "Students",
                column: "SemesterId",
                principalTable: "semesters",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tests_semesters_SemesterId",
                table: "Tests",
                column: "SemesterId",
                principalTable: "semesters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_semesters_SemesterId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_semesters_SemesterId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Tests_semesters_SemesterId",
                table: "Tests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_semesters",
                table: "semesters");

            migrationBuilder.RenameTable(
                name: "semesters",
                newName: "Semester");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Semester",
                table: "Semester",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Semester_SemesterId",
                table: "AspNetUsers",
                column: "SemesterId",
                principalTable: "Semester",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Semester_SemesterId",
                table: "Students",
                column: "SemesterId",
                principalTable: "Semester",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tests_Semester_SemesterId",
                table: "Tests",
                column: "SemesterId",
                principalTable: "Semester",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
