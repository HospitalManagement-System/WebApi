using Microsoft.EntityFrameworkCore.Migrations;

namespace LoginAPI.Migrations
{
    public partial class NewGetNotes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"CREATE PROCEDURE [dbo].[sp_GetSentAndRecievedNotes]
                    @Id UNIQUEIDENTIFIER
                AS
                BEGIN
                    SET NOCOUNT ON;
                        SELECT t1.Id, t1.Message, t1.NotesDateTime,
                            CONCAT(t2.FirstName, t2.LastName ) AS Name,
							case WHEN @Id = t1.SenderEmployeeId THEN 'SENT' ELSE 'RECIEVED' END
							AS IsSentOrRecived
                        FROM NOTES t1
                            INNER JOIN 
                        EMPLOYEEDETAILS t2
                       ON t1.SenderEmployeeId = t2.Id OR t1.RecieverEmployeeId = t2.Id
                       WHERE t2.Id = @Id
                END";

            migrationBuilder.Sql(sp);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE dbo.sp_GetNotes DROP PROCEDURE dbo.sp_GetSentAndRecievedNotes");
        }
    }
}
