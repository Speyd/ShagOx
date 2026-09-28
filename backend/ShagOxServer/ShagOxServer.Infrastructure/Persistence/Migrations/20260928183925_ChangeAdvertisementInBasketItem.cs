using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShagOxServer.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeAdvertisementInBasketItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BasketItems_Advertisements_AdvertisementId",
                table: "BasketItems");

            migrationBuilder.RenameColumn(
                name: "AdvertisementId",
                table: "BasketItems",
                newName: "AdvertisementVariantId");

            migrationBuilder.RenameIndex(
                name: "IX_BasketItems_BasketId_AdvertisementId",
                table: "BasketItems",
                newName: "IX_BasketItems_BasketId_AdvertisementVariantId");

            migrationBuilder.RenameIndex(
                name: "IX_BasketItems_AdvertisementId",
                table: "BasketItems",
                newName: "IX_BasketItems_AdvertisementVariantId");

            migrationBuilder.AddForeignKey(
                name: "FK_BasketItems_AdvertisementVariants_AdvertisementVariantId",
                table: "BasketItems",
                column: "AdvertisementVariantId",
                principalTable: "AdvertisementVariants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BasketItems_AdvertisementVariants_AdvertisementVariantId",
                table: "BasketItems");

            migrationBuilder.RenameColumn(
                name: "AdvertisementVariantId",
                table: "BasketItems",
                newName: "AdvertisementId");

            migrationBuilder.RenameIndex(
                name: "IX_BasketItems_BasketId_AdvertisementVariantId",
                table: "BasketItems",
                newName: "IX_BasketItems_BasketId_AdvertisementId");

            migrationBuilder.RenameIndex(
                name: "IX_BasketItems_AdvertisementVariantId",
                table: "BasketItems",
                newName: "IX_BasketItems_AdvertisementId");

            migrationBuilder.AddForeignKey(
                name: "FK_BasketItems_Advertisements_AdvertisementId",
                table: "BasketItems",
                column: "AdvertisementId",
                principalTable: "Advertisements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
