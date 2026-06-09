using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ShagOxServer.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddAdvertisementSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AttributeDefinition_Category_CategoryId",
                table: "AttributeDefinition");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Image",
                table: "Image");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Currency",
                table: "Currency");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Condition",
                table: "Condition");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Category",
                table: "Category");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AttributeDefinition",
                table: "AttributeDefinition");

            migrationBuilder.RenameTable(
                name: "Image",
                newName: "Images");

            migrationBuilder.RenameTable(
                name: "Currency",
                newName: "Currencies");

            migrationBuilder.RenameTable(
                name: "Condition",
                newName: "Conditions");

            migrationBuilder.RenameTable(
                name: "Category",
                newName: "Categories");

            migrationBuilder.RenameTable(
                name: "AttributeDefinition",
                newName: "AttributeDefinitions");

            migrationBuilder.RenameIndex(
                name: "IX_Category_ProductType",
                table: "Categories",
                newName: "IX_Categories_ProductType");

            migrationBuilder.RenameIndex(
                name: "IX_AttributeDefinition_CategoryId_Key",
                table: "AttributeDefinitions",
                newName: "IX_AttributeDefinitions_CategoryId_Key");

            migrationBuilder.AddColumn<int>(
                name: "AdvertisementId",
                table: "Images",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Images",
                table: "Images",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Currencies",
                table: "Currencies",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Conditions",
                table: "Conditions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categories",
                table: "Categories",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AttributeDefinitions",
                table: "AttributeDefinitions",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Advertisements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Popularity = table.Column<int>(type: "integer", nullable: false),
                    Price = table.Column<int>(type: "integer", nullable: false),
                    PreviousPrice = table.Column<int>(type: "integer", nullable: false),
                    CurrencyId = table.Column<int>(type: "integer", nullable: false),
                    CategoryId = table.Column<int>(type: "integer", nullable: false),
                    SellerId = table.Column<int>(type: "integer", nullable: false),
                    BuyerId = table.Column<int>(type: "integer", nullable: false),
                    SoldAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Properties = table.Column<string>(type: "jsonb", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Advertisements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Advertisements_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Advertisements_Currencies_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Advertisements_Users_BuyerId",
                        column: x => x.BuyerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Advertisements_Users_SellerId",
                        column: x => x.SellerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Images_AdvertisementId_Order",
                table: "Images",
                columns: new[] { "AdvertisementId", "Order" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Advertisements_BuyerId",
                table: "Advertisements",
                column: "BuyerId");

            migrationBuilder.CreateIndex(
                name: "IX_Advertisements_CategoryId",
                table: "Advertisements",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Advertisements_CategoryId_CreatedAt",
                table: "Advertisements",
                columns: new[] { "CategoryId", "CreatedAt" });

            migrationBuilder.CreateIndex(
                name: "IX_Advertisements_CategoryId_Popularity",
                table: "Advertisements",
                columns: new[] { "CategoryId", "Popularity" });

            migrationBuilder.CreateIndex(
                name: "IX_Advertisements_CategoryId_Price",
                table: "Advertisements",
                columns: new[] { "CategoryId", "Price" });

            migrationBuilder.CreateIndex(
                name: "IX_Advertisements_CreatedAt",
                table: "Advertisements",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Advertisements_CurrencyId",
                table: "Advertisements",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Advertisements_Popularity",
                table: "Advertisements",
                column: "Popularity");

            migrationBuilder.CreateIndex(
                name: "IX_Advertisements_Price",
                table: "Advertisements",
                column: "Price");

            migrationBuilder.CreateIndex(
                name: "IX_Advertisements_SellerId",
                table: "Advertisements",
                column: "SellerId");

            migrationBuilder.AddForeignKey(
                name: "FK_AttributeDefinitions_Categories_CategoryId",
                table: "AttributeDefinitions",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Advertisements_AdvertisementId",
                table: "Images",
                column: "AdvertisementId",
                principalTable: "Advertisements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AttributeDefinitions_Categories_CategoryId",
                table: "AttributeDefinitions");

            migrationBuilder.DropForeignKey(
                name: "FK_Images_Advertisements_AdvertisementId",
                table: "Images");

            migrationBuilder.DropTable(
                name: "Advertisements");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Images",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_AdvertisementId_Order",
                table: "Images");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Currencies",
                table: "Currencies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Conditions",
                table: "Conditions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categories",
                table: "Categories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AttributeDefinitions",
                table: "AttributeDefinitions");

            migrationBuilder.DropColumn(
                name: "AdvertisementId",
                table: "Images");

            migrationBuilder.RenameTable(
                name: "Images",
                newName: "Image");

            migrationBuilder.RenameTable(
                name: "Currencies",
                newName: "Currency");

            migrationBuilder.RenameTable(
                name: "Conditions",
                newName: "Condition");

            migrationBuilder.RenameTable(
                name: "Categories",
                newName: "Category");

            migrationBuilder.RenameTable(
                name: "AttributeDefinitions",
                newName: "AttributeDefinition");

            migrationBuilder.RenameIndex(
                name: "IX_Categories_ProductType",
                table: "Category",
                newName: "IX_Category_ProductType");

            migrationBuilder.RenameIndex(
                name: "IX_AttributeDefinitions_CategoryId_Key",
                table: "AttributeDefinition",
                newName: "IX_AttributeDefinition_CategoryId_Key");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Image",
                table: "Image",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Currency",
                table: "Currency",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Condition",
                table: "Condition",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Category",
                table: "Category",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AttributeDefinition",
                table: "AttributeDefinition",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AttributeDefinition_Category_CategoryId",
                table: "AttributeDefinition",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
