using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NZAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitiaDatas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RegionData",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegionImageURL = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegionData", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WalkDifficultyData",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WalkDifficultyData", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WalkData",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Length = table.Column<double>(type: "float", nullable: false),
                    WalkImageURL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WalkDifficultyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WalkData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WalkData_RegionData_RegionId",
                        column: x => x.RegionId,
                        principalTable: "RegionData",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WalkData_WalkDifficultyData_WalkDifficultyId",
                        column: x => x.WalkDifficultyId,
                        principalTable: "WalkDifficultyData",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "RegionData",
                columns: new[] { "Id", "Code", "Name", "RegionImageURL" },
                values: new object[,]
                {
                    { new Guid("14ceba71-4b51-4777-9b17-46602cf66153"), "BOP", "Bay Of Plenty", null },
                    { new Guid("6884f7d7-ad1f-4101-8df3-7a6fa7387d81"), "NTL", "Northland", null },
                    { new Guid("906cb139-415a-4bbb-a174-1a1faf9fb1f6"), "NSN", "Nelson", "https://images.pexels.com/photos/13918194/pexels-photo-13918194.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1" },
                    { new Guid("cfa06ed2-bf65-4b65-93ed-c9d286ddb0de"), "WGN", "Wellington", "https://images.pexels.com/photos/4350631/pexels-photo-4350631.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1" },
                    { new Guid("f077a22e-4248-4bf6-b564-c7cf4e250263"), "STL", "Southland", null },
                    { new Guid("f7248fc3-2585-4efb-8d1d-1c555f4087f6"), "AKL", "Auckland", "https://images.pexels.com/photos/5169056/pexels-photo-5169056.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1" }
                });

            migrationBuilder.InsertData(
                table: "WalkDifficultyData",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("339cc45c-3b66-460d-9e84-a57f4dc12668"), "Hard" },
                    { new Guid("80f11038-f74d-4e01-91b3-fa13e00b9a13"), "Medium" },
                    { new Guid("c79225d1-3d06-4beb-9971-0dbaecc65e36"), "Easy" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_WalkData_RegionId",
                table: "WalkData",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_WalkData_WalkDifficultyId",
                table: "WalkData",
                column: "WalkDifficultyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WalkData");

            migrationBuilder.DropTable(
                name: "RegionData");

            migrationBuilder.DropTable(
                name: "WalkDifficultyData");
        }
    }
}
