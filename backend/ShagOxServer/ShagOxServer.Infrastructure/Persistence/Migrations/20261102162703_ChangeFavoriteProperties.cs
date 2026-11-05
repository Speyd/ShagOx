using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShagOxServer.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeFavoriteProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Favorites_Users_UserId",
                table: "Favorites");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Favorites",
                newName: "OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_Favorites_UserId_AdvertisementId",
                table: "Favorites",
                newName: "IX_Favorites_OwnerId_AdvertisementId");

            migrationBuilder.AddForeignKey(
                name: "FK_Favorites_Users_OwnerId",
                table: "Favorites",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Favorites_Users_OwnerId",
                table: "Favorites");

            migrationBuilder.RenameColumn(
                name: "OwnerId",
                table: "Favorites",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Favorites_OwnerId_AdvertisementId",
                table: "Favorites",
                newName: "IX_Favorites_UserId_AdvertisementId");

            migrationBuilder.AddForeignKey(
                name: "FK_Favorites_Users_UserId",
                table: "Favorites",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
