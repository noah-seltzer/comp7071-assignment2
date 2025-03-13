using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Comp7071_A2.Migrations
{
    /// <inheritdoc />
    public partial class AddRentAmountToApplication : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "RentAmount",
                table: "Applications",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RentAmount",
                table: "Applications");
        }
    }
}
