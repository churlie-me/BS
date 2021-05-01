using Microsoft.EntityFrameworkCore.Migrations;

namespace BS.Data.Migrations
{
    public partial class _0062 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AppUrl",
                table: "Store",
                newName: "IosAppUrl");

            migrationBuilder.AddColumn<string>(
                name: "AndroidAppUrl",
                table: "Store",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AndroidAppUrl",
                table: "Store");

            migrationBuilder.RenameColumn(
                name: "IosAppUrl",
                table: "Store",
                newName: "AppUrl");
        }
    }
}
