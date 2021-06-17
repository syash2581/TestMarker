using Microsoft.EntityFrameworkCore.Migrations;

namespace OTM.Migrations
{
    public partial class init3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_semesters",
                table: "semesters");

            migrationBuilder.RenameTable(
                name: "semesters",
                newName: "Semester");

            migrationBuilder.AddColumn<int>(
                name: "SemesterId",
                table: "Students",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Semester",
                table: "Semester",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Students_SemesterId",
                table: "Students",
                column: "SemesterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Semester_SemesterId",
                table: "Students",
                column: "SemesterId",
                principalTable: "Semester",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Semester_SemesterId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_SemesterId",
                table: "Students");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Semester",
                table: "Semester");

            migrationBuilder.DropColumn(
                name: "SemesterId",
                table: "Students");

            migrationBuilder.RenameTable(
                name: "Semester",
                newName: "semesters");

            migrationBuilder.AddPrimaryKey(
                name: "PK_semesters",
                table: "semesters",
                column: "Id");
        }
    }
}
