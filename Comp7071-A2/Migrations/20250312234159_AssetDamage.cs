using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Comp7071_A2.Migrations
{
    /// <inheritdoc />
    public partial class AssetDamage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AssetDamages",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "TEXT", nullable: false),
                    AssetID = table.Column<Guid>(type: "TEXT", nullable: false),
                    RenterID = table.Column<Guid>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    RecordedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    FixedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetDamages", x => x.ID);
                    table.ForeignKey(
                        name: "FK_AssetDamages_Assets_AssetID",
                        column: x => x.AssetID,
                        principalTable: "Assets",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AssetDamages_Renters_RenterID",
                        column: x => x.RenterID,
                        principalTable: "Renters",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DamageImages",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "TEXT", nullable: false),
                    AssetDamageID = table.Column<Guid>(type: "TEXT", nullable: false),
                    Photo = table.Column<byte[]>(type: "BLOB", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DamageImages", x => x.ID);
                    table.ForeignKey(
                        name: "FK_DamageImages_AssetDamages_AssetDamageID",
                        column: x => x.AssetDamageID,
                        principalTable: "AssetDamages",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AssetDamages_AssetID",
                table: "AssetDamages",
                column: "AssetID");

            migrationBuilder.CreateIndex(
                name: "IX_AssetDamages_RenterID",
                table: "AssetDamages",
                column: "RenterID");

            migrationBuilder.CreateIndex(
                name: "IX_DamageImages_AssetDamageID",
                table: "DamageImages",
                column: "AssetDamageID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DamageImages");

            migrationBuilder.DropTable(
                name: "AssetDamages");
        }
    }
}
