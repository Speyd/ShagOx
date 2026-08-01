using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShagOxServer.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddProductTypeToCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Categories_ProductType",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "ProductType",
                table: "Categories");

            migrationBuilder.AddColumn<int>(
                name: "ProductTypeId",
                table: "Categories",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ProductTypeId",
                table: "Categories",
                column: "ProductTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_ProductTypes_ProductTypeId",
                table: "Categories",
                column: "ProductTypeId",
                principalTable: "ProductTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_ProductTypes_ProductTypeId",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_ProductTypeId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "ProductTypeId",
                table: "Categories");

            migrationBuilder.AddColumn<string>(
                name: "ProductType",
                table: "Categories",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ProductType",
                table: "Categories",
                column: "ProductType");
        }
    }
}
