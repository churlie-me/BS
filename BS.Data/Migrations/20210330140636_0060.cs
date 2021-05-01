using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BS.Data.Migrations
{
    public partial class _0060 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Service_ServiceType_TypeId",
                table: "Service");

            migrationBuilder.DropIndex(
                name: "IX_Service_TypeId",
                table: "Service");

            migrationBuilder.DropColumn(
                name: "TypeId",
                table: "Service");

            migrationBuilder.AddColumn<byte[]>(
                name: "DarkIcon",
                table: "ServiceType",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "LightIcon",
                table: "ServiceType",
                type: "varbinary(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DarkIcon",
                table: "ServiceType");

            migrationBuilder.DropColumn(
                name: "LightIcon",
                table: "ServiceType");

            migrationBuilder.AddColumn<Guid>(
                name: "TypeId",
                table: "Service",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Service_TypeId",
                table: "Service",
                column: "TypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Service_ServiceType_TypeId",
                table: "Service",
                column: "TypeId",
                principalTable: "ServiceType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
