using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StammPhoenix.Persistence.Migrations
{
    public partial class RenameTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PlannedEvents",
                table: "PlannedEvents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PageContacts",
                table: "PageContacts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LoginUsers",
                table: "LoginUsers");

            migrationBuilder.RenameTable(
                name: "PlannedEvents",
                newName: "PLANNED_EVENT");

            migrationBuilder.RenameTable(
                name: "PageContacts",
                newName: "PAGE_CONTACT");

            migrationBuilder.RenameTable(
                name: "LoginUsers",
                newName: "LOGIN_USER");

            migrationBuilder.RenameIndex(
                name: "IX_LoginUsers_EMAIL",
                table: "LOGIN_USER",
                newName: "IX_LOGIN_USER_EMAIL");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PLANNED_EVENT",
                table: "PLANNED_EVENT",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PAGE_CONTACT",
                table: "PAGE_CONTACT",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LOGIN_USER",
                table: "LOGIN_USER",
                column: "ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PLANNED_EVENT",
                table: "PLANNED_EVENT");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PAGE_CONTACT",
                table: "PAGE_CONTACT");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LOGIN_USER",
                table: "LOGIN_USER");

            migrationBuilder.RenameTable(
                name: "PLANNED_EVENT",
                newName: "PlannedEvents");

            migrationBuilder.RenameTable(
                name: "PAGE_CONTACT",
                newName: "PageContacts");

            migrationBuilder.RenameTable(
                name: "LOGIN_USER",
                newName: "LoginUsers");

            migrationBuilder.RenameIndex(
                name: "IX_LOGIN_USER_EMAIL",
                table: "LoginUsers",
                newName: "IX_LoginUsers_EMAIL");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PlannedEvents",
                table: "PlannedEvents",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PageContacts",
                table: "PageContacts",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LoginUsers",
                table: "LoginUsers",
                column: "ID");
        }
    }
}
