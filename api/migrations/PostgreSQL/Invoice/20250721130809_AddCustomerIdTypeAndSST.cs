using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NexKoala.WebApi.Migrations.PostgreSQL.Invoice
{
    /// <inheritdoc />
    public partial class AddCustomerIdTypeAndSST : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IdType",
                schema: "invoice",
                table: "Customers",
                type: "character varying(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SstRegistrationNumber",
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
                name: "IdType",
                schema: "invoice",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "SstRegistrationNumber",
                schema: "invoice",
                table: "Customers");
        }
    }
}
