using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StammPhoenix.Persistence.Migrations
{
    public partial class ExtendLoginUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Password",
                table: "LoginUsers",
                newName: "PasswordHash");

            migrationBuilder.RenameColumn(
                name: "LoginUserId",
                table: "LoginUsers",
                newName: "Id");

            migrationBuilder.AddColumn<bool>(
                name: "IsLocked",
                table: "LoginUsers",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "NeedPasswordChange",
                table: "LoginUsers",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsLocked",
                table: "LoginUsers");

            migrationBuilder.DropColumn(
                name: "NeedPasswordChange",
                table: "LoginUsers");

            migrationBuilder.RenameColumn(
                name: "PasswordHash",
                table: "LoginUsers",
                newName: "Password");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "LoginUsers",
                newName: "LoginUserId");
        }
    }
}
