using Microsoft.EntityFrameworkCore.Migrations;

namespace RazorWebAppProject.Migrations
{
    public partial class SP_CreateNewRecord : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string procedure = @"Create Procedure sp_create_new_record
                                @FirstName varchar(50),
                                @LastName varchar(50),
                                @Email varchar(50),
                                @Sex int,
                                @Department int,
                                @Photo varchar(255)
                                As
                                Begin
                                Insert Into tblEmployee
                                Values(@FirstName, @LastName, @Email, @Sex, @Department, @Photo)
                                End";


            migrationBuilder.Sql(procedure);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            string procedure = @"Drop Procedure sp_create_new_record";


            migrationBuilder.Sql(procedure);
        }
    }
}
