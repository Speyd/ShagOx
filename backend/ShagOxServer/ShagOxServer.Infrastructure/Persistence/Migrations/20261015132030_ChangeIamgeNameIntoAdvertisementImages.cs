using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ShagOxServer.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeIamgeNameIntoAdvertisementImages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Avatars_Users_UserId",
                table: "Avatars");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Avatars",
                newName: "OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_Avatars_UserId",
                table: "Avatars",
                newName: "IX_Avatars_OwnerId");

            migrationBuilder.CreateTable(
                name: "AdvertisementImages",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Order = table.Column<int>(type: "integer", nullable: false),
                    Url = table.Column<string>(type: "text", nullable: false),
                    PublicId = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    OwnerId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdvertisementImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdvertisementImages_Advertisements_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Advertisements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdvertisementImages_OwnerId_Order",
                table: "AdvertisementImages",
                columns: new[] { "OwnerId", "Order" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Avatars_Users_OwnerId",
                table: "Avatars",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Avatars_Users_OwnerId",
                table: "Avatars");

            migrationBuilder.DropTable(
                name: "AdvertisementImages");

            migrationBuilder.RenameColumn(
                name: "OwnerId",
                table: "Avatars",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Avatars_OwnerId",
                table: "Avatars",
                newName: "IX_Avatars_UserId");

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AdvertisementId = table.Column<long>(type: "bigint", nullable: false),
                    Order = table.Column<int>(type: "integer", nullable: false),
                    PublicId = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Url = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Images_Advertisements_AdvertisementId",
                        column: x => x.AdvertisementId,
                        principalTable: "Advertisements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Images_AdvertisementId_Order",
                table: "Images",
                columns: new[] { "AdvertisementId", "Order" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Avatars_Users_UserId",
                table: "Avatars",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
