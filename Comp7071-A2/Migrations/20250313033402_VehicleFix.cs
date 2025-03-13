using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Comp7071_A2.Migrations
{
    /// <inheritdoc />
    public partial class VehicleFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_RenterID",
                table: "Vehicles",
                column: "RenterID");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_Renters_RenterID",
                table: "Vehicles",
                column: "RenterID",
                principalTable: "Renters",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_Renters_RenterID",
                table: "Vehicles");

            migrationBuilder.DropIndex(
                name: "IX_Vehicles_RenterID",
                table: "Vehicles");
        }
    }
}
