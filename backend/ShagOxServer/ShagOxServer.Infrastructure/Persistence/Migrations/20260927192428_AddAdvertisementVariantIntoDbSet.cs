using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShagOxServer.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddAdvertisementVariantIntoDbSet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdvertisementVariant_Advertisements_AdvertisementId",
                table: "AdvertisementVariant");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AdvertisementVariant",
                table: "AdvertisementVariant");

            migrationBuilder.RenameTable(
                name: "AdvertisementVariant",
                newName: "AdvertisementVariants");

            migrationBuilder.RenameIndex(
                name: "IX_AdvertisementVariant_AdvertisementId",
                table: "AdvertisementVariants",
                newName: "IX_AdvertisementVariants_AdvertisementId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AdvertisementVariants",
                table: "AdvertisementVariants",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AdvertisementVariants_Advertisements_AdvertisementId",
                table: "AdvertisementVariants",
                column: "AdvertisementId",
                principalTable: "Advertisements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdvertisementVariants_Advertisements_AdvertisementId",
                table: "AdvertisementVariants");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AdvertisementVariants",
                table: "AdvertisementVariants");

            migrationBuilder.RenameTable(
                name: "AdvertisementVariants",
                newName: "AdvertisementVariant");

            migrationBuilder.RenameIndex(
                name: "IX_AdvertisementVariants_AdvertisementId",
                table: "AdvertisementVariant",
                newName: "IX_AdvertisementVariant_AdvertisementId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AdvertisementVariant",
                table: "AdvertisementVariant",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AdvertisementVariant_Advertisements_AdvertisementId",
                table: "AdvertisementVariant",
                column: "AdvertisementId",
                principalTable: "Advertisements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
