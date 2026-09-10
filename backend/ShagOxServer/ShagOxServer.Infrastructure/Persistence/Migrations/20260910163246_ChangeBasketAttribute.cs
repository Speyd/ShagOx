using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShagOxServer.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeBasketAttribute : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BasketAttributes_Categories_CategoryId",
                table: "BasketAttributes");

            migrationBuilder.DropIndex(
                name: "IX_BasketAttributes_AttributeDefinitionId",
                table: "BasketAttributes");

            migrationBuilder.DropIndex(
                name: "IX_BasketAttributes_CategoryId_AttributeDefinitionId",
                table: "BasketAttributes");

            migrationBuilder.DropColumn(
                name: "SortOrder",
                table: "BasketAttributes");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "ProductTypes");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "BasketAttributes",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "BasketAttributes",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_BasketAttributes_AttributeDefinitionId",
                table: "BasketAttributes",
                column: "AttributeDefinitionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BasketAttributes_CategoryId",
                table: "BasketAttributes",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_BasketAttributes_Categories_CategoryId",
                table: "BasketAttributes",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BasketAttributes_Categories_CategoryId",
                table: "BasketAttributes");

            migrationBuilder.DropIndex(
                name: "IX_BasketAttributes_AttributeDefinitionId",
                table: "BasketAttributes");

            migrationBuilder.DropIndex(
                name: "IX_BasketAttributes_CategoryId",
                table: "BasketAttributes");

            migrationBuilder.DropColumn(
                name: "Order",
                table: "BasketAttributes");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "BasketAttributes",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddColumn<List<int>>(
                name: "SortOrder",
                table: "BasketAttributes",
                type: "integer[]",
                nullable: false);

            migrationBuilder.CreateIndex(
                name: "IX_BasketAttributes_AttributeDefinitionId",
                table: "BasketAttributes",
                column: "AttributeDefinitionId");

            migrationBuilder.CreateIndex(
                name: "IX_BasketAttributes_CategoryId_AttributeDefinitionId",
                table: "BasketAttributes",
                columns: new[] { "CategoryId", "AttributeDefinitionId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BasketAttributes_Categories_CategoryId",
                table: "BasketAttributes",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
