using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BS.Data.Migrations
{
    public partial class _0059 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "TypeId",
                table: "Service",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ServiceType",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceType", x => x.Id);
                });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Service_ServiceType_TypeId",
                table: "Service");

            migrationBuilder.DropTable(
                name: "ServiceType");

            migrationBuilder.DropIndex(
                name: "IX_Service_TypeId",
                table: "Service");

            migrationBuilder.DropColumn(
                name: "TypeId",
                table: "Service");
        }
    }
}
