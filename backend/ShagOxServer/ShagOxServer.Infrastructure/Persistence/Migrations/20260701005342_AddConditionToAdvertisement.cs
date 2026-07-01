using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShagOxServer.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddConditionToAdvertisement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ConditionId",
                table: "Advertisements",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Advertisements_CategoryId_ConditionId_CreatedAt",
                table: "Advertisements",
                columns: new[] { "CategoryId", "ConditionId", "CreatedAt" });

            migrationBuilder.CreateIndex(
                name: "IX_Advertisements_ConditionId",
                table: "Advertisements",
                column: "ConditionId");

            migrationBuilder.CreateIndex(
                name: "IX_Advertisements_SellerId_CreatedAt",
                table: "Advertisements",
                columns: new[] { "SellerId", "CreatedAt" });

            migrationBuilder.AddForeignKey(
                name: "FK_Advertisements_Conditions_ConditionId",
                table: "Advertisements",
                column: "ConditionId",
                principalTable: "Conditions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Advertisements_Conditions_ConditionId",
                table: "Advertisements");

            migrationBuilder.DropIndex(
                name: "IX_Advertisements_CategoryId_ConditionId_CreatedAt",
                table: "Advertisements");

            migrationBuilder.DropIndex(
                name: "IX_Advertisements_ConditionId",
                table: "Advertisements");

            migrationBuilder.DropIndex(
                name: "IX_Advertisements_SellerId_CreatedAt",
                table: "Advertisements");

            migrationBuilder.DropColumn(
                name: "ConditionId",
                table: "Advertisements");
        }
    }
}
