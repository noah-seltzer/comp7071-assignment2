using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Comp7071_A2.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Application_Assets_AssetID",
                table: "Application");

            migrationBuilder.DropForeignKey(
                name: "FK_Application_Renters_RenterID",
                table: "Application");

            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationReference_Application_ApplicationID",
                table: "ApplicationReference");

            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationReference_Contact_ContactID",
                table: "ApplicationReference");

            migrationBuilder.DropForeignKey(
                name: "FK_Contact_Renters_RenterID",
                table: "Contact");

            migrationBuilder.DropForeignKey(
                name: "FK_Renters_Application_ApplicationID",
                table: "Renters");

            migrationBuilder.DropForeignKey(
                name: "FK_Renters_Assets_AssetID",
                table: "Renters");

            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_ParkingSpots_ParkingSpotID",
                table: "Vehicles");

            migrationBuilder.DropTable(
                name: "Suites");

            migrationBuilder.DropTable(
                name: "Lockers");

            migrationBuilder.DropTable(
                name: "ParkingSpots");

            migrationBuilder.DropIndex(
                name: "IX_Renters_ApplicationID",
                table: "Renters");

            migrationBuilder.DropIndex(
                name: "IX_Renters_AssetID",
                table: "Renters");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Contact",
                table: "Contact");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ApplicationReference",
                table: "ApplicationReference");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Application",
                table: "Application");

            migrationBuilder.DropColumn(
                name: "ApplicationID",
                table: "Renters");

            migrationBuilder.DropColumn(
                name: "AssetID",
                table: "Renters");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Renters");

            migrationBuilder.RenameTable(
                name: "Contact",
                newName: "Contacts");

            migrationBuilder.RenameTable(
                name: "ApplicationReference",
                newName: "ApplicationReferences");

            migrationBuilder.RenameTable(
                name: "Application",
                newName: "Applications");

            migrationBuilder.RenameIndex(
                name: "IX_Contact_RenterID",
                table: "Contacts",
                newName: "IX_Contacts_RenterID");

            migrationBuilder.RenameIndex(
                name: "IX_ApplicationReference_ContactID",
                table: "ApplicationReferences",
                newName: "IX_ApplicationReferences_ContactID");

            migrationBuilder.RenameIndex(
                name: "IX_ApplicationReference_ApplicationID",
                table: "ApplicationReferences",
                newName: "IX_ApplicationReferences_ApplicationID");

            migrationBuilder.RenameIndex(
                name: "IX_Application_RenterID",
                table: "Applications",
                newName: "IX_Applications_RenterID");

            migrationBuilder.RenameIndex(
                name: "IX_Application_AssetID",
                table: "Applications",
                newName: "IX_Applications_AssetID");

            migrationBuilder.AddColumn<string>(
                name: "IdentityID",
                table: "Renters",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AssetType",
                table: "Assets",
                type: "TEXT",
                maxLength: 13,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Bathrooms",
                table: "Assets",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Floor",
                table: "Assets",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LockerID",
                table: "Assets",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LockerNumber",
                table: "Assets",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LockerSize",
                table: "Assets",
                type: "TEXT",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Occupants",
                table: "Assets",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ParkingSpotID",
                table: "Assets",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ParkingSpot_SuiteID",
                table: "Assets",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Rooms",
                table: "Assets",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SpotNumber",
                table: "Assets",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SuiteID",
                table: "Assets",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UnitNumber",
                table: "Assets",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "VehicleID",
                table: "Assets",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "RenterID",
                table: "Applications",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "AssetID",
                table: "Applications",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Contacts",
                table: "Contacts",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApplicationReferences",
                table: "ApplicationReferences",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Applications",
                table: "Applications",
                column: "ID");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "6a4d3c5f-95df-4e18-bc3b-0e69c457c234", null, "Renter", null });

            migrationBuilder.CreateIndex(
                name: "IX_Renters_IdentityID",
                table: "Renters",
                column: "IdentityID");

            migrationBuilder.CreateIndex(
                name: "IX_Assets_LockerID",
                table: "Assets",
                column: "LockerID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Assets_ParkingSpotID",
                table: "Assets",
                column: "ParkingSpotID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationReferences_Applications_ApplicationID",
                table: "ApplicationReferences",
                column: "ApplicationID",
                principalTable: "Applications",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationReferences_Contacts_ContactID",
                table: "ApplicationReferences",
                column: "ContactID",
                principalTable: "Contacts",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_Assets_AssetID",
                table: "Applications",
                column: "AssetID",
                principalTable: "Assets",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_Renters_RenterID",
                table: "Applications",
                column: "RenterID",
                principalTable: "Renters",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Assets_Assets_LockerID",
                table: "Assets",
                column: "LockerID",
                principalTable: "Assets",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Assets_Assets_ParkingSpotID",
                table: "Assets",
                column: "ParkingSpotID",
                principalTable: "Assets",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_Renters_RenterID",
                table: "Contacts",
                column: "RenterID",
                principalTable: "Renters",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Renters_AspNetUsers_IdentityID",
                table: "Renters",
                column: "IdentityID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_Assets_ParkingSpotID",
                table: "Vehicles",
                column: "ParkingSpotID",
                principalTable: "Assets",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationReferences_Applications_ApplicationID",
                table: "ApplicationReferences");

            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationReferences_Contacts_ContactID",
                table: "ApplicationReferences");

            migrationBuilder.DropForeignKey(
                name: "FK_Applications_Assets_AssetID",
                table: "Applications");

            migrationBuilder.DropForeignKey(
                name: "FK_Applications_Renters_RenterID",
                table: "Applications");

            migrationBuilder.DropForeignKey(
                name: "FK_Assets_Assets_LockerID",
                table: "Assets");

            migrationBuilder.DropForeignKey(
                name: "FK_Assets_Assets_ParkingSpotID",
                table: "Assets");

            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_Renters_RenterID",
                table: "Contacts");

            migrationBuilder.DropForeignKey(
                name: "FK_Renters_AspNetUsers_IdentityID",
                table: "Renters");

            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_Assets_ParkingSpotID",
                table: "Vehicles");

            migrationBuilder.DropIndex(
                name: "IX_Renters_IdentityID",
                table: "Renters");

            migrationBuilder.DropIndex(
                name: "IX_Assets_LockerID",
                table: "Assets");

            migrationBuilder.DropIndex(
                name: "IX_Assets_ParkingSpotID",
                table: "Assets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Contacts",
                table: "Contacts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Applications",
                table: "Applications");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ApplicationReferences",
                table: "ApplicationReferences");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6a4d3c5f-95df-4e18-bc3b-0e69c457c234");

            migrationBuilder.DropColumn(
                name: "IdentityID",
                table: "Renters");

            migrationBuilder.DropColumn(
                name: "AssetType",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "Bathrooms",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "Floor",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "LockerID",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "LockerNumber",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "LockerSize",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "Occupants",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "ParkingSpotID",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "ParkingSpot_SuiteID",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "Rooms",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "SpotNumber",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "SuiteID",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "UnitNumber",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "VehicleID",
                table: "Assets");

            migrationBuilder.RenameTable(
                name: "Contacts",
                newName: "Contact");

            migrationBuilder.RenameTable(
                name: "Applications",
                newName: "Application");

            migrationBuilder.RenameTable(
                name: "ApplicationReferences",
                newName: "ApplicationReference");

            migrationBuilder.RenameIndex(
                name: "IX_Contacts_RenterID",
                table: "Contact",
                newName: "IX_Contact_RenterID");

            migrationBuilder.RenameIndex(
                name: "IX_Applications_RenterID",
                table: "Application",
                newName: "IX_Application_RenterID");

            migrationBuilder.RenameIndex(
                name: "IX_Applications_AssetID",
                table: "Application",
                newName: "IX_Application_AssetID");

            migrationBuilder.RenameIndex(
                name: "IX_ApplicationReferences_ContactID",
                table: "ApplicationReference",
                newName: "IX_ApplicationReference_ContactID");

            migrationBuilder.RenameIndex(
                name: "IX_ApplicationReferences_ApplicationID",
                table: "ApplicationReference",
                newName: "IX_ApplicationReference_ApplicationID");

            migrationBuilder.AddColumn<Guid>(
                name: "ApplicationID",
                table: "Renters",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "AssetID",
                table: "Renters",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Renters",
                type: "TEXT",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<Guid>(
                name: "RenterID",
                table: "Application",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<Guid>(
                name: "AssetID",
                table: "Application",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Contact",
                table: "Contact",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Application",
                table: "Application",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApplicationReference",
                table: "ApplicationReference",
                column: "ID");

            migrationBuilder.CreateTable(
                name: "Lockers",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "TEXT", nullable: false),
                    AssetID = table.Column<Guid>(type: "TEXT", nullable: false),
                    LockerNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    LockerSize = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    SuiteID = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lockers", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Lockers_Assets_AssetID",
                        column: x => x.AssetID,
                        principalTable: "Assets",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ParkingSpots",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "TEXT", nullable: false),
                    AssetID = table.Column<Guid>(type: "TEXT", nullable: false),
                    SpotNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    SuiteID = table.Column<Guid>(type: "TEXT", nullable: true),
                    VehicleID = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParkingSpots", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ParkingSpots_Assets_AssetID",
                        column: x => x.AssetID,
                        principalTable: "Assets",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Suites",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "TEXT", nullable: false),
                    AssetID = table.Column<Guid>(type: "TEXT", nullable: false),
                    LockerID = table.Column<Guid>(type: "TEXT", nullable: true),
                    ParkingSpotID = table.Column<Guid>(type: "TEXT", nullable: true),
                    Bathrooms = table.Column<int>(type: "INTEGER", nullable: false),
                    Floor = table.Column<int>(type: "INTEGER", nullable: false),
                    Occupants = table.Column<int>(type: "INTEGER", nullable: false),
                    Rooms = table.Column<int>(type: "INTEGER", nullable: false),
                    UnitNumber = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suites", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Suites_Assets_AssetID",
                        column: x => x.AssetID,
                        principalTable: "Assets",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Suites_Lockers_LockerID",
                        column: x => x.LockerID,
                        principalTable: "Lockers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Suites_ParkingSpots_ParkingSpotID",
                        column: x => x.ParkingSpotID,
                        principalTable: "ParkingSpots",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Renters_ApplicationID",
                table: "Renters",
                column: "ApplicationID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Renters_AssetID",
                table: "Renters",
                column: "AssetID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Lockers_AssetID",
                table: "Lockers",
                column: "AssetID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ParkingSpots_AssetID",
                table: "ParkingSpots",
                column: "AssetID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Suites_AssetID",
                table: "Suites",
                column: "AssetID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Suites_LockerID",
                table: "Suites",
                column: "LockerID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Suites_ParkingSpotID",
                table: "Suites",
                column: "ParkingSpotID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Application_Assets_AssetID",
                table: "Application",
                column: "AssetID",
                principalTable: "Assets",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Application_Renters_RenterID",
                table: "Application",
                column: "RenterID",
                principalTable: "Renters",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationReference_Application_ApplicationID",
                table: "ApplicationReference",
                column: "ApplicationID",
                principalTable: "Application",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationReference_Contact_ContactID",
                table: "ApplicationReference",
                column: "ContactID",
                principalTable: "Contact",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Contact_Renters_RenterID",
                table: "Contact",
                column: "RenterID",
                principalTable: "Renters",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Renters_Application_ApplicationID",
                table: "Renters",
                column: "ApplicationID",
                principalTable: "Application",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Renters_Assets_AssetID",
                table: "Renters",
                column: "AssetID",
                principalTable: "Assets",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_ParkingSpots_ParkingSpotID",
                table: "Vehicles",
                column: "ParkingSpotID",
                principalTable: "ParkingSpots",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
