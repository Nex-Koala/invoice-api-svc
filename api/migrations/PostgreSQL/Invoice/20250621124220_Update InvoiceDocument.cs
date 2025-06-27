using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NexKoala.WebApi.Migrations.PostgreSQL.Invoice
{
    /// <inheritdoc />
    public partial class UpdateInvoiceDocument : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DateTimeValidated",
                schema: "invoice",
                table: "InvoiceDocuments",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InvoiceTypeCode",
                schema: "invoice",
                table: "InvoiceDocuments",
                type: "character varying(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LongId",
                schema: "invoice",
                table: "InvoiceDocuments",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateTimeValidated",
                schema: "invoice",
                table: "InvoiceDocuments");

            migrationBuilder.DropColumn(
                name: "InvoiceTypeCode",
                schema: "invoice",
                table: "InvoiceDocuments");

            migrationBuilder.DropColumn(
                name: "LongId",
                schema: "invoice",
                table: "InvoiceDocuments");
        }
    }
}
