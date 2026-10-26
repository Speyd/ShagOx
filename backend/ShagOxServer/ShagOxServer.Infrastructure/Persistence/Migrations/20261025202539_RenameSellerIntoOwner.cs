using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShagOxServer.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RenameSellerIntoOwner : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Advertisements_Users_SellerId",
                table: "Advertisements");

            migrationBuilder.DropForeignKey(
                name: "FK_Baskets_Users_UserId",
                table: "Baskets");

            migrationBuilder.DropColumn(
                name: "xmin",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Baskets",
                newName: "OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_Baskets_UserId",
                table: "Baskets",
                newName: "IX_Baskets_OwnerId");

            migrationBuilder.RenameColumn(
                name: "SellerId",
                table: "Advertisements",
                newName: "OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_Advertisements_SellerId_CreatedAt",
                table: "Advertisements",
                newName: "IX_Advertisements_OwnerId_CreatedAt");

            migrationBuilder.RenameIndex(
                name: "IX_Advertisements_SellerId",
                table: "Advertisements",
                newName: "IX_Advertisements_OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Advertisements_Users_OwnerId",
                table: "Advertisements",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Baskets_Users_OwnerId",
                table: "Baskets",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Advertisements_Users_OwnerId",
                table: "Advertisements");

            migrationBuilder.DropForeignKey(
                name: "FK_Baskets_Users_OwnerId",
                table: "Baskets");

            migrationBuilder.RenameColumn(
                name: "OwnerId",
                table: "Baskets",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Baskets_OwnerId",
                table: "Baskets",
                newName: "IX_Baskets_UserId");

            migrationBuilder.RenameColumn(
                name: "OwnerId",
                table: "Advertisements",
                newName: "SellerId");

            migrationBuilder.RenameIndex(
                name: "IX_Advertisements_OwnerId_CreatedAt",
                table: "Advertisements",
                newName: "IX_Advertisements_SellerId_CreatedAt");

            migrationBuilder.RenameIndex(
                name: "IX_Advertisements_OwnerId",
                table: "Advertisements",
                newName: "IX_Advertisements_SellerId");

            migrationBuilder.AddColumn<uint>(
                name: "xmin",
                table: "Users",
                type: "xid",
                rowVersion: true,
                nullable: false,
                defaultValue: 0u);

            migrationBuilder.AddForeignKey(
                name: "FK_Advertisements_Users_SellerId",
                table: "Advertisements",
                column: "SellerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Baskets_Users_UserId",
                table: "Baskets",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
