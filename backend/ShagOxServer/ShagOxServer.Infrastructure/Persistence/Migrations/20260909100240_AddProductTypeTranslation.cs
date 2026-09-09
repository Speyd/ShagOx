using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ShagOxServer.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddProductTypeTranslation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "ProductTypes",
                newName: "Code");

            migrationBuilder.RenameIndex(
                name: "IX_ProductTypes_Name",
                table: "ProductTypes",
                newName: "IX_ProductTypes_Code");

            migrationBuilder.CreateTable(
                name: "ProductTypeTranslations",
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
                    table.PrimaryKey("PK_ProductTypeTranslations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductTypeTranslations_ProductTypes_TranslatableId",
                        column: x => x.TranslatableId,
                        principalTable: "ProductTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductTypeTranslations");

            migrationBuilder.RenameColumn(
                name: "Code",
                table: "ProductTypes",
                newName: "Name");

            migrationBuilder.RenameIndex(
                name: "IX_ProductTypes_Code",
                table: "ProductTypes",
                newName: "IX_ProductTypes_Name");
        }
    }
}
