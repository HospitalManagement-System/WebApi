using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LoginAPI.Migrations
{
    public partial class AppointmentModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_EmployeeDetails_NurseEmployeeId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_EmployeeDetails_PhysicianEmployeeId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_PatientDetails_PatientId",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_NurseEmployeeId",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_PatientId",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_PhysicianEmployeeId",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "NurseEmployeeId",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "PhysicianEmployeeId",
                table: "Appointments");

            migrationBuilder.AlterColumn<string>(
                name: "ModifiedReason",
                table: "Appointments",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Diagnosis",
                table: "Appointments",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AppointmentType",
                table: "Appointments",
                type: "nvarchar(70)",
                maxLength: 70,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(1)");

            migrationBuilder.AlterColumn<string>(
                name: "AppointmentStatus",
                table: "Appointments",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(1)");

            migrationBuilder.AddColumn<string>(
                name: "bookslot",
                table: "Appointments",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "bookslot",
                table: "Appointments");

            migrationBuilder.AlterColumn<string>(
                name: "ModifiedReason",
                table: "Appointments",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Diagnosis",
                table: "Appointments",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AppointmentType",
                table: "Appointments",
                type: "nvarchar(1)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(70)",
                oldMaxLength: 70,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AppointmentStatus",
                table: "Appointments",
                type: "nvarchar(1)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "NurseEmployeeId",
                table: "Appointments",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PhysicianEmployeeId",
                table: "Appointments",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_NurseEmployeeId",
                table: "Appointments",
                column: "NurseEmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_PatientId",
                table: "Appointments",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_PhysicianEmployeeId",
                table: "Appointments",
                column: "PhysicianEmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_EmployeeDetails_NurseEmployeeId",
                table: "Appointments",
                column: "NurseEmployeeId",
                principalTable: "EmployeeDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_EmployeeDetails_PhysicianEmployeeId",
                table: "Appointments",
                column: "PhysicianEmployeeId",
                principalTable: "EmployeeDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_PatientDetails_PatientId",
                table: "Appointments",
                column: "PatientId",
                principalTable: "PatientDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
