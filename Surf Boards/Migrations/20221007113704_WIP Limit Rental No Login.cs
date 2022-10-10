using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Surf_Boards.Migrations
{
    public partial class WIPLimitRentalNoLogin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserIp",
                table: "Rental",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserIp",
                table: "Rental");
        }
    }
}
