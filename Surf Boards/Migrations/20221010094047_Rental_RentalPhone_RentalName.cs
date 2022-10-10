using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Surf_Boards.Migrations
{
    public partial class Rental_RentalPhone_RentalName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RentalName",
                table: "Rental",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RentalPhone",
                table: "Rental",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RentalName",
                table: "Rental");

            migrationBuilder.DropColumn(
                name: "RentalPhone",
                table: "Rental");
        }
    }
}
