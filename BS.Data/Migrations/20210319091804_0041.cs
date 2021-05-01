using Microsoft.EntityFrameworkCore.Migrations;

namespace BS.Data.Migrations
{
    public partial class _0041 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppointmentService_AspNetUsers_StylsitId",
                table: "AppointmentService");

            migrationBuilder.DropForeignKey(
                name: "FK_AppointmentService_Seat_SeatId",
                table: "AppointmentService");

            migrationBuilder.DropForeignKey(
                name: "FK_AppointmentService_Service_ServiceId",
                table: "AppointmentService");

            migrationBuilder.DropIndex(
                name: "IX_AppointmentService_AppointmentId",
                table: "AppointmentService");

            migrationBuilder.DropIndex(
                name: "IX_AppointmentService_SeatId",
                table: "AppointmentService");

            migrationBuilder.DropIndex(
                name: "IX_AppointmentService_ServiceId",
                table: "AppointmentService");

            migrationBuilder.DropIndex(
                name: "IX_AppointmentService_StylsitId",
                table: "AppointmentService");

            migrationBuilder.RenameColumn(
                name: "StylsitId",
                table: "AppointmentService",
                newName: "StylistId");

            migrationBuilder.CreateIndex(
                name: "IX_AppointmentService_AppointmentId",
                table: "AppointmentService",
                column: "AppointmentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AppointmentService_AppointmentId",
                table: "AppointmentService");

            migrationBuilder.RenameColumn(
                name: "StylistId",
                table: "AppointmentService",
                newName: "StylsitId");

            migrationBuilder.CreateIndex(
                name: "IX_AppointmentService_AppointmentId",
                table: "AppointmentService",
                column: "AppointmentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppointmentService_SeatId",
                table: "AppointmentService",
                column: "SeatId");

            migrationBuilder.CreateIndex(
                name: "IX_AppointmentService_ServiceId",
                table: "AppointmentService",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_AppointmentService_StylsitId",
                table: "AppointmentService",
                column: "StylsitId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppointmentService_AspNetUsers_StylsitId",
                table: "AppointmentService",
                column: "StylsitId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AppointmentService_Seat_SeatId",
                table: "AppointmentService",
                column: "SeatId",
                principalTable: "Seat",
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
    }
}
