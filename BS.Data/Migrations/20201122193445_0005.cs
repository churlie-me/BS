using Microsoft.EntityFrameworkCore.Migrations;

namespace BS.Data.Migrations
{
    public partial class _0005 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ThumbNail",
                table: "Content",
                newName: "UrlText");

            migrationBuilder.RenameColumn(
                name: "Color",
                table: "Content",
                newName: "TextColor");

            migrationBuilder.AddColumn<bool>(
                name: "IsHomePage",
                table: "Page",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Align",
                table: "Content",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "BorderRadius",
                table: "Content",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "ContentType",
                table: "Content",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "FontSize",
                table: "Content",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Content",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsRounded",
                table: "Content",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsHomePage",
                table: "Page");

            migrationBuilder.DropColumn(
                name: "Align",
                table: "Content");

            migrationBuilder.DropColumn(
                name: "BorderRadius",
                table: "Content");

            migrationBuilder.DropColumn(
                name: "ContentType",
                table: "Content");

            migrationBuilder.DropColumn(
                name: "FontSize",
                table: "Content");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Content");

            migrationBuilder.DropColumn(
                name: "IsRounded",
                table: "Content");

            migrationBuilder.RenameColumn(
                name: "UrlText",
                table: "Content",
                newName: "ThumbNail");

            migrationBuilder.RenameColumn(
                name: "TextColor",
                table: "Content",
                newName: "Color");
        }
    }
}
