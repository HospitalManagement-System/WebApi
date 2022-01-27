using Microsoft.EntityFrameworkCore.Migrations;

namespace LoginAPI.Migrations
{
    public partial class EmployeeDetailAndNotesChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsSent",
                table: "Notes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Designation",
                table: "EmployeeDetails",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSent",
                table: "Notes");

            migrationBuilder.DropColumn(
                name: "Designation",
                table: "EmployeeDetails");
        }
    }
}
