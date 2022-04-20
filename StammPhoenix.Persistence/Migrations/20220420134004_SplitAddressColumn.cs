using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StammPhoenix.Persistence.Migrations
{
    public partial class SplitAddressColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ADDRESS_STREET",
                table: "PAGE_CONTACT",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ADDRESS_STREET",
                table: "PAGE_CONTACT");
        }
    }
}
