using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace momsAppServer.Migrations
{
    /// <inheritdoc />
    public partial class AddDistrictAndCityRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Districts_Cities_CityId",
                table: "Districts");

            migrationBuilder.DropIndex(
                name: "IX_Districts_CityId",
                table: "Districts");

            migrationBuilder.DropColumn(
                name: "CityId",
                table: "Districts");

            migrationBuilder.AddColumn<int>(
                name: "DistrictId",
                table: "Cities",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Cities_DistrictId",
                table: "Cities",
                column: "DistrictId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cities_Districts_DistrictId",
                table: "Cities",
                column: "DistrictId",
                principalTable: "Districts",
                principalColumn: "DistrictId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cities_Districts_DistrictId",
                table: "Cities");

            migrationBuilder.DropIndex(
                name: "IX_Cities_DistrictId",
                table: "Cities");

            migrationBuilder.DropColumn(
                name: "DistrictId",
                table: "Cities");

            migrationBuilder.AddColumn<int>(
                name: "CityId",
                table: "Districts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Districts_CityId",
                table: "Districts",
                column: "CityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Districts_Cities_CityId",
                table: "Districts",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "CityId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
