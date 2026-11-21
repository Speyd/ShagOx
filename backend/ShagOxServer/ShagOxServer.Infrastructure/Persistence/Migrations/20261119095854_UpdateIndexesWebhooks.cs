using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShagOxServer.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateIndexesWebhooks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // IF EXISTS: databases that already applied 20261119124028_SyncCurrencyCcyAndWebhookIndexes
            // (an earlier version of it) have these indexes changed/dropped already.
            migrationBuilder.Sql("""
                DROP INDEX IF EXISTS "IX_PaymentWebhooks_InvoiceId";
                DROP INDEX IF EXISTS "IX_PaymentWebhooks_IsProcessed_ReceivedAt";
                """);

            migrationBuilder.CreateIndex(
                name: "IX_PaymentWebhooks_InvoiceId",
                table: "PaymentWebhooks",
                column: "InvoiceId",
                unique: true,
                filter: "(\"EntityStatus\" IN ('Active', 'Hidden')) AND (\"Status\" = 'success')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PaymentWebhooks_InvoiceId",
                table: "PaymentWebhooks");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentWebhooks_InvoiceId",
                table: "PaymentWebhooks",
                column: "InvoiceId",
                unique: true,
                filter: "\"EntityStatus\" IN ('Active', 'Hidden')");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentWebhooks_IsProcessed_ReceivedAt",
                table: "PaymentWebhooks",
                columns: new[] { "IsProcessed", "ReceivedAt" });
        }
    }
}
