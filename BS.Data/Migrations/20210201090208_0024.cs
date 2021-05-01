using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BS.Data.Migrations
{
    public partial class _0024 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StoreId",
                table: "Reason");

            migrationBuilder.AlterColumn<Guid>(
                name: "BranchId",
                table: "Order",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<DateTime>(
                name: "SubmittedOn",
                table: "Application",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Order_BranchId",
                table: "Order",
                column: "BranchId");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Branch_BranchId",
                table: "Order",
                column: "BranchId",
                principalTable: "Branch",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Branch_BranchId",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_Order_BranchId",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "SubmittedOn",
                table: "Application");

            migrationBuilder.AddColumn<Guid>(
                name: "StoreId",
                table: "Reason",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<Guid>(
                name: "BranchId",
                table: "Order",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);
        }
    }
}
