using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BS.Data.Migrations
{
    public partial class _0021 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItem_Article_ArticleId",
                table: "OrderItem");

            migrationBuilder.AlterColumn<Guid>(
                name: "ArticleId",
                table: "OrderItem",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "ServiceId",
                table: "OrderItem",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "AppointmentId",
                table: "Order",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "BranchId",
                table: "Appointment",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_ServiceId",
                table: "OrderItem",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_AppointmentId",
                table: "Order",
                column: "AppointmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointment_SeatId",
                table: "Appointment",
                column: "SeatId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointment_Seat_SeatId",
                table: "Appointment",
                column: "SeatId",
                principalTable: "Seat",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Appointment_AppointmentId",
                table: "Order",
                column: "AppointmentId",
                principalTable: "Appointment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItem_Article_ArticleId",
                table: "OrderItem",
                column: "ArticleId",
                principalTable: "Article",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItem_Service_ServiceId",
                table: "OrderItem",
                column: "ServiceId",
                principalTable: "Service",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointment_Seat_SeatId",
                table: "Appointment");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_Appointment_AppointmentId",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItem_Article_ArticleId",
                table: "OrderItem");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItem_Service_ServiceId",
                table: "OrderItem");

            migrationBuilder.DropIndex(
                name: "IX_OrderItem_ServiceId",
                table: "OrderItem");

            migrationBuilder.DropIndex(
                name: "IX_Order_AppointmentId",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_Appointment_SeatId",
                table: "Appointment");

            migrationBuilder.DropColumn(
                name: "ServiceId",
                table: "OrderItem");

            migrationBuilder.DropColumn(
                name: "AppointmentId",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "BranchId",
                table: "Appointment");

            migrationBuilder.AlterColumn<Guid>(
                name: "ArticleId",
                table: "OrderItem",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItem_Article_ArticleId",
                table: "OrderItem",
                column: "ArticleId",
                principalTable: "Article",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
