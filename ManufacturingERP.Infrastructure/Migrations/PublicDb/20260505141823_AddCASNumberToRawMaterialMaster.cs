using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ManufacturingERP.Infrastructure.Migrations.PublicDb
{
    /// <inheritdoc />
    public partial class AddCASNumberToRawMaterialMaster : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CASNumber",
                schema: "public",
                table: "RawMaterialMasters",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CASNumber",
                schema: "public",
                table: "RawMaterialMasters");
        }
    }
}
