using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blazor.Server.Data.Migrations
{
    public partial class rental2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rentals_SurfBoards_SurfboardId",
                table: "Rentals");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Rentals",
                table: "Rentals");

            migrationBuilder.RenameTable(
                name: "Rentals",
                newName: "Rental");

            migrationBuilder.RenameIndex(
                name: "IX_Rentals_SurfboardId",
                table: "Rental",
                newName: "IX_Rental_SurfboardId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Rental",
                table: "Rental",
                column: "RentalId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rental_SurfBoards_SurfboardId",
                table: "Rental",
                column: "SurfboardId",
                principalTable: "SurfBoards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rental_SurfBoards_SurfboardId",
                table: "Rental");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Rental",
                table: "Rental");

            migrationBuilder.RenameTable(
                name: "Rental",
                newName: "Rentals");

            migrationBuilder.RenameIndex(
                name: "IX_Rental_SurfboardId",
                table: "Rentals",
                newName: "IX_Rentals_SurfboardId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Rentals",
                table: "Rentals",
                column: "RentalId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rentals_SurfBoards_SurfboardId",
                table: "Rentals",
                column: "SurfboardId",
                principalTable: "SurfBoards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
