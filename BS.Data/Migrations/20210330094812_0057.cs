using Microsoft.EntityFrameworkCore.Migrations;

namespace BS.Data.Migrations
{
    public partial class _0057 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Logo",
                table: "Store");

            migrationBuilder.DropColumn(
                name: "StoreImage",
                table: "Store");

            migrationBuilder.DropColumn(
                name: "BackgroundColor",
                table: "Row");

            migrationBuilder.DropColumn(
                name: "BackgroundImage",
                table: "Row");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Content");

            migrationBuilder.DropColumn(
                name: "BackgroundImage",
                table: "Column");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Logo",
                table: "Store",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StoreImage",
                table: "Store",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BackgroundColor",
                table: "Row",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BackgroundImage",
                table: "Row",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Content",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BackgroundImage",
                table: "Column",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
