using Microsoft.EntityFrameworkCore.Migrations;

namespace RazorWebAppProject.Repository.Migrations
{
    public partial class SP_UpdateRecord : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string procedure = @"Create Procedure sp_update_employee
                                    @Id int,
                                    @FirstName varchar(50),
                                    @LastName varchar(50),
                                    @Email varchar(50),
                                    @Sex int,
                                    @Department int,
                                    @Photo varchar(255)
                                    As
                                    Begin
	                                    Update tblEmployee SET FirstName = @FirstName,
	                                    LastName = @LastName, Email = @Email, Sex = @Sex,
	                                    Department = @Department, Photo = @Photo Where Id = @Id
                                    End";

            migrationBuilder.Sql(procedure);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            string procedure = @"Drop Procedure sp_update_employee";
            migrationBuilder.Sql(procedure);
        }
    }
}
