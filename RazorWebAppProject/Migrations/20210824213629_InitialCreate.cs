using Microsoft.EntityFrameworkCore.Migrations;

namespace RazorWebAppProject.Repository.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblEmployee",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Sex = table.Column<int>(type: "int", nullable: false),
                    Department = table.Column<int>(type: "int", nullable: false),
                    Photo = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblEmployee", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "tblEmployee",
                columns: new[] { "Id", "Department", "Email", "FirstName", "Sex", "Photo", "LastName" },
                values: new object[] { 1, 1, "tuse@iamtuse.com", "tuse", 1, null, "theProgrammer" });

            migrationBuilder.InsertData(
                table: "tblEmployee",
                columns: new[] { "Id", "Department", "Email", "FirstName", "Sex", "Photo", "LastName" },
                values: new object[] { 2, 5, "wonkulahp@iamtuse.com", "Precious", 2, "focus.jpg", "Wonkulah" });

            migrationBuilder.InsertData(
                table: "tblEmployee",
                columns: new[] { "Id", "Department", "Email", "FirstName", "Sex", "Photo", "LastName" },
                values: new object[] { 3, 2, "test@iamtuse.com", "Test", 3, null, "User" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblEmployee");
        }
    }
}
