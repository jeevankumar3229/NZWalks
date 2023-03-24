using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NZWalks_Api.Migrations
{
    /// <inheritdoc />
    public partial class NEW : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RegionImagarURL",
                table: "Regions",
                newName: "RegionImageURL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RegionImageURL",
                table: "Regions",
                newName: "RegionImagarURL");
        }
    }
}
