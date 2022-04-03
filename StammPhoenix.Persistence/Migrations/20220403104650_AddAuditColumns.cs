using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StammPhoenix.Persistence.Migrations
{
    public partial class AddAuditColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CREATED_BY",
                table: "PLANNED_EVENT",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CREATED_ON",
                table: "PLANNED_EVENT",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "MODIFIED_BY",
                table: "PLANNED_EVENT",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "MODIFIED_ON",
                table: "PLANNED_EVENT",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CREATED_BY",
                table: "PAGE_CONTACT",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CREATED_ON",
                table: "PAGE_CONTACT",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "MODIFIED_BY",
                table: "PAGE_CONTACT",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "MODIFIED_ON",
                table: "PAGE_CONTACT",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CREATED_BY",
                table: "LOGIN_USER",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CREATED_ON",
                table: "LOGIN_USER",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "MODIFIED_BY",
                table: "LOGIN_USER",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "MODIFIED_ON",
                table: "LOGIN_USER",
                type: "timestamp with time zone",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CREATED_BY",
                table: "PLANNED_EVENT");

            migrationBuilder.DropColumn(
                name: "CREATED_ON",
                table: "PLANNED_EVENT");

            migrationBuilder.DropColumn(
                name: "MODIFIED_BY",
                table: "PLANNED_EVENT");

            migrationBuilder.DropColumn(
                name: "MODIFIED_ON",
                table: "PLANNED_EVENT");

            migrationBuilder.DropColumn(
                name: "CREATED_BY",
                table: "PAGE_CONTACT");

            migrationBuilder.DropColumn(
                name: "CREATED_ON",
                table: "PAGE_CONTACT");

            migrationBuilder.DropColumn(
                name: "MODIFIED_BY",
                table: "PAGE_CONTACT");

            migrationBuilder.DropColumn(
                name: "MODIFIED_ON",
                table: "PAGE_CONTACT");

            migrationBuilder.DropColumn(
                name: "CREATED_BY",
                table: "LOGIN_USER");

            migrationBuilder.DropColumn(
                name: "CREATED_ON",
                table: "LOGIN_USER");

            migrationBuilder.DropColumn(
                name: "MODIFIED_BY",
                table: "LOGIN_USER");

            migrationBuilder.DropColumn(
                name: "MODIFIED_ON",
                table: "LOGIN_USER");
        }
    }
}
