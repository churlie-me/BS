using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BS.Data.Migrations
{
    public partial class _0016 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Row_Page_PageId",
                table: "Row");

            migrationBuilder.AlterColumn<Guid>(
                name: "PageId",
                table: "Row",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Row_Page_PageId",
                table: "Row",
                column: "PageId",
                principalTable: "Page",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Row_Page_PageId",
                table: "Row");

            migrationBuilder.AlterColumn<Guid>(
                name: "PageId",
                table: "Row",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Row_Page_PageId",
                table: "Row",
                column: "PageId",
                principalTable: "Page",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
