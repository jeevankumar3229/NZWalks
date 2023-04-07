using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace logindemo.Migrations
{
    /// <inheritdoc />
    public partial class initial3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6F0A4D24-292F-4F7A-BC7D-0F59981F0F2D",
                column: "NormalizedName",
                value: "WRITE");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6F0A4D24-292F-4F7A-BC7D-0F59981F0F2D",
                column: "NormalizedName",
                value: "READ");
        }
    }
}
