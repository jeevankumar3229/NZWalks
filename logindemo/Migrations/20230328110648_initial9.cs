using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace logindemo.Migrations
{
    /// <inheritdoc />
    public partial class initial9 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "20CA1BFE-E04E-4770-BB15-F57347580821");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6F0A4D24-292F-4F7A-BC7D-0F59981F0F2C");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2ab5daac-b3d9-4899-9d8f-d189ff7583f6", "2ab5daac-b3d9-4899-9d8f-d189ff7583f6", "Read", "READ" },
                    { "4b2bcc8b-32f8-4840-ab2d-4824abe758d6", "4b2bcc8b-32f8-4840-ab2d-4824abe758d6", "Write", "WRITE" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2ab5daac-b3d9-4899-9d8f-d189ff7583f6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4b2bcc8b-32f8-4840-ab2d-4824abe758d6");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "20CA1BFE-E04E-4770-BB15-F57347580821", "20CA1BFE-E04E-4770-BB15-F57347580821", "Write", "WRITE" },
                    { "6F0A4D24-292F-4F7A-BC7D-0F59981F0F2C", "6F0A4D24-292F-4F7A-BC7D-0F59981F0F2C", "Read", "READ" }
                });
        }
    }
}
