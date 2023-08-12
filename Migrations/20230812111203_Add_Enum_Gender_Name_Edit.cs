using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FluentValidationApp.Web.Migrations
{
    public partial class Add_Enum_Gender_Name_Edit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "gender",
                table: "Customers",
                newName: "Gender");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Gender",
                table: "Customers",
                newName: "gender");
        }
    }
}
