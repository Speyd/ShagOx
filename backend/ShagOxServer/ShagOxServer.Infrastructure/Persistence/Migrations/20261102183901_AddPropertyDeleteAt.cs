using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShagOxServer.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPropertyDeleteAt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "VerificationCodes",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Users",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "UserRoles",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "StatusTranslations",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Statuses",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Roles",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "ReviewRepliImages",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "ReviewRepliComments",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "ReviewImages",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "ReviewComments",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "RegionTranslations",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Regions",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "ProductTypeTranslations",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "ProductTypes",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Favorites",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Currencies",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "ConditionTranslations",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Conditions",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "CityTranslations",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Cities",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "CategoryTranslations",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Categories",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Baskets",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "BasketItems",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "BasketAttributes",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Avatars",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "AttributeDictionaryValueTranslations",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "AttributeDictionaryValues",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "AttributeDictionaries",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "AttributeDefinitionTranslations",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "AttributeDefinitions",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "AdvertisementVariants",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Advertisements",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "AdvertisementImages",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "VerificationCodes");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "UserRoles");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "StatusTranslations");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Statuses");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "ReviewRepliImages");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "ReviewRepliComments");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "ReviewImages");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "ReviewComments");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "RegionTranslations");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Regions");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "ProductTypeTranslations");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "ProductTypes");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Favorites");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Currencies");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "ConditionTranslations");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Conditions");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "CityTranslations");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Cities");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "CategoryTranslations");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Baskets");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "BasketItems");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "BasketAttributes");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Avatars");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "AttributeDictionaryValueTranslations");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "AttributeDictionaryValues");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "AttributeDictionaries");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "AttributeDefinitionTranslations");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "AttributeDefinitions");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "AdvertisementVariants");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Advertisements");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "AdvertisementImages");
        }
    }
}
