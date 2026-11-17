using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShagOxServer.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RenameBasketAttributeToLineItemAttribute : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "BasketAttributes",
                newName: "LineItemAttributes");

            migrationBuilder.RenameIndex(
                name: "IX_BasketAttributes_AttributeDefinitionId",
                table: "LineItemAttributes",
                newName: "IX_LineItemAttributes_AttributeDefinitionId");

            migrationBuilder.Sql("""
                ALTER TABLE "LineItemAttributes"
                RENAME CONSTRAINT "PK_BasketAttributes"
                TO "PK_LineItemAttributes";
                """);

            migrationBuilder.Sql("""
                ALTER TABLE "LineItemAttributes"
                RENAME CONSTRAINT "FK_BasketAttributes_AttributeDefinitions_AttributeDefinitionId"
                TO "FK_LineItemAttributes_AttributeDefinitions_AttributeDefinitionId";
                """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
                ALTER TABLE "LineItemAttributes"
                RENAME CONSTRAINT "FK_LineItemAttributes_AttributeDefinitions_AttributeDefinitionId"
                TO "FK_BasketAttributes_AttributeDefinitions_AttributeDefinitionId";
                """);

            migrationBuilder.Sql("""
                ALTER TABLE "LineItemAttributes"
                RENAME CONSTRAINT "PK_LineItemAttributes"
                TO "PK_BasketAttributes";
                """);

            migrationBuilder.RenameIndex(
                name: "IX_LineItemAttributes_AttributeDefinitionId",
                table: "LineItemAttributes",
                newName: "IX_BasketAttributes_AttributeDefinitionId");

            migrationBuilder.RenameTable(
                name: "LineItemAttributes",
                newName: "BasketAttributes");
        }
    }
}
