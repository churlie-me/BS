using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BS.Data.Migrations
{
    public partial class _0012 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "RowId",
                table: "Content",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Content_RowId",
                table: "Content",
                column: "RowId");

            migrationBuilder.AddForeignKey(
                name: "FK_Content_Row_RowId",
                table: "Content",
                column: "RowId",
                principalTable: "Row",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Content_Row_RowId",
                table: "Content");

            migrationBuilder.DropIndex(
                name: "IX_Content_RowId",
                table: "Content");

            migrationBuilder.DropColumn(
                name: "RowId",
                table: "Content");
        }
    }
}
