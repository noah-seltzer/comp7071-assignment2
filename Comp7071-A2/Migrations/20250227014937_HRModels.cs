using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Comp7071_A2.Migrations
{
    /// <inheritdoc />
    public partial class HRModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HREmployees",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Adderess = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Emergency_Contact = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Job_Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Employment_Type = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HREmployees", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "HRSchedules",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Start_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    End_Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Hours_Scheduled = table.Column<float>(type: "real", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Recurrance = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HRSchedules", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PayPeriods",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Start_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    End_Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Rate = table.Column<float>(type: "real", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    HREmployeeID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
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

            migrationBuilder.CreateTable(
                name: "Shifts",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Hours_Worked = table.Column<float>(type: "real", nullable: false),
                    Start_Time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    HRScheduleID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
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

            migrationBuilder.CreateIndex(
                name: "IX_PayPeriods_HREmployeeID",
                table: "PayPeriods",
                column: "HREmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_Shifts_HRScheduleID",
                table: "Shifts",
                column: "HRScheduleID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PayPeriods");

            migrationBuilder.DropTable(
                name: "Shifts");

            migrationBuilder.DropTable(
                name: "HREmployees");

            migrationBuilder.DropTable(
                name: "HRSchedules");
        }
    }
}
