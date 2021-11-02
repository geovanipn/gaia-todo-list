using Microsoft.EntityFrameworkCore.Migrations;

namespace Gaia.ToDoList.Data.Migrations
{
    public partial class DefaultUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Login", "Name", "Password" },
                values: new object[] { 1L, "gaia@gaia.com.br", "gaia", "Gaia", "j7yJU0+c4JlQGiiKm6IPJA==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L);
        }
    }
}
