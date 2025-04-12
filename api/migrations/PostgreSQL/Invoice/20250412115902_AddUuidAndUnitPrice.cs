using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NexKoala.WebApi.Migrations.PostgreSQL.Invoice
{
    /// <inheritdoc />
    public partial class AddUuidAndUnitPrice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "UnitPrice",
                schema: "invoice",
                table: "InvoiceLines",
                type: "numeric(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Uuid",
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
                name: "UnitPrice",
                schema: "invoice",
                table: "InvoiceLines");

            migrationBuilder.DropColumn(
                name: "Uuid",
                schema: "invoice",
                table: "InvoiceDocuments");
        }
    }
}
