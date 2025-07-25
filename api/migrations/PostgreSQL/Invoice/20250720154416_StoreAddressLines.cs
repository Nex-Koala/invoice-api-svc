using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NexKoala.WebApi.Migrations.PostgreSQL.Invoice
{
    /// <inheritdoc />
    public partial class StoreAddressLines : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Address",
                schema: "invoice",
                table: "Suppliers",
                newName: "Address1");

            migrationBuilder.RenameColumn(
                name: "Address",
                schema: "invoice",
                table: "Customers",
                newName: "Address1");

            migrationBuilder.AddColumn<string>(
                name: "Address2",
                schema: "invoice",
                table: "Suppliers",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address3",
                schema: "invoice",
                table: "Suppliers",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address2",
                schema: "invoice",
                table: "Customers",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address3",
                schema: "invoice",
                table: "Customers",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address2",
                schema: "invoice",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "Address3",
                schema: "invoice",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "Address2",
                schema: "invoice",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "Address3",
                schema: "invoice",
                table: "Customers");

            migrationBuilder.RenameColumn(
                name: "Address1",
                schema: "invoice",
                table: "Suppliers",
                newName: "Address");

            migrationBuilder.RenameColumn(
                name: "Address1",
                schema: "invoice",
                table: "Customers",
                newName: "Address");
        }
    }
}
