using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace logindemo.Migrations
{
    /// <inheritdoc />
    public partial class Initial20 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2ab5daac-b3d9-4899-9d8f-d189ff7583f6",
                column: "NormalizedName",
                value: "READERS");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4b2bcc8b-32f8-4840-ab2d-4824abe758d6",
                column: "NormalizedName",
                value: "WRITERS");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2ab5daac-b3d9-4899-9d8f-d189ff7583f6",
                column: "NormalizedName",
                value: "reader");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4b2bcc8b-32f8-4840-ab2d-4824abe758d6",
                column: "NormalizedName",
                value: null);
        }
    }
}
