using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Comp7071_A2.Migrations
{
    /// <inheritdoc />
    public partial class BuildingChanged : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Buildings",
                type: "TEXT",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Buildings",
                type: "TEXT",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PostalCode",
                table: "Buildings",
                type: "TEXT",
                maxLength: 6,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Buildings");

            migrationBuilder.DropColumn(
                name: "City",
                table: "Buildings");

            migrationBuilder.DropColumn(
                name: "PostalCode",
                table: "Buildings");
        }
    }
}
