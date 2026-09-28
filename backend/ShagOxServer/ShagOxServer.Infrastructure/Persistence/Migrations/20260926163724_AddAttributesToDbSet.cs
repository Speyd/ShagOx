using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShagOxServer.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddAttributesToDbSet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AttributeDictionaryValue_AttributeDictionary_DictionaryId",
                table: "AttributeDictionaryValue");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AttributeDictionaryValue",
                table: "AttributeDictionaryValue");

            migrationBuilder.RenameTable(
                name: "AttributeDictionaryValue",
                newName: "AttributeDictionaryValues");

            migrationBuilder.RenameIndex(
                name: "IX_AttributeDictionaryValue_DictionaryId_Code",
                table: "AttributeDictionaryValues",
                newName: "IX_AttributeDictionaryValues_DictionaryId_Code");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AttributeDictionaryValues",
                table: "AttributeDictionaryValues",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AttributeDictionaryValues_AttributeDictionary_DictionaryId",
                table: "AttributeDictionaryValues",
                column: "DictionaryId",
                principalTable: "AttributeDictionary",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AttributeDictionaryValues_AttributeDictionary_DictionaryId",
                table: "AttributeDictionaryValues");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AttributeDictionaryValues",
                table: "AttributeDictionaryValues");

            migrationBuilder.RenameTable(
                name: "AttributeDictionaryValues",
                newName: "AttributeDictionaryValue");

            migrationBuilder.RenameIndex(
                name: "IX_AttributeDictionaryValues_DictionaryId_Code",
                table: "AttributeDictionaryValue",
                newName: "IX_AttributeDictionaryValue_DictionaryId_Code");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AttributeDictionaryValue",
                table: "AttributeDictionaryValue",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AttributeDictionaryValue_AttributeDictionary_DictionaryId",
                table: "AttributeDictionaryValue",
                column: "DictionaryId",
                principalTable: "AttributeDictionary",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
