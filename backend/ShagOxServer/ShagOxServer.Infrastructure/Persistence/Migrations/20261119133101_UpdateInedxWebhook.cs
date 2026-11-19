using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShagOxServer.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateInedxWebhook : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PaymentWebhooks_InvoiceId",
                table: "PaymentWebhooks");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentWebhooks_InvoiceId",
                table: "PaymentWebhooks",
                column: "InvoiceId",
                unique: true,
                filter: "(\"EntityStatus\" IN ('Active', 'Hidden')) AND (\"Status\" = 'Succeeded')");
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
                filter: "(\"EntityStatus\" IN ('Active', 'Hidden')) AND (\"Status\" = 'success')");
        }
    }
}
