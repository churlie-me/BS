using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BS.Data.Migrations
{
    public partial class _0058 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Logo",
                table: "Store",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "StoreImage",
                table: "Store",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "BackgroundColor",
                table: "Row",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "BackgroundImage",
                table: "Row",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "Content",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "BackgroundImage",
                table: "Column",
                type: "varbinary(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Logo",
                table: "Store");

            migrationBuilder.DropColumn(
                name: "StoreImage",
                table: "Store");

            migrationBuilder.DropColumn(
                name: "BackgroundColor",
                table: "Row");

            migrationBuilder.DropColumn(
                name: "BackgroundImage",
                table: "Row");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Content");

            migrationBuilder.DropColumn(
                name: "BackgroundImage",
                table: "Column");
        }
    }
}
