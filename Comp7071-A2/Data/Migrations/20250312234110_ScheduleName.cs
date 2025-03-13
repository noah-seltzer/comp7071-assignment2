using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Comp7071_A2.Data.Migrations
{
    /// <inheritdoc />
    public partial class ScheduleName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "HRSchedules",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "HRSchedules");
        }
    }
}
