using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace logindemo.Migrations
{
    /// <inheritdoc />
    public partial class initial1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "6F0A4D24-292F-4F7A-BC7D-0F59981F0F2E");

            migrationBuilder.UpdateData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "6F0A4D24-292F-4F7A-BC7D-0F59981F0F2C",
                column: "NormalizedName",
                value: "READ");

            migrationBuilder.UpdateData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "6F0A4D24-292F-4F7A-BC7D-0F59981F0F2D",
                column: "NormalizedName",
                value: "READ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "6F0A4D24-292F-4F7A-BC7D-0F59981F0F2C",
                column: "NormalizedName",
                value: "Read");

            migrationBuilder.UpdateData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "6F0A4D24-292F-4F7A-BC7D-0F59981F0F2D",
                column: "NormalizedName",
                value: "Read");

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "6F0A4D24-292F-4F7A-BC7D-0F59981F0F2E", "6F0A4D24-292F-4F7A-BC7D-0F59981F0F2E", "ReadWrite", "ReadWrite" });
        }
    }
}
