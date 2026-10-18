using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShagOxServer.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddReviewRepliImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReviewRepliImage_ReviewRepliComment_OwnerId",
                table: "ReviewRepliImage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ReviewRepliImage",
                table: "ReviewRepliImage");

            migrationBuilder.DropIndex(
                name: "IX_ReviewRepliImage_OwnerId",
                table: "ReviewRepliImage");

            migrationBuilder.RenameTable(
                name: "ReviewRepliImage",
                newName: "ReviewRepliImages");

            migrationBuilder.AlterColumn<string>(
                name: "PublicId",
                table: "ReviewRepliImages",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReviewRepliImages",
                table: "ReviewRepliImages",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewRepliImages_OwnerId_Order",
                table: "ReviewRepliImages",
                columns: new[] { "OwnerId", "Order" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ReviewRepliImages_ReviewRepliComment_OwnerId",
                table: "ReviewRepliImages",
                column: "OwnerId",
                principalTable: "ReviewRepliComment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReviewRepliImages_ReviewRepliComment_OwnerId",
                table: "ReviewRepliImages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ReviewRepliImages",
                table: "ReviewRepliImages");

            migrationBuilder.DropIndex(
                name: "IX_ReviewRepliImages_OwnerId_Order",
                table: "ReviewRepliImages");

            migrationBuilder.RenameTable(
                name: "ReviewRepliImages",
                newName: "ReviewRepliImage");

            migrationBuilder.AlterColumn<string>(
                name: "PublicId",
                table: "ReviewRepliImage",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReviewRepliImage",
                table: "ReviewRepliImage",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewRepliImage_OwnerId",
                table: "ReviewRepliImage",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReviewRepliImage_ReviewRepliComment_OwnerId",
                table: "ReviewRepliImage",
                column: "OwnerId",
                principalTable: "ReviewRepliComment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
