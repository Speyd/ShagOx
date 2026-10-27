using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShagOxServer.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SyncPendingChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReviewComment_Advertisements_CommentableEntityId",
                table: "ReviewComment");

            migrationBuilder.DropForeignKey(
                name: "FK_ReviewComment_Users_OwnerId",
                table: "ReviewComment");

            migrationBuilder.DropForeignKey(
                name: "FK_ReviewImages_ReviewComment_OwnerId",
                table: "ReviewImages");

            migrationBuilder.DropForeignKey(
                name: "FK_ReviewRepliComment_ReviewComment_CommentableEntityId",
                table: "ReviewRepliComment");

            migrationBuilder.DropForeignKey(
                name: "FK_ReviewRepliComment_ReviewRepliComment_ParentReplyId",
                table: "ReviewRepliComment");

            migrationBuilder.DropForeignKey(
                name: "FK_ReviewRepliComment_Users_OwnerId",
                table: "ReviewRepliComment");

            migrationBuilder.DropForeignKey(
                name: "FK_ReviewRepliImages_ReviewRepliComment_OwnerId",
                table: "ReviewRepliImages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ReviewRepliComment",
                table: "ReviewRepliComment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ReviewComment",
                table: "ReviewComment");

            migrationBuilder.RenameTable(
                name: "ReviewRepliComment",
                newName: "ReviewRepliComments");

            migrationBuilder.RenameTable(
                name: "ReviewComment",
                newName: "ReviewComments");

            migrationBuilder.RenameIndex(
                name: "IX_ReviewRepliComment_ParentReplyId_CreatedAt",
                table: "ReviewRepliComments",
                newName: "IX_ReviewRepliComments_ParentReplyId_CreatedAt");

            migrationBuilder.RenameIndex(
                name: "IX_ReviewRepliComment_ParentReplyId",
                table: "ReviewRepliComments",
                newName: "IX_ReviewRepliComments_ParentReplyId");

            migrationBuilder.RenameIndex(
                name: "IX_ReviewRepliComment_OwnerId",
                table: "ReviewRepliComments",
                newName: "IX_ReviewRepliComments_OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_ReviewRepliComment_CommentableEntityId_CreatedAt",
                table: "ReviewRepliComments",
                newName: "IX_ReviewRepliComments_CommentableEntityId_CreatedAt");

            migrationBuilder.RenameIndex(
                name: "IX_ReviewRepliComment_CommentableEntityId",
                table: "ReviewRepliComments",
                newName: "IX_ReviewRepliComments_CommentableEntityId");

            migrationBuilder.RenameIndex(
                name: "IX_ReviewComment_OwnerId",
                table: "ReviewComments",
                newName: "IX_ReviewComments_OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_ReviewComment_CommentableEntityId_CreatedAt",
                table: "ReviewComments",
                newName: "IX_ReviewComments_CommentableEntityId_CreatedAt");

            migrationBuilder.RenameIndex(
                name: "IX_ReviewComment_CommentableEntityId",
                table: "ReviewComments",
                newName: "IX_ReviewComments_CommentableEntityId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReviewRepliComments",
                table: "ReviewRepliComments",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReviewComments",
                table: "ReviewComments",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ReviewComments_Advertisements_CommentableEntityId",
                table: "ReviewComments",
                column: "CommentableEntityId",
                principalTable: "Advertisements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReviewComments_Users_OwnerId",
                table: "ReviewComments",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ReviewImages_ReviewComments_OwnerId",
                table: "ReviewImages",
                column: "OwnerId",
                principalTable: "ReviewComments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReviewRepliComments_ReviewComments_CommentableEntityId",
                table: "ReviewRepliComments",
                column: "CommentableEntityId",
                principalTable: "ReviewComments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReviewRepliComments_ReviewRepliComments_ParentReplyId",
                table: "ReviewRepliComments",
                column: "ParentReplyId",
                principalTable: "ReviewRepliComments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReviewRepliComments_Users_OwnerId",
                table: "ReviewRepliComments",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ReviewRepliImages_ReviewRepliComments_OwnerId",
                table: "ReviewRepliImages",
                column: "OwnerId",
                principalTable: "ReviewRepliComments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReviewComments_Advertisements_CommentableEntityId",
                table: "ReviewComments");

            migrationBuilder.DropForeignKey(
                name: "FK_ReviewComments_Users_OwnerId",
                table: "ReviewComments");

            migrationBuilder.DropForeignKey(
                name: "FK_ReviewImages_ReviewComments_OwnerId",
                table: "ReviewImages");

            migrationBuilder.DropForeignKey(
                name: "FK_ReviewRepliComments_ReviewComments_CommentableEntityId",
                table: "ReviewRepliComments");

            migrationBuilder.DropForeignKey(
                name: "FK_ReviewRepliComments_ReviewRepliComments_ParentReplyId",
                table: "ReviewRepliComments");

            migrationBuilder.DropForeignKey(
                name: "FK_ReviewRepliComments_Users_OwnerId",
                table: "ReviewRepliComments");

            migrationBuilder.DropForeignKey(
                name: "FK_ReviewRepliImages_ReviewRepliComments_OwnerId",
                table: "ReviewRepliImages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ReviewRepliComments",
                table: "ReviewRepliComments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ReviewComments",
                table: "ReviewComments");

            migrationBuilder.RenameTable(
                name: "ReviewRepliComments",
                newName: "ReviewRepliComment");

            migrationBuilder.RenameTable(
                name: "ReviewComments",
                newName: "ReviewComment");

            migrationBuilder.RenameIndex(
                name: "IX_ReviewRepliComments_ParentReplyId_CreatedAt",
                table: "ReviewRepliComment",
                newName: "IX_ReviewRepliComment_ParentReplyId_CreatedAt");

            migrationBuilder.RenameIndex(
                name: "IX_ReviewRepliComments_ParentReplyId",
                table: "ReviewRepliComment",
                newName: "IX_ReviewRepliComment_ParentReplyId");

            migrationBuilder.RenameIndex(
                name: "IX_ReviewRepliComments_OwnerId",
                table: "ReviewRepliComment",
                newName: "IX_ReviewRepliComment_OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_ReviewRepliComments_CommentableEntityId_CreatedAt",
                table: "ReviewRepliComment",
                newName: "IX_ReviewRepliComment_CommentableEntityId_CreatedAt");

            migrationBuilder.RenameIndex(
                name: "IX_ReviewRepliComments_CommentableEntityId",
                table: "ReviewRepliComment",
                newName: "IX_ReviewRepliComment_CommentableEntityId");

            migrationBuilder.RenameIndex(
                name: "IX_ReviewComments_OwnerId",
                table: "ReviewComment",
                newName: "IX_ReviewComment_OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_ReviewComments_CommentableEntityId_CreatedAt",
                table: "ReviewComment",
                newName: "IX_ReviewComment_CommentableEntityId_CreatedAt");

            migrationBuilder.RenameIndex(
                name: "IX_ReviewComments_CommentableEntityId",
                table: "ReviewComment",
                newName: "IX_ReviewComment_CommentableEntityId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReviewRepliComment",
                table: "ReviewRepliComment",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReviewComment",
                table: "ReviewComment",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ReviewComment_Advertisements_CommentableEntityId",
                table: "ReviewComment",
                column: "CommentableEntityId",
                principalTable: "Advertisements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReviewComment_Users_OwnerId",
                table: "ReviewComment",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ReviewImages_ReviewComment_OwnerId",
                table: "ReviewImages",
                column: "OwnerId",
                principalTable: "ReviewComment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReviewRepliComment_ReviewComment_CommentableEntityId",
                table: "ReviewRepliComment",
                column: "CommentableEntityId",
                principalTable: "ReviewComment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReviewRepliComment_ReviewRepliComment_ParentReplyId",
                table: "ReviewRepliComment",
                column: "ParentReplyId",
                principalTable: "ReviewRepliComment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReviewRepliComment_Users_OwnerId",
                table: "ReviewRepliComment",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ReviewRepliImages_ReviewRepliComment_OwnerId",
                table: "ReviewRepliImages",
                column: "OwnerId",
                principalTable: "ReviewRepliComment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
