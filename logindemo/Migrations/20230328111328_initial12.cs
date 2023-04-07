using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace logindemo.Migrations
{
    /// <inheritdoc />
    public partial class initial12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2ab5daac-b3d9-4899-9d8f-d189ff7583f6",
                column: "Name",
                value: "Read");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4b2bcc8b-32f8-4840-ab2d-4824abe758d6",
                column: "Name",
                value: "Write");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2ab5daac-b3d9-4899-9d8f-d189ff7583f6",
                column: "Name",
                value: "Reader");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4b2bcc8b-32f8-4840-ab2d-4824abe758d6",
                column: "Name",
                value: "Writer");
        }
    }
}
