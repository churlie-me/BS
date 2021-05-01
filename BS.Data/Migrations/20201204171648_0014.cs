using Microsoft.EntityFrameworkCore.Migrations;

namespace BS.Data.Migrations
{
    public partial class _0014 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Content_Row_UserId",
                table: "Content");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Content",
                newName: "RowId");

            migrationBuilder.RenameIndex(
                name: "IX_Content_UserId",
                table: "Content",
                newName: "IX_Content_RowId");

            migrationBuilder.AddForeignKey(
                name: "FK_Content_Row_RowId",
                table: "Content",
                column: "RowId",
                principalTable: "Row",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Content_Row_RowId",
                table: "Content");

            migrationBuilder.RenameColumn(
                name: "RowId",
                table: "Content",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Content_RowId",
                table: "Content",
                newName: "IX_Content_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Content_Row_UserId",
                table: "Content",
                column: "UserId",
                principalTable: "Row",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
