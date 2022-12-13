using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blazor.Server.Data.Migrations
{
    public partial class board2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rental_SurfBoards_SurfboardId",
                table: "Rental");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SurfBoards",
                table: "SurfBoards");

            migrationBuilder.RenameTable(
                name: "SurfBoards",
                newName: "SurfBoard");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SurfBoard",
                table: "SurfBoard",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Rental_SurfBoard_SurfboardId",
                table: "Rental",
                column: "SurfboardId",
                principalTable: "SurfBoard",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rental_SurfBoard_SurfboardId",
                table: "Rental");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SurfBoard",
                table: "SurfBoard");

            migrationBuilder.RenameTable(
                name: "SurfBoard",
                newName: "SurfBoards");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SurfBoards",
                table: "SurfBoards",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Rental_SurfBoards_SurfboardId",
                table: "Rental",
                column: "SurfboardId",
                principalTable: "SurfBoards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
