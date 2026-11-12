using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShagOxServer.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RenamePropertiesInOderItemAndBasketItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BasketItems_Baskets_BasketId",
                table: "BasketItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_Currencies_CurrencyId",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_DeliveryProviders_DeliveryProviderId",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_Users_OwnerId",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItem_AdvertisementVariants_AdvertisementVariantId",
                table: "OrderItem");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItem_Advertisements_AdvertisementId",
                table: "OrderItem");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItem_Order_OrderId",
                table: "OrderItem");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItem_Users_UserId",
                table: "OrderItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderItem",
                table: "OrderItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Order",
                table: "Order");

            migrationBuilder.RenameTable(
                name: "OrderItem",
                newName: "OrderItems");

            migrationBuilder.RenameTable(
                name: "Order",
                newName: "Orders");

            migrationBuilder.RenameColumn(
                name: "BasketId",
                table: "BasketItems",
                newName: "ParentId");

            migrationBuilder.RenameIndex(
                name: "IX_BasketItems_BasketId_AdvertisementVariantId",
                table: "BasketItems",
                newName: "IX_BasketItems_ParentId_AdvertisementVariantId");

            migrationBuilder.RenameIndex(
                name: "IX_BasketItems_BasketId_AdvertisementId",
                table: "BasketItems",
                newName: "IX_BasketItems_ParentId_AdvertisementId");

            migrationBuilder.RenameColumn(
                name: "OrderId",
                table: "OrderItems",
                newName: "ParentId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderItem_UserId",
                table: "OrderItems",
                newName: "IX_OrderItems_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderItem_OrderId",
                table: "OrderItems",
                newName: "IX_OrderItems_ParentId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderItem_AdvertisementVariantId",
                table: "OrderItems",
                newName: "IX_OrderItems_AdvertisementVariantId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderItem_AdvertisementId",
                table: "OrderItems",
                newName: "IX_OrderItems_AdvertisementId");

            migrationBuilder.RenameIndex(
                name: "IX_Order_PaymentProvider_PaymentTransactionId",
                table: "Orders",
                newName: "IX_Orders_PaymentProvider_PaymentTransactionId");

            migrationBuilder.RenameIndex(
                name: "IX_Order_PaymentMethod_PaymentStatus",
                table: "Orders",
                newName: "IX_Orders_PaymentMethod_PaymentStatus");

            migrationBuilder.RenameIndex(
                name: "IX_Order_OwnerId_CreatedAt",
                table: "Orders",
                newName: "IX_Orders_OwnerId_CreatedAt");

            migrationBuilder.RenameIndex(
                name: "IX_Order_OrderStatus_CreatedAt",
                table: "Orders",
                newName: "IX_Orders_OrderStatus_CreatedAt");

            migrationBuilder.RenameIndex(
                name: "IX_Order_OrderNumber",
                table: "Orders",
                newName: "IX_Orders_OrderNumber");

            migrationBuilder.RenameIndex(
                name: "IX_Order_DeliveryType",
                table: "Orders",
                newName: "IX_Orders_DeliveryType");

            migrationBuilder.RenameIndex(
                name: "IX_Order_DeliveryProviderId",
                table: "Orders",
                newName: "IX_Orders_DeliveryProviderId");

            migrationBuilder.RenameIndex(
                name: "IX_Order_CurrencyId",
                table: "Orders",
                newName: "IX_Orders_CurrencyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderItems",
                table: "OrderItems",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Orders",
                table: "Orders",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BasketItems_Baskets_ParentId",
                table: "BasketItems",
                column: "ParentId",
                principalTable: "Baskets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_AdvertisementVariants_AdvertisementVariantId",
                table: "OrderItems",
                column: "AdvertisementVariantId",
                principalTable: "AdvertisementVariants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Advertisements_AdvertisementId",
                table: "OrderItems",
                column: "AdvertisementId",
                principalTable: "Advertisements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Orders_ParentId",
                table: "OrderItems",
                column: "ParentId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Users_UserId",
                table: "OrderItems",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Currencies_CurrencyId",
                table: "Orders",
                column: "CurrencyId",
                principalTable: "Currencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_DeliveryProviders_DeliveryProviderId",
                table: "Orders",
                column: "DeliveryProviderId",
                principalTable: "DeliveryProviders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Users_OwnerId",
                table: "Orders",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BasketItems_Baskets_ParentId",
                table: "BasketItems");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_AdvertisementVariants_AdvertisementVariantId",
                table: "OrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Advertisements_AdvertisementId",
                table: "OrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Orders_ParentId",
                table: "OrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Users_UserId",
                table: "OrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Currencies_CurrencyId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_DeliveryProviders_DeliveryProviderId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Users_OwnerId",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Orders",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderItems",
                table: "OrderItems");

            migrationBuilder.RenameTable(
                name: "Orders",
                newName: "Order");

            migrationBuilder.RenameTable(
                name: "OrderItems",
                newName: "OrderItem");

            migrationBuilder.RenameColumn(
                name: "ParentId",
                table: "BasketItems",
                newName: "BasketId");

            migrationBuilder.RenameIndex(
                name: "IX_BasketItems_ParentId_AdvertisementVariantId",
                table: "BasketItems",
                newName: "IX_BasketItems_BasketId_AdvertisementVariantId");

            migrationBuilder.RenameIndex(
                name: "IX_BasketItems_ParentId_AdvertisementId",
                table: "BasketItems",
                newName: "IX_BasketItems_BasketId_AdvertisementId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_PaymentProvider_PaymentTransactionId",
                table: "Order",
                newName: "IX_Order_PaymentProvider_PaymentTransactionId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_PaymentMethod_PaymentStatus",
                table: "Order",
                newName: "IX_Order_PaymentMethod_PaymentStatus");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_OwnerId_CreatedAt",
                table: "Order",
                newName: "IX_Order_OwnerId_CreatedAt");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_OrderStatus_CreatedAt",
                table: "Order",
                newName: "IX_Order_OrderStatus_CreatedAt");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_OrderNumber",
                table: "Order",
                newName: "IX_Order_OrderNumber");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_DeliveryType",
                table: "Order",
                newName: "IX_Order_DeliveryType");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_DeliveryProviderId",
                table: "Order",
                newName: "IX_Order_DeliveryProviderId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_CurrencyId",
                table: "Order",
                newName: "IX_Order_CurrencyId");

            migrationBuilder.RenameColumn(
                name: "ParentId",
                table: "OrderItem",
                newName: "OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderItems_UserId",
                table: "OrderItem",
                newName: "IX_OrderItem_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderItems_ParentId",
                table: "OrderItem",
                newName: "IX_OrderItem_OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderItems_AdvertisementVariantId",
                table: "OrderItem",
                newName: "IX_OrderItem_AdvertisementVariantId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderItems_AdvertisementId",
                table: "OrderItem",
                newName: "IX_OrderItem_AdvertisementId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Order",
                table: "Order",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderItem",
                table: "OrderItem",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BasketItems_Baskets_BasketId",
                table: "BasketItems",
                column: "BasketId",
                principalTable: "Baskets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Currencies_CurrencyId",
                table: "Order",
                column: "CurrencyId",
                principalTable: "Currencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_DeliveryProviders_DeliveryProviderId",
                table: "Order",
                column: "DeliveryProviderId",
                principalTable: "DeliveryProviders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Users_OwnerId",
                table: "Order",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItem_AdvertisementVariants_AdvertisementVariantId",
                table: "OrderItem",
                column: "AdvertisementVariantId",
                principalTable: "AdvertisementVariants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItem_Advertisements_AdvertisementId",
                table: "OrderItem",
                column: "AdvertisementId",
                principalTable: "Advertisements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItem_Order_OrderId",
                table: "OrderItem",
                column: "OrderId",
                principalTable: "Order",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItem_Users_UserId",
                table: "OrderItem",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
