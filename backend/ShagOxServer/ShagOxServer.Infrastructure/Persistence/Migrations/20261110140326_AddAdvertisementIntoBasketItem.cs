using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShagOxServer.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddAdvertisementIntoBasketItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_BasketItems_BasketId_AdvertisementVariantId",
                table: "BasketItems");

            migrationBuilder.AlterColumn<long>(
                name: "AdvertisementVariantId",
                table: "BasketItems",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<long>(
                name: "AdvertisementId",
                table: "BasketItems",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<int>(
                name: "Stock",
                table: "Advertisements",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_BasketItems_AdvertisementId",
                table: "BasketItems",
                column: "AdvertisementId");

            migrationBuilder.CreateIndex(
                name: "IX_BasketItems_BasketId_AdvertisementId",
                table: "BasketItems",
                columns: new[] { "BasketId", "AdvertisementId" },
                unique: true,
                filter: "(\"EntityStatus\" IN ('Active', 'Hidden')) AND (\"AdvertisementVariantId\" IS NULL)");

            migrationBuilder.CreateIndex(
                name: "IX_BasketItems_BasketId_AdvertisementVariantId",
                table: "BasketItems",
                columns: new[] { "BasketId", "AdvertisementVariantId" },
                unique: true,
                filter: "(\"EntityStatus\" IN ('Active', 'Hidden')) AND (\"AdvertisementVariantId\" IS NOT NULL)");

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

            migrationBuilder.DropIndex(
                name: "IX_BasketItems_BasketId_AdvertisementId",
                table: "BasketItems");

            migrationBuilder.DropIndex(
                name: "IX_BasketItems_BasketId_AdvertisementVariantId",
                table: "BasketItems");

            migrationBuilder.DropColumn(
                name: "AdvertisementId",
                table: "BasketItems");

            migrationBuilder.DropColumn(
                name: "Stock",
                table: "Advertisements");

            migrationBuilder.AlterColumn<long>(
                name: "AdvertisementVariantId",
                table: "BasketItems",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BasketItems_BasketId_AdvertisementVariantId",
                table: "BasketItems",
                columns: new[] { "BasketId", "AdvertisementVariantId" },
                unique: true,
                filter: "\"EntityStatus\" IN ('Active', 'Hidden')");
        }
    }
}
