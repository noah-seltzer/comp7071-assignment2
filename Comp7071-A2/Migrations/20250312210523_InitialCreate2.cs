using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Comp7071_A2.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6a4d3c5f-95df-4e18-bc3b-0e69c457c234",
                column: "NormalizedName",
                value: "RENTER");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6a4d3c5f-95df-4e18-bc3b-0e69c457c6a4",
                column: "NormalizedName",
                value: "USER");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b5f0c6a4-45d7-4e18-94df-bc3b0e69c456",
                column: "NormalizedName",
                value: "HOUSINGADMIN");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6a4d3c5f-95df-4e18-bc3b-0e69c457c234",
                column: "NormalizedName",
                value: null);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6a4d3c5f-95df-4e18-bc3b-0e69c457c6a4",
                column: "NormalizedName",
                value: null);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b5f0c6a4-45d7-4e18-94df-bc3b0e69c456",
                column: "NormalizedName",
                value: null);
        }
    }
}
