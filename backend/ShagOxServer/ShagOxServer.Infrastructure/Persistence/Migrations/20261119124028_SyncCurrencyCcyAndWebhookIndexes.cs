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

            // PaymentWebhooks index changes live in 20261119095854_UpdateIndexesWebhooks
            // and 20261119133101_UpdateInedxWebhook; repeating them here breaks databases
            // that already applied those migrations.
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
                ALTER TABLE "Currencies" ALTER COLUMN "Ccy" DROP DEFAULT;
                ALTER TABLE "Currencies" ALTER COLUMN "Ccy" TYPE character varying(3)
                    USING "Ccy"::text;
                ALTER TABLE "Currencies" ALTER COLUMN "Ccy" SET DEFAULT '';
                """);
        }
    }
}
