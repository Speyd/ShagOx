using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShagOxServer.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateConfigurationBasketItems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BasketItems_Advertisements_BasketId",
                table: "BasketItems");

            migrationBuilder.CreateIndex(
                name: "IX_BasketItems_AdvertisementId",
                table: "BasketItems",
                column: "AdvertisementId");

            migrationBuilder.AddForeignKey(
                name: "FK_BasketItems_Advertisements_AdvertisementId",
                table: "BasketItems",
                column: "AdvertisementId",
                principalTable: "Advertisements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BasketItems_Advertisements_AdvertisementId",
                table: "BasketItems");

            migrationBuilder.DropIndex(
                name: "IX_BasketItems_AdvertisementId",
                table: "BasketItems");

            migrationBuilder.AddForeignKey(
                name: "FK_BasketItems_Advertisements_BasketId",
                table: "BasketItems",
                column: "BasketId",
                principalTable: "Advertisements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
