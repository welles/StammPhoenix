using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StammPhoenix.Persistence.Migrations
{
    public partial class AddNormalizedIdColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NormalizedId",
                table: "LoginUsers",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NormalizedId",
                table: "LoginUsers");
        }
    }
}
