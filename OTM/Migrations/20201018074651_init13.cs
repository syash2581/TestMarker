using Microsoft.EntityFrameworkCore.Migrations;

namespace OTM.Migrations
{
    public partial class init13 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DeptSname",
                table: "Students",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeptSname",
                table: "Faculties",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeptSname",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "DeptSname",
                table: "Faculties");
        }
    }
}
