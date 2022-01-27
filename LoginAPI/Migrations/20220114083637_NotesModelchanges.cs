using Microsoft.EntityFrameworkCore.Migrations;

namespace LoginAPI.Migrations
{
    public partial class NotesModelchanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSent",
                table: "Notes");

            migrationBuilder.AddColumn<string>(
                name: "Designation",
                table: "Notes",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Designation",
                table: "Notes");

            migrationBuilder.AddColumn<bool>(
                name: "IsSent",
                table: "Notes",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
