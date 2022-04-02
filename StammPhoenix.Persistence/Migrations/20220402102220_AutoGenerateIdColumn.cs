using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StammPhoenix.Persistence.Migrations
{
    public partial class AutoGenerateIdColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                ALTER TABLE ""PlannedEvents""
                ALTER COLUMN ""Id""
                TYPE uuid
                USING ""Id""::uuid;");

            migrationBuilder.Sql(@"
                ALTER TABLE ""PageContacts""
                ALTER COLUMN ""Id""
                TYPE uuid
                USING ""Id""::uuid;");

            migrationBuilder.Sql(@"
                ALTER TABLE ""LoginUsers""
                ALTER COLUMN ""Id""
                TYPE uuid
                USING ""Id""::uuid;");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "PlannedEvents",
                type: "text",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "PageContacts",
                type: "text",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "LoginUsers",
                type: "text",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid");
        }
    }
}
