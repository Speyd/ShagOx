using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ShagOxServer.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddDeliveryProviderTranslation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DeliveryProviderTranslations",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    EntityStatus = table.Column<string>(type: "text", nullable: false, defaultValue: "Active"),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    TranslatableId = table.Column<long>(type: "bigint", nullable: false),
                    Language = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryProviderTranslations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeliveryProviderTranslations_DeliveryProviders_TranslatableId",
                        column: x => x.TranslatableId,
                        principalTable: "DeliveryProviders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryProviderTranslations_Language_Name",
                table: "DeliveryProviderTranslations",
                columns: new[] { "Language", "Name" });

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryProviderTranslations_Name",
                table: "DeliveryProviderTranslations",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryProviderTranslations_TranslatableId_Language",
                table: "DeliveryProviderTranslations",
                columns: new[] { "TranslatableId", "Language" },
                unique: true,
                filter: "\"EntityStatus\" IN ('Active', 'Hidden')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeliveryProviderTranslations");
        }
    }
}
