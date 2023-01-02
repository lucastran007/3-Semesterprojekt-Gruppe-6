using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blazor.Server.Data.Migrations
{
    public partial class test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.AlterColumn<byte[]>(
            //    name: "RowVersion",
            //    table: "SurfBoard",
            //    type: "rowversion",
            //    rowVersion: true,
            //    nullable: true,
            //    oldClrType: typeof(byte[]),
            //    oldType: "rowversion",
            //    oldRowVersion: true);

            migrationBuilder.AlterColumn<string>(
                name: "ImageName",
                table: "SurfBoard",
                type: "nvarchar (100)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar (100)");

            migrationBuilder.AlterColumn<string>(
                name: "Equipment",
                table: "SurfBoard",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            //migrationBuilder.AlterColumn<byte[]>(
            //    name: "RowVersion",
            //    table: "Rental",
            //    type: "rowversion",
            //    rowVersion: true,
            //    nullable: true,
            //    oldClrType: typeof(byte[]),
            //    oldType: "rowversion",
            //    oldRowVersion: true);

            migrationBuilder.AlterColumn<string>(
                name: "RentalPhone",
                table: "Rental",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.AlterColumn<byte[]>(
            //    name: "RowVersion",
            //    table: "SurfBoard",
            //    type: "rowversion",
            //    rowVersion: true,
            //    nullable: false,
            //    defaultValue: new byte[0],
            //    oldClrType: typeof(byte[]),
            //    oldType: "rowversion",
            //    oldRowVersion: true,
            //    oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ImageName",
                table: "SurfBoard",
                type: "nvarchar (100)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar (100)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Equipment",
                table: "SurfBoard",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            //migrationBuilder.AlterColumn<byte[]>(
            //    name: "RowVersion",
            //    table: "Rental",
            //    type: "rowversion",
            //    rowVersion: true,
            //    nullable: false,
            //    defaultValue: new byte[0],
            //    oldClrType: typeof(byte[]),
            //    oldType: "rowversion",
            //    oldRowVersion: true,
            //    oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RentalPhone",
                table: "Rental",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
