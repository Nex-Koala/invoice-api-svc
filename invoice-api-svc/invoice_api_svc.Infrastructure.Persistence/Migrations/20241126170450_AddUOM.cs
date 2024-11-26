using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace invoice_api_svc.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddUOM : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_uom_mappings_uoms_UomId",
                table: "uom_mappings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_uoms",
                table: "uoms");

            migrationBuilder.DropPrimaryKey(
                name: "PK_uom_mappings",
                table: "uom_mappings");

            migrationBuilder.RenameTable(
                name: "uoms",
                newName: "Uom");

            migrationBuilder.RenameTable(
                name: "uom_mappings",
                newName: "UomMapping");

            migrationBuilder.RenameIndex(
                name: "IX_uom_mappings_UomId",
                table: "UomMapping",
                newName: "IX_UomMapping_UomId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Uom",
                table: "Uom",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UomMapping",
                table: "UomMapping",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UomMapping_Uom_UomId",
                table: "UomMapping",
                column: "UomId",
                principalTable: "Uom",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UomMapping_Uom_UomId",
                table: "UomMapping");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UomMapping",
                table: "UomMapping");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Uom",
                table: "Uom");

            migrationBuilder.RenameTable(
                name: "UomMapping",
                newName: "uom_mappings");

            migrationBuilder.RenameTable(
                name: "Uom",
                newName: "uoms");

            migrationBuilder.RenameIndex(
                name: "IX_UomMapping_UomId",
                table: "uom_mappings",
                newName: "IX_uom_mappings_UomId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_uom_mappings",
                table: "uom_mappings",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_uoms",
                table: "uoms",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_uom_mappings_uoms_UomId",
                table: "uom_mappings",
                column: "UomId",
                principalTable: "uoms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
