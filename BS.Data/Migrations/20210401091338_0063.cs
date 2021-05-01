using Microsoft.EntityFrameworkCore.Migrations;

namespace BS.Data.Migrations
{
    public partial class _0063 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TypeId",
                table: "Service",
                newName: "ServiceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Service_ServiceTypeId",
                table: "Service",
                column: "ServiceTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Service_ServiceType_ServiceTypeId",
                table: "Service",
                column: "ServiceTypeId",
                principalTable: "ServiceType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Service_ServiceType_ServiceTypeId",
                table: "Service");

            migrationBuilder.DropIndex(
                name: "IX_Service_ServiceTypeId",
                table: "Service");

            migrationBuilder.RenameColumn(
                name: "ServiceTypeId",
                table: "Service",
                newName: "TypeId");
        }
    }
}
