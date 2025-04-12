using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NexKoala.WebApi.Migrations.PostgreSQL.Invoice
{
    /// <inheritdoc />
    public partial class PasalCaseColName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SchemeID",
                schema: "invoice",
                table: "Partners",
                newName: "SchemeId");

            migrationBuilder.RenameColumn(
                name: "SSTRegistrationNumber",
                schema: "invoice",
                table: "Partners",
                newName: "SstRegistrationNumber");

            migrationBuilder.RenameColumn(
                name: "MSICCode",
                schema: "invoice",
                table: "Partners",
                newName: "MsicCode");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SstRegistrationNumber",
                schema: "invoice",
                table: "Partners",
                newName: "SSTRegistrationNumber");

            migrationBuilder.RenameColumn(
                name: "SchemeId",
                schema: "invoice",
                table: "Partners",
                newName: "SchemeID");

            migrationBuilder.RenameColumn(
                name: "MsicCode",
                schema: "invoice",
                table: "Partners",
                newName: "MSICCode");
        }
    }
}
