using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShagOxServer.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddReviewImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReviewImage_ReviewComment_OwnerId",
                table: "ReviewImage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ReviewImage",
                table: "ReviewImage");

            migrationBuilder.DropIndex(
                name: "IX_ReviewImage_OwnerId",
                table: "ReviewImage");

            migrationBuilder.RenameTable(
                name: "ReviewImage",
                newName: "ReviewImages");

            migrationBuilder.AlterColumn<string>(
                name: "PublicId",
                table: "ReviewImages",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReviewImages",
                table: "ReviewImages",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewImages_OwnerId_Order",
                table: "ReviewImages",
                columns: new[] { "OwnerId", "Order" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ReviewImages_ReviewComment_OwnerId",
                table: "ReviewImages",
                column: "OwnerId",
                principalTable: "ReviewComment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReviewImages_ReviewComment_OwnerId",
                table: "ReviewImages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ReviewImages",
                table: "ReviewImages");

            migrationBuilder.DropIndex(
                name: "IX_ReviewImages_OwnerId_Order",
                table: "ReviewImages");

            migrationBuilder.RenameTable(
                name: "ReviewImages",
                newName: "ReviewImage");

            migrationBuilder.AlterColumn<string>(
                name: "PublicId",
                table: "ReviewImage",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReviewImage",
                table: "ReviewImage",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewImage_OwnerId",
                table: "ReviewImage",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReviewImage_ReviewComment_OwnerId",
                table: "ReviewImage",
                column: "OwnerId",
                principalTable: "ReviewComment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
