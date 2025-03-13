using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Comp7071_A2.Migrations
{
    /// <inheritdoc />
    public partial class restartMigrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    UserName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "INTEGER", nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", nullable: true),
                    SecurityStamp = table.Column<string>(type: "TEXT", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "INTEGER", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "TEXT", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Certifications",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Certifications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Address = table.Column<string>(type: "TEXT", nullable: true),
                    Email = table.Column<string>(type: "TEXT", nullable: true),
                    EmergencyContactPhone = table.Column<string>(type: "TEXT", nullable: true),
                    JobTitle = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    EmployeeType = table.Column<string>(type: "TEXT", nullable: true),
                    ManagerId = table.Column<Guid>(type: "TEXT", nullable: true),
                    Department = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_Employees_ManagerId",
                        column: x => x.ManagerId,
                        principalTable: "Employees",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "HRSchedules",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "TEXT", nullable: false),
                    Start_Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    End_Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Start_Time = table.Column<TimeOnly>(type: "TEXT", nullable: false),
                    Hours_Scheduled = table.Column<float>(type: "REAL", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Status = table.Column<int>(type: "INTEGER", nullable: false),
                    Recurrance = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HRSchedules", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Type = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    Rate = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RoleId = table.Column<string>(type: "TEXT", nullable: false),
                    ClaimType = table.Column<string>(type: "TEXT", nullable: true),
                    ClaimValue = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    ClaimType = table.Column<string>(type: "TEXT", nullable: true),
                    ClaimValue = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "TEXT", nullable: true),
                    UserId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    RoleId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    LoginProvider = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HousingGroups",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "TEXT", nullable: false),
                    ManagerID = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Address = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    City = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    PostalCode = table.Column<string>(type: "TEXT", maxLength: 6, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HousingGroups", x => x.ID);
                    table.ForeignKey(
                        name: "FK_HousingGroups_AspNetUsers_ManagerID",
                        column: x => x.ManagerID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Renters",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "TEXT", nullable: false),
                    IdentityID = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Photo = table.Column<byte[]>(type: "BLOB", nullable: true),
                    PhoneNumber = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Renters", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Renters_AspNetUsers_IdentityID",
                        column: x => x.IdentityID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Invoices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    StartDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EndDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CustomerId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Invoices_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CertificationEmployee",
                columns: table => new
                {
                    CertificationsId = table.Column<Guid>(type: "TEXT", nullable: false),
                    EmployeesId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CertificationEmployee", x => new { x.CertificationsId, x.EmployeesId });
                    table.ForeignKey(
                        name: "FK_CertificationEmployee_Certifications_CertificationsId",
                        column: x => x.CertificationsId,
                        principalTable: "Certifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CertificationEmployee_Employees_EmployeesId",
                        column: x => x.EmployeesId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Shifts",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "TEXT", nullable: false),
                    Hours_Worked = table.Column<float>(type: "REAL", nullable: false),
                    Hours_Scheduled = table.Column<float>(type: "REAL", nullable: false),
                    Start_Time = table.Column<DateTime>(type: "TEXT", nullable: false),
                    End_Time = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false),
                    HRScheduleID = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shifts", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Shifts_HRSchedules_HRScheduleID",
                        column: x => x.HRScheduleID,
                        principalTable: "HRSchedules",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Schedule",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    StartTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EndTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ServiceId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedule", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Schedule_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServiceCertification",
                columns: table => new
                {
                    CertificationsId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ServicesId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceCertification", x => new { x.CertificationsId, x.ServicesId });
                    table.ForeignKey(
                        name: "FK_ServiceCertification_Certifications_CertificationsId",
                        column: x => x.CertificationsId,
                        principalTable: "Certifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServiceCertification_Services_ServicesId",
                        column: x => x.ServicesId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Buildings",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "TEXT", nullable: false),
                    HousingGroupID = table.Column<Guid>(type: "TEXT", nullable: true),
                    NumUnits = table.Column<int>(type: "INTEGER", nullable: false),
                    NumLockers = table.Column<int>(type: "INTEGER", nullable: false),
                    NumParking = table.Column<int>(type: "INTEGER", nullable: false),
                    Address = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    City = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    PostalCode = table.Column<string>(type: "TEXT", maxLength: 6, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Buildings", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Buildings_HousingGroups_HousingGroupID",
                        column: x => x.HousingGroupID,
                        principalTable: "HousingGroups",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "TEXT", nullable: false),
                    RenterID = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Contacts_Renters_RenterID",
                        column: x => x.RenterID,
                        principalTable: "Renters",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceLines",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    InvoiceId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    Quantity = table.Column<decimal>(type: "TEXT", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "TEXT", nullable: false),
                    Amount = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceLines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvoiceLines_Invoices_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CustomerSchedule",
                columns: table => new
                {
                    CustomersId = table.Column<Guid>(type: "TEXT", nullable: false),
                    SchedulesId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerSchedule", x => new { x.CustomersId, x.SchedulesId });
                    table.ForeignKey(
                        name: "FK_CustomerSchedule_Customers_CustomersId",
                        column: x => x.CustomersId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomerSchedule_Schedule_SchedulesId",
                        column: x => x.SchedulesId,
                        principalTable: "Schedule",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeSchedule",
                columns: table => new
                {
                    EmployeesId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ScheduleId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeSchedule", x => new { x.EmployeesId, x.ScheduleId });
                    table.ForeignKey(
                        name: "FK_EmployeeSchedule_Employees_EmployeesId",
                        column: x => x.EmployeesId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeSchedule_Schedule_ScheduleId",
                        column: x => x.ScheduleId,
                        principalTable: "Schedule",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Assets",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "TEXT", nullable: false),
                    HousingGroupID = table.Column<Guid>(type: "TEXT", nullable: true),
                    RenterID = table.Column<Guid>(type: "TEXT", nullable: true),
                    BuildingID = table.Column<Guid>(type: "TEXT", nullable: true),
                    IsAvailable = table.Column<bool>(type: "INTEGER", nullable: false),
                    RentAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AssetType = table.Column<string>(type: "TEXT", maxLength: 13, nullable: false),
                    LockerNumber = table.Column<int>(type: "INTEGER", nullable: true),
                    LockerSize = table.Column<string>(type: "TEXT", maxLength: 10, nullable: true),
                    SuiteID = table.Column<Guid>(type: "TEXT", nullable: true),
                    SpotNumber = table.Column<int>(type: "INTEGER", nullable: true),
                    ParkingSpot_SuiteID = table.Column<Guid>(type: "TEXT", nullable: true),
                    VehicleID = table.Column<Guid>(type: "TEXT", nullable: true),
                    LockerID = table.Column<Guid>(type: "TEXT", nullable: true),
                    ParkingSpotID = table.Column<Guid>(type: "TEXT", nullable: true),
                    UnitNumber = table.Column<int>(type: "INTEGER", nullable: true),
                    Floor = table.Column<int>(type: "INTEGER", nullable: true),
                    Occupants = table.Column<int>(type: "INTEGER", nullable: true),
                    Rooms = table.Column<int>(type: "INTEGER", nullable: true),
                    Bathrooms = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assets", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Assets_Assets_LockerID",
                        column: x => x.LockerID,
                        principalTable: "Assets",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Assets_Assets_ParkingSpotID",
                        column: x => x.ParkingSpotID,
                        principalTable: "Assets",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Assets_Buildings_BuildingID",
                        column: x => x.BuildingID,
                        principalTable: "Buildings",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Assets_HousingGroups_HousingGroupID",
                        column: x => x.HousingGroupID,
                        principalTable: "HousingGroups",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Assets_Renters_RenterID",
                        column: x => x.RenterID,
                        principalTable: "Renters",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Applications",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "TEXT", nullable: false),
                    RenterID = table.Column<Guid>(type: "TEXT", nullable: false),
                    AssetID = table.Column<Guid>(type: "TEXT", nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false),
                    RentAmount = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Applications", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Applications_Assets_AssetID",
                        column: x => x.AssetID,
                        principalTable: "Assets",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Applications_Renters_RenterID",
                        column: x => x.RenterID,
                        principalTable: "Renters",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

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
                name: "Vehicles",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "TEXT", nullable: false),
                    ParkingSpotID = table.Column<Guid>(type: "TEXT", nullable: true),
                    RenterID = table.Column<Guid>(type: "TEXT", nullable: false),
                    VehicleType = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    LicensePlate = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Vehicles_Assets_ParkingSpotID",
                        column: x => x.ParkingSpotID,
                        principalTable: "Assets",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Vehicles_Renters_RenterID",
                        column: x => x.RenterID,
                        principalTable: "Renters",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationReferences",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "TEXT", nullable: false),
                    ApplicationID = table.Column<Guid>(type: "TEXT", nullable: false),
                    ContactID = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationReferences", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ApplicationReferences_Applications_ApplicationID",
                        column: x => x.ApplicationID,
                        principalTable: "Applications",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationReferences_Contacts_ContactID",
                        column: x => x.ContactID,
                        principalTable: "Contacts",
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

            migrationBuilder.CreateTable(
                name: "HREmployees",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Adderess = table.Column<string>(type: "TEXT", nullable: false),
                    Emergency_Contact = table.Column<string>(type: "TEXT", nullable: false),
                    Job_Title = table.Column<string>(type: "TEXT", nullable: false),
                    UserId = table.Column<string>(type: "TEXT", nullable: true),
                    Employment_Type = table.Column<string>(type: "TEXT", nullable: false),
                    HRManagerID = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HREmployees", x => x.ID);
                });

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

            migrationBuilder.CreateTable(
                name: "PayPeriods",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "TEXT", nullable: false),
                    Start_Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    End_Date = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Rate = table.Column<float>(type: "REAL", nullable: false),
                    Type = table.Column<int>(type: "INTEGER", nullable: false),
                    HREmployeeID = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PayPeriods", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PayPeriods_HREmployees_HREmployeeID",
                        column: x => x.HREmployeeID,
                        principalTable: "HREmployees",
                        principalColumn: "ID");
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "6a4d3c5f-95df-4e18-bc3b-0e69c457c234", null, "Renter", "RENTER" },
                    { "6a4d3c5f-95df-4e18-bc3b-0e69c457c6a4", null, "User", "USER" },
                    { "b5f0c6a4-45d7-4e18-94df-bc3b0e69c456", null, "HousingAdmin", "HOUSINGADMIN" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationReferences_ApplicationID",
                table: "ApplicationReferences",
                column: "ApplicationID");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationReferences_ContactID",
                table: "ApplicationReferences",
                column: "ContactID");

            migrationBuilder.CreateIndex(
                name: "IX_Applications_AssetID",
                table: "Applications",
                column: "AssetID");

            migrationBuilder.CreateIndex(
                name: "IX_Applications_RenterID",
                table: "Applications",
                column: "RenterID");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AssetDamages_AssetID",
                table: "AssetDamages",
                column: "AssetID");

            migrationBuilder.CreateIndex(
                name: "IX_AssetDamages_RenterID",
                table: "AssetDamages",
                column: "RenterID");

            migrationBuilder.CreateIndex(
                name: "IX_Assets_BuildingID",
                table: "Assets",
                column: "BuildingID");

            migrationBuilder.CreateIndex(
                name: "IX_Assets_HousingGroupID",
                table: "Assets",
                column: "HousingGroupID");

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

            migrationBuilder.CreateIndex(
                name: "IX_Assets_RenterID",
                table: "Assets",
                column: "RenterID");

            migrationBuilder.CreateIndex(
                name: "IX_Buildings_HousingGroupID",
                table: "Buildings",
                column: "HousingGroupID");

            migrationBuilder.CreateIndex(
                name: "IX_CertificationEmployee_EmployeesId",
                table: "CertificationEmployee",
                column: "EmployeesId");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_RenterID",
                table: "Contacts",
                column: "RenterID");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerSchedule_SchedulesId",
                table: "CustomerSchedule",
                column: "SchedulesId");

            migrationBuilder.CreateIndex(
                name: "IX_DamageImages_AssetDamageID",
                table: "DamageImages",
                column: "AssetDamageID");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_ManagerId",
                table: "Employees",
                column: "ManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeSchedule_ScheduleId",
                table: "EmployeeSchedule",
                column: "ScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_HousingGroups_ManagerID",
                table: "HousingGroups",
                column: "ManagerID");

            migrationBuilder.CreateIndex(
                name: "IX_HREmployees_HRManagerID",
                table: "HREmployees",
                column: "HRManagerID");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceLines_InvoiceId",
                table: "InvoiceLines",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_CustomerId",
                table: "Invoices",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_PayPeriods_HREmployeeID",
                table: "PayPeriods",
                column: "HREmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_Renters_IdentityID",
                table: "Renters",
                column: "IdentityID");

            migrationBuilder.CreateIndex(
                name: "IX_Schedule_ServiceId",
                table: "Schedule",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceCertification_ServicesId",
                table: "ServiceCertification",
                column: "ServicesId");

            migrationBuilder.CreateIndex(
                name: "IX_Shifts_HRScheduleID",
                table: "Shifts",
                column: "HRScheduleID");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_ParkingSpotID",
                table: "Vehicles",
                column: "ParkingSpotID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_RenterID",
                table: "Vehicles",
                column: "RenterID");

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
                name: "ApplicationReferences");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "CertificationEmployee");

            migrationBuilder.DropTable(
                name: "CustomerSchedule");

            migrationBuilder.DropTable(
                name: "DamageImages");

            migrationBuilder.DropTable(
                name: "EmployeeSchedule");

            migrationBuilder.DropTable(
                name: "InvoiceLines");

            migrationBuilder.DropTable(
                name: "PayPeriods");

            migrationBuilder.DropTable(
                name: "ServiceCertification");

            migrationBuilder.DropTable(
                name: "Shifts");

            migrationBuilder.DropTable(
                name: "Vehicles");

            migrationBuilder.DropTable(
                name: "Applications");

            migrationBuilder.DropTable(
                name: "Contacts");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AssetDamages");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Schedule");

            migrationBuilder.DropTable(
                name: "Invoices");

            migrationBuilder.DropTable(
                name: "Certifications");

            migrationBuilder.DropTable(
                name: "HRSchedules");

            migrationBuilder.DropTable(
                name: "Assets");

            migrationBuilder.DropTable(
                name: "Services");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Buildings");

            migrationBuilder.DropTable(
                name: "Renters");

            migrationBuilder.DropTable(
                name: "HousingGroups");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "HRManagers");

            migrationBuilder.DropTable(
                name: "HREmployees");
        }
    }
}
