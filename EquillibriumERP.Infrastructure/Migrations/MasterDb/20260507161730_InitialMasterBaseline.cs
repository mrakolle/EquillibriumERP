using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EquillibriumERP.Infrastructure.Migrations.MasterDb
{
    /// <inheritdoc />
    public partial class InitialMasterBaseline : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "public");

            migrationBuilder.CreateTable(
                name: "RawMaterialMasters",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Category = table.Column<string>(type: "text", nullable: false),
                    ChemicalFormula = table.Column<string>(type: "text", nullable: false),
                    PurityMin = table.Column<decimal>(type: "numeric", nullable: false),
                    PurityMax = table.Column<decimal>(type: "numeric", nullable: false),
                    Grade = table.Column<string>(type: "text", nullable: false),
                    UnitOfMeasure = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RawMaterialMasters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Suppliers",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Phone = table.Column<string>(type: "text", nullable: false),
                    RegistrationNumber = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tenants",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Schema = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tenants", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Branches",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    City = table.Column<string>(type: "text", nullable: false),
                    Province = table.Column<string>(type: "text", nullable: false),
                    Address = table.Column<string>(type: "text", nullable: false),
                    SupplierId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Branches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Branches_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalSchema: "public",
                        principalTable: "Suppliers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SupplierRawMaterials",
                schema: "public",
                columns: table => new
                {
                    SupplierId = table.Column<Guid>(type: "uuid", nullable: false),
                    RawMaterialMasterId = table.Column<Guid>(type: "uuid", nullable: false),
                    PricePerKg = table.Column<decimal>(type: "numeric", nullable: false),
                    LeadTimeDays = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupplierRawMaterials", x => new { x.SupplierId, x.RawMaterialMasterId });
                    table.ForeignKey(
                        name: "FK_SupplierRawMaterials_RawMaterialMasters_RawMaterialMasterId",
                        column: x => x.RawMaterialMasterId,
                        principalSchema: "public",
                        principalTable: "RawMaterialMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SupplierRawMaterials_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalSchema: "public",
                        principalTable: "Suppliers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Branches_SupplierId_Name",
                schema: "public",
                table: "Branches",
                columns: new[] { "SupplierId", "Name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RawMaterialMasters_Name",
                schema: "public",
                table: "RawMaterialMasters",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SupplierRawMaterials_RawMaterialMasterId",
                schema: "public",
                table: "SupplierRawMaterials",
                column: "RawMaterialMasterId");

            migrationBuilder.CreateIndex(
                name: "IX_Suppliers_Name",
                schema: "public",
                table: "Suppliers",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Branches",
                schema: "public");

            migrationBuilder.DropTable(
                name: "SupplierRawMaterials",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Tenants",
                schema: "public");

            migrationBuilder.DropTable(
                name: "RawMaterialMasters",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Suppliers",
                schema: "public");
        }
    }
}
