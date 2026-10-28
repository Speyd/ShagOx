using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShagOxServer.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class StatusInBaseEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EntityStatus",
                table: "VerificationCodes",
                type: "text",
                nullable: false,
                defaultValue: "Active");

            migrationBuilder.AddColumn<string>(
                name: "EntityStatus",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "Active");

            migrationBuilder.AddColumn<string>(
                name: "EntityStatus",
                table: "UserRoles",
                type: "text",
                nullable: false,
                defaultValue: "Active");

            migrationBuilder.AddColumn<string>(
                name: "EntityStatus",
                table: "StatusTranslations",
                type: "text",
                nullable: false,
                defaultValue: "Active");

            migrationBuilder.AddColumn<string>(
                name: "EntityStatus",
                table: "Statuses",
                type: "text",
                nullable: false,
                defaultValue: "Active");

            migrationBuilder.AddColumn<string>(
                name: "EntityStatus",
                table: "Roles",
                type: "text",
                nullable: false,
                defaultValue: "Active");

            migrationBuilder.AddColumn<string>(
                name: "EntityStatus",
                table: "ReviewRepliImages",
                type: "text",
                nullable: false,
                defaultValue: "Active");

            migrationBuilder.AddColumn<string>(
                name: "EntityStatus",
                table: "ReviewRepliComments",
                type: "text",
                nullable: false,
                defaultValue: "Active");

            migrationBuilder.AddColumn<string>(
                name: "EntityStatus",
                table: "ReviewImages",
                type: "text",
                nullable: false,
                defaultValue: "Active");

            migrationBuilder.AddColumn<string>(
                name: "EntityStatus",
                table: "ReviewComments",
                type: "text",
                nullable: false,
                defaultValue: "Active");

            migrationBuilder.AddColumn<string>(
                name: "EntityStatus",
                table: "RegionTranslations",
                type: "text",
                nullable: false,
                defaultValue: "Active");

            migrationBuilder.AddColumn<string>(
                name: "EntityStatus",
                table: "Regions",
                type: "text",
                nullable: false,
                defaultValue: "Active");

            migrationBuilder.AddColumn<string>(
                name: "EntityStatus",
                table: "ProductTypeTranslations",
                type: "text",
                nullable: false,
                defaultValue: "Active");

            migrationBuilder.AddColumn<string>(
                name: "EntityStatus",
                table: "ProductTypes",
                type: "text",
                nullable: false,
                defaultValue: "Active");

            migrationBuilder.AddColumn<string>(
                name: "EntityStatus",
                table: "Favorites",
                type: "text",
                nullable: false,
                defaultValue: "Active");

            migrationBuilder.AddColumn<string>(
                name: "EntityStatus",
                table: "Currencies",
                type: "text",
                nullable: false,
                defaultValue: "Active");

            migrationBuilder.AddColumn<string>(
                name: "EntityStatus",
                table: "ConditionTranslations",
                type: "text",
                nullable: false,
                defaultValue: "Active");

            migrationBuilder.AddColumn<string>(
                name: "EntityStatus",
                table: "Conditions",
                type: "text",
                nullable: false,
                defaultValue: "Active");

            migrationBuilder.AddColumn<string>(
                name: "EntityStatus",
                table: "CityTranslations",
                type: "text",
                nullable: false,
                defaultValue: "Active");

            migrationBuilder.AddColumn<string>(
                name: "EntityStatus",
                table: "Cities",
                type: "text",
                nullable: false,
                defaultValue: "Active");

            migrationBuilder.AddColumn<string>(
                name: "EntityStatus",
                table: "CategoryTranslations",
                type: "text",
                nullable: false,
                defaultValue: "Active");

            migrationBuilder.AddColumn<string>(
                name: "EntityStatus",
                table: "Categories",
                type: "text",
                nullable: false,
                defaultValue: "Active");

            migrationBuilder.AddColumn<string>(
                name: "EntityStatus",
                table: "Baskets",
                type: "text",
                nullable: false,
                defaultValue: "Active");

            migrationBuilder.AddColumn<string>(
                name: "EntityStatus",
                table: "BasketItems",
                type: "text",
                nullable: false,
                defaultValue: "Active");

            migrationBuilder.AddColumn<string>(
                name: "EntityStatus",
                table: "BasketAttributes",
                type: "text",
                nullable: false,
                defaultValue: "Active");

            migrationBuilder.AddColumn<string>(
                name: "EntityStatus",
                table: "Avatars",
                type: "text",
                nullable: false,
                defaultValue: "Active");

            migrationBuilder.AddColumn<string>(
                name: "EntityStatus",
                table: "AttributeDictionaryValueTranslations",
                type: "text",
                nullable: false,
                defaultValue: "Active");

            migrationBuilder.AddColumn<string>(
                name: "EntityStatus",
                table: "AttributeDictionaryValues",
                type: "text",
                nullable: false,
                defaultValue: "Active");

            migrationBuilder.AddColumn<string>(
                name: "EntityStatus",
                table: "AttributeDictionaries",
                type: "text",
                nullable: false,
                defaultValue: "Active");

            migrationBuilder.AddColumn<string>(
                name: "EntityStatus",
                table: "AttributeDefinitionTranslations",
                type: "text",
                nullable: false,
                defaultValue: "Active");

            migrationBuilder.AddColumn<string>(
                name: "EntityStatus",
                table: "AttributeDefinitions",
                type: "text",
                nullable: false,
                defaultValue: "Active");

            migrationBuilder.AddColumn<string>(
                name: "EntityStatus",
                table: "AdvertisementVariants",
                type: "text",
                nullable: false,
                defaultValue: "Active");

            migrationBuilder.AddColumn<string>(
                name: "EntityStatus",
                table: "Advertisements",
                type: "text",
                nullable: false,
                defaultValue: "Active");

            migrationBuilder.AddColumn<string>(
                name: "EntityStatus",
                table: "AdvertisementImages",
                type: "text",
                nullable: false,
                defaultValue: "Active");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EntityStatus",
                table: "VerificationCodes");

            migrationBuilder.DropColumn(
                name: "EntityStatus",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "EntityStatus",
                table: "UserRoles");

            migrationBuilder.DropColumn(
                name: "EntityStatus",
                table: "StatusTranslations");

            migrationBuilder.DropColumn(
                name: "EntityStatus",
                table: "Statuses");

            migrationBuilder.DropColumn(
                name: "EntityStatus",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "EntityStatus",
                table: "ReviewRepliImages");

            migrationBuilder.DropColumn(
                name: "EntityStatus",
                table: "ReviewRepliComments");

            migrationBuilder.DropColumn(
                name: "EntityStatus",
                table: "ReviewImages");

            migrationBuilder.DropColumn(
                name: "EntityStatus",
                table: "ReviewComments");

            migrationBuilder.DropColumn(
                name: "EntityStatus",
                table: "RegionTranslations");

            migrationBuilder.DropColumn(
                name: "EntityStatus",
                table: "Regions");

            migrationBuilder.DropColumn(
                name: "EntityStatus",
                table: "ProductTypeTranslations");

            migrationBuilder.DropColumn(
                name: "EntityStatus",
                table: "ProductTypes");

            migrationBuilder.DropColumn(
                name: "EntityStatus",
                table: "Favorites");

            migrationBuilder.DropColumn(
                name: "EntityStatus",
                table: "Currencies");

            migrationBuilder.DropColumn(
                name: "EntityStatus",
                table: "ConditionTranslations");

            migrationBuilder.DropColumn(
                name: "EntityStatus",
                table: "Conditions");

            migrationBuilder.DropColumn(
                name: "EntityStatus",
                table: "CityTranslations");

            migrationBuilder.DropColumn(
                name: "EntityStatus",
                table: "Cities");

            migrationBuilder.DropColumn(
                name: "EntityStatus",
                table: "CategoryTranslations");

            migrationBuilder.DropColumn(
                name: "EntityStatus",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "EntityStatus",
                table: "Baskets");

            migrationBuilder.DropColumn(
                name: "EntityStatus",
                table: "BasketItems");

            migrationBuilder.DropColumn(
                name: "EntityStatus",
                table: "BasketAttributes");

            migrationBuilder.DropColumn(
                name: "EntityStatus",
                table: "Avatars");

            migrationBuilder.DropColumn(
                name: "EntityStatus",
                table: "AttributeDictionaryValueTranslations");

            migrationBuilder.DropColumn(
                name: "EntityStatus",
                table: "AttributeDictionaryValues");

            migrationBuilder.DropColumn(
                name: "EntityStatus",
                table: "AttributeDictionaries");

            migrationBuilder.DropColumn(
                name: "EntityStatus",
                table: "AttributeDefinitionTranslations");

            migrationBuilder.DropColumn(
                name: "EntityStatus",
                table: "AttributeDefinitions");

            migrationBuilder.DropColumn(
                name: "EntityStatus",
                table: "AdvertisementVariants");

            migrationBuilder.DropColumn(
                name: "EntityStatus",
                table: "Advertisements");

            migrationBuilder.DropColumn(
                name: "EntityStatus",
                table: "AdvertisementImages");
        }
    }
}
