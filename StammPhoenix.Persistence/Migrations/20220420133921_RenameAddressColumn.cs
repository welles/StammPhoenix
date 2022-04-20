using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StammPhoenix.Persistence.Migrations
{
    public partial class RenameAddressColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ADDRESS",
                table: "PAGE_CONTACT",
                newName: "ADDRESS_CITY");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ADDRESS_CITY",
                table: "PAGE_CONTACT",
                newName: "ADDRESS");
        }
    }
}
