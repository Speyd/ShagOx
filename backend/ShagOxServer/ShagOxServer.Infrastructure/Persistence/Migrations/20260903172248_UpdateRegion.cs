using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShagOxServer.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRegion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Regions",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.Sql("""
                UPDATE "Regions"
                SET "Code" = 'REGION-' || "Id";
            """);

            migrationBuilder.CreateIndex(
                name: "IX_Regions_Code",
                table: "Regions",
                column: "Code",
                unique: true);

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Regions");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "Regions");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Regions",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Regions_Name",
                table: "Regions",
                column: "Name");
        }
    }
}
