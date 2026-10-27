using System;
using System.Text.Json;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ShagOxServer.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class StatusInBaseEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserRoles_Roles_RoleId",
                table: "UserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRoles_Users_UserId",
                table: "UserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_VerificationCodes_Users_UserId",
                table: "VerificationCodes");

            migrationBuilder.DropTable(
                name: "AdvertisementImages");

            migrationBuilder.DropTable(
                name: "AttributeDefinitionTranslations");

            migrationBuilder.DropTable(
                name: "AttributeDictionaryValueTranslations");

            migrationBuilder.DropTable(
                name: "Avatars");

            migrationBuilder.DropTable(
                name: "BasketAttributes");

            migrationBuilder.DropTable(
                name: "BasketItems");

            migrationBuilder.DropTable(
                name: "CategoryTranslations");

            migrationBuilder.DropTable(
                name: "CityTranslations");

            migrationBuilder.DropTable(
                name: "ConditionTranslations");

            migrationBuilder.DropTable(
                name: "Favorites");

            migrationBuilder.DropTable(
                name: "ProductTypeTranslations");

            migrationBuilder.DropTable(
                name: "RegionTranslations");

            migrationBuilder.DropTable(
                name: "ReviewImages");

            migrationBuilder.DropTable(
                name: "ReviewRepliImages");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "StatusTranslations");

            migrationBuilder.DropTable(
                name: "AttributeDictionaryValues");

            migrationBuilder.DropTable(
                name: "AttributeDefinitions");

            migrationBuilder.DropTable(
                name: "AdvertisementVariants");

            migrationBuilder.DropTable(
                name: "Baskets");

            migrationBuilder.DropTable(
                name: "ReviewRepliComments");

            migrationBuilder.DropTable(
                name: "AttributeDictionaries");

            migrationBuilder.DropTable(
                name: "ReviewComments");

            migrationBuilder.DropTable(
                name: "Advertisements");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Conditions");

            migrationBuilder.DropTable(
                name: "Currencies");

            migrationBuilder.DropTable(
                name: "Statuses");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "ProductTypes");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "Regions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VerificationCodes",
                table: "VerificationCodes");

            migrationBuilder.DropIndex(
                name: "IX_VerificationCodes_UserId",
                table: "VerificationCodes");

            migrationBuilder.RenameTable(
                name: "VerificationCodes",
                newName: "BaseEntity");

            migrationBuilder.AddColumn<int>(
                name: "EntityStatus",
                table: "UserRoles",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<long>(
                name: "UserId",
                table: "BaseEntity",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<string>(
                name: "Purpose",
                table: "BaseEntity",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ExpiresAt",
                table: "BaseEntity",
                type: "timestamptz",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamptz");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "BaseEntity",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamptz");

            migrationBuilder.AlterColumn<string>(
                name: "CodeHash",
                table: "BaseEntity",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<int>(
                name: "Attempts",
                table: "BaseEntity",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<long>(
                name: "AdvertisementId",
                table: "BaseEntity",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AdvertisementImage_Order",
                table: "BaseEntity",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "AdvertisementVariantId",
                table: "BaseEntity",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<JsonDocument>(
                name: "AdvertisementVariant_Attributes",
                table: "BaseEntity",
                type: "jsonb",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Advertisement_Description",
                table: "BaseEntity",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "AttributeDefinitionId",
                table: "BaseEntity",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AttributeDefinitionTranslation_Language",
                table: "BaseEntity",
                type: "character varying(30)",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AttributeDefinitionTranslation_Name",
                table: "BaseEntity",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "AttributeDefinitionTranslation_TranslatableId",
                table: "BaseEntity",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "AttributeDefinition_CategoryId",
                table: "BaseEntity",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AttributeDictionaryValueTranslation_Language",
                table: "BaseEntity",
                type: "character varying(30)",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AttributeDictionaryValueTranslation_Name",
                table: "BaseEntity",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "AttributeDictionaryValueTranslation_TranslatableId",
                table: "BaseEntity",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AttributeDictionaryValue_Code",
                table: "BaseEntity",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "AttributeDictionaryValue_DictionaryId",
                table: "BaseEntity",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AttributeDictionary_Code",
                table: "BaseEntity",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<JsonDocument>(
                name: "Attributes",
                table: "BaseEntity",
                type: "jsonb",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "AvatarId",
                table: "BaseEntity",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "BasketId",
                table: "BaseEntity",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Bio",
                table: "BaseEntity",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "BuyerId",
                table: "BaseEntity",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CategoryId",
                table: "BaseEntity",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CategoryTranslation_Language",
                table: "BaseEntity",
                type: "character varying(30)",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CategoryTranslation_Name",
                table: "BaseEntity",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CategoryTranslation_TranslatableId",
                table: "BaseEntity",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Category_Code",
                table: "BaseEntity",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CityId",
                table: "BaseEntity",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CityTranslation_Language",
                table: "BaseEntity",
                type: "character varying(30)",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CityTranslation_Name",
                table: "BaseEntity",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CityTranslation_TranslatableId",
                table: "BaseEntity",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "City_Code",
                table: "BaseEntity",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "BaseEntity",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CommentableEntityId",
                table: "BaseEntity",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ConditionId",
                table: "BaseEntity",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ConditionTranslation_Language",
                table: "BaseEntity",
                type: "character varying(30)",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ConditionTranslation_Name",
                table: "BaseEntity",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ConditionTranslation_TranslatableId",
                table: "BaseEntity",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Condition_Code",
                table: "BaseEntity",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CurrencyId",
                table: "BaseEntity",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Currency_Code",
                table: "BaseEntity",
                type: "character varying(3)",
                maxLength: 3,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Currency_Name",
                table: "BaseEntity",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "BaseEntity",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DictionaryId",
                table: "BaseEntity",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "BaseEntity",
                type: "character varying(55)",
                maxLength: 55,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "BaseEntity",
                type: "character varying(254)",
                maxLength: 254,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "EmailConfirmed",
                table: "BaseEntity",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EntityStatus",
                table: "BaseEntity",
                type: "text",
                nullable: false,
                defaultValue: "Active");

            migrationBuilder.AddColumn<long>(
                name: "Favorite_AdvertisementId",
                table: "BaseEntity",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "BaseEntity",
                type: "character varying(35)",
                maxLength: 35,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Grade",
                table: "BaseEntity",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsVariant",
                table: "BaseEntity",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Key",
                table: "BaseEntity",
                type: "character varying(150)",
                maxLength: 150,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Language",
                table: "BaseEntity",
                type: "character varying(30)",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "BaseEntity",
                type: "character varying(35)",
                maxLength: 35,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastSeenAt",
                table: "BaseEntity",
                type: "timestamptz",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Max",
                table: "BaseEntity",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Min",
                table: "BaseEntity",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Multiple",
                table: "BaseEntity",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "BaseEntity",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "BaseEntity",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "OwnerId",
                table: "BaseEntity",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ParentReplyId",
                table: "BaseEntity",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                table: "BaseEntity",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "BaseEntity",
                type: "character varying(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "PhoneConfirmed",
                table: "BaseEntity",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Popularity",
                table: "BaseEntity",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PreviousPrice",
                table: "BaseEntity",
                type: "numeric(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "BaseEntity",
                type: "numeric(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ProductTypeId",
                table: "BaseEntity",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductTypeTranslation_Language",
                table: "BaseEntity",
                type: "character varying(30)",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductTypeTranslation_Name",
                table: "BaseEntity",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ProductTypeTranslation_TranslatableId",
                table: "BaseEntity",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductType_Code",
                table: "BaseEntity",
                type: "character varying(150)",
                maxLength: 150,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductType_Description",
                table: "BaseEntity",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PublicId",
                table: "BaseEntity",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "BaseEntity",
                type: "integer",
                nullable: true,
                defaultValue: 1);

            migrationBuilder.AddColumn<long>(
                name: "RegionId",
                table: "BaseEntity",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RegionTranslation_Language",
                table: "BaseEntity",
                type: "character varying(30)",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RegionTranslation_Name",
                table: "BaseEntity",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "RegionTranslation_TranslatableId",
                table: "BaseEntity",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Region_Code",
                table: "BaseEntity",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RegisteredAt",
                table: "BaseEntity",
                type: "timestamptz",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Required",
                table: "BaseEntity",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ReviewComment_CreatedAt",
                table: "BaseEntity",
                type: "timestamp with time zone",
                nullable: true,
                defaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AddColumn<int>(
                name: "ReviewImage_Order",
                table: "BaseEntity",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ReviewRepliComment_CreatedAt",
                table: "BaseEntity",
                type: "timestamp with time zone",
                nullable: true,
                defaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AddColumn<int>(
                name: "ReviewRepliImage_Order",
                table: "BaseEntity",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "SoldAt",
                table: "BaseEntity",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "BaseEntity",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "StatusId",
                table: "BaseEntity",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StatusTranslation_Description",
                table: "BaseEntity",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StatusTranslation_Name",
                table: "BaseEntity",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Stock",
                table: "BaseEntity",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Symbol",
                table: "BaseEntity",
                type: "character varying(5)",
                maxLength: 5,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Text",
                table: "BaseEntity",
                type: "character varying(2000)",
                maxLength: 2000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "BaseEntity",
                type: "character varying(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "TranslatableId",
                table: "BaseEntity",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "BaseEntity",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "BaseEntity",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "BaseEntity",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "BaseEntity",
                type: "character varying(40)",
                maxLength: 40,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Value",
                table: "BaseEntity",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "VerificationCode_CreatedAt",
                table: "BaseEntity",
                type: "timestamptz",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "VerificationCode_UserId",
                table: "BaseEntity",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_BaseEntity",
                table: "BaseEntity",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_AdvertisementImages_OwnerId_Order",
                table: "BaseEntity",
                columns: new[] { "OwnerId", "AdvertisementImage_Order" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Advertisements_BuyerId",
                table: "BaseEntity",
                column: "BuyerId");

            migrationBuilder.CreateIndex(
                name: "IX_Advertisements_CategoryId",
                table: "BaseEntity",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Advertisements_CategoryId_ConditionId_CreatedAt",
                table: "BaseEntity",
                columns: new[] { "CategoryId", "ConditionId", "CreatedAt" });

            migrationBuilder.CreateIndex(
                name: "IX_Advertisements_CategoryId_CreatedAt",
                table: "BaseEntity",
                columns: new[] { "CategoryId", "CreatedAt" });

            migrationBuilder.CreateIndex(
                name: "IX_Advertisements_CategoryId_Popularity",
                table: "BaseEntity",
                columns: new[] { "CategoryId", "Popularity" });

            migrationBuilder.CreateIndex(
                name: "IX_Advertisements_ConditionId",
                table: "BaseEntity",
                column: "ConditionId");

            migrationBuilder.CreateIndex(
                name: "IX_Advertisements_CreatedAt",
                table: "BaseEntity",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Advertisements_CurrencyId",
                table: "BaseEntity",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Advertisements_OwnerId",
                table: "BaseEntity",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Advertisements_OwnerId_CreatedAt",
                table: "BaseEntity",
                columns: new[] { "OwnerId", "CreatedAt" });

            migrationBuilder.CreateIndex(
                name: "IX_Advertisements_Popularity",
                table: "BaseEntity",
                column: "Popularity");

            migrationBuilder.CreateIndex(
                name: "IX_Advertisements_StatusId",
                table: "BaseEntity",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_AdvertisementVariants_AdvertisementId",
                table: "BaseEntity",
                column: "AdvertisementId");

            migrationBuilder.CreateIndex(
                name: "IX_AttributeDefinitions_CategoryId_Key",
                table: "BaseEntity",
                columns: new[] { "AttributeDefinition_CategoryId", "Key" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AttributeDefinitions_DictionaryId",
                table: "BaseEntity",
                column: "DictionaryId");

            migrationBuilder.CreateIndex(
                name: "IX_AttributeDefinitionTranslations_Language_Name",
                table: "BaseEntity",
                columns: new[] { "AttributeDefinitionTranslation_Language", "AttributeDefinitionTranslation_Name" });

            migrationBuilder.CreateIndex(
                name: "IX_AttributeDefinitionTranslations_Name",
                table: "BaseEntity",
                column: "AttributeDefinitionTranslation_Name");

            migrationBuilder.CreateIndex(
                name: "IX_AttributeDefinitionTranslations_TranslatableId_Language",
                table: "BaseEntity",
                columns: new[] { "AttributeDefinitionTranslation_TranslatableId", "AttributeDefinitionTranslation_Language" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AttributeDictionaries_Code",
                table: "BaseEntity",
                column: "AttributeDictionary_Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AttributeDictionaryValues_DictionaryId_Code",
                table: "BaseEntity",
                columns: new[] { "AttributeDictionaryValue_DictionaryId", "AttributeDictionaryValue_Code" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AttributeDictionaryValueTranslations_Language_Name",
                table: "BaseEntity",
                columns: new[] { "AttributeDictionaryValueTranslation_Language", "AttributeDictionaryValueTranslation_Name" });

            migrationBuilder.CreateIndex(
                name: "IX_AttributeDictionaryValueTranslations_Name",
                table: "BaseEntity",
                column: "AttributeDictionaryValueTranslation_Name");

            migrationBuilder.CreateIndex(
                name: "IX_AttributeDictionaryValueTranslations_TranslatableId_Language",
                table: "BaseEntity",
                columns: new[] { "AttributeDictionaryValueTranslation_TranslatableId", "AttributeDictionaryValueTranslation_Language" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Avatars_OwnerId",
                table: "BaseEntity",
                column: "OwnerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BasketAttributes_AttributeDefinitionId",
                table: "BaseEntity",
                column: "AttributeDefinitionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BasketItems_AdvertisementVariantId",
                table: "BaseEntity",
                column: "AdvertisementVariantId");

            migrationBuilder.CreateIndex(
                name: "IX_BasketItems_BasketId_AdvertisementVariantId",
                table: "BaseEntity",
                columns: new[] { "BasketId", "AdvertisementVariantId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Baskets_OwnerId",
                table: "BaseEntity",
                column: "OwnerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ProductTypeId",
                table: "BaseEntity",
                column: "ProductTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ProductTypeId_Code",
                table: "BaseEntity",
                columns: new[] { "ProductTypeId", "Category_Code" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CategoryTranslations_Language_Name",
                table: "BaseEntity",
                columns: new[] { "CategoryTranslation_Language", "CategoryTranslation_Name" });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryTranslations_Name",
                table: "BaseEntity",
                column: "CategoryTranslation_Name");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryTranslations_TranslatableId_Language",
                table: "BaseEntity",
                columns: new[] { "CategoryTranslation_TranslatableId", "CategoryTranslation_Language" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cities_Code",
                table: "BaseEntity",
                column: "City_Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cities_RegionId",
                table: "BaseEntity",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_CityTranslations_Language_Name",
                table: "BaseEntity",
                columns: new[] { "CityTranslation_Language", "CityTranslation_Name" });

            migrationBuilder.CreateIndex(
                name: "IX_CityTranslations_Name",
                table: "BaseEntity",
                column: "CityTranslation_Name");

            migrationBuilder.CreateIndex(
                name: "IX_CityTranslations_TranslatableId_Language",
                table: "BaseEntity",
                columns: new[] { "CityTranslation_TranslatableId", "CityTranslation_Language" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Conditions_Code",
                table: "BaseEntity",
                column: "Condition_Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ConditionTranslations_Language_Name",
                table: "BaseEntity",
                columns: new[] { "ConditionTranslation_Language", "ConditionTranslation_Name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ConditionTranslations_TranslatableId",
                table: "BaseEntity",
                column: "ConditionTranslation_TranslatableId");

            migrationBuilder.CreateIndex(
                name: "IX_Currencies_Code",
                table: "BaseEntity",
                column: "Currency_Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Favorites_AdvertisementId",
                table: "BaseEntity",
                column: "Favorite_AdvertisementId");

            migrationBuilder.CreateIndex(
                name: "IX_Favorites_UserId_AdvertisementId",
                table: "BaseEntity",
                columns: new[] { "UserId", "Favorite_AdvertisementId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductTypes_Code",
                table: "BaseEntity",
                column: "ProductType_Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductTypeTranslations_Language_Name",
                table: "BaseEntity",
                columns: new[] { "ProductTypeTranslation_Language", "ProductTypeTranslation_Name" });

            migrationBuilder.CreateIndex(
                name: "IX_ProductTypeTranslations_Name",
                table: "BaseEntity",
                column: "ProductTypeTranslation_Name");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTypeTranslations_TranslatableId_Language",
                table: "BaseEntity",
                columns: new[] { "ProductTypeTranslation_TranslatableId", "ProductTypeTranslation_Language" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Regions_Code",
                table: "BaseEntity",
                column: "Region_Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RegionTranslations_Language_Name",
                table: "BaseEntity",
                columns: new[] { "RegionTranslation_Language", "RegionTranslation_Name" });

            migrationBuilder.CreateIndex(
                name: "IX_RegionTranslations_Name",
                table: "BaseEntity",
                column: "RegionTranslation_Name");

            migrationBuilder.CreateIndex(
                name: "IX_RegionTranslations_TranslatableId_Language",
                table: "BaseEntity",
                columns: new[] { "RegionTranslation_TranslatableId", "RegionTranslation_Language" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ReviewComments_CommentableEntityId",
                table: "BaseEntity",
                column: "CommentableEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewComments_CommentableEntityId_CreatedAt",
                table: "BaseEntity",
                columns: new[] { "CommentableEntityId", "ReviewComment_CreatedAt" });

            migrationBuilder.CreateIndex(
                name: "IX_ReviewComments_OwnerId",
                table: "BaseEntity",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewImages_OwnerId_Order",
                table: "BaseEntity",
                columns: new[] { "OwnerId", "ReviewImage_Order" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ReviewRepliComments_CommentableEntityId",
                table: "BaseEntity",
                column: "CommentableEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewRepliComments_CommentableEntityId_CreatedAt",
                table: "BaseEntity",
                columns: new[] { "CommentableEntityId", "ReviewRepliComment_CreatedAt" });

            migrationBuilder.CreateIndex(
                name: "IX_ReviewRepliComments_OwnerId",
                table: "BaseEntity",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewRepliComments_ParentReplyId",
                table: "BaseEntity",
                column: "ParentReplyId");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewRepliComments_ParentReplyId_CreatedAt",
                table: "BaseEntity",
                columns: new[] { "ParentReplyId", "ReviewRepliComment_CreatedAt" });

            migrationBuilder.CreateIndex(
                name: "IX_ReviewRepliImages_OwnerId_Order",
                table: "BaseEntity",
                columns: new[] { "OwnerId", "ReviewRepliImage_Order" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Statuses_Code",
                table: "BaseEntity",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StatusTranslations_Language_Name",
                table: "BaseEntity",
                columns: new[] { "Language", "StatusTranslation_Name" });

            migrationBuilder.CreateIndex(
                name: "IX_StatusTranslations_Name",
                table: "BaseEntity",
                column: "StatusTranslation_Name");

            migrationBuilder.CreateIndex(
                name: "IX_StatusTranslations_TranslatableId_Language",
                table: "BaseEntity",
                columns: new[] { "TranslatableId", "Language" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_CityId",
                table: "BaseEntity",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "BaseEntity",
                column: "Email",
                unique: true,
                filter: "\"Email\" IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Phone",
                table: "BaseEntity",
                column: "Phone",
                unique: true,
                filter: "\"Phone\" IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserName",
                table: "BaseEntity",
                column: "UserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VerificationCodes_UserId",
                table: "BaseEntity",
                column: "VerificationCode_UserId");

            migrationBuilder.AddCheckConstraint(
                name: "CK_ReviewComments_Grade",
                table: "BaseEntity",
                sql: "\"Grade\" BETWEEN 1 AND 5");

            migrationBuilder.AddForeignKey(
                name: "FK_AdvertisementImages_Advertisements_OwnerId",
                table: "BaseEntity",
                column: "OwnerId",
                principalTable: "BaseEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AdvertisementVariants_Advertisements_AdvertisementId",
                table: "BaseEntity",
                column: "AdvertisementId",
                principalTable: "BaseEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Advertisements_Categories_CategoryId",
                table: "BaseEntity",
                column: "CategoryId",
                principalTable: "BaseEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Advertisements_Conditions_ConditionId",
                table: "BaseEntity",
                column: "ConditionId",
                principalTable: "BaseEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Advertisements_Currencies_CurrencyId",
                table: "BaseEntity",
                column: "CurrencyId",
                principalTable: "BaseEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Advertisements_Statuses_StatusId",
                table: "BaseEntity",
                column: "StatusId",
                principalTable: "BaseEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Advertisements_Users_BuyerId",
                table: "BaseEntity",
                column: "BuyerId",
                principalTable: "BaseEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Advertisements_Users_OwnerId",
                table: "BaseEntity",
                column: "OwnerId",
                principalTable: "BaseEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AttributeDefinitionTranslations_AttributeDefinitions_TranslatableId",
                table: "BaseEntity",
                column: "AttributeDefinitionTranslation_TranslatableId",
                principalTable: "BaseEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AttributeDefinitions_AttributeDictionaries_DictionaryId",
                table: "BaseEntity",
                column: "DictionaryId",
                principalTable: "BaseEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AttributeDefinitions_Categories_CategoryId",
                table: "BaseEntity",
                column: "AttributeDefinition_CategoryId",
                principalTable: "BaseEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AttributeDictionaryValueTranslations_AttributeDictionaryValues_TranslatableId",
                table: "BaseEntity",
                column: "AttributeDictionaryValueTranslation_TranslatableId",
                principalTable: "BaseEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AttributeDictionaryValues_AttributeDictionaries_DictionaryId",
                table: "BaseEntity",
                column: "AttributeDictionaryValue_DictionaryId",
                principalTable: "BaseEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Avatars_Users_OwnerId",
                table: "BaseEntity",
                column: "OwnerId",
                principalTable: "BaseEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BasketAttributes_AttributeDefinitions_AttributeDefinitionId",
                table: "BaseEntity",
                column: "AttributeDefinitionId",
                principalTable: "BaseEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BasketItems_AdvertisementVariants_AdvertisementVariantId",
                table: "BaseEntity",
                column: "AdvertisementVariantId",
                principalTable: "BaseEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BasketItems_Baskets_BasketId",
                table: "BaseEntity",
                column: "BasketId",
                principalTable: "BaseEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Baskets_Users_OwnerId",
                table: "BaseEntity",
                column: "OwnerId",
                principalTable: "BaseEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_ProductTypes_ProductTypeId",
                table: "BaseEntity",
                column: "ProductTypeId",
                principalTable: "BaseEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryTranslations_Categories_TranslatableId",
                table: "BaseEntity",
                column: "CategoryTranslation_TranslatableId",
                principalTable: "BaseEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Cities_Regions_RegionId",
                table: "BaseEntity",
                column: "RegionId",
                principalTable: "BaseEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CityTranslations_Cities_TranslatableId",
                table: "BaseEntity",
                column: "CityTranslation_TranslatableId",
                principalTable: "BaseEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ConditionTranslations_Conditions_TranslatableId",
                table: "BaseEntity",
                column: "ConditionTranslation_TranslatableId",
                principalTable: "BaseEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Favorites_Advertisements_AdvertisementId",
                table: "BaseEntity",
                column: "Favorite_AdvertisementId",
                principalTable: "BaseEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Favorites_Users_UserId",
                table: "BaseEntity",
                column: "UserId",
                principalTable: "BaseEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTypeTranslations_ProductTypes_TranslatableId",
                table: "BaseEntity",
                column: "ProductTypeTranslation_TranslatableId",
                principalTable: "BaseEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RegionTranslations_Regions_TranslatableId",
                table: "BaseEntity",
                column: "RegionTranslation_TranslatableId",
                principalTable: "BaseEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReviewComments_Advertisements_CommentableEntityId",
                table: "BaseEntity",
                column: "CommentableEntityId",
                principalTable: "BaseEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReviewComments_Users_OwnerId",
                table: "BaseEntity",
                column: "OwnerId",
                principalTable: "BaseEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ReviewImages_ReviewComments_OwnerId",
                table: "BaseEntity",
                column: "OwnerId",
                principalTable: "BaseEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReviewRepliComments_ReviewComments_CommentableEntityId",
                table: "BaseEntity",
                column: "CommentableEntityId",
                principalTable: "BaseEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReviewRepliComments_ReviewRepliComments_ParentReplyId",
                table: "BaseEntity",
                column: "ParentReplyId",
                principalTable: "BaseEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReviewRepliComments_Users_OwnerId",
                table: "BaseEntity",
                column: "OwnerId",
                principalTable: "BaseEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ReviewRepliImages_ReviewRepliComments_OwnerId",
                table: "BaseEntity",
                column: "OwnerId",
                principalTable: "BaseEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StatusTranslations_Statuses_TranslatableId",
                table: "BaseEntity",
                column: "TranslatableId",
                principalTable: "BaseEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Cities_CityId",
                table: "BaseEntity",
                column: "CityId",
                principalTable: "BaseEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VerificationCodes_Users_UserId",
                table: "BaseEntity",
                column: "VerificationCode_UserId",
                principalTable: "BaseEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoles_Roles_RoleId",
                table: "UserRoles",
                column: "RoleId",
                principalTable: "BaseEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoles_Users_UserId",
                table: "UserRoles",
                column: "UserId",
                principalTable: "BaseEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdvertisementImages_Advertisements_OwnerId",
                table: "BaseEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_AdvertisementVariants_Advertisements_AdvertisementId",
                table: "BaseEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_Advertisements_Categories_CategoryId",
                table: "BaseEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_Advertisements_Conditions_ConditionId",
                table: "BaseEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_Advertisements_Currencies_CurrencyId",
                table: "BaseEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_Advertisements_Statuses_StatusId",
                table: "BaseEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_Advertisements_Users_BuyerId",
                table: "BaseEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_Advertisements_Users_OwnerId",
                table: "BaseEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_AttributeDefinitionTranslations_AttributeDefinitions_TranslatableId",
                table: "BaseEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_AttributeDefinitions_AttributeDictionaries_DictionaryId",
                table: "BaseEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_AttributeDefinitions_Categories_CategoryId",
                table: "BaseEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_AttributeDictionaryValueTranslations_AttributeDictionaryValues_TranslatableId",
                table: "BaseEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_AttributeDictionaryValues_AttributeDictionaries_DictionaryId",
                table: "BaseEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_Avatars_Users_OwnerId",
                table: "BaseEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_BasketAttributes_AttributeDefinitions_AttributeDefinitionId",
                table: "BaseEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_BasketItems_AdvertisementVariants_AdvertisementVariantId",
                table: "BaseEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_BasketItems_Baskets_BasketId",
                table: "BaseEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_Baskets_Users_OwnerId",
                table: "BaseEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_Categories_ProductTypes_ProductTypeId",
                table: "BaseEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_CategoryTranslations_Categories_TranslatableId",
                table: "BaseEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_Cities_Regions_RegionId",
                table: "BaseEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_CityTranslations_Cities_TranslatableId",
                table: "BaseEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_ConditionTranslations_Conditions_TranslatableId",
                table: "BaseEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_Favorites_Advertisements_AdvertisementId",
                table: "BaseEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_Favorites_Users_UserId",
                table: "BaseEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductTypeTranslations_ProductTypes_TranslatableId",
                table: "BaseEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_RegionTranslations_Regions_TranslatableId",
                table: "BaseEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_ReviewComments_Advertisements_CommentableEntityId",
                table: "BaseEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_ReviewComments_Users_OwnerId",
                table: "BaseEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_ReviewImages_ReviewComments_OwnerId",
                table: "BaseEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_ReviewRepliComments_ReviewComments_CommentableEntityId",
                table: "BaseEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_ReviewRepliComments_ReviewRepliComments_ParentReplyId",
                table: "BaseEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_ReviewRepliComments_Users_OwnerId",
                table: "BaseEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_ReviewRepliImages_ReviewRepliComments_OwnerId",
                table: "BaseEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_StatusTranslations_Statuses_TranslatableId",
                table: "BaseEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Cities_CityId",
                table: "BaseEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_VerificationCodes_Users_UserId",
                table: "BaseEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRoles_Roles_RoleId",
                table: "UserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRoles_Users_UserId",
                table: "UserRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BaseEntity",
                table: "BaseEntity");

            migrationBuilder.DropIndex(
                name: "IX_AdvertisementImages_OwnerId_Order",
                table: "BaseEntity");

            migrationBuilder.DropIndex(
                name: "IX_Advertisements_BuyerId",
                table: "BaseEntity");

            migrationBuilder.DropIndex(
                name: "IX_Advertisements_CategoryId",
                table: "BaseEntity");

            migrationBuilder.DropIndex(
                name: "IX_Advertisements_CategoryId_ConditionId_CreatedAt",
                table: "BaseEntity");

            migrationBuilder.DropIndex(
                name: "IX_Advertisements_CategoryId_CreatedAt",
                table: "BaseEntity");

            migrationBuilder.DropIndex(
                name: "IX_Advertisements_CategoryId_Popularity",
                table: "BaseEntity");

            migrationBuilder.DropIndex(
                name: "IX_Advertisements_ConditionId",
                table: "BaseEntity");

            migrationBuilder.DropIndex(
                name: "IX_Advertisements_CreatedAt",
                table: "BaseEntity");

            migrationBuilder.DropIndex(
                name: "IX_Advertisements_CurrencyId",
                table: "BaseEntity");

            migrationBuilder.DropIndex(
                name: "IX_Advertisements_OwnerId",
                table: "BaseEntity");

            migrationBuilder.DropIndex(
                name: "IX_Advertisements_OwnerId_CreatedAt",
                table: "BaseEntity");

            migrationBuilder.DropIndex(
                name: "IX_Advertisements_Popularity",
                table: "BaseEntity");

            migrationBuilder.DropIndex(
                name: "IX_Advertisements_StatusId",
                table: "BaseEntity");

            migrationBuilder.DropIndex(
                name: "IX_AdvertisementVariants_AdvertisementId",
                table: "BaseEntity");

            migrationBuilder.DropIndex(
                name: "IX_AttributeDefinitions_CategoryId_Key",
                table: "BaseEntity");

            migrationBuilder.DropIndex(
                name: "IX_AttributeDefinitions_DictionaryId",
                table: "BaseEntity");

            migrationBuilder.DropIndex(
                name: "IX_AttributeDefinitionTranslations_Language_Name",
                table: "BaseEntity");

            migrationBuilder.DropIndex(
                name: "IX_AttributeDefinitionTranslations_Name",
                table: "BaseEntity");

            migrationBuilder.DropIndex(
                name: "IX_AttributeDefinitionTranslations_TranslatableId_Language",
                table: "BaseEntity");

            migrationBuilder.DropIndex(
                name: "IX_AttributeDictionaries_Code",
                table: "BaseEntity");

            migrationBuilder.DropIndex(
                name: "IX_AttributeDictionaryValues_DictionaryId_Code",
                table: "BaseEntity");

            migrationBuilder.DropIndex(
                name: "IX_AttributeDictionaryValueTranslations_Language_Name",
                table: "BaseEntity");

            migrationBuilder.DropIndex(
                name: "IX_AttributeDictionaryValueTranslations_Name",
                table: "BaseEntity");

            migrationBuilder.DropIndex(
                name: "IX_AttributeDictionaryValueTranslations_TranslatableId_Language",
                table: "BaseEntity");

            migrationBuilder.DropIndex(
                name: "IX_Avatars_OwnerId",
                table: "BaseEntity");

            migrationBuilder.DropIndex(
                name: "IX_BasketAttributes_AttributeDefinitionId",
                table: "BaseEntity");

            migrationBuilder.DropIndex(
                name: "IX_BasketItems_AdvertisementVariantId",
                table: "BaseEntity");

            migrationBuilder.DropIndex(
                name: "IX_BasketItems_BasketId_AdvertisementVariantId",
                table: "BaseEntity");

            migrationBuilder.DropIndex(
                name: "IX_Baskets_OwnerId",
                table: "BaseEntity");

            migrationBuilder.DropIndex(
                name: "IX_Categories_ProductTypeId",
                table: "BaseEntity");

            migrationBuilder.DropIndex(
                name: "IX_Categories_ProductTypeId_Code",
                table: "BaseEntity");

            migrationBuilder.DropIndex(
                name: "IX_CategoryTranslations_Language_Name",
                table: "BaseEntity");

            migrationBuilder.DropIndex(
                name: "IX_CategoryTranslations_Name",
                table: "BaseEntity");

            migrationBuilder.DropIndex(
                name: "IX_CategoryTranslations_TranslatableId_Language",
                table: "BaseEntity");

            migrationBuilder.DropIndex(
                name: "IX_Cities_Code",
                table: "BaseEntity");

            migrationBuilder.DropIndex(
                name: "IX_Cities_RegionId",
                table: "BaseEntity");

            migrationBuilder.DropIndex(
                name: "IX_CityTranslations_Language_Name",
                table: "BaseEntity");

            migrationBuilder.DropIndex(
                name: "IX_CityTranslations_Name",
                table: "BaseEntity");

            migrationBuilder.DropIndex(
                name: "IX_CityTranslations_TranslatableId_Language",
                table: "BaseEntity");

            migrationBuilder.DropIndex(
                name: "IX_Conditions_Code",
                table: "BaseEntity");

            migrationBuilder.DropIndex(
                name: "IX_ConditionTranslations_Language_Name",
                table: "BaseEntity");

            migrationBuilder.DropIndex(
                name: "IX_ConditionTranslations_TranslatableId",
                table: "BaseEntity");

            migrationBuilder.DropIndex(
                name: "IX_Currencies_Code",
                table: "BaseEntity");

            migrationBuilder.DropIndex(
                name: "IX_Favorites_AdvertisementId",
                table: "BaseEntity");

            migrationBuilder.DropIndex(
                name: "IX_Favorites_UserId_AdvertisementId",
                table: "BaseEntity");

            migrationBuilder.DropIndex(
                name: "IX_ProductTypes_Code",
                table: "BaseEntity");

            migrationBuilder.DropIndex(
                name: "IX_ProductTypeTranslations_Language_Name",
                table: "BaseEntity");

            migrationBuilder.DropIndex(
                name: "IX_ProductTypeTranslations_Name",
                table: "BaseEntity");

            migrationBuilder.DropIndex(
                name: "IX_ProductTypeTranslations_TranslatableId_Language",
                table: "BaseEntity");

            migrationBuilder.DropIndex(
                name: "IX_Regions_Code",
                table: "BaseEntity");

            migrationBuilder.DropIndex(
                name: "IX_RegionTranslations_Language_Name",
                table: "BaseEntity");

            migrationBuilder.DropIndex(
                name: "IX_RegionTranslations_Name",
                table: "BaseEntity");

            migrationBuilder.DropIndex(
                name: "IX_RegionTranslations_TranslatableId_Language",
                table: "BaseEntity");

            migrationBuilder.DropIndex(
                name: "IX_ReviewComments_CommentableEntityId",
                table: "BaseEntity");

            migrationBuilder.DropIndex(
                name: "IX_ReviewComments_CommentableEntityId_CreatedAt",
                table: "BaseEntity");

            migrationBuilder.DropIndex(
                name: "IX_ReviewComments_OwnerId",
                table: "BaseEntity");

            migrationBuilder.DropIndex(
                name: "IX_ReviewImages_OwnerId_Order",
                table: "BaseEntity");

            migrationBuilder.DropIndex(
                name: "IX_ReviewRepliComments_CommentableEntityId",
                table: "BaseEntity");

            migrationBuilder.DropIndex(
                name: "IX_ReviewRepliComments_CommentableEntityId_CreatedAt",
                table: "BaseEntity");

            migrationBuilder.DropIndex(
                name: "IX_ReviewRepliComments_OwnerId",
                table: "BaseEntity");

            migrationBuilder.DropIndex(
                name: "IX_ReviewRepliComments_ParentReplyId",
                table: "BaseEntity");

            migrationBuilder.DropIndex(
                name: "IX_ReviewRepliComments_ParentReplyId_CreatedAt",
                table: "BaseEntity");

            migrationBuilder.DropIndex(
                name: "IX_ReviewRepliImages_OwnerId_Order",
                table: "BaseEntity");

            migrationBuilder.DropIndex(
                name: "IX_Statuses_Code",
                table: "BaseEntity");

            migrationBuilder.DropIndex(
                name: "IX_StatusTranslations_Language_Name",
                table: "BaseEntity");

            migrationBuilder.DropIndex(
                name: "IX_StatusTranslations_Name",
                table: "BaseEntity");

            migrationBuilder.DropIndex(
                name: "IX_StatusTranslations_TranslatableId_Language",
                table: "BaseEntity");

            migrationBuilder.DropIndex(
                name: "IX_Users_CityId",
                table: "BaseEntity");

            migrationBuilder.DropIndex(
                name: "IX_Users_Email",
                table: "BaseEntity");

            migrationBuilder.DropIndex(
                name: "IX_Users_Phone",
                table: "BaseEntity");

            migrationBuilder.DropIndex(
                name: "IX_Users_UserName",
                table: "BaseEntity");

            migrationBuilder.DropIndex(
                name: "IX_VerificationCodes_UserId",
                table: "BaseEntity");

            migrationBuilder.DropCheckConstraint(
                name: "CK_ReviewComments_Grade",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "EntityStatus",
                table: "UserRoles");

            migrationBuilder.DropColumn(
                name: "AdvertisementId",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "AdvertisementImage_Order",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "AdvertisementVariantId",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "AdvertisementVariant_Attributes",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "Advertisement_Description",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "AttributeDefinitionId",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "AttributeDefinitionTranslation_Language",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "AttributeDefinitionTranslation_Name",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "AttributeDefinitionTranslation_TranslatableId",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "AttributeDefinition_CategoryId",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "AttributeDictionaryValueTranslation_Language",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "AttributeDictionaryValueTranslation_Name",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "AttributeDictionaryValueTranslation_TranslatableId",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "AttributeDictionaryValue_Code",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "AttributeDictionaryValue_DictionaryId",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "AttributeDictionary_Code",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "Attributes",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "AvatarId",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "BasketId",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "Bio",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "BuyerId",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "CategoryTranslation_Language",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "CategoryTranslation_Name",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "CategoryTranslation_TranslatableId",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "Category_Code",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "CityId",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "CityTranslation_Language",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "CityTranslation_Name",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "CityTranslation_TranslatableId",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "City_Code",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "CommentableEntityId",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "ConditionId",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "ConditionTranslation_Language",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "ConditionTranslation_Name",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "ConditionTranslation_TranslatableId",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "Condition_Code",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "CurrencyId",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "Currency_Code",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "Currency_Name",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "DictionaryId",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "EmailConfirmed",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "EntityStatus",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "Favorite_AdvertisementId",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "Grade",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "IsVariant",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "Key",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "Language",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "LastSeenAt",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "Max",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "Min",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "Multiple",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "Order",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "ParentReplyId",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "PhoneConfirmed",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "Popularity",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "PreviousPrice",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "ProductTypeId",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "ProductTypeTranslation_Language",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "ProductTypeTranslation_Name",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "ProductTypeTranslation_TranslatableId",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "ProductType_Code",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "ProductType_Description",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "PublicId",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "RegionId",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "RegionTranslation_Language",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "RegionTranslation_Name",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "RegionTranslation_TranslatableId",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "Region_Code",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "RegisteredAt",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "Required",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "ReviewComment_CreatedAt",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "ReviewImage_Order",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "ReviewRepliComment_CreatedAt",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "ReviewRepliImage_Order",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "SoldAt",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "StatusTranslation_Description",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "StatusTranslation_Name",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "Stock",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "Symbol",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "Text",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "TranslatableId",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "Url",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "Value",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "VerificationCode_CreatedAt",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "VerificationCode_UserId",
                table: "BaseEntity");

            migrationBuilder.RenameTable(
                name: "BaseEntity",
                newName: "VerificationCodes");

            migrationBuilder.AlterColumn<long>(
                name: "UserId",
                table: "VerificationCodes",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Purpose",
                table: "VerificationCodes",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ExpiresAt",
                table: "VerificationCodes",
                type: "timestamptz",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamptz",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "VerificationCodes",
                type: "timestamptz",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CodeHash",
                table: "VerificationCodes",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Attempts",
                table: "VerificationCodes",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_VerificationCodes",
                table: "VerificationCodes",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "AttributeDictionaries",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttributeDictionaries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Conditions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conditions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Currencies",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Symbol = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currencies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductTypes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Regions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Statuses",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AttributeDictionaryValues",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DictionaryId = table.Column<long>(type: "bigint", nullable: false),
                    Code = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttributeDictionaryValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AttributeDictionaryValues_AttributeDictionaries_DictionaryId",
                        column: x => x.DictionaryId,
                        principalTable: "AttributeDictionaries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ConditionTranslations",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TranslatableId = table.Column<long>(type: "bigint", nullable: false),
                    Language = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConditionTranslations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConditionTranslations_Conditions_TranslatableId",
                        column: x => x.TranslatableId,
                        principalTable: "Conditions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProductTypeId = table.Column<long>(type: "bigint", nullable: false),
                    Code = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categories_ProductTypes_ProductTypeId",
                        column: x => x.ProductTypeId,
                        principalTable: "ProductTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductTypeTranslations",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TranslatableId = table.Column<long>(type: "bigint", nullable: false),
                    Language = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTypeTranslations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductTypeTranslations_ProductTypes_TranslatableId",
                        column: x => x.TranslatableId,
                        principalTable: "ProductTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RegionId = table.Column<long>(type: "bigint", nullable: false),
                    Code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cities_Regions_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Regions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RegionTranslations",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TranslatableId = table.Column<long>(type: "bigint", nullable: false),
                    Language = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegionTranslations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RegionTranslations_Regions_TranslatableId",
                        column: x => x.TranslatableId,
                        principalTable: "Regions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StatusTranslations",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TranslatableId = table.Column<long>(type: "bigint", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Language = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusTranslations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StatusTranslations_Statuses_TranslatableId",
                        column: x => x.TranslatableId,
                        principalTable: "Statuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AttributeDictionaryValueTranslations",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TranslatableId = table.Column<long>(type: "bigint", nullable: false),
                    Language = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttributeDictionaryValueTranslations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AttributeDictionaryValueTranslations_AttributeDictionaryVal~",
                        column: x => x.TranslatableId,
                        principalTable: "AttributeDictionaryValues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AttributeDefinitions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CategoryId = table.Column<long>(type: "bigint", nullable: false),
                    DictionaryId = table.Column<long>(type: "bigint", nullable: true),
                    IsVariant = table.Column<bool>(type: "boolean", nullable: false),
                    Key = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Max = table.Column<decimal>(type: "numeric", nullable: true),
                    Min = table.Column<decimal>(type: "numeric", nullable: true),
                    Multiple = table.Column<bool>(type: "boolean", nullable: false),
                    Required = table.Column<bool>(type: "boolean", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttributeDefinitions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AttributeDefinitions_AttributeDictionaries_DictionaryId",
                        column: x => x.DictionaryId,
                        principalTable: "AttributeDictionaries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AttributeDefinitions_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CategoryTranslations",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TranslatableId = table.Column<long>(type: "bigint", nullable: false),
                    Language = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryTranslations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CategoryTranslations_Categories_TranslatableId",
                        column: x => x.TranslatableId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CityTranslations",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TranslatableId = table.Column<long>(type: "bigint", nullable: false),
                    Language = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CityTranslations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CityTranslations_Cities_TranslatableId",
                        column: x => x.TranslatableId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CityId = table.Column<long>(type: "bigint", nullable: true),
                    AvatarId = table.Column<long>(type: "bigint", nullable: true),
                    Bio = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Email = table.Column<string>(type: "character varying(254)", maxLength: 254, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    FirstName = table.Column<string>(type: "character varying(35)", maxLength: 35, nullable: true),
                    LastName = table.Column<string>(type: "character varying(35)", maxLength: 35, nullable: true),
                    LastSeenAt = table.Column<DateTime>(type: "timestamptz", nullable: true),
                    PasswordHash = table.Column<string>(type: "text", nullable: false),
                    Phone = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    PhoneConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    RegisteredAt = table.Column<DateTime>(type: "timestamptz", nullable: false),
                    Status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    UserName = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AttributeDefinitionTranslations",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TranslatableId = table.Column<long>(type: "bigint", nullable: false),
                    Language = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttributeDefinitionTranslations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AttributeDefinitionTranslations_AttributeDefinitions_Transl~",
                        column: x => x.TranslatableId,
                        principalTable: "AttributeDefinitions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BasketAttributes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AttributeDefinitionId = table.Column<long>(type: "bigint", nullable: false),
                    Order = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BasketAttributes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BasketAttributes_AttributeDefinitions_AttributeDefinitionId",
                        column: x => x.AttributeDefinitionId,
                        principalTable: "AttributeDefinitions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Advertisements",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BuyerId = table.Column<long>(type: "bigint", nullable: true),
                    CategoryId = table.Column<long>(type: "bigint", nullable: false),
                    ConditionId = table.Column<long>(type: "bigint", nullable: false),
                    CurrencyId = table.Column<long>(type: "bigint", nullable: false),
                    OwnerId = table.Column<long>(type: "bigint", nullable: false),
                    StatusId = table.Column<long>(type: "bigint", nullable: false),
                    Attributes = table.Column<JsonDocument>(type: "jsonb", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Popularity = table.Column<int>(type: "integer", nullable: false),
                    SoldAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Title = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Advertisements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Advertisements_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Advertisements_Conditions_ConditionId",
                        column: x => x.ConditionId,
                        principalTable: "Conditions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Advertisements_Currencies_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Advertisements_Statuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Statuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Advertisements_Users_BuyerId",
                        column: x => x.BuyerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Advertisements_Users_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Avatars",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OwnerId = table.Column<long>(type: "bigint", nullable: false),
                    PublicId = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Url = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Avatars", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Avatars_Users_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Baskets",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OwnerId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Baskets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Baskets_Users_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AdvertisementImages",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OwnerId = table.Column<long>(type: "bigint", nullable: false),
                    Order = table.Column<int>(type: "integer", nullable: false),
                    PublicId = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Url = table.Column<string>(type: "text", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "AdvertisementVariants",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AdvertisementId = table.Column<long>(type: "bigint", nullable: false),
                    Attributes = table.Column<JsonDocument>(type: "jsonb", nullable: false),
                    PreviousPrice = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    Price = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    Stock = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdvertisementVariants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdvertisementVariants_Advertisements_AdvertisementId",
                        column: x => x.AdvertisementId,
                        principalTable: "Advertisements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Favorites",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AdvertisementId = table.Column<long>(type: "bigint", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Favorites", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Favorites_Advertisements_AdvertisementId",
                        column: x => x.AdvertisementId,
                        principalTable: "Advertisements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Favorites_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReviewComments",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CommentableEntityId = table.Column<long>(type: "bigint", nullable: false),
                    OwnerId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Grade = table.Column<int>(type: "integer", nullable: false),
                    Text = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReviewComments", x => x.Id);
                    table.CheckConstraint("CK_ReviewComments_Grade", "\"Grade\" BETWEEN 1 AND 5");
                    table.ForeignKey(
                        name: "FK_ReviewComments_Advertisements_CommentableEntityId",
                        column: x => x.CommentableEntityId,
                        principalTable: "Advertisements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReviewComments_Users_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BasketItems",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AdvertisementVariantId = table.Column<long>(type: "bigint", nullable: false),
                    BasketId = table.Column<long>(type: "bigint", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false, defaultValue: 1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BasketItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BasketItems_AdvertisementVariants_AdvertisementVariantId",
                        column: x => x.AdvertisementVariantId,
                        principalTable: "AdvertisementVariants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BasketItems_Baskets_BasketId",
                        column: x => x.BasketId,
                        principalTable: "Baskets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReviewImages",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OwnerId = table.Column<long>(type: "bigint", nullable: false),
                    Order = table.Column<int>(type: "integer", nullable: false),
                    PublicId = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Url = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReviewImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReviewImages_ReviewComments_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "ReviewComments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReviewRepliComments",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CommentableEntityId = table.Column<long>(type: "bigint", nullable: false),
                    OwnerId = table.Column<long>(type: "bigint", nullable: false),
                    ParentReplyId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Text = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReviewRepliComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReviewRepliComments_ReviewComments_CommentableEntityId",
                        column: x => x.CommentableEntityId,
                        principalTable: "ReviewComments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReviewRepliComments_ReviewRepliComments_ParentReplyId",
                        column: x => x.ParentReplyId,
                        principalTable: "ReviewRepliComments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReviewRepliComments_Users_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReviewRepliImages",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OwnerId = table.Column<long>(type: "bigint", nullable: false),
                    Order = table.Column<int>(type: "integer", nullable: false),
                    PublicId = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Url = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReviewRepliImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReviewRepliImages_ReviewRepliComments_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "ReviewRepliComments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VerificationCodes_UserId",
                table: "VerificationCodes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AdvertisementImages_OwnerId_Order",
                table: "AdvertisementImages",
                columns: new[] { "OwnerId", "Order" },
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
                name: "IX_Advertisements_CategoryId_ConditionId_CreatedAt",
                table: "Advertisements",
                columns: new[] { "CategoryId", "ConditionId", "CreatedAt" });

            migrationBuilder.CreateIndex(
                name: "IX_Advertisements_CategoryId_CreatedAt",
                table: "Advertisements",
                columns: new[] { "CategoryId", "CreatedAt" });

            migrationBuilder.CreateIndex(
                name: "IX_Advertisements_CategoryId_Popularity",
                table: "Advertisements",
                columns: new[] { "CategoryId", "Popularity" });

            migrationBuilder.CreateIndex(
                name: "IX_Advertisements_ConditionId",
                table: "Advertisements",
                column: "ConditionId");

            migrationBuilder.CreateIndex(
                name: "IX_Advertisements_CreatedAt",
                table: "Advertisements",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Advertisements_CurrencyId",
                table: "Advertisements",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Advertisements_OwnerId",
                table: "Advertisements",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Advertisements_OwnerId_CreatedAt",
                table: "Advertisements",
                columns: new[] { "OwnerId", "CreatedAt" });

            migrationBuilder.CreateIndex(
                name: "IX_Advertisements_Popularity",
                table: "Advertisements",
                column: "Popularity");

            migrationBuilder.CreateIndex(
                name: "IX_Advertisements_StatusId",
                table: "Advertisements",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_AdvertisementVariants_AdvertisementId",
                table: "AdvertisementVariants",
                column: "AdvertisementId");

            migrationBuilder.CreateIndex(
                name: "IX_AttributeDefinitions_CategoryId_Key",
                table: "AttributeDefinitions",
                columns: new[] { "CategoryId", "Key" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AttributeDefinitions_DictionaryId",
                table: "AttributeDefinitions",
                column: "DictionaryId");

            migrationBuilder.CreateIndex(
                name: "IX_AttributeDefinitionTranslations_Language_Name",
                table: "AttributeDefinitionTranslations",
                columns: new[] { "Language", "Name" });

            migrationBuilder.CreateIndex(
                name: "IX_AttributeDefinitionTranslations_Name",
                table: "AttributeDefinitionTranslations",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_AttributeDefinitionTranslations_TranslatableId_Language",
                table: "AttributeDefinitionTranslations",
                columns: new[] { "TranslatableId", "Language" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AttributeDictionaries_Code",
                table: "AttributeDictionaries",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AttributeDictionaryValues_DictionaryId_Code",
                table: "AttributeDictionaryValues",
                columns: new[] { "DictionaryId", "Code" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AttributeDictionaryValueTranslations_Language_Name",
                table: "AttributeDictionaryValueTranslations",
                columns: new[] { "Language", "Name" });

            migrationBuilder.CreateIndex(
                name: "IX_AttributeDictionaryValueTranslations_Name",
                table: "AttributeDictionaryValueTranslations",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_AttributeDictionaryValueTranslations_TranslatableId_Language",
                table: "AttributeDictionaryValueTranslations",
                columns: new[] { "TranslatableId", "Language" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Avatars_OwnerId",
                table: "Avatars",
                column: "OwnerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BasketAttributes_AttributeDefinitionId",
                table: "BasketAttributes",
                column: "AttributeDefinitionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BasketItems_AdvertisementVariantId",
                table: "BasketItems",
                column: "AdvertisementVariantId");

            migrationBuilder.CreateIndex(
                name: "IX_BasketItems_BasketId_AdvertisementVariantId",
                table: "BasketItems",
                columns: new[] { "BasketId", "AdvertisementVariantId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Baskets_OwnerId",
                table: "Baskets",
                column: "OwnerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ProductTypeId",
                table: "Categories",
                column: "ProductTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ProductTypeId_Code",
                table: "Categories",
                columns: new[] { "ProductTypeId", "Code" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CategoryTranslations_Language_Name",
                table: "CategoryTranslations",
                columns: new[] { "Language", "Name" });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryTranslations_Name",
                table: "CategoryTranslations",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryTranslations_TranslatableId_Language",
                table: "CategoryTranslations",
                columns: new[] { "TranslatableId", "Language" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cities_Code",
                table: "Cities",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cities_RegionId",
                table: "Cities",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_CityTranslations_Language_Name",
                table: "CityTranslations",
                columns: new[] { "Language", "Name" });

            migrationBuilder.CreateIndex(
                name: "IX_CityTranslations_Name",
                table: "CityTranslations",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_CityTranslations_TranslatableId_Language",
                table: "CityTranslations",
                columns: new[] { "TranslatableId", "Language" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Conditions_Code",
                table: "Conditions",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ConditionTranslations_Language_Name",
                table: "ConditionTranslations",
                columns: new[] { "Language", "Name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ConditionTranslations_TranslatableId",
                table: "ConditionTranslations",
                column: "TranslatableId");

            migrationBuilder.CreateIndex(
                name: "IX_Currencies_Code",
                table: "Currencies",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Favorites_AdvertisementId",
                table: "Favorites",
                column: "AdvertisementId");

            migrationBuilder.CreateIndex(
                name: "IX_Favorites_UserId_AdvertisementId",
                table: "Favorites",
                columns: new[] { "UserId", "AdvertisementId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductTypes_Code",
                table: "ProductTypes",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductTypeTranslations_Language_Name",
                table: "ProductTypeTranslations",
                columns: new[] { "Language", "Name" });

            migrationBuilder.CreateIndex(
                name: "IX_ProductTypeTranslations_Name",
                table: "ProductTypeTranslations",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTypeTranslations_TranslatableId_Language",
                table: "ProductTypeTranslations",
                columns: new[] { "TranslatableId", "Language" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Regions_Code",
                table: "Regions",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RegionTranslations_Language_Name",
                table: "RegionTranslations",
                columns: new[] { "Language", "Name" });

            migrationBuilder.CreateIndex(
                name: "IX_RegionTranslations_Name",
                table: "RegionTranslations",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_RegionTranslations_TranslatableId_Language",
                table: "RegionTranslations",
                columns: new[] { "TranslatableId", "Language" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ReviewComments_CommentableEntityId",
                table: "ReviewComments",
                column: "CommentableEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewComments_CommentableEntityId_CreatedAt",
                table: "ReviewComments",
                columns: new[] { "CommentableEntityId", "CreatedAt" });

            migrationBuilder.CreateIndex(
                name: "IX_ReviewComments_OwnerId",
                table: "ReviewComments",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewImages_OwnerId_Order",
                table: "ReviewImages",
                columns: new[] { "OwnerId", "Order" },
                unique: true);

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
                name: "IX_ReviewRepliImages_OwnerId_Order",
                table: "ReviewRepliImages",
                columns: new[] { "OwnerId", "Order" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Statuses_Code",
                table: "Statuses",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StatusTranslations_Language_Name",
                table: "StatusTranslations",
                columns: new[] { "Language", "Name" });

            migrationBuilder.CreateIndex(
                name: "IX_StatusTranslations_Name",
                table: "StatusTranslations",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_StatusTranslations_TranslatableId_Language",
                table: "StatusTranslations",
                columns: new[] { "TranslatableId", "Language" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_CityId",
                table: "Users",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true,
                filter: "\"Email\" IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Phone",
                table: "Users",
                column: "Phone",
                unique: true,
                filter: "\"Phone\" IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserName",
                table: "Users",
                column: "UserName",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoles_Roles_RoleId",
                table: "UserRoles",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoles_Users_UserId",
                table: "UserRoles",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VerificationCodes_Users_UserId",
                table: "VerificationCodes",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
