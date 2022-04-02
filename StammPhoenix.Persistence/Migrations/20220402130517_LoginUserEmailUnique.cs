using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StammPhoenix.Persistence.Migrations
{
    public partial class LoginUserEmailUnique : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_LoginUsers_EMAIL",
                table: "LoginUsers",
                column: "EMAIL",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_LoginUsers_EMAIL",
                table: "LoginUsers");
        }
    }
}
