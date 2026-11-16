using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShagOxServer.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCityStreetOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StreetExternalId",
                table: "Orders",
                newName: "RecipientStreetExternalId");

            migrationBuilder.RenameColumn(
                name: "FlatNumber",
                table: "Orders",
                newName: "RecipientFlatNumber");

            migrationBuilder.RenameColumn(
                name: "DeliveryCityExternalId",
                table: "Orders",
                newName: "RecipientCityExternalId");

            migrationBuilder.RenameColumn(
                name: "BuildingNumber",
                table: "Orders",
                newName: "RecipientBuildingNumber");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RecipientStreetExternalId",
                table: "Orders",
                newName: "StreetExternalId");

            migrationBuilder.RenameColumn(
                name: "RecipientFlatNumber",
                table: "Orders",
                newName: "FlatNumber");

            migrationBuilder.RenameColumn(
                name: "RecipientCityExternalId",
                table: "Orders",
                newName: "DeliveryCityExternalId");

            migrationBuilder.RenameColumn(
                name: "RecipientBuildingNumber",
                table: "Orders",
                newName: "BuildingNumber");
        }
    }
}
