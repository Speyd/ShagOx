using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShagOxServer.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddXminOnlyUser : Migration
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

            migrationBuilder.DropColumn(
                name: "xmin",
                table: "VerificationCodes");

            migrationBuilder.DropColumn(
                name: "xmin",
                table: "UserRoles");

            migrationBuilder.DropColumn(
                name: "xmin",
                table: "StatusTranslations");

            migrationBuilder.DropColumn(
                name: "xmin",
                table: "Statuses");

            migrationBuilder.DropColumn(
                name: "xmin",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "xmin",
                table: "ReviewRepliImages");

            migrationBuilder.DropColumn(
                name: "xmin",
                table: "ReviewImages");

            migrationBuilder.DropColumn(
                name: "xmin",
                table: "RegionTranslations");

            migrationBuilder.DropColumn(
                name: "xmin",
                table: "Regions");

            migrationBuilder.DropColumn(
                name: "xmin",
                table: "ProductTypeTranslations");

            migrationBuilder.DropColumn(
                name: "xmin",
                table: "ProductTypes");

            migrationBuilder.DropColumn(
                name: "xmin",
                table: "Favorites");

            migrationBuilder.DropColumn(
                name: "xmin",
                table: "Currencies");

            migrationBuilder.DropColumn(
                name: "xmin",
                table: "ConditionTranslations");

            migrationBuilder.DropColumn(
                name: "xmin",
                table: "Conditions");

            migrationBuilder.DropColumn(
                name: "xmin",
                table: "CityTranslations");

            migrationBuilder.DropColumn(
                name: "xmin",
                table: "Cities");

            migrationBuilder.DropColumn(
                name: "xmin",
                table: "CategoryTranslations");

            migrationBuilder.DropColumn(
                name: "xmin",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "xmin",
                table: "Baskets");

            migrationBuilder.DropColumn(
                name: "xmin",
                table: "BasketItems");

            migrationBuilder.DropColumn(
                name: "xmin",
                table: "BasketAttributes");

            migrationBuilder.DropColumn(
                name: "xmin",
                table: "Avatars");

            migrationBuilder.DropColumn(
                name: "xmin",
                table: "AttributeDictionaryValueTranslations");

            migrationBuilder.DropColumn(
                name: "xmin",
                table: "AttributeDictionaryValues");

            migrationBuilder.DropColumn(
                name: "xmin",
                table: "AttributeDictionaries");

            migrationBuilder.DropColumn(
                name: "xmin",
                table: "AttributeDefinitionTranslations");

            migrationBuilder.DropColumn(
                name: "xmin",
                table: "AttributeDefinitions");

            migrationBuilder.DropColumn(
                name: "xmin",
                table: "AdvertisementVariants");

            migrationBuilder.DropColumn(
                name: "xmin",
                table: "Advertisements");

            migrationBuilder.DropColumn(
                name: "xmin",
                table: "AdvertisementImages");

            migrationBuilder.DropColumn(
                name: "xmin",
                table: "ReviewRepliComment");

            migrationBuilder.DropColumn(
                name: "xmin",
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

            migrationBuilder.AddColumn<uint>(
                name: "xmin",
                table: "VerificationCodes",
                type: "xid",
                rowVersion: true,
                nullable: false,
                defaultValue: 0u);

            migrationBuilder.AddColumn<uint>(
                name: "xmin",
                table: "UserRoles",
                type: "xid",
                rowVersion: true,
                nullable: false,
                defaultValue: 0u);

            migrationBuilder.AddColumn<uint>(
                name: "xmin",
                table: "StatusTranslations",
                type: "xid",
                rowVersion: true,
                nullable: false,
                defaultValue: 0u);

            migrationBuilder.AddColumn<uint>(
                name: "xmin",
                table: "Statuses",
                type: "xid",
                rowVersion: true,
                nullable: false,
                defaultValue: 0u);

            migrationBuilder.AddColumn<uint>(
                name: "xmin",
                table: "Roles",
                type: "xid",
                rowVersion: true,
                nullable: false,
                defaultValue: 0u);

            migrationBuilder.AddColumn<uint>(
                name: "xmin",
                table: "ReviewRepliImages",
                type: "xid",
                rowVersion: true,
                nullable: false,
                defaultValue: 0u);

            migrationBuilder.AddColumn<uint>(
                name: "xmin",
                table: "ReviewImages",
                type: "xid",
                rowVersion: true,
                nullable: false,
                defaultValue: 0u);

            migrationBuilder.AddColumn<uint>(
                name: "xmin",
                table: "RegionTranslations",
                type: "xid",
                rowVersion: true,
                nullable: false,
                defaultValue: 0u);

            migrationBuilder.AddColumn<uint>(
                name: "xmin",
                table: "Regions",
                type: "xid",
                rowVersion: true,
                nullable: false,
                defaultValue: 0u);

            migrationBuilder.AddColumn<uint>(
                name: "xmin",
                table: "ProductTypeTranslations",
                type: "xid",
                rowVersion: true,
                nullable: false,
                defaultValue: 0u);

            migrationBuilder.AddColumn<uint>(
                name: "xmin",
                table: "ProductTypes",
                type: "xid",
                rowVersion: true,
                nullable: false,
                defaultValue: 0u);

            migrationBuilder.AddColumn<uint>(
                name: "xmin",
                table: "Favorites",
                type: "xid",
                rowVersion: true,
                nullable: false,
                defaultValue: 0u);

            migrationBuilder.AddColumn<uint>(
                name: "xmin",
                table: "Currencies",
                type: "xid",
                rowVersion: true,
                nullable: false,
                defaultValue: 0u);

            migrationBuilder.AddColumn<uint>(
                name: "xmin",
                table: "ConditionTranslations",
                type: "xid",
                rowVersion: true,
                nullable: false,
                defaultValue: 0u);

            migrationBuilder.AddColumn<uint>(
                name: "xmin",
                table: "Conditions",
                type: "xid",
                rowVersion: true,
                nullable: false,
                defaultValue: 0u);

            migrationBuilder.AddColumn<uint>(
                name: "xmin",
                table: "CityTranslations",
                type: "xid",
                rowVersion: true,
                nullable: false,
                defaultValue: 0u);

            migrationBuilder.AddColumn<uint>(
                name: "xmin",
                table: "Cities",
                type: "xid",
                rowVersion: true,
                nullable: false,
                defaultValue: 0u);

            migrationBuilder.AddColumn<uint>(
                name: "xmin",
                table: "CategoryTranslations",
                type: "xid",
                rowVersion: true,
                nullable: false,
                defaultValue: 0u);

            migrationBuilder.AddColumn<uint>(
                name: "xmin",
                table: "Categories",
                type: "xid",
                rowVersion: true,
                nullable: false,
                defaultValue: 0u);

            migrationBuilder.AddColumn<uint>(
                name: "xmin",
                table: "Baskets",
                type: "xid",
                rowVersion: true,
                nullable: false,
                defaultValue: 0u);

            migrationBuilder.AddColumn<uint>(
                name: "xmin",
                table: "BasketItems",
                type: "xid",
                rowVersion: true,
                nullable: false,
                defaultValue: 0u);

            migrationBuilder.AddColumn<uint>(
                name: "xmin",
                table: "BasketAttributes",
                type: "xid",
                rowVersion: true,
                nullable: false,
                defaultValue: 0u);

            migrationBuilder.AddColumn<uint>(
                name: "xmin",
                table: "Avatars",
                type: "xid",
                rowVersion: true,
                nullable: false,
                defaultValue: 0u);

            migrationBuilder.AddColumn<uint>(
                name: "xmin",
                table: "AttributeDictionaryValueTranslations",
                type: "xid",
                rowVersion: true,
                nullable: false,
                defaultValue: 0u);

            migrationBuilder.AddColumn<uint>(
                name: "xmin",
                table: "AttributeDictionaryValues",
                type: "xid",
                rowVersion: true,
                nullable: false,
                defaultValue: 0u);

            migrationBuilder.AddColumn<uint>(
                name: "xmin",
                table: "AttributeDictionaries",
                type: "xid",
                rowVersion: true,
                nullable: false,
                defaultValue: 0u);

            migrationBuilder.AddColumn<uint>(
                name: "xmin",
                table: "AttributeDefinitionTranslations",
                type: "xid",
                rowVersion: true,
                nullable: false,
                defaultValue: 0u);

            migrationBuilder.AddColumn<uint>(
                name: "xmin",
                table: "AttributeDefinitions",
                type: "xid",
                rowVersion: true,
                nullable: false,
                defaultValue: 0u);

            migrationBuilder.AddColumn<uint>(
                name: "xmin",
                table: "AdvertisementVariants",
                type: "xid",
                rowVersion: true,
                nullable: false,
                defaultValue: 0u);

            migrationBuilder.AddColumn<uint>(
                name: "xmin",
                table: "Advertisements",
                type: "xid",
                rowVersion: true,
                nullable: false,
                defaultValue: 0u);

            migrationBuilder.AddColumn<uint>(
                name: "xmin",
                table: "AdvertisementImages",
                type: "xid",
                rowVersion: true,
                nullable: false,
                defaultValue: 0u);

            migrationBuilder.AddColumn<uint>(
                name: "xmin",
                table: "ReviewRepliComment",
                type: "xid",
                rowVersion: true,
                nullable: false,
                defaultValue: 0u);

            migrationBuilder.AddColumn<uint>(
                name: "xmin",
                table: "ReviewComment",
                type: "xid",
                rowVersion: true,
                nullable: false,
                defaultValue: 0u);

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
