using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ShagOxServer.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddAttributeDictionaryAndAttributeDictionaryValue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "DictionaryId",
                table: "AttributeDefinitions",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AttributeDictionary",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttributeDictionary", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AttributeDictionaryValue",
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
                    table.PrimaryKey("PK_AttributeDictionaryValue", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AttributeDictionaryValue_AttributeDictionary_DictionaryId",
                        column: x => x.DictionaryId,
                        principalTable: "AttributeDictionary",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AttributeDefinitions_DictionaryId",
                table: "AttributeDefinitions",
                column: "DictionaryId");

            migrationBuilder.CreateIndex(
                name: "IX_AttributeDictionary_Code",
                table: "AttributeDictionary",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AttributeDictionaryValue_DictionaryId_Code",
                table: "AttributeDictionaryValue",
                columns: new[] { "DictionaryId", "Code" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AttributeDefinitions_AttributeDictionary_DictionaryId",
                table: "AttributeDefinitions",
                column: "DictionaryId",
                principalTable: "AttributeDictionary",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AttributeDefinitions_AttributeDictionary_DictionaryId",
                table: "AttributeDefinitions");

            migrationBuilder.DropTable(
                name: "AttributeDictionaryValue");

            migrationBuilder.DropTable(
                name: "AttributeDictionary");

            migrationBuilder.DropIndex(
                name: "IX_AttributeDefinitions_DictionaryId",
                table: "AttributeDefinitions");

            migrationBuilder.DropColumn(
                name: "DictionaryId",
                table: "AttributeDefinitions");
        }
    }
}
