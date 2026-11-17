using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ShagOxServer.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddOrderAndOrderItems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OrderNumber = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    FirstName = table.Column<string>(type: "character varying(35)", maxLength: 35, nullable: false),
                    LastName = table.Column<string>(type: "character varying(35)", maxLength: 35, nullable: false),
                    Phone = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "character varying(254)", maxLength: 254, nullable: false),
                    Comment = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    Subtotal = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    DeliveryPrice = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    TotalPrice = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    CurrencyId = table.Column<long>(type: "bigint", nullable: false),
                    DeliveryType = table.Column<string>(type: "text", nullable: false),
                    DeliveryProviderId = table.Column<long>(type: "bigint", nullable: true),
                    DeliveryPointExternalId = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    DeliveryPointNumber = table.Column<string>(type: "character varying(35)", maxLength: 35, nullable: true),
                    DeliveryPointName = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    DeliveryPointAddress = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    DeliveryPointType = table.Column<string>(type: "text", nullable: false),
                    DeliveryAddress = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    DeliveryTrackingNumber = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    DeliveryTrackingUrl = table.Column<string>(type: "character varying(2048)", maxLength: 2048, nullable: true),
                    ShippedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeliveredAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    EstimatedDeliveryAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    PaymentMethod = table.Column<string>(type: "text", nullable: false),
                    PaymentStatus = table.Column<string>(type: "text", nullable: false),
                    PaidAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    PaymentTransactionId = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    PaymentProvider = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    OrderStatus = table.Column<string>(type: "text", nullable: false),
                    ConfirmedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CancelledAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    EntityStatus = table.Column<string>(type: "text", nullable: false, defaultValue: "Active"),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    OwnerId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Order_Currencies_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Order_DeliveryProviders_DeliveryProviderId",
                        column: x => x.DeliveryProviderId,
                        principalTable: "DeliveryProviders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Order_Users_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderItem",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OrderId = table.Column<long>(type: "bigint", nullable: false),
                    AdvertisementId = table.Column<long>(type: "bigint", nullable: false),
                    AdvertisementVariantId = table.Column<long>(type: "bigint", nullable: true),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: true),
                    EntityStatus = table.Column<string>(type: "text", nullable: false, defaultValue: "Active"),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItem_AdvertisementVariants_AdvertisementVariantId",
                        column: x => x.AdvertisementVariantId,
                        principalTable: "AdvertisementVariants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderItem_Advertisements_AdvertisementId",
                        column: x => x.AdvertisementId,
                        principalTable: "Advertisements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderItem_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItem_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Order_CurrencyId",
                table: "Order",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_DeliveryProviderId",
                table: "Order",
                column: "DeliveryProviderId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_DeliveryType",
                table: "Order",
                column: "DeliveryType");

            migrationBuilder.CreateIndex(
                name: "IX_Order_OrderNumber",
                table: "Order",
                column: "OrderNumber",
                unique: true,
                filter: "\"EntityStatus\" IN ('Active', 'Hidden')");

            migrationBuilder.CreateIndex(
                name: "IX_Order_OrderStatus_CreatedAt",
                table: "Order",
                columns: new[] { "OrderStatus", "CreatedAt" });

            migrationBuilder.CreateIndex(
                name: "IX_Order_OwnerId_CreatedAt",
                table: "Order",
                columns: new[] { "OwnerId", "CreatedAt" });

            migrationBuilder.CreateIndex(
                name: "IX_Order_PaymentMethod_PaymentStatus",
                table: "Order",
                columns: new[] { "PaymentMethod", "PaymentStatus" });

            migrationBuilder.CreateIndex(
                name: "IX_Order_PaymentProvider_PaymentTransactionId",
                table: "Order",
                columns: new[] { "PaymentProvider", "PaymentTransactionId" });

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_AdvertisementId",
                table: "OrderItem",
                column: "AdvertisementId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_AdvertisementVariantId",
                table: "OrderItem",
                column: "AdvertisementVariantId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_OrderId",
                table: "OrderItem",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_UserId",
                table: "OrderItem",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderItem");

            migrationBuilder.DropTable(
                name: "Order");
        }
    }
}
