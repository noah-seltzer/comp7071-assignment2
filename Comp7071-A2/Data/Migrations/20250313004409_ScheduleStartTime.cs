using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Comp7071_A2.Data.Migrations
{
    /// <inheritdoc />
    public partial class ScheduleStartTime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<TimeOnly>(
                name: "Start_Time",
                table: "HRSchedules",
                type: "TEXT",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Start_Time",
                table: "HRSchedules");
        }
    }
}
