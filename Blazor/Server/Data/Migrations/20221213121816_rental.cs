using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blazor.Server.Data.Migrations
{
    public partial class rental : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Rentals",
                columns: table => new
                {
                    RentalId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RentalName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RentalPhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    SurfboardId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserIp = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RentalDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rentals", x => x.RentalId);
                    table.ForeignKey(
                        name: "FK_Rentals_SurfBoards_SurfboardId",
                        column: x => x.SurfboardId,
                        principalTable: "SurfBoards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rentals_SurfboardId",
                table: "Rentals",
                column: "SurfboardId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rentals");
        }
    }
}
