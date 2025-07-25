using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NexKoala.WebApi.Migrations.PostgreSQL.Invoice
{
    /// <inheritdoc />
    public partial class AddLicenseKeyEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LicenseKey",
                schema: "invoice",
                table: "Partners");

            migrationBuilder.DropColumn(
                name: "MaxSubmissions",
                schema: "invoice",
                table: "Partners");

            migrationBuilder.DropColumn(
                name: "SubmissionCount",
                schema: "invoice",
                table: "Partners");

            migrationBuilder.CreateTable(
                name: "LicenseKey",
                schema: "invoice",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Key = table.Column<Guid>(type: "uuid", maxLength: 100, nullable: false),
                    PartnerId = table.Column<Guid>(type: "uuid", nullable: false),
                    MaxSubmissions = table.Column<int>(type: "integer", nullable: false),
                    SubmissionCount = table.Column<int>(type: "integer", nullable: false),
                    ExpiryDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    IsRevoked = table.Column<bool>(type: "boolean", nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    LastModified = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    LastModifiedBy = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LicenseKey", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LicenseKey_Partners_PartnerId",
                        column: x => x.PartnerId,
                        principalSchema: "invoice",
                        principalTable: "Partners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LicenseKey_Key",
                schema: "invoice",
                table: "LicenseKey",
                column: "Key",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LicenseKey_PartnerId",
                schema: "invoice",
                table: "LicenseKey",
                column: "PartnerId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LicenseKey",
                schema: "invoice");

            migrationBuilder.AddColumn<string>(
                name: "LicenseKey",
                schema: "invoice",
                table: "Partners",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "MaxSubmissions",
                schema: "invoice",
                table: "Partners",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SubmissionCount",
                schema: "invoice",
                table: "Partners",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
