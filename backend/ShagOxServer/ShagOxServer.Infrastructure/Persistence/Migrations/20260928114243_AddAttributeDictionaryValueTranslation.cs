using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ShagOxServer.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddAttributeDictionaryValueTranslation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AttributeDictionaryValueTranslations",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    TranslatableId = table.Column<long>(type: "bigint", nullable: false),
                    Language = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false)
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AttributeDictionaryValueTranslations");
        }
    }
}
