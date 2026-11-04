using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShagOxServer.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddStatusHidenInIndexes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_UserName",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_StatusTranslations_TranslatableId_Language",
                table: "StatusTranslations");

            migrationBuilder.DropIndex(
                name: "IX_Statuses_Code",
                table: "Statuses");

            migrationBuilder.DropIndex(
                name: "IX_ReviewRepliImages_OwnerId_Order",
                table: "ReviewRepliImages");

            migrationBuilder.DropIndex(
                name: "IX_ReviewImages_OwnerId_Order",
                table: "ReviewImages");

            migrationBuilder.DropIndex(
                name: "IX_RegionTranslations_TranslatableId_Language",
                table: "RegionTranslations");

            migrationBuilder.DropIndex(
                name: "IX_Regions_Code",
                table: "Regions");

            migrationBuilder.DropIndex(
                name: "IX_ProductTypeTranslations_TranslatableId_Language",
                table: "ProductTypeTranslations");

            migrationBuilder.DropIndex(
                name: "IX_ProductTypes_Code",
                table: "ProductTypes");

            migrationBuilder.DropIndex(
                name: "IX_Favorites_OwnerId_AdvertisementId",
                table: "Favorites");

            migrationBuilder.DropIndex(
                name: "IX_Currencies_Code",
                table: "Currencies");

            migrationBuilder.DropIndex(
                name: "IX_ConditionTranslations_Language_Name",
                table: "ConditionTranslations");

            migrationBuilder.DropIndex(
                name: "IX_Conditions_Code",
                table: "Conditions");

            migrationBuilder.DropIndex(
                name: "IX_CityTranslations_TranslatableId_Language",
                table: "CityTranslations");

            migrationBuilder.DropIndex(
                name: "IX_Cities_Code",
                table: "Cities");

            migrationBuilder.DropIndex(
                name: "IX_CategoryTranslations_TranslatableId_Language",
                table: "CategoryTranslations");

            migrationBuilder.DropIndex(
                name: "IX_Categories_ProductTypeId_Code",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Baskets_OwnerId",
                table: "Baskets");

            migrationBuilder.DropIndex(
                name: "IX_BasketItems_BasketId_AdvertisementVariantId",
                table: "BasketItems");

            migrationBuilder.DropIndex(
                name: "IX_BasketAttributes_AttributeDefinitionId",
                table: "BasketAttributes");

            migrationBuilder.DropIndex(
                name: "IX_Avatars_OwnerId",
                table: "Avatars");

            migrationBuilder.DropIndex(
                name: "IX_AttributeDictionaryValueTranslations_TranslatableId_Language",
                table: "AttributeDictionaryValueTranslations");

            migrationBuilder.DropIndex(
                name: "IX_AttributeDictionaryValues_DictionaryId_Code",
                table: "AttributeDictionaryValues");

            migrationBuilder.DropIndex(
                name: "IX_AttributeDictionaries_Code",
                table: "AttributeDictionaries");

            migrationBuilder.DropIndex(
                name: "IX_AttributeDefinitionTranslations_TranslatableId_Language",
                table: "AttributeDefinitionTranslations");

            migrationBuilder.DropIndex(
                name: "IX_AttributeDefinitions_CategoryId_Key",
                table: "AttributeDefinitions");

            migrationBuilder.DropIndex(
                name: "IX_AdvertisementImages_OwnerId_Order",
                table: "AdvertisementImages");

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserName",
                table: "Users",
                column: "UserName",
                unique: true,
                filter: "\"EntityStatus\" IN ('Active', 'Hidden')");

            migrationBuilder.CreateIndex(
                name: "IX_StatusTranslations_TranslatableId_Language",
                table: "StatusTranslations",
                columns: new[] { "TranslatableId", "Language" },
                unique: true,
                filter: "\"EntityStatus\" IN ('Active', 'Hidden')");

            migrationBuilder.CreateIndex(
                name: "IX_Statuses_Code",
                table: "Statuses",
                column: "Code",
                unique: true,
                filter: "\"EntityStatus\" IN ('Active', 'Hidden')");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewRepliImages_OwnerId_Order",
                table: "ReviewRepliImages",
                columns: new[] { "OwnerId", "Order" },
                unique: true,
                filter: "\"EntityStatus\" IN ('Active', 'Hidden')");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewImages_OwnerId_Order",
                table: "ReviewImages",
                columns: new[] { "OwnerId", "Order" },
                unique: true,
                filter: "\"EntityStatus\" IN ('Active', 'Hidden')");

            migrationBuilder.CreateIndex(
                name: "IX_RegionTranslations_TranslatableId_Language",
                table: "RegionTranslations",
                columns: new[] { "TranslatableId", "Language" },
                unique: true,
                filter: "\"EntityStatus\" IN ('Active', 'Hidden')");

            migrationBuilder.CreateIndex(
                name: "IX_Regions_Code",
                table: "Regions",
                column: "Code",
                unique: true,
                filter: "\"EntityStatus\" IN ('Active', 'Hidden')");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTypeTranslations_TranslatableId_Language",
                table: "ProductTypeTranslations",
                columns: new[] { "TranslatableId", "Language" },
                unique: true,
                filter: "\"EntityStatus\" IN ('Active', 'Hidden')");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTypes_Code",
                table: "ProductTypes",
                column: "Code",
                unique: true,
                filter: "\"EntityStatus\" IN ('Active', 'Hidden')");

            migrationBuilder.CreateIndex(
                name: "IX_Favorites_OwnerId_AdvertisementId",
                table: "Favorites",
                columns: new[] { "OwnerId", "AdvertisementId" },
                unique: true,
                filter: "\"EntityStatus\" IN ('Active', 'Hidden')");

            migrationBuilder.CreateIndex(
                name: "IX_Currencies_Code",
                table: "Currencies",
                column: "Code",
                unique: true,
                filter: "\"EntityStatus\" IN ('Active', 'Hidden')");

            migrationBuilder.CreateIndex(
                name: "IX_ConditionTranslations_Language_Name",
                table: "ConditionTranslations",
                columns: new[] { "Language", "Name" },
                unique: true,
                filter: "\"EntityStatus\" IN ('Active', 'Hidden')");

            migrationBuilder.CreateIndex(
                name: "IX_Conditions_Code",
                table: "Conditions",
                column: "Code",
                unique: true,
                filter: "\"EntityStatus\" IN ('Active', 'Hidden')");

            migrationBuilder.CreateIndex(
                name: "IX_CityTranslations_TranslatableId_Language",
                table: "CityTranslations",
                columns: new[] { "TranslatableId", "Language" },
                unique: true,
                filter: "\"EntityStatus\" IN ('Active', 'Hidden')");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_Code",
                table: "Cities",
                column: "Code",
                unique: true,
                filter: "\"EntityStatus\" IN ('Active', 'Hidden')");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryTranslations_TranslatableId_Language",
                table: "CategoryTranslations",
                columns: new[] { "TranslatableId", "Language" },
                unique: true,
                filter: "\"EntityStatus\" IN ('Active', 'Hidden')");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ProductTypeId_Code",
                table: "Categories",
                columns: new[] { "ProductTypeId", "Code" },
                unique: true,
                filter: "\"EntityStatus\" IN ('Active', 'Hidden')");

            migrationBuilder.CreateIndex(
                name: "IX_Baskets_OwnerId",
                table: "Baskets",
                column: "OwnerId",
                unique: true,
                filter: "\"EntityStatus\" IN ('Active', 'Hidden')");

            migrationBuilder.CreateIndex(
                name: "IX_BasketItems_BasketId_AdvertisementVariantId",
                table: "BasketItems",
                columns: new[] { "BasketId", "AdvertisementVariantId" },
                unique: true,
                filter: "\"EntityStatus\" IN ('Active', 'Hidden')");

            migrationBuilder.CreateIndex(
                name: "IX_BasketAttributes_AttributeDefinitionId",
                table: "BasketAttributes",
                column: "AttributeDefinitionId",
                unique: true,
                filter: "\"EntityStatus\" IN ('Active', 'Hidden')");

            migrationBuilder.CreateIndex(
                name: "IX_Avatars_OwnerId",
                table: "Avatars",
                column: "OwnerId",
                unique: true,
                filter: "\"EntityStatus\" IN ('Active', 'Hidden')");

            migrationBuilder.CreateIndex(
                name: "IX_AttributeDictionaryValueTranslations_TranslatableId_Language",
                table: "AttributeDictionaryValueTranslations",
                columns: new[] { "TranslatableId", "Language" },
                unique: true,
                filter: "\"EntityStatus\" IN ('Active', 'Hidden')");

            migrationBuilder.CreateIndex(
                name: "IX_AttributeDictionaryValues_DictionaryId_Code",
                table: "AttributeDictionaryValues",
                columns: new[] { "DictionaryId", "Code" },
                unique: true,
                filter: "\"EntityStatus\" IN ('Active', 'Hidden')");

            migrationBuilder.CreateIndex(
                name: "IX_AttributeDictionaries_Code",
                table: "AttributeDictionaries",
                column: "Code",
                unique: true,
                filter: "\"EntityStatus\" IN ('Active', 'Hidden')");

            migrationBuilder.CreateIndex(
                name: "IX_AttributeDefinitionTranslations_TranslatableId_Language",
                table: "AttributeDefinitionTranslations",
                columns: new[] { "TranslatableId", "Language" },
                unique: true,
                filter: "\"EntityStatus\" IN ('Active', 'Hidden')");

            migrationBuilder.CreateIndex(
                name: "IX_AttributeDefinitions_CategoryId_Key",
                table: "AttributeDefinitions",
                columns: new[] { "CategoryId", "Key" },
                unique: true,
                filter: "\"EntityStatus\" IN ('Active', 'Hidden')");

            migrationBuilder.CreateIndex(
                name: "IX_AdvertisementImages_OwnerId_Order",
                table: "AdvertisementImages",
                columns: new[] { "OwnerId", "Order" },
                unique: true,
                filter: "\"EntityStatus\" IN ('Active', 'Hidden')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_UserName",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_StatusTranslations_TranslatableId_Language",
                table: "StatusTranslations");

            migrationBuilder.DropIndex(
                name: "IX_Statuses_Code",
                table: "Statuses");

            migrationBuilder.DropIndex(
                name: "IX_ReviewRepliImages_OwnerId_Order",
                table: "ReviewRepliImages");

            migrationBuilder.DropIndex(
                name: "IX_ReviewImages_OwnerId_Order",
                table: "ReviewImages");

            migrationBuilder.DropIndex(
                name: "IX_RegionTranslations_TranslatableId_Language",
                table: "RegionTranslations");

            migrationBuilder.DropIndex(
                name: "IX_Regions_Code",
                table: "Regions");

            migrationBuilder.DropIndex(
                name: "IX_ProductTypeTranslations_TranslatableId_Language",
                table: "ProductTypeTranslations");

            migrationBuilder.DropIndex(
                name: "IX_ProductTypes_Code",
                table: "ProductTypes");

            migrationBuilder.DropIndex(
                name: "IX_Favorites_OwnerId_AdvertisementId",
                table: "Favorites");

            migrationBuilder.DropIndex(
                name: "IX_Currencies_Code",
                table: "Currencies");

            migrationBuilder.DropIndex(
                name: "IX_ConditionTranslations_Language_Name",
                table: "ConditionTranslations");

            migrationBuilder.DropIndex(
                name: "IX_Conditions_Code",
                table: "Conditions");

            migrationBuilder.DropIndex(
                name: "IX_CityTranslations_TranslatableId_Language",
                table: "CityTranslations");

            migrationBuilder.DropIndex(
                name: "IX_Cities_Code",
                table: "Cities");

            migrationBuilder.DropIndex(
                name: "IX_CategoryTranslations_TranslatableId_Language",
                table: "CategoryTranslations");

            migrationBuilder.DropIndex(
                name: "IX_Categories_ProductTypeId_Code",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Baskets_OwnerId",
                table: "Baskets");

            migrationBuilder.DropIndex(
                name: "IX_BasketItems_BasketId_AdvertisementVariantId",
                table: "BasketItems");

            migrationBuilder.DropIndex(
                name: "IX_BasketAttributes_AttributeDefinitionId",
                table: "BasketAttributes");

            migrationBuilder.DropIndex(
                name: "IX_Avatars_OwnerId",
                table: "Avatars");

            migrationBuilder.DropIndex(
                name: "IX_AttributeDictionaryValueTranslations_TranslatableId_Language",
                table: "AttributeDictionaryValueTranslations");

            migrationBuilder.DropIndex(
                name: "IX_AttributeDictionaryValues_DictionaryId_Code",
                table: "AttributeDictionaryValues");

            migrationBuilder.DropIndex(
                name: "IX_AttributeDictionaries_Code",
                table: "AttributeDictionaries");

            migrationBuilder.DropIndex(
                name: "IX_AttributeDefinitionTranslations_TranslatableId_Language",
                table: "AttributeDefinitionTranslations");

            migrationBuilder.DropIndex(
                name: "IX_AttributeDefinitions_CategoryId_Key",
                table: "AttributeDefinitions");

            migrationBuilder.DropIndex(
                name: "IX_AdvertisementImages_OwnerId_Order",
                table: "AdvertisementImages");

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserName",
                table: "Users",
                column: "UserName",
                unique: true,
                filter: "\"EntityStatus\" = 'Active'");

            migrationBuilder.CreateIndex(
                name: "IX_StatusTranslations_TranslatableId_Language",
                table: "StatusTranslations",
                columns: new[] { "TranslatableId", "Language" },
                unique: true,
                filter: "\"EntityStatus\" = 'Active'");

            migrationBuilder.CreateIndex(
                name: "IX_Statuses_Code",
                table: "Statuses",
                column: "Code",
                unique: true,
                filter: "\"EntityStatus\" = 'Active'");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewRepliImages_OwnerId_Order",
                table: "ReviewRepliImages",
                columns: new[] { "OwnerId", "Order" },
                unique: true,
                filter: "\"EntityStatus\" = 'Active'");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewImages_OwnerId_Order",
                table: "ReviewImages",
                columns: new[] { "OwnerId", "Order" },
                unique: true,
                filter: "\"EntityStatus\" = 'Active'");

            migrationBuilder.CreateIndex(
                name: "IX_RegionTranslations_TranslatableId_Language",
                table: "RegionTranslations",
                columns: new[] { "TranslatableId", "Language" },
                unique: true,
                filter: "\"EntityStatus\" = 'Active'");

            migrationBuilder.CreateIndex(
                name: "IX_Regions_Code",
                table: "Regions",
                column: "Code",
                unique: true,
                filter: "\"EntityStatus\" = 'Active'");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTypeTranslations_TranslatableId_Language",
                table: "ProductTypeTranslations",
                columns: new[] { "TranslatableId", "Language" },
                unique: true,
                filter: "\"EntityStatus\" = 'Active'");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTypes_Code",
                table: "ProductTypes",
                column: "Code",
                unique: true,
                filter: "\"EntityStatus\" = 'Active'");

            migrationBuilder.CreateIndex(
                name: "IX_Favorites_OwnerId_AdvertisementId",
                table: "Favorites",
                columns: new[] { "OwnerId", "AdvertisementId" },
                unique: true,
                filter: "\"EntityStatus\" = 'Active'");

            migrationBuilder.CreateIndex(
                name: "IX_Currencies_Code",
                table: "Currencies",
                column: "Code",
                unique: true,
                filter: "\"EntityStatus\" = 'Active'");

            migrationBuilder.CreateIndex(
                name: "IX_ConditionTranslations_Language_Name",
                table: "ConditionTranslations",
                columns: new[] { "Language", "Name" },
                unique: true,
                filter: "\"EntityStatus\" = 'Active'");

            migrationBuilder.CreateIndex(
                name: "IX_Conditions_Code",
                table: "Conditions",
                column: "Code",
                unique: true,
                filter: "\"EntityStatus\" = 'Active'");

            migrationBuilder.CreateIndex(
                name: "IX_CityTranslations_TranslatableId_Language",
                table: "CityTranslations",
                columns: new[] { "TranslatableId", "Language" },
                unique: true,
                filter: "\"EntityStatus\" = 'Active'");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_Code",
                table: "Cities",
                column: "Code",
                unique: true,
                filter: "\"EntityStatus\" = 'Active'");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryTranslations_TranslatableId_Language",
                table: "CategoryTranslations",
                columns: new[] { "TranslatableId", "Language" },
                unique: true,
                filter: "\"EntityStatus\" = 'Active'");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ProductTypeId_Code",
                table: "Categories",
                columns: new[] { "ProductTypeId", "Code" },
                unique: true,
                filter: "\"EntityStatus\" = 'Active'");

            migrationBuilder.CreateIndex(
                name: "IX_Baskets_OwnerId",
                table: "Baskets",
                column: "OwnerId",
                unique: true,
                filter: "\"EntityStatus\" = 'Active'");

            migrationBuilder.CreateIndex(
                name: "IX_BasketItems_BasketId_AdvertisementVariantId",
                table: "BasketItems",
                columns: new[] { "BasketId", "AdvertisementVariantId" },
                unique: true,
                filter: "\"EntityStatus\" = 'Active'");

            migrationBuilder.CreateIndex(
                name: "IX_BasketAttributes_AttributeDefinitionId",
                table: "BasketAttributes",
                column: "AttributeDefinitionId",
                unique: true,
                filter: "\"EntityStatus\" = 'Active'");

            migrationBuilder.CreateIndex(
                name: "IX_Avatars_OwnerId",
                table: "Avatars",
                column: "OwnerId",
                unique: true,
                filter: "\"EntityStatus\" = 'Active'");

            migrationBuilder.CreateIndex(
                name: "IX_AttributeDictionaryValueTranslations_TranslatableId_Language",
                table: "AttributeDictionaryValueTranslations",
                columns: new[] { "TranslatableId", "Language" },
                unique: true,
                filter: "\"EntityStatus\" = 'Active'");

            migrationBuilder.CreateIndex(
                name: "IX_AttributeDictionaryValues_DictionaryId_Code",
                table: "AttributeDictionaryValues",
                columns: new[] { "DictionaryId", "Code" },
                unique: true,
                filter: "\"EntityStatus\" = 'Active'");

            migrationBuilder.CreateIndex(
                name: "IX_AttributeDictionaries_Code",
                table: "AttributeDictionaries",
                column: "Code",
                unique: true,
                filter: "\"EntityStatus\" = 'Active'");

            migrationBuilder.CreateIndex(
                name: "IX_AttributeDefinitionTranslations_TranslatableId_Language",
                table: "AttributeDefinitionTranslations",
                columns: new[] { "TranslatableId", "Language" },
                unique: true,
                filter: "\"EntityStatus\" = 'Active'");

            migrationBuilder.CreateIndex(
                name: "IX_AttributeDefinitions_CategoryId_Key",
                table: "AttributeDefinitions",
                columns: new[] { "CategoryId", "Key" },
                unique: true,
                filter: "\"EntityStatus\" = 'Active'");

            migrationBuilder.CreateIndex(
                name: "IX_AdvertisementImages_OwnerId_Order",
                table: "AdvertisementImages",
                columns: new[] { "OwnerId", "Order" },
                unique: true,
                filter: "\"EntityStatus\" = 'Active'");
        }
    }
}
