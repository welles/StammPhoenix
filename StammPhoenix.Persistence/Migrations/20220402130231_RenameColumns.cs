using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StammPhoenix.Persistence.Migrations
{
    public partial class RenameColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "PlannedEvents",
                newName: "NAME");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "PlannedEvents",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "PlannedEvents",
                newName: "START_DATE");

            migrationBuilder.RenameColumn(
                name: "ParticipatingRanks",
                table: "PlannedEvents",
                newName: "PARTICIPATING_RANKS");

            migrationBuilder.RenameColumn(
                name: "EndDate",
                table: "PlannedEvents",
                newName: "END_DATE");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "PageContacts",
                newName: "NAME");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "PageContacts",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "PageContacts",
                newName: "PHONE_NUMBER");

            migrationBuilder.RenameColumn(
                name: "Role",
                table: "LoginUsers",
                newName: "ROLE");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "LoginUsers",
                newName: "NAME");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "LoginUsers",
                newName: "EMAIL");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "LoginUsers",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "PasswordHash",
                table: "LoginUsers",
                newName: "PASSWORD_HASH");

            migrationBuilder.RenameColumn(
                name: "NeedPasswordChange",
                table: "LoginUsers",
                newName: "NEED_PASSWORD_CHANGE");

            migrationBuilder.RenameColumn(
                name: "IsLocked",
                table: "LoginUsers",
                newName: "IS_LOCKED");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NAME",
                table: "PlannedEvents",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "PlannedEvents",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "START_DATE",
                table: "PlannedEvents",
                newName: "StartDate");

            migrationBuilder.RenameColumn(
                name: "PARTICIPATING_RANKS",
                table: "PlannedEvents",
                newName: "ParticipatingRanks");

            migrationBuilder.RenameColumn(
                name: "END_DATE",
                table: "PlannedEvents",
                newName: "EndDate");

            migrationBuilder.RenameColumn(
                name: "NAME",
                table: "PageContacts",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "PageContacts",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "PHONE_NUMBER",
                table: "PageContacts",
                newName: "PhoneNumber");

            migrationBuilder.RenameColumn(
                name: "ROLE",
                table: "LoginUsers",
                newName: "Role");

            migrationBuilder.RenameColumn(
                name: "NAME",
                table: "LoginUsers",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "EMAIL",
                table: "LoginUsers",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "LoginUsers",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "PASSWORD_HASH",
                table: "LoginUsers",
                newName: "PasswordHash");

            migrationBuilder.RenameColumn(
                name: "NEED_PASSWORD_CHANGE",
                table: "LoginUsers",
                newName: "NeedPasswordChange");

            migrationBuilder.RenameColumn(
                name: "IS_LOCKED",
                table: "LoginUsers",
                newName: "IsLocked");
        }
    }
}
