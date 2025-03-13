using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Comp7071_A2.Migrations
{
    /// <inheritdoc />
    public partial class emp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "HRManagerID",
                table: "HREmployees",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "HREmployees",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "HRManagers",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HRManagers", x => x.ID);
                    table.ForeignKey(
                        name: "FK_HRManagers_HREmployees_ID",
                        column: x => x.ID,
                        principalTable: "HREmployees",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HREmployees_HRManagerID",
                table: "HREmployees",
                column: "HRManagerID");

            migrationBuilder.AddForeignKey(
                name: "FK_HREmployees_HRManagers_HRManagerID",
                table: "HREmployees",
                column: "HRManagerID",
                principalTable: "HRManagers",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HREmployees_HRManagers_HRManagerID",
                table: "HREmployees");

            migrationBuilder.DropTable(
                name: "HRManagers");

            migrationBuilder.DropIndex(
                name: "IX_HREmployees_HRManagerID",
                table: "HREmployees");

            migrationBuilder.DropColumn(
                name: "HRManagerID",
                table: "HREmployees");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "HREmployees");
        }
    }
}
