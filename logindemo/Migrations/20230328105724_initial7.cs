using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace logindemo.Migrations
{
    /// <inheritdoc />
    public partial class initial7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6F0A4D24-292F-4F7A-BC7D-0F59981F0F2D");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "20CA1BFE-E04E-4770-BB15-F57347580821", "20CA1BFE-E04E-4770-BB15-F57347580821", "Write", "WRITE" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "20CA1BFE-E04E-4770-BB15-F57347580821");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "6F0A4D24-292F-4F7A-BC7D-0F59981F0F2D", "6F0A4D24-292F-4F7A-BC7D-0F59981F0F2D", "Write", "WRITE" });
        }
    }
}
