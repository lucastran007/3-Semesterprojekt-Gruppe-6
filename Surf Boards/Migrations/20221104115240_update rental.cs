using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Surf_Boards.Migrations
{
    public partial class updaterental : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AlterColumn<string>(
    name: "UserId",
    table: "Rental",
    type: "nvarchar(450)",
    nullable: true,
    defaultValue: "",
    oldClrType: typeof(string),
    oldType: "nvarchar(450)",
    oldNullable: true);

        }
    }
}
