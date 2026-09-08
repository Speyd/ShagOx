using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ShagOxServer.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddAttributeDefinitionTranslations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_StatusTranslations_Language_Name",
                table: "StatusTranslations");

            migrationBuilder.DropIndex(
                name: "IX_StatusTranslations_TranslatableId",
                table: "StatusTranslations");

            migrationBuilder.DropIndex(
                name: "IX_RegionTranslations_Language_Name",
                table: "RegionTranslations");

            migrationBuilder.DropIndex(
                name: "IX_RegionTranslations_TranslatableId",
                table: "RegionTranslations");

            migrationBuilder.DropIndex(
                name: "IX_CityTranslations_Language_Name",
                table: "CityTranslations");

            migrationBuilder.DropIndex(
                name: "IX_CityTranslations_TranslatableId",
                table: "CityTranslations");

            migrationBuilder.CreateTable(
                name: "AttributeDefinitionTranslations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    TranslatableId = table.Column<int>(type: "integer", nullable: false),
                    Language = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false)
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AttributeDefinitionTranslations");

            migrationBuilder.DropIndex(
                name: "IX_StatusTranslations_Language_Name",
                table: "StatusTranslations");

            migrationBuilder.DropIndex(
                name: "IX_StatusTranslations_Name",
                table: "StatusTranslations");

            migrationBuilder.DropIndex(
                name: "IX_StatusTranslations_TranslatableId_Language",
                table: "StatusTranslations");

            migrationBuilder.DropIndex(
                name: "IX_RegionTranslations_Language_Name",
                table: "RegionTranslations");

            migrationBuilder.DropIndex(
                name: "IX_RegionTranslations_Name",
                table: "RegionTranslations");

            migrationBuilder.DropIndex(
                name: "IX_RegionTranslations_TranslatableId_Language",
                table: "RegionTranslations");

            migrationBuilder.DropIndex(
                name: "IX_CityTranslations_Language_Name",
                table: "CityTranslations");

            migrationBuilder.DropIndex(
                name: "IX_CityTranslations_Name",
                table: "CityTranslations");

            migrationBuilder.DropIndex(
                name: "IX_CityTranslations_TranslatableId_Language",
                table: "CityTranslations");

            migrationBuilder.CreateIndex(
                name: "IX_StatusTranslations_Language_Name",
                table: "StatusTranslations",
                columns: new[] { "Language", "Name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StatusTranslations_TranslatableId",
                table: "StatusTranslations",
                column: "TranslatableId");

            migrationBuilder.CreateIndex(
                name: "IX_RegionTranslations_Language_Name",
                table: "RegionTranslations",
                columns: new[] { "Language", "Name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RegionTranslations_TranslatableId",
                table: "RegionTranslations",
                column: "TranslatableId");

            migrationBuilder.CreateIndex(
                name: "IX_CityTranslations_Language_Name",
                table: "CityTranslations",
                columns: new[] { "Language", "Name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CityTranslations_TranslatableId",
                table: "CityTranslations",
                column: "TranslatableId");
        }
    }
}
