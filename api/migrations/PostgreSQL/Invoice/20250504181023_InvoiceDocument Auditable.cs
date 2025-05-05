using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NexKoala.WebApi.Migrations.PostgreSQL.Invoice
{
    /// <inheritdoc />
    public partial class InvoiceDocumentAuditable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "Created",
                schema: "invoice",
                table: "InvoiceDocuments",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                schema: "invoice",
                table: "InvoiceDocuments",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "LastModified",
                schema: "invoice",
                table: "InvoiceDocuments",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<Guid>(
                name: "LastModifiedBy",
                schema: "invoice",
                table: "InvoiceDocuments",
                type: "uuid",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Created",
                schema: "invoice",
                table: "InvoiceDocuments");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "invoice",
                table: "InvoiceDocuments");

            migrationBuilder.DropColumn(
                name: "LastModified",
                schema: "invoice",
                table: "InvoiceDocuments");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                schema: "invoice",
                table: "InvoiceDocuments");
        }
    }
}
