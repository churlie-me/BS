using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BS.Data.Migrations
{
    public partial class _0015 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AccountId",
                table: "Holiday",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Holiday_AccountId",
                table: "Holiday",
                column: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Holiday_AspNetUsers_AccountId",
                table: "Holiday",
                column: "AccountId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Holiday_AspNetUsers_AccountId",
                table: "Holiday");

            migrationBuilder.DropIndex(
                name: "IX_Holiday_AccountId",
                table: "Holiday");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "Holiday");
        }
    }
}
