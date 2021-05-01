using Microsoft.EntityFrameworkCore.Migrations;

namespace BS.Data.Migrations
{
    public partial class _0031 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FooterBackgroundColor",
                table: "Store",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HeaderBackgroundColor",
                table: "Store",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HeaderTextColor",
                table: "Store",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TextColor",
                table: "Store",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FooterBackgroundColor",
                table: "Store");

            migrationBuilder.DropColumn(
                name: "HeaderBackgroundColor",
                table: "Store");

            migrationBuilder.DropColumn(
                name: "HeaderTextColor",
                table: "Store");

            migrationBuilder.DropColumn(
                name: "TextColor",
                table: "Store");
        }
    }
}
