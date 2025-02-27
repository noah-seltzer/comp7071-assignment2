using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Comp7071_A2.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AssetID",
                table: "Application",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Application_AssetID",
                table: "Application",
                column: "AssetID");

            migrationBuilder.AddForeignKey(
                name: "FK_Application_Assets_AssetID",
                table: "Application",
                column: "AssetID",
                principalTable: "Assets",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Application_Assets_AssetID",
                table: "Application");

            migrationBuilder.DropIndex(
                name: "IX_Application_AssetID",
                table: "Application");

            migrationBuilder.DropColumn(
                name: "AssetID",
                table: "Application");
        }
    }
}
