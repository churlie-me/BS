using Microsoft.EntityFrameworkCore.Migrations;

namespace BS.Data.Migrations
{
    public partial class _0006 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "Village",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "User",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "Stylist",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "Store",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "Service",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "Seat",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "Schedule",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "SaleItem",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "Row",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "Request",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "Region",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "Reason",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "Page",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "OrderItem",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "Order",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "Instruction",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "Holiday",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "District",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "Country",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "Content",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "Contact",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "Column",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "Category",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "Brand",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "Branch",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "Article",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "Appointment",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "Address",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Village");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Stylist");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Store");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Service");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Seat");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Schedule");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "SaleItem");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Row");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Request");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Region");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Reason");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Page");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "OrderItem");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Instruction");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Holiday");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "District");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Country");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Content");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Column");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Brand");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Branch");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Article");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Appointment");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Address");
        }
    }
}
