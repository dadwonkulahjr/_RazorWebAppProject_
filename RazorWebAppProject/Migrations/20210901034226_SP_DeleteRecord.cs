using Microsoft.EntityFrameworkCore.Migrations;

namespace RazorWebAppProject.Migrations
{
    public partial class SP_DeleteRecord : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string procedure = @"Create Procedure sp_delete_record
                                @Id int
                                As 
                                Begin
                                DELETE FROM tblEmployee
                                Where Id = @Id
                                End";

            migrationBuilder.Sql(procedure);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            string procedure = @"Drop Procedure sp_delete_record";

            migrationBuilder.Sql(procedure);
        }
    }
}
