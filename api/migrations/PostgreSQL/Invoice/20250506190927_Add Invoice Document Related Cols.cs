using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NexKoala.WebApi.Migrations.PostgreSQL.Invoice
{
    /// <inheritdoc />
    public partial class AddInvoiceDocumentRelatedCols : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BusinessActivityDescription",
                schema: "invoice",
                table: "Suppliers",
                type: "character varying(300)",
                maxLength: 300,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContactNumber",
                schema: "invoice",
                table: "Suppliers",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                schema: "invoice",
                table: "Suppliers",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IdType",
                schema: "invoice",
                table: "Suppliers",
                type: "character varying(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MsicCode",
                schema: "invoice",
                table: "Suppliers",
                type: "character varying(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SstRegistrationNumber",
                schema: "invoice",
                table: "Suppliers",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TaxTourismRegistrationNumber",
                schema: "invoice",
                table: "Suppliers",
                type: "character varying(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "TaxAmount",
                schema: "invoice",
                table: "InvoiceLines",
                type: "numeric(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalExcludingTax",
                schema: "invoice",
                table: "InvoiceDocuments",
                type: "numeric(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalIncludingTax",
                schema: "invoice",
                table: "InvoiceDocuments",
                type: "numeric(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "ContactNumber",
                schema: "invoice",
                table: "Customers",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                schema: "invoice",
                table: "Customers",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BusinessActivityDescription",
                schema: "invoice",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "ContactNumber",
                schema: "invoice",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "Email",
                schema: "invoice",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "IdType",
                schema: "invoice",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "MsicCode",
                schema: "invoice",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "SstRegistrationNumber",
                schema: "invoice",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "TaxTourismRegistrationNumber",
                schema: "invoice",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "TaxAmount",
                schema: "invoice",
                table: "InvoiceLines");

            migrationBuilder.DropColumn(
                name: "TotalExcludingTax",
                schema: "invoice",
                table: "InvoiceDocuments");

            migrationBuilder.DropColumn(
                name: "TotalIncludingTax",
                schema: "invoice",
                table: "InvoiceDocuments");

            migrationBuilder.DropColumn(
                name: "ContactNumber",
                schema: "invoice",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "Email",
                schema: "invoice",
                table: "Customers");
        }
    }
}
