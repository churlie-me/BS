using Microsoft.EntityFrameworkCore.Migrations;

namespace BS.Data.Migrations
{
    public partial class _0064 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Host",
                table: "Store",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsSslEnabled",
                table: "Store",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Port",
                table: "Store",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SenderEmail",
                table: "Store",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SenderEmailPassword",
                table: "Store",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Host",
                table: "Store");

            migrationBuilder.DropColumn(
                name: "IsSslEnabled",
                table: "Store");

            migrationBuilder.DropColumn(
                name: "Port",
                table: "Store");

            migrationBuilder.DropColumn(
                name: "SenderEmail",
                table: "Store");

            migrationBuilder.DropColumn(
                name: "SenderEmailPassword",
                table: "Store");
        }
    }
}
