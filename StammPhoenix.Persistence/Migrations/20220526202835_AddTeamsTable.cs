using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StammPhoenix.Persistence.Migrations
{
    public partial class AddTeamsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TEAM",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    RANK = table.Column<int>(type: "integer", nullable: false),
                    MEMBERS = table.Column<string>(type: "text", nullable: true),
                    MEETING_TIME = table.Column<string>(type: "text", nullable: true),
                    MEETING_PLACE = table.Column<string>(type: "text", nullable: true),
                    IMAGE = table.Column<byte[]>(type: "bytea", nullable: true),
                    CREATED_ON = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CREATED_BY = table.Column<string>(type: "text", nullable: false),
                    MODIFIED_ON = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    MODIFIED_BY = table.Column<string>(type: "text", nullable: true),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TEAM", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TEAM_RANK",
                table: "TEAM",
                column: "RANK",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TEAM");
        }
    }
}
