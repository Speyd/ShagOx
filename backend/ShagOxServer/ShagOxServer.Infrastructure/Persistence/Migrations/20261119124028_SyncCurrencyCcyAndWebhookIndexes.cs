using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShagOxServer.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class SyncCurrencyCcyAndWebhookIndexes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
                ALTER TABLE "Currencies" ALTER COLUMN "Ccy" DROP DEFAULT;
                ALTER TABLE "Currencies" ALTER COLUMN "Ccy" TYPE integer
                    USING COALESCE(NULLIF("Ccy"::text, ''), '0')::integer;
                ALTER TABLE "Currencies" ALTER COLUMN "Ccy" SET DEFAULT 0;

                UPDATE "Currencies" SET "Ccy" = 980 WHERE "Code" = 'UAH' AND "Ccy" = 0;
                UPDATE "Currencies" SET "Ccy" = 840 WHERE "Code" = 'USD' AND "Ccy" = 0;
                UPDATE "Currencies" SET "Ccy" = 978 WHERE "Code" = 'EUR' AND "Ccy" = 0;
                UPDATE "Currencies" SET "DecimalPlaces" = 2 WHERE "DecimalPlaces" = 0;
                """);

            migrationBuilder.DropIndex(
                name: "IX_PaymentWebhooks_InvoiceId",
                table: "PaymentWebhooks");

            migrationBuilder.DropIndex(
                name: "IX_PaymentWebhooks_IsProcessed_ReceivedAt",
                table: "PaymentWebhooks");

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

            migrationBuilder.Sql("""
                ALTER TABLE "Currencies" ALTER COLUMN "Ccy" DROP DEFAULT;
                ALTER TABLE "Currencies" ALTER COLUMN "Ccy" TYPE character varying(3)
                    USING "Ccy"::text;
                ALTER TABLE "Currencies" ALTER COLUMN "Ccy" SET DEFAULT '';
                """);
        }
    }
}
