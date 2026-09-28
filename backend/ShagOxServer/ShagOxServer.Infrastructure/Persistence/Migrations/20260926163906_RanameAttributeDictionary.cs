using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShagOxServer.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RanameAttributeDictionary : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AttributeDefinitions_AttributeDictionary_DictionaryId",
                table: "AttributeDefinitions");

            migrationBuilder.DropForeignKey(
                name: "FK_AttributeDictionaryValues_AttributeDictionary_DictionaryId",
                table: "AttributeDictionaryValues");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AttributeDictionary",
                table: "AttributeDictionary");

            migrationBuilder.RenameTable(
                name: "AttributeDictionary",
                newName: "AttributeDictionaries");

            migrationBuilder.RenameIndex(
                name: "IX_AttributeDictionary_Code",
                table: "AttributeDictionaries",
                newName: "IX_AttributeDictionaries_Code");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AttributeDictionaries",
                table: "AttributeDictionaries",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AttributeDefinitions_AttributeDictionaries_DictionaryId",
                table: "AttributeDefinitions",
                column: "DictionaryId",
                principalTable: "AttributeDictionaries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AttributeDictionaryValues_AttributeDictionaries_DictionaryId",
                table: "AttributeDictionaryValues",
                column: "DictionaryId",
                principalTable: "AttributeDictionaries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AttributeDefinitions_AttributeDictionaries_DictionaryId",
                table: "AttributeDefinitions");

            migrationBuilder.DropForeignKey(
                name: "FK_AttributeDictionaryValues_AttributeDictionaries_DictionaryId",
                table: "AttributeDictionaryValues");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AttributeDictionaries",
                table: "AttributeDictionaries");

            migrationBuilder.RenameTable(
                name: "AttributeDictionaries",
                newName: "AttributeDictionary");

            migrationBuilder.RenameIndex(
                name: "IX_AttributeDictionaries_Code",
                table: "AttributeDictionary",
                newName: "IX_AttributeDictionary_Code");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AttributeDictionary",
                table: "AttributeDictionary",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AttributeDefinitions_AttributeDictionary_DictionaryId",
                table: "AttributeDefinitions",
                column: "DictionaryId",
                principalTable: "AttributeDictionary",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AttributeDictionaryValues_AttributeDictionary_DictionaryId",
                table: "AttributeDictionaryValues",
                column: "DictionaryId",
                principalTable: "AttributeDictionary",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
