using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.Persistence.Migrations
{
    public partial class AddedUniqueKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_Id_UserName",
                table: "Users");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Id_UserName",
                table: "Users",
                columns: new[] { "Id", "UserName" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_Id_UserName",
                table: "Users");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Id_UserName",
                table: "Users",
                columns: new[] { "Id", "UserName" });
        }
    }
}
