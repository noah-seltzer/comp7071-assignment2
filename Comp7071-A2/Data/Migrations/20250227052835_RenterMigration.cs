using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Comp7071_A2.Data.Migrations
{
    /// <inheritdoc />
    public partial class RenterMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Application_Renters_RenterID",
                table: "Application");

            migrationBuilder.AlterColumn<Guid>(
                name: "RenterID",
                table: "Application",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.AddForeignKey(
                name: "FK_Application_Renters_RenterID",
                table: "Application",
                column: "RenterID",
                principalTable: "Renters",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Application_Renters_RenterID",
                table: "Application");

            migrationBuilder.AlterColumn<Guid>(
                name: "RenterID",
                table: "Application",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Application_Renters_RenterID",
                table: "Application",
                column: "RenterID",
                principalTable: "Renters",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
