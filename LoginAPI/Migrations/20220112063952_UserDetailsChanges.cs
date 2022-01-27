using Microsoft.EntityFrameworkCore.Migrations;

namespace LoginAPI.Migrations
{
    public partial class UserDetailsChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeDetails_UserDetails_UserId",
                table: "EmployeeDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_PatientDetails_UserDetails_UserId",
                table: "PatientDetails");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeDetails_UserDetails_UserId",
                table: "EmployeeDetails",
                column: "UserId",
                principalTable: "UserDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PatientDetails_UserDetails_UserId",
                table: "PatientDetails",
                column: "UserId",
                principalTable: "UserDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeDetails_UserDetails_UserId",
                table: "EmployeeDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_PatientDetails_UserDetails_UserId",
                table: "PatientDetails");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeDetails_UserDetails_UserId",
                table: "EmployeeDetails",
                column: "UserId",
                principalTable: "UserDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PatientDetails_UserDetails_UserId",
                table: "PatientDetails",
                column: "UserId",
                principalTable: "UserDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
