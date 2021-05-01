using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BS.Data.Migrations
{
    public partial class _0054 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Store_AspNetUsers_AccountId",
                table: "Store");

            migrationBuilder.DropIndex(
                name: "IX_Store_AccountId",
                table: "Store");

            migrationBuilder.AddColumn<Guid>(
                name: "AccountId",
                table: "Seat",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Seat_AccountId",
                table: "Seat",
                column: "AccountId",
                unique: true,
                filter: "[AccountId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Seat_AspNetUsers_AccountId",
                table: "Seat",
                column: "AccountId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Seat_AspNetUsers_AccountId",
                table: "Seat");

            migrationBuilder.DropIndex(
                name: "IX_Seat_AccountId",
                table: "Seat");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "Seat");

            migrationBuilder.CreateIndex(
                name: "IX_Store_AccountId",
                table: "Store",
                column: "AccountId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Store_AspNetUsers_AccountId",
                table: "Store",
                column: "AccountId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
