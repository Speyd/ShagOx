using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShagOxServer.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddStockToAdvertisement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Stock",
                table: "Advertisements",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Stock",
                table: "Advertisements");
        }
    }
}
