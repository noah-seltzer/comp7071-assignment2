using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Comp7071_A2.Migrations
{
    /// <inheritdoc />
    public partial class cm2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CertificationEmployee_Certification_CertificationsId",
                table: "CertificationEmployee");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceCertification_Certification_CertificationsId",
                table: "ServiceCertification");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Certification",
                table: "Certification");

            migrationBuilder.RenameTable(
                name: "Certification",
                newName: "Certifications");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Certifications",
                table: "Certifications",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CertificationEmployee_Certifications_CertificationsId",
                table: "CertificationEmployee",
                column: "CertificationsId",
                principalTable: "Certifications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceCertification_Certifications_CertificationsId",
                table: "ServiceCertification",
                column: "CertificationsId",
                principalTable: "Certifications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CertificationEmployee_Certifications_CertificationsId",
                table: "CertificationEmployee");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceCertification_Certifications_CertificationsId",
                table: "ServiceCertification");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Certifications",
                table: "Certifications");

            migrationBuilder.RenameTable(
                name: "Certifications",
                newName: "Certification");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Certification",
                table: "Certification",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CertificationEmployee_Certification_CertificationsId",
                table: "CertificationEmployee",
                column: "CertificationsId",
                principalTable: "Certification",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceCertification_Certification_CertificationsId",
                table: "ServiceCertification",
                column: "CertificationsId",
                principalTable: "Certification",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
