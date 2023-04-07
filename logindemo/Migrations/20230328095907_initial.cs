using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace logindemo.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IdentityRole",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityRole", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "6F0A4D24-292F-4F7A-BC7D-0F59981F0F2C", "6F0A4D24-292F-4F7A-BC7D-0F59981F0F2E", "Read", "Read" },
                    { "6F0A4D24-292F-4F7A-BC7D-0F59981F0F2D", "6F0A4D24-292F-4F7A-BC7D-0F59981F0F2E", "Write", "Read" },
                    { "6F0A4D24-292F-4F7A-BC7D-0F59981F0F2E", "6F0A4D24-292F-4F7A-BC7D-0F59981F0F2E", "ReadWrite", "ReadWrite" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IdentityRole");
        }
    }
}
