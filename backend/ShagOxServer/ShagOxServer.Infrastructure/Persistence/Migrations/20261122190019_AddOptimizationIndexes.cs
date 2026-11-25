using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShagOxServer.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddOptimizationIndexes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ReviewRepliComments_CommentableEntityId",
                table: "ReviewRepliComments");

            migrationBuilder.DropIndex(
                name: "IX_ReviewRepliComments_CommentableEntityId_CreatedAt",
                table: "ReviewRepliComments");

            migrationBuilder.DropIndex(
                name: "IX_ReviewRepliComments_OwnerId",
                table: "ReviewRepliComments");

            migrationBuilder.DropIndex(
                name: "IX_ReviewRepliComments_ParentReplyId",
                table: "ReviewRepliComments");

            migrationBuilder.DropIndex(
                name: "IX_ReviewRepliComments_ParentReplyId_CreatedAt",
                table: "ReviewRepliComments");

            migrationBuilder.DropIndex(
                name: "IX_ReviewComments_CommentableEntityId_CreatedAt",
                table: "ReviewComments");

            migrationBuilder.DropIndex(
                name: "IX_ReviewComments_OwnerId",
                table: "ReviewComments");

            migrationBuilder.DropIndex(
                name: "IX_Orders_DeliveryType",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_OrderStatus_CreatedAt",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_OwnerId_CreatedAt",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_PaymentMethod_PaymentStatus",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_PaymentProvider_PaymentTransactionId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_OrderItems_AdvertisementId",
                table: "OrderItems");

            migrationBuilder.DropIndex(
                name: "IX_OrderItems_AdvertisementVariantId",
                table: "OrderItems");

            migrationBuilder.DropIndex(
                name: "IX_OrderItems_ParentId",
                table: "OrderItems");

            migrationBuilder.DropIndex(
                name: "IX_Favorites_AdvertisementId",
                table: "Favorites");

            migrationBuilder.DropIndex(
                name: "IX_Cities_RegionId",
                table: "Cities");

            migrationBuilder.DropIndex(
                name: "IX_Categories_ProductTypeId",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_BasketItems_AdvertisementId",
                table: "BasketItems");

            migrationBuilder.DropIndex(
                name: "IX_BasketItems_AdvertisementVariantId",
                table: "BasketItems");

            migrationBuilder.DropIndex(
                name: "IX_AttributeDefinitions_DictionaryId",
                table: "AttributeDefinitions");

            migrationBuilder.DropIndex(
                name: "IX_AdvertisementVariants_AdvertisementId",
                table: "AdvertisementVariants");

            migrationBuilder.DropIndex(
                name: "IX_Advertisements_BuyerId",
                table: "Advertisements");

            migrationBuilder.DropIndex(
                name: "IX_Advertisements_CategoryId",
                table: "Advertisements");

            migrationBuilder.DropIndex(
                name: "IX_Advertisements_CategoryId_ConditionId_CreatedAt",
                table: "Advertisements");

            migrationBuilder.DropIndex(
                name: "IX_Advertisements_OwnerId",
                table: "Advertisements");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RegisteredAt_Id",
                table: "Users",
                columns: new[] { "RegisteredAt", "Id" },
                descending: new bool[0],
                filter: "\"EntityStatus\" IN ('Active', 'Hidden')");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_UserId_EntityStatus_RoleId",
                table: "UserRoles",
                columns: new[] { "UserId", "EntityStatus", "RoleId" });

            migrationBuilder.CreateIndex(
                name: "IX_Statuses_Code_EntityStatus",
                table: "Statuses",
                columns: new[] { "Code", "EntityStatus" });

            migrationBuilder.CreateIndex(
                name: "IX_Roles_Name",
                table: "Roles",
                column: "Name",
                unique: true,
                filter: "\"EntityStatus\" IN ('Active', 'Hidden')");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewRepliImages_EntityStatus_OwnerId",
                table: "ReviewRepliImages",
                columns: new[] { "EntityStatus", "OwnerId" });

            migrationBuilder.CreateIndex(
                name: "IX_ReviewRepliComments_CommentableEntityId_EntityStatus_Id",
                table: "ReviewRepliComments",
                columns: new[] { "CommentableEntityId", "EntityStatus", "Id" });

            migrationBuilder.CreateIndex(
                name: "IX_ReviewRepliComments_OwnerId_EntityStatus_Id",
                table: "ReviewRepliComments",
                columns: new[] { "OwnerId", "EntityStatus", "Id" });

            migrationBuilder.CreateIndex(
                name: "IX_ReviewRepliComments_ParentReplyId_EntityStatus_CreatedAt",
                table: "ReviewRepliComments",
                columns: new[] { "ParentReplyId", "EntityStatus", "CreatedAt" });

            migrationBuilder.CreateIndex(
                name: "IX_ReviewImages_EntityStatus_OwnerId",
                table: "ReviewImages",
                columns: new[] { "EntityStatus", "OwnerId" });

            migrationBuilder.CreateIndex(
                name: "IX_ReviewComments_CommentableEntityId_EntityStatus_Id",
                table: "ReviewComments",
                columns: new[] { "CommentableEntityId", "EntityStatus", "Id" });

            migrationBuilder.CreateIndex(
                name: "IX_ReviewComments_Grade",
                table: "ReviewComments",
                column: "Grade",
                filter: "\"EntityStatus\" IN ('Active', 'Hidden')");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewComments_OwnerId_EntityStatus_Id",
                table: "ReviewComments",
                columns: new[] { "OwnerId", "EntityStatus", "Id" });

            migrationBuilder.CreateIndex(
                name: "IX_PaymentWebhooks_InvoiceId_Status",
                table: "PaymentWebhooks",
                columns: new[] { "InvoiceId", "Status" },
                filter: "\"EntityStatus\" = 'Active'");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentWebhooks_ReceivedAt",
                table: "PaymentWebhooks",
                column: "ReceivedAt",
                filter: "\"EntityStatus\" = 'Active' AND \"IsProcessed\" = false");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_EntityStatus_DeliveryProviderId",
                table: "Orders",
                columns: new[] { "EntityStatus", "DeliveryProviderId" });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_EntityStatus_DeliveryTrackingNumber",
                table: "Orders",
                columns: new[] { "EntityStatus", "DeliveryTrackingNumber" });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_EntityStatus_DeliveryType",
                table: "Orders",
                columns: new[] { "EntityStatus", "DeliveryType" });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_EntityStatus_OrderStatus_CreatedAt",
                table: "Orders",
                columns: new[] { "EntityStatus", "OrderStatus", "CreatedAt" });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_EntityStatus_OwnerId_CreatedAt",
                table: "Orders",
                columns: new[] { "EntityStatus", "OwnerId", "CreatedAt" });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_EntityStatus_PaymentMethod_PaymentStatus",
                table: "Orders",
                columns: new[] { "EntityStatus", "PaymentMethod", "PaymentStatus" });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_EntityStatus_PaymentProvider_PaymentTransactionId",
                table: "Orders",
                columns: new[] { "EntityStatus", "PaymentProvider", "PaymentTransactionId" });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OwnerId",
                table: "Orders",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_AdvertisementId_EntityStatus_Id",
                table: "OrderItems",
                columns: new[] { "AdvertisementId", "EntityStatus", "Id" });

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_AdvertisementVariantId_EntityStatus_Id",
                table: "OrderItems",
                columns: new[] { "AdvertisementVariantId", "EntityStatus", "Id" });

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_ParentId_EntityStatus_Id",
                table: "OrderItems",
                columns: new[] { "ParentId", "EntityStatus", "Id" });

            migrationBuilder.CreateIndex(
                name: "IX_LineItemAttributes_Order_EntityStatus",
                table: "LineItemAttributes",
                columns: new[] { "Order", "EntityStatus" });

            migrationBuilder.CreateIndex(
                name: "IX_Favorites_AdvertisementId_EntityStatus",
                table: "Favorites",
                columns: new[] { "AdvertisementId", "EntityStatus" });

            migrationBuilder.CreateIndex(
                name: "IX_Favorites_OwnerId_EntityStatus_Id",
                table: "Favorites",
                columns: new[] { "OwnerId", "EntityStatus", "Id" });

            migrationBuilder.CreateIndex(
                name: "IX_Cities_RegionId_EntityStatus_Id",
                table: "Cities",
                columns: new[] { "RegionId", "EntityStatus", "Id" });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryImages_EntityStatus_OwnerId",
                table: "CategoryImages",
                columns: new[] { "EntityStatus", "OwnerId" });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ProductTypeId_EntityStatus_Id",
                table: "Categories",
                columns: new[] { "ProductTypeId", "EntityStatus", "Id" });

            migrationBuilder.CreateIndex(
                name: "IX_BasketItems_AdvertisementId_EntityStatus_Id",
                table: "BasketItems",
                columns: new[] { "AdvertisementId", "EntityStatus", "Id" });

            migrationBuilder.CreateIndex(
                name: "IX_BasketItems_AdvertisementVariantId_EntityStatus_Id",
                table: "BasketItems",
                columns: new[] { "AdvertisementVariantId", "EntityStatus", "Id" });

            migrationBuilder.CreateIndex(
                name: "IX_BasketItems_ParentId_EntityStatus_Id",
                table: "BasketItems",
                columns: new[] { "ParentId", "EntityStatus", "Id" });

            migrationBuilder.CreateIndex(
                name: "IX_Avatars_EntityStatus_OwnerId",
                table: "Avatars",
                columns: new[] { "EntityStatus", "OwnerId" });

            migrationBuilder.CreateIndex(
                name: "IX_AttributeDictionaryValues_Code_EntityStatus",
                table: "AttributeDictionaryValues",
                columns: new[] { "Code", "EntityStatus" });

            migrationBuilder.CreateIndex(
                name: "IX_AttributeDictionaryValues_DictionaryId_EntityStatus_Id",
                table: "AttributeDictionaryValues",
                columns: new[] { "DictionaryId", "EntityStatus", "Id" });

            migrationBuilder.CreateIndex(
                name: "IX_AttributeDefinitions_CategoryId_EntityStatus",
                table: "AttributeDefinitions",
                columns: new[] { "CategoryId", "EntityStatus" });

            migrationBuilder.CreateIndex(
                name: "IX_AttributeDefinitions_DictionaryId_EntityStatus_Id",
                table: "AttributeDefinitions",
                columns: new[] { "DictionaryId", "EntityStatus", "Id" });

            migrationBuilder.CreateIndex(
                name: "IX_AdvertisementVariants_AdvertisementId",
                table: "AdvertisementVariants",
                column: "AdvertisementId",
                filter: "\"EntityStatus\" <> 'Deleted'");

            migrationBuilder.CreateIndex(
                name: "IX_AdvertisementVariants_AdvertisementId_EntityStatus",
                table: "AdvertisementVariants",
                columns: new[] { "AdvertisementId", "EntityStatus" });

            migrationBuilder.CreateIndex(
                name: "IX_Advertisements_BuyerId_EntityStatus",
                table: "Advertisements",
                columns: new[] { "BuyerId", "EntityStatus" });

            migrationBuilder.CreateIndex(
                name: "IX_Advertisements_CategoryId",
                table: "Advertisements",
                column: "CategoryId",
                filter: "\"EntityStatus\" IN ('Active', 'Hidden')");

            migrationBuilder.CreateIndex(
                name: "IX_Advertisements_CategoryId_EntityStatus",
                table: "Advertisements",
                columns: new[] { "CategoryId", "EntityStatus" });

            migrationBuilder.CreateIndex(
                name: "IX_Advertisements_CreatedAt_Id",
                table: "Advertisements",
                columns: new[] { "CreatedAt", "Id" },
                descending: new bool[0],
                filter: "\"EntityStatus\" IN ('Active', 'Hidden')");

            migrationBuilder.CreateIndex(
                name: "IX_Advertisements_OwnerId",
                table: "Advertisements",
                column: "OwnerId",
                filter: "\"EntityStatus\" IN ('Active', 'Hidden')");

            migrationBuilder.CreateIndex(
                name: "IX_Advertisements_OwnerId_EntityStatus",
                table: "Advertisements",
                columns: new[] { "OwnerId", "EntityStatus" });

            migrationBuilder.CreateIndex(
                name: "IX_Advertisements_Popularity",
                table: "Advertisements",
                column: "Popularity");

            migrationBuilder.CreateIndex(
                name: "IX_Advertisements_SoldAt",
                table: "Advertisements",
                column: "SoldAt",
                filter: "\"EntityStatus\" IN ('Active', 'Hidden') AND \"SoldAt\" IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Advertisements_StatusId_EntityStatus",
                table: "Advertisements",
                columns: new[] { "StatusId", "EntityStatus" });

            migrationBuilder.CreateIndex(
                name: "IX_AdvertisementImages_EntityStatus_OwnerId",
                table: "AdvertisementImages",
                columns: new[] { "EntityStatus", "OwnerId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_RegisteredAt_Id",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_UserRoles_UserId_EntityStatus_RoleId",
                table: "UserRoles");

            migrationBuilder.DropIndex(
                name: "IX_Statuses_Code_EntityStatus",
                table: "Statuses");

            migrationBuilder.DropIndex(
                name: "IX_Roles_Name",
                table: "Roles");

            migrationBuilder.DropIndex(
                name: "IX_ReviewRepliImages_EntityStatus_OwnerId",
                table: "ReviewRepliImages");

            migrationBuilder.DropIndex(
                name: "IX_ReviewRepliComments_CommentableEntityId_EntityStatus_Id",
                table: "ReviewRepliComments");

            migrationBuilder.DropIndex(
                name: "IX_ReviewRepliComments_OwnerId_EntityStatus_Id",
                table: "ReviewRepliComments");

            migrationBuilder.DropIndex(
                name: "IX_ReviewRepliComments_ParentReplyId_EntityStatus_CreatedAt",
                table: "ReviewRepliComments");

            migrationBuilder.DropIndex(
                name: "IX_ReviewImages_EntityStatus_OwnerId",
                table: "ReviewImages");

            migrationBuilder.DropIndex(
                name: "IX_ReviewComments_CommentableEntityId_EntityStatus_Id",
                table: "ReviewComments");

            migrationBuilder.DropIndex(
                name: "IX_ReviewComments_Grade",
                table: "ReviewComments");

            migrationBuilder.DropIndex(
                name: "IX_ReviewComments_OwnerId_EntityStatus_Id",
                table: "ReviewComments");

            migrationBuilder.DropIndex(
                name: "IX_PaymentWebhooks_InvoiceId_Status",
                table: "PaymentWebhooks");

            migrationBuilder.DropIndex(
                name: "IX_PaymentWebhooks_ReceivedAt",
                table: "PaymentWebhooks");

            migrationBuilder.DropIndex(
                name: "IX_Orders_EntityStatus_DeliveryProviderId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_EntityStatus_DeliveryTrackingNumber",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_EntityStatus_DeliveryType",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_EntityStatus_OrderStatus_CreatedAt",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_EntityStatus_OwnerId_CreatedAt",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_EntityStatus_PaymentMethod_PaymentStatus",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_EntityStatus_PaymentProvider_PaymentTransactionId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_OwnerId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_OrderItems_AdvertisementId_EntityStatus_Id",
                table: "OrderItems");

            migrationBuilder.DropIndex(
                name: "IX_OrderItems_AdvertisementVariantId_EntityStatus_Id",
                table: "OrderItems");

            migrationBuilder.DropIndex(
                name: "IX_OrderItems_ParentId_EntityStatus_Id",
                table: "OrderItems");

            migrationBuilder.DropIndex(
                name: "IX_LineItemAttributes_Order_EntityStatus",
                table: "LineItemAttributes");

            migrationBuilder.DropIndex(
                name: "IX_Favorites_AdvertisementId_EntityStatus",
                table: "Favorites");

            migrationBuilder.DropIndex(
                name: "IX_Favorites_OwnerId_EntityStatus_Id",
                table: "Favorites");

            migrationBuilder.DropIndex(
                name: "IX_Cities_RegionId_EntityStatus_Id",
                table: "Cities");

            migrationBuilder.DropIndex(
                name: "IX_CategoryImages_EntityStatus_OwnerId",
                table: "CategoryImages");

            migrationBuilder.DropIndex(
                name: "IX_Categories_ProductTypeId_EntityStatus_Id",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_BasketItems_AdvertisementId_EntityStatus_Id",
                table: "BasketItems");

            migrationBuilder.DropIndex(
                name: "IX_BasketItems_AdvertisementVariantId_EntityStatus_Id",
                table: "BasketItems");

            migrationBuilder.DropIndex(
                name: "IX_BasketItems_ParentId_EntityStatus_Id",
                table: "BasketItems");

            migrationBuilder.DropIndex(
                name: "IX_Avatars_EntityStatus_OwnerId",
                table: "Avatars");

            migrationBuilder.DropIndex(
                name: "IX_AttributeDictionaryValues_Code_EntityStatus",
                table: "AttributeDictionaryValues");

            migrationBuilder.DropIndex(
                name: "IX_AttributeDictionaryValues_DictionaryId_EntityStatus_Id",
                table: "AttributeDictionaryValues");

            migrationBuilder.DropIndex(
                name: "IX_AttributeDefinitions_CategoryId_EntityStatus",
                table: "AttributeDefinitions");

            migrationBuilder.DropIndex(
                name: "IX_AttributeDefinitions_DictionaryId_EntityStatus_Id",
                table: "AttributeDefinitions");

            migrationBuilder.DropIndex(
                name: "IX_AdvertisementVariants_AdvertisementId",
                table: "AdvertisementVariants");

            migrationBuilder.DropIndex(
                name: "IX_AdvertisementVariants_AdvertisementId_EntityStatus",
                table: "AdvertisementVariants");

            migrationBuilder.DropIndex(
                name: "IX_Advertisements_BuyerId_EntityStatus",
                table: "Advertisements");

            migrationBuilder.DropIndex(
                name: "IX_Advertisements_CategoryId",
                table: "Advertisements");

            migrationBuilder.DropIndex(
                name: "IX_Advertisements_CategoryId_EntityStatus",
                table: "Advertisements");

            migrationBuilder.DropIndex(
                name: "IX_Advertisements_CreatedAt_Id",
                table: "Advertisements");

            migrationBuilder.DropIndex(
                name: "IX_Advertisements_OwnerId",
                table: "Advertisements");

            migrationBuilder.DropIndex(
                name: "IX_Advertisements_OwnerId_EntityStatus",
                table: "Advertisements");

            migrationBuilder.DropIndex(
                name: "IX_Advertisements_Popularity",
                table: "Advertisements");

            migrationBuilder.DropIndex(
                name: "IX_Advertisements_SoldAt",
                table: "Advertisements");

            migrationBuilder.DropIndex(
                name: "IX_Advertisements_StatusId_EntityStatus",
                table: "Advertisements");

            migrationBuilder.DropIndex(
                name: "IX_AdvertisementImages_EntityStatus_OwnerId",
                table: "AdvertisementImages");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewRepliComments_CommentableEntityId",
                table: "ReviewRepliComments",
                column: "CommentableEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewRepliComments_CommentableEntityId_CreatedAt",
                table: "ReviewRepliComments",
                columns: new[] { "CommentableEntityId", "CreatedAt" });

            migrationBuilder.CreateIndex(
                name: "IX_ReviewRepliComments_OwnerId",
                table: "ReviewRepliComments",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewRepliComments_ParentReplyId",
                table: "ReviewRepliComments",
                column: "ParentReplyId");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewRepliComments_ParentReplyId_CreatedAt",
                table: "ReviewRepliComments",
                columns: new[] { "ParentReplyId", "CreatedAt" });


            migrationBuilder.CreateIndex(
                name: "IX_ReviewComments_CommentableEntityId_CreatedAt",
                table: "ReviewComments",
                columns: new[] { "CommentableEntityId", "CreatedAt" });

            migrationBuilder.CreateIndex(
                name: "IX_ReviewComments_OwnerId",
                table: "ReviewComments",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_DeliveryType",
                table: "Orders",
                column: "DeliveryType");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OrderStatus_CreatedAt",
                table: "Orders",
                columns: new[] { "OrderStatus", "CreatedAt" });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OwnerId_CreatedAt",
                table: "Orders",
                columns: new[] { "OwnerId", "CreatedAt" });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_PaymentMethod_PaymentStatus",
                table: "Orders",
                columns: new[] { "PaymentMethod", "PaymentStatus" });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_PaymentProvider_PaymentTransactionId",
                table: "Orders",
                columns: new[] { "PaymentProvider", "PaymentTransactionId" });

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_AdvertisementId",
                table: "OrderItems",
                column: "AdvertisementId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_AdvertisementVariantId",
                table: "OrderItems",
                column: "AdvertisementVariantId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_ParentId",
                table: "OrderItems",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Favorites_AdvertisementId",
                table: "Favorites",
                column: "AdvertisementId");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_RegionId",
                table: "Cities",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ProductTypeId",
                table: "Categories",
                column: "ProductTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_BasketItems_AdvertisementId",
                table: "BasketItems",
                column: "AdvertisementId");

            migrationBuilder.CreateIndex(
                name: "IX_BasketItems_AdvertisementVariantId",
                table: "BasketItems",
                column: "AdvertisementVariantId");

            migrationBuilder.CreateIndex(
                name: "IX_AttributeDefinitions_DictionaryId",
                table: "AttributeDefinitions",
                column: "DictionaryId");

            migrationBuilder.CreateIndex(
                name: "IX_AdvertisementVariants_AdvertisementId",
                table: "AdvertisementVariants",
                column: "AdvertisementId");

            migrationBuilder.CreateIndex(
                name: "IX_Advertisements_BuyerId",
                table: "Advertisements",
                column: "BuyerId");

            migrationBuilder.CreateIndex(
                name: "IX_Advertisements_CategoryId",
                table: "Advertisements",
                column: "CategoryId",
                filter: "\"EntityStatus\" = 'Active'");

            migrationBuilder.CreateIndex(
                name: "IX_Advertisements_CategoryId_ConditionId_CreatedAt",
                table: "Advertisements",
                columns: new[] { "CategoryId", "ConditionId", "CreatedAt" });

            migrationBuilder.CreateIndex(
                name: "IX_Advertisements_OwnerId",
                table: "Advertisements",
                column: "OwnerId");
        }
    }
}
