using Microsoft.EntityFrameworkCore.Migrations;

namespace BS.Data.Migrations
{
    public partial class _0042 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_AppointmentService_ServiceId",
                table: "AppointmentService",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_AppointmentService_StylistId",
                table: "AppointmentService",
                column: "StylistId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppointmentService_AspNetUsers_StylistId",
                table: "AppointmentService",
                column: "StylistId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AppointmentService_Service_ServiceId",
                table: "AppointmentService",
                column: "ServiceId",
                principalTable: "Service",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppointmentService_AspNetUsers_StylistId",
                table: "AppointmentService");

            migrationBuilder.DropForeignKey(
                name: "FK_AppointmentService_Service_ServiceId",
                table: "AppointmentService");

            migrationBuilder.DropIndex(
                name: "IX_AppointmentService_ServiceId",
                table: "AppointmentService");

            migrationBuilder.DropIndex(
                name: "IX_AppointmentService_StylistId",
                table: "AppointmentService");
        }
    }
}
