﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NexKoala.WebApi.Migrations.PostgreSQL.Invoice
{
    /// <inheritdoc />
    public partial class AuditTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AuditTrails",
                schema: "invoice",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Operation = table.Column<string>(type: "text", nullable: true),
                    Entity = table.Column<string>(type: "text", nullable: true),
                    DateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    PreviousValues = table.Column<string>(type: "text", nullable: true),
                    NewValues = table.Column<string>(type: "text", nullable: true),
                    ModifiedProperties = table.Column<string>(type: "text", nullable: true),
                    PrimaryKey = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditTrails", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuditTrails",
                schema: "invoice");
        }
    }
}
