using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BS.Data.Migrations
{
    public partial class _0052 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Seat_AspNetUsers_AccountId",
                table: "Seat");

            migrationBuilder.DropForeignKey(
                name: "FK_Seat_Branch_BranchId",
                table: "Seat");

            migrationBuilder.DropIndex(
                name: "IX_Seat_AccountId",
                table: "Seat");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "Seat");

            migrationBuilder.AlterColumn<Guid>(
                name: "BranchId",
                table: "Seat",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "SeatId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_SeatId",
                table: "AspNetUsers",
                column: "SeatId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Seat_SeatId",
                table: "AspNetUsers",
                column: "SeatId",
                principalTable: "Seat",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Seat_Branch_BranchId",
                table: "Seat",
                column: "BranchId",
                principalTable: "Branch",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Seat_SeatId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Seat_Branch_BranchId",
                table: "Seat");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_SeatId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "SeatId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<Guid>(
                name: "BranchId",
                table: "Seat",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "AccountId",
                table: "Seat",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Seat_AccountId",
                table: "Seat",
                column: "AccountId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Seat_AspNetUsers_AccountId",
                table: "Seat",
                column: "AccountId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Seat_Branch_BranchId",
                table: "Seat",
                column: "BranchId",
                principalTable: "Branch",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
